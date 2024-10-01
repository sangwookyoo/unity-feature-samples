using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class HierarchyRenamerWindow : EditorWindow
{
    private const string WINDOW_TITLE = "Hierarchy Rename Tool";
    private const string MENU_ITEM_PATH = "Tools/Hierarchy Rename Tool";
    private const string INFO_MESSAGE = "This tool allows you to rename multiple GameObjects at once. Enter the text to search for and the text to replace it with.";

    private string _searchPattern = "";
    private string _replacePattern = "";
    private Vector2 _scrollPosition;
    private List<RenamePreview> _previewList = new();
    private Color _addedColor = new(0.4f, 1f, 0.4f);
    private Color _removedColor = new(1f, 0.4f, 0.4f);

    [MenuItem(MENU_ITEM_PATH)]
    public static void ShowWindow() => GetWindow<HierarchyRenamerWindow>(WINDOW_TITLE);

    private void OnGUI()
    {
        DrawHeader();
        DrawInputFields();
        DrawPreviewButton();
        DrawDiffView();
        DrawApplyRenameButton();
        DrawColorCustomization();
    }

    private void DrawHeader()
    {
        GUILayout.Label(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        EditorGUILayout.HelpBox(INFO_MESSAGE, MessageType.Info);
        EditorGUILayout.Space();
    }

    private void DrawInputFields()
    {
        _searchPattern = EditorGUILayout.TextField("Search Pattern", _searchPattern);
        _replacePattern = EditorGUILayout.TextField("Replace Pattern", _replacePattern);
        EditorGUILayout.Space();
    }

    private void DrawPreviewButton()
    {
        if (GUILayout.Button("Preview Rename"))
        {
            PreviewRename();
        }
        EditorGUILayout.Space();
    }

    private void DrawDiffView()
    {
        GUILayout.Label("Diff View", EditorStyles.boldLabel);
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        foreach (var preview in _previewList)
        {
            DrawPreviewItem(preview);
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space();
    }

    private void DrawPreviewItem(RenamePreview preview)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(preview.OriginalName, new GUIStyle(EditorStyles.label) { normal = { textColor = _removedColor } });
        EditorGUILayout.LabelField("→", GUILayout.Width(20));
        EditorGUILayout.LabelField(preview.NewName, new GUIStyle(EditorStyles.label) { normal = { textColor = _addedColor } });
        preview.ShouldRename = EditorGUILayout.Toggle(preview.ShouldRename, GUILayout.Width(20));
        EditorGUILayout.EndHorizontal();
    }

    private void DrawApplyRenameButton()
    {
        if (GUILayout.Button("Apply Rename"))
        {
            ApplyRename();
        }
        EditorGUILayout.Space();
    }

    private void DrawColorCustomization()
    {
        GUILayout.Label("Customize Diff Colors", EditorStyles.boldLabel);
        _addedColor = EditorGUILayout.ColorField("Added Color", _addedColor);
        _removedColor = EditorGUILayout.ColorField("Removed Color", _removedColor);
    }

    private void PreviewRename()
    {
        _previewList.Clear();

        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            string newName = obj.name.Replace(_searchPattern, _replacePattern);
            if (newName != obj.name)
            {
                _previewList.Add(new RenamePreview(obj, obj.name, newName, true));
            }
        }
    }

    private void ApplyRename()
    {
        foreach (var preview in _previewList.Where(p => p.ShouldRename))
        {
            if (preview.Asset != null)
            {
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(preview.Asset), preview.NewName);
            }
            else if (preview.GameObject != null)
            {
                Undo.RecordObject(preview.GameObject, "Rename GameObject");
                preview.GameObject.name = preview.NewName;
            }
            else if (!string.IsNullOrEmpty(preview.AssetPath))
            {
                AssetDatabase.RenameAsset(preview.AssetPath, preview.NewName);
            }
        }

        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
        ShowNotification(new GUIContent("Rename operation completed!"));
    }

    private class RenamePreview
    {
        public string AssetPath { get; private set; }
        public Object Asset { get; private set; }
        public GameObject GameObject { get; private set; }
        public string OriginalName { get; private set; }
        public string NewName { get; private set; }
        public bool ShouldRename { get; set; }

        public RenamePreview(GameObject gameObject, string originalName, string newName, bool shouldRename)
        {
            GameObject = gameObject;
            OriginalName = originalName;
            NewName = newName;
            ShouldRename = shouldRename;
        }
    }
}