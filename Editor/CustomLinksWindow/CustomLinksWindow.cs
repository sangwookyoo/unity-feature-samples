using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 사용자 정의 링크를 표시하고 열 수 있는 에디터 툴입니다.
/// </summary>
public class CustomLinksWindow : EditorWindow
{
    private const string WINDOW_TITLE = "Custom Links";
    private const string MENU_ITEM_PATH = "Tools/Custom Links";
    private const string INFO_MESSAGE = "This tool provides quick access to custom links that you can open directly from the Unity Editor.";

    private static readonly List<(string name, string url)> LINKS = new()
    {
        ("Unity Documentation", "https://docs.unity3d.com/"),
        ("Unity Asset Store", "https://assetstore.unity.com/"),
        ("Unity Forum", "https://forum.unity.com/"),
        ("Scripting API", "https://docs.unity3d.com/ScriptReference/"),
        ("Google Drive", "https://drive.google.com/drive/u/0/home")
    };

    [MenuItem(MENU_ITEM_PATH)]
    public static void ShowWindow() => GetWindow<CustomLinksWindow>(WINDOW_TITLE);

    private void OnGUI()
    {
        DrawHeader();
        DrawLinkButtons();
    }
    
    private void DrawHeader()
    {
        EditorGUILayout.LabelField(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        EditorGUILayout.HelpBox(INFO_MESSAGE, MessageType.Info);
        EditorGUILayout.Space();
    }
    
    private void DrawLinkButtons()
    {
        foreach (var (name, url) in LINKS)
        {
            if (GUILayout.Button(name))
            {
                OpenURL(url);
            }
        }
    }

    private static void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}