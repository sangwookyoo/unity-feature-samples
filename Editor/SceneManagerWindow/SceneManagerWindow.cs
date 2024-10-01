#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Unity 에디터에서 씬을 쉽게 선택하고 로드할 수 있게 해주는 에디터 툴입니다.
/// </summary>
public class SceneManagerWindow : EditorWindow
{
    private const string WINDOW_TITLE = "Scene Manager";
    private const string MENU_ITEM_PATH = "Tools/Scene Manager";
    private const string INFO_MESSAGE = "This tool allows you to easily select and load scenes from your build settings.";
    
    private string[] _sceneNames;
    private int _selectedSceneIndex;

    [MenuItem(MENU_ITEM_PATH)]
    public static void ShowWindow() => GetWindow<SceneManagerWindow>(WINDOW_TITLE);

    private void OnEnable() => RefreshSceneList();

    private void RefreshSceneList()
    {
        var enabledScenes = new List<string>();
        var scenes = EditorBuildSettings.scenes;
        
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].enabled)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scenes[i].path);
                enabledScenes.Add(sceneName);
            }
        }

        _sceneNames = enabledScenes.ToArray();
        _selectedSceneIndex = 0;
    }

    private void OnGUI()
    {
        DrawHeader();
        
        if (_sceneNames.Length > 0)
        {
            DrawSceneSelector();
            DrawLoadButton();
        }
        else
        {
            DrawWarningMessage();
        }
    }

    private void DrawHeader()
    {
        GUILayout.Label(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        EditorGUILayout.HelpBox(INFO_MESSAGE, MessageType.Info);
        EditorGUILayout.Space();
    }

    private void DrawSceneSelector() => _selectedSceneIndex = EditorGUILayout.Popup("Scene", _selectedSceneIndex, _sceneNames);

    private void DrawLoadButton()
    {
        if (GUILayout.Button("Load Scene"))
        {
            LoadSelectedScene();
        }
    }

    private void DrawWarningMessage() => EditorGUILayout.HelpBox("No scenes are included in the build settings.", MessageType.Warning);

    private void LoadSelectedScene()
    {
        int actualSceneIndex = GetActualSceneIndex();
        if (actualSceneIndex != -1)
        {
            string scenePath = EditorBuildSettings.scenes[actualSceneIndex].path;
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
                ShowNotification(new GUIContent($"Scene '{_sceneNames[_selectedSceneIndex]}' loaded successfully!"));
            }
        }
    }

    private int GetActualSceneIndex()
    {
        var scenes = EditorBuildSettings.scenes;
        int enabledSceneCount = 0;
        
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].enabled)
            {
                if (enabledSceneCount == _selectedSceneIndex)
                {
                    return i;
                }
                enabledSceneCount++;
            }
        }
        
        return -1;
    }
}

#endif