#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_OSX

using System.Diagnostics;

public static class MacOSMessageUtility
{
    private const string AppleScriptPath = "/usr/bin/osascript";

    public static void Show(string title, string message)
    {
        string script = $"display notification \"{message}\" with title \"{title}\"";
        ExecuteAppleScript(script);
    }

    private static void ExecuteAppleScript(string script)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = AppleScriptPath,
            Arguments = $"-e '{script}'",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();
    }
}

#endif
