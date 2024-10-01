#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.IO;

/// <summary>
/// 현재 씬의 화면을 타임스탬프가 포함된 이미지(.png) 파일로 저장하는 에디터 툴입니다.
/// </summary>
public class SceneCaptureWindow : EditorWindow
{
    private const string WINDOW_TITLE = "Scene Capture";
    private const string MENU_ITEM_PATH = "Tools/Scene Capture";
    private const string INFO_MESSAGE = "This tool captures the current scene view and saves it as a PNG file with a timestamped filename in the specified directory.";
    
    private string _saveDirectory = "";

    [MenuItem(MENU_ITEM_PATH)]
    public static void ShowWindow() => GetWindow<SceneCaptureWindow>(WINDOW_TITLE);

    private void OnGUI()
    {
        DrawHeader();
        DrawSaveDirectoryField();
        DrawCaptureButton();
    }

    private void DrawHeader()
    {
        GUILayout.Label(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        EditorGUILayout.HelpBox(INFO_MESSAGE, MessageType.Info);
        EditorGUILayout.Space();
    }

    private void DrawSaveDirectoryField()
    {
        EditorGUILayout.BeginHorizontal();
        _saveDirectory = EditorGUILayout.TextField("Save Directory:", _saveDirectory);
        if (GUILayout.Button("Browse", GUILayout.Width(60)))
        {
            BrowseForSaveDirectory();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawCaptureButton()
    {
        if (GUILayout.Button("Capture Scene"))
        {
            CaptureScene();
        }
    }

    private void BrowseForSaveDirectory()
    {
        string selectedPath = EditorUtility.OpenFolderPanel("Select Save Directory", _saveDirectory, "");
        if (!string.IsNullOrEmpty(selectedPath))
        {
            _saveDirectory = selectedPath;
        }
    }

    private void CaptureScene()
    {
        if (string.IsNullOrEmpty(_saveDirectory))
        {
            EditorUtility.DisplayDialog("Error", "Please specify a save directory.", "OK");
            return;
        }

        string fileName = $"SceneCapture_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        string fullPath = Path.Combine(_saveDirectory, fileName);

        ScreenCapture.CaptureScreenshot(fullPath);
        ShowNotification(new GUIContent($"Scene capture saved to: {fullPath}"));
    }
}

#endif