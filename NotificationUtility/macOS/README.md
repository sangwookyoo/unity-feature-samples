# macOS Notification Utility

이 코드는 macOS 플랫폼에서 알림(notification)을 표시하는 유틸리티입니다. `osascript` 명령어를 통해 AppleScript를 실행하여 알림을 표시합니다.

## 주요 기능
- **알림 표시**: 지정한 제목과 메시지를 포함한 macOS 알림을 표시합니다.
- **AppleScript 실행**: 내부적으로 `osascript` 명령어를 사용하여 AppleScript를 실행합니다.

## 사용 방법
`MacOSMessageUtility.Show(string title, string message)` 메서드를 호출하여 원하는 제목과 메시지를 가진 알림을 표시할 수 있습니다.

```csharp
MacOSMessageUtility.Show("제목", "메시지 내용");
```