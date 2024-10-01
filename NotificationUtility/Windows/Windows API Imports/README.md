# Windows Notification Utility (Windows API Imports)

이 코드는 Windows 플랫폼에서 메시지 박스를 표시하는 유틸리티입니다. `user32.dll`의 함수를 사용하여 현재 포그라운드 창에 메시지 박스를 띄우고, 창이 최소화된 경우 자동으로 복원합니다.

## 주요 기능
- **MessageBox 표시**: 현재 포그라운드 창에 지정한 제목과 메시지를 가진 메시지 박스를 표시합니다.
- **창 복원**: 메시지 박스가 표시된 창이 최소화되어 있을 경우, 창을 복원합니다.

## 사용 방법
`WindowMessageUtility.Show(string title, string message)` 메서드를 호출하여 원하는 제목과 메시지를 가진 메시지 박스를 표시할 수 있습니다.

```csharp
WindowMessageUtility.Show("제목", "메시지 내용");
```