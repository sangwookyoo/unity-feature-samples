#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using System.Runtime.InteropServices;

public static class WindowMessageUtility
{
    private const int SW_RESTORE = 9;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public static void Show(string title, string message)
    {
        IntPtr hWnd = GetForegroundWindow();
        MessageBox(hWnd, message, title, 0);

        if (IsIconic(hWnd))
        {
            ShowWindow(hWnd, SW_RESTORE);
        }
    }
}

#endif
