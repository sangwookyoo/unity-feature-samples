#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;

public class AudioResourceManagerWindow : EditorWindow
{
    private const string WINDOW_TITLE = "Audio Resource Manager";
    private const string MENU_ITEM_PATH = "Tools/Audio Resource Manager %#F1";
    private const string INFO_MESSAGE = "Manage your audio files in BGM and Effect folders. Refresh the lists and add new audio files by selecting them from your computer.";
    
    private const string BGM_FOLDER_PATH = "Assets/Resources/Sounds/BGM";
    private const string EFFECT_FOLDER_PATH = "Assets/Resources/Sounds/Effect";
    
    private const string FILE_DIALOG_EXTENSION = "mp3,wav";

    private string _selectedFilePath = "";
    private Vector2 _bgmScrollPos;
    private Vector2 _effectScrollPos;

    [MenuItem(MENU_ITEM_PATH, false, 100000)]
    public static void ShowWindow()
    {
        GetWindow<AudioResourceManager>(WINDOW_TITLE);
    }

    private void OnGUI()
    {
        DrawHeader();
        EditorGUILayout.BeginHorizontal();
        float sectionWidth = (EditorGUIUtility.currentViewWidth - 30) / 2;
        DrawAudioSection("BGM", BGM_FOLDER_PATH, ref _bgmScrollPos, sectionWidth);
        DrawAudioSection("Effect", EFFECT_FOLDER_PATH, ref _effectScrollPos, sectionWidth);
        EditorGUILayout.EndHorizontal();
    }
    
    private void DrawHeader()
    {
        GUILayout.Label(WINDOW_TITLE, EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        EditorGUILayout.HelpBox(INFO_MESSAGE, MessageType.Info);
        EditorGUILayout.Space();
    }

    private void DrawAudioSection(string sectionName, string folderPath, ref Vector2 scrollPos, float sectionWidth)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(sectionWidth));
        GUILayout.Label(sectionName, EditorStyles.boldLabel);
        
        if (GUILayout.Button($"Show {sectionName} in Project"))
        {
            ShowFolderInProject(folderPath);
        }
        
        if (GUILayout.Button($"Show {sectionName} in Project"))
        {
            ShowFolderInExplorer(folderPath);
        }

        if (GUILayout.Button($"Refresh {sectionName} List"))
        {
            RefreshAudioList(folderPath);
        }

        EditorGUILayout.LabelField($"Audio Files in {sectionName}:");
        
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(150));
        DrawAudioFiles(folderPath, "*.mp3");
        DrawAudioFiles(folderPath, "*.wav");
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();
        
        if (GUILayout.Button($"Add Audio File to {sectionName}"))
        {
            AddAudioFile(folderPath);
        }

        EditorGUILayout.EndVertical();
    }

    private void ShowFolderInProject(string folderPath)
    {
        string assetPath = folderPath.Replace("\\", "/");
        Object folderObject = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
        Selection.activeObject = folderObject;
        EditorUtility.FocusProjectWindow();
    }
    
    private void ShowFolderInExplorer(string folderPath)
    {
        string assetPath = folderPath.Replace("\\", "/");
        string fullPath = Path.GetFullPath(assetPath);
        System.Diagnostics.Process.Start(fullPath);
    }

    private void RefreshAudioList(string folderPath)
    {
        AssetDatabase.Refresh();
        Debug.Log($"{folderPath} list refreshed.");
    }

    private void DrawAudioFiles(string folderPath, string searchPattern)
    {
        string[] files = Directory.GetFiles(folderPath, searchPattern);
        foreach (string audioFile in files)
        {
            DrawAudioFileEntry(audioFile);
        }
    }

    private void DrawAudioFileEntry(string audioFile)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(EditorGUIUtility.Load("AudioClip Icon") as Texture2D, GUILayout.Width(20), GUILayout.Height(20));
        EditorGUILayout.LabelField(Path.GetFileName(audioFile));
        EditorGUILayout.EndHorizontal();
    }

    private void AddAudioFile(string folderPath)
    {
        _selectedFilePath = EditorUtility.OpenFilePanel("Select Audio File", "", FILE_DIALOG_EXTENSION);
        
        if (!string.IsNullOrEmpty(_selectedFilePath))
        {
            string fileName = Path.GetFileName(_selectedFilePath);
            string destinationPath = Path.Combine(folderPath, fileName);

            File.Copy(_selectedFilePath, destinationPath, true);
            AssetDatabase.Refresh();

            Debug.Log($"Audio file '{fileName}' added to {folderPath}.");
            ShowNotification(new GUIContent($"'{fileName}' added to {folderPath}!"));
        }
    }
}

#endif