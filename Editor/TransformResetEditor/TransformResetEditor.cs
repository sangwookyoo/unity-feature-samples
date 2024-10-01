#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Transform), true)]
public class TransformResetEditor : Editor
{
    [System.Flags]
    enum Axes : byte
    {
        None = 0,
        X = 1 << 0,
        Y = 1 << 1,
        Z = 1 << 2,
        All = X | Y | Z,
    }

    private const float BUTTON_WIDTH = 20f;
    private const string POSITION_PROP = "m_LocalPosition";
    private const string ROTATION_PROP = "m_LocalRotation";
    private const string SCALE_PROP = "m_LocalScale";

    private Transform mTransform;
    private SerializedProperty mPosition;
    private SerializedProperty mRotation;
    private SerializedProperty mScale;

    private void OnEnable()
    {
        mTransform = target as Transform;
        mPosition = serializedObject.FindProperty(POSITION_PROP);
        mRotation = serializedObject.FindProperty(ROTATION_PROP);
        mScale = serializedObject.FindProperty(SCALE_PROP);
    }

    public override void OnInspectorGUI()
    {
        EditorGUIUtility.labelWidth = 15f;
        serializedObject.Update();

        DrawVectorProperty("P", mPosition, Vector3.zero);
        DrawRotation();
        DrawVectorProperty("S", mScale, Vector3.one);

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawVectorProperty(string label, SerializedProperty property, Vector3 resetValue)
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(label, GUILayout.Width(BUTTON_WIDTH)))
        {
            property.vector3Value = resetValue;
            GUI.FocusControl(null);
        }
        EditorGUILayout.PropertyField(property.FindPropertyRelative("x"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("y"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("z"));
        EditorGUILayout.EndHorizontal();
    }

    private void DrawRotation()
    {
        EditorGUILayout.BeginHorizontal();
        bool reset = GUILayout.Button("R", GUILayout.Width(BUTTON_WIDTH));
        Vector3 eulerAngles = mTransform.localEulerAngles;

        Vector3 newEulerAngles = new Vector3(
            DrawRotationField("X", eulerAngles.x),
            DrawRotationField("Y", eulerAngles.y),
            DrawRotationField("Z", eulerAngles.z)
        );

        if (newEulerAngles != eulerAngles)
        {
            Undo.RecordObjects(serializedObject.targetObjects, "Rotation Change");
            foreach (Object obj in serializedObject.targetObjects)
            {
                if (obj is Transform t)
                {
                    t.localEulerAngles = newEulerAngles;
                }
            }
        }

        if (reset)
        {
            mRotation.quaternionValue = Quaternion.identity;
            GUI.FocusControl(null);
        }
        EditorGUILayout.EndHorizontal();
    }

    private float DrawRotationField(string label, float value)
    {
        value = WrapAngle(value);
        Axes diff = CheckDifference(mRotation);
        bool isHidden = (diff & (Axes)System.Enum.Parse(typeof(Axes), label)) != 0;
        
        if (isHidden)
        {
            GUI.color = new Color(0.75f, 0.75f, 0.75f);
            EditorGUILayout.TextField(label, "─");
            GUI.color = Color.white;
            return value;
        }
        else
        {
            return EditorGUILayout.FloatField(label, value);
        }
    }

    private float WrapAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    private Axes CheckDifference(SerializedProperty rotation)
    {
        if (!rotation.hasMultipleDifferentValues)
            return Axes.None;

        Axes axes = Axes.None;
        Quaternion original = rotation.quaternionValue;

        foreach (Object obj in serializedObject.targetObjects)
        {
            if (obj is Transform t)
            {
                Vector3 diff = (t.localRotation * Quaternion.Inverse(original)).eulerAngles;
                if (Mathf.Abs(diff.x) > 0.0001f) axes |= Axes.X;
                if (Mathf.Abs(diff.y) > 0.0001f) axes |= Axes.Y;
                if (Mathf.Abs(diff.z) > 0.0001f) axes |= Axes.Z;
                if (axes == Axes.All) break;
            }
        }
        return axes;
    }
}

#endif