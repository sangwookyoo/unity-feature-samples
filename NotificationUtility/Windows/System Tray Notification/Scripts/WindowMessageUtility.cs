#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public static class WindowMessageUtility
{
    private static NotifyIcon _notifyIcon;

    public static void Show(string title, string message, string iconPath = null)
    {
        Init(iconPath);

        _notifyIcon.BalloonTipTitle = title;
        _notifyIcon.BalloonTipText = message;
        _notifyIcon.ShowBalloonTip(3000);
        _notifyIcon.BalloonTipClicked += OnBalloonTipDispose;
        _notifyIcon.BalloonTipClosed += OnBalloonTipDispose;
    }

    private static void Init(string iconPath)
    {
        _notifyIcon?.Dispose();
        _notifyIcon = new NotifyIcon
        {
            Visible = true,
            Icon = SetIcon(iconPath)
        };
    }

    private static Icon SetIcon(string iconPath)
    {
        if (!string.IsNullOrEmpty(iconPath) && File.Exists(iconPath))
        {
            return new Icon(iconPath);
        }
        return SystemIcons.Information;
    }

    private static void OnBalloonTipDispose(object sender, EventArgs e)
    {
        _notifyIcon?.Dispose();
    }
}

#endif