using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WindowHelper
{
    // 윈도우가 현재 프레임이 있는지 프레임이 없는지를 결정하는 부울 플래그
    public static bool framed = true;
 
    // 윈도우 작업을 처리하기 위한 user32.dll에서의 외부 메서드 선언
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, out WinRect lpRect);
 
    // 윈도우의 직사각형을 나타내는 구조체
    private struct WinRect { public int left, top, right, bottom; }
 
    // 윈도우 스타일 및 명령에 대한 상수
    private const int GWL_STYLE = -16;
    private const int SW_MINIMIZE = 6;
    private const int SW_MAXIMIZE = 3;
    private const int SW_RESTORE = 9;
    private const uint WS_VISIBLE = 0x10000000;    
    private const uint WS_POPUP = 0x80000000;
    private const uint WS_BORDER = 0x00800000;
    private const uint WS_OVERLAPPED = 0x00000000;
    private const uint WS_CAPTION = 0x00C00000;
    private const uint WS_SYSMENU = 0x00080000;
    private const uint WS_THICKFRAME = 0x00040000; // WS_SIZEBOX
    private const uint WS_MINIMIZEBOX = 0x00020000;
    private const uint WS_MAXIMIZEBOX = 0x00010000;
    private const uint WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
 
 
    // 메서드가 Unity 로고 스플래시 화면 이후 게임 시작 시 실행되도록 하는 속성입니다.
    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeOnLoad()
    {
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN   // Unity 에디터에서는 이 작업을 수행하지 않습니다!
        SetFramelessWindow();
#endif
    }
 
    // 윈도우를 프레임이 없는 형태로 설정하는 메서드
    public static void SetFramelessWindow()
    {
        var hwnd = GetActiveWindow();
        SetWindowLong(hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        framed = false;
    }
 
    // 윈도우를 프레임이 있는 형태로 설정하는 메서드
    public static void SetFramedWindow()
    {
        var hwnd = GetActiveWindow();
        SetWindowLong(hwnd, GWL_STYLE, WS_OVERLAPPEDWINDOW | WS_VISIBLE);
        framed = true;
    }
 
    // 윈도우를 최소화하는 메서드
    public static void MinimizeWindow()
    {
        var hwnd = GetActiveWindow();
        ShowWindow(hwnd, SW_MINIMIZE);
    }
 
    // 윈도우를 최대화하는 메서드
    public static void MaximizeWindow()
    {
        var hwnd = GetActiveWindow();
        ShowWindow(hwnd, SW_MAXIMIZE);
    }
 
    // 윈도우를 복원하는 메서드
    public static void RestoreWindow()
    {
        var hwnd = GetActiveWindow();
        ShowWindow(hwnd, SW_RESTORE);
    }
 
    // 윈도우 위치를 이동하는 메서드
    public static void MoveWindowPos(Vector2 posDelta, int newWidth, int newHeight)
    {
        var hwnd = GetActiveWindow();
 
        var windowRect = GetWindowRect(hwnd, out WinRect winRect);
 
        var x = winRect.left + (int)posDelta.x;
        var y = winRect.top - (int)posDelta.y;
        MoveWindow(hwnd, x, y, newWidth, newHeight, false);
    }
}