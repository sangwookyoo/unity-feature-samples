#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

/// <summary>
/// 선택된 게임 오브젝트와 하위 계층에서 Missing 스크립트 컴포넌트를 제거하는 에디터 툴입니다.
/// </summary>
public class MissingScriptRemoverWindow : EditorWindow
{
    private const string WINDOW_TITLE = "Missing Script Remover";
    private const string MENU_ITEM_PATH = "Tools/Missing Script Remover";
    private const string INFO_MESSAGE = "This tool removes missing script components from selected GameObjects and their children.";

    [MenuItem(MENU_ITEM_PATH)]
    private static void ShowWindow() => GetWindow<MissingScriptRemoverWindow>(WINDOW_TITLE);

    private void OnGUI()
    {
        DrawHeader();
        DrawRemoveButton();
    }

    private void DrawHeader()
    {
        EditorGUILayout.LabelField(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox(INFO_MESSAGE, MessageType.Info);
        EditorGUILayout.Space();
    }

    private void DrawRemoveButton()
    {
        if (GUILayout.Button("Remove Missing Scripts"))
        {
            RemoveAllMissingScriptComponents();
        }
    }

    private void RemoveAllMissingScriptComponents()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        int componentCount = 0;
        int gameObjectCount = 0;

        foreach (GameObject go in selectedObjects)
        {
            componentCount += RemoveMissingScriptsRecursively(go, ref gameObjectCount);
        }

        Debug.Log($"Removed {componentCount} missing script(s) from {gameObjectCount} GameObject(s)");
        ShowNotification(new GUIContent($"Removed {componentCount} missing script(s) from {gameObjectCount} GameObject(s)"));
    }

    private int RemoveMissingScriptsRecursively(GameObject go, ref int gameObjectCount)
    {
        int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);

        if (count > 0)
        {
            Undo.RegisterCompleteObjectUndo(go, "Remove Missing Scripts");
            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
            gameObjectCount++;
        }

        foreach (Transform child in go.transform)
        {
            count += RemoveMissingScriptsRecursively(child.gameObject, ref gameObjectCount);
        }

        return count;
    }
}

#endif