using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.Collections.Generic;

public static class BuildPlayer
{
    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        BuildPlayerForTarget(BuildTarget.StandaloneWindows64, "Build/Windows/Game.exe");
    }

    [MenuItem("Build/Build macOS")]
    public static void BuildMacOS()
    {
        BuildPlayerForTarget(BuildTarget.StandaloneOSX, "Build/macOS/Game.app");
    }

    [MenuItem("Build/Build Android")]
    public static void BuildAndroid()
    {
        BuildPlayerForTarget(BuildTarget.Android, "Build/Android.apk");
    }

    [MenuItem("Build/Build iOS")]
    public static void BuildIOS()
    {
        BuildPlayerForTarget(BuildTarget.iOS, "Build/iOS");
    }

    private static void BuildPlayerForTarget(BuildTarget target, string locationPathName)
    {
        string[] scenes = FindEnabledEditorScenes();

        if (scenes.Length == 0)
        {
            Debug.LogError("No enabled scenes found in build settings. Please add and enable at least one scene.");
            return;
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = locationPathName,
            target = target,
            options = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"Build succeeded for {target}: {summary.totalSize} bytes");
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"Build failed for {target}");
        }
        else if (summary.result == BuildResult.Cancelled)
        {
            Debug.LogWarning("Build cancelled");
        }
    }

    private static string[] FindEnabledEditorScenes()
    {
        var editorScenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            editorScenes.Add(scene.path);
        }

        return editorScenes.ToArray();
    }
}