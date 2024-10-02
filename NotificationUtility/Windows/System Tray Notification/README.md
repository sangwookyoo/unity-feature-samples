# Windows Notification Utility (System Tray)

이 코드는 Unity에서 Windows 알림 기능을 사용하는 유틸리티입니다. `System.Windows.Forms` 라이브러리를 사용하여 Windows 환경에서 알림을 표시하고, 사용자 정의 아이콘을 설정할 수 있습니다.

## 필수 사항
- **`System.Windows.Forms.dll`** 필요: 이 코드는 `System.Windows.Forms` 라이브러리를 사용하기 때문에, 해당 DLL 파일이 필요합니다.
    - Unity 에디터에서 해당 DLL을 참조하려면 `System.Windows.Forms.dll` 파일을 프로젝트에 추가해야 합니다.
    - 해당 DLL은 Unity 프로젝트 내의 `Assets/Plugins` 폴더에 넣어 사용할 수 있습니다.

- **.NET Framework 호환성**: 이 코드는 .NET Framework 호환성을 필요로 하므로, Unity의 **Api Compatibility Level**을 `.NET Framework`로 설정해야 합니다.
    - Unity 메뉴에서 `Edit > Project Settings > Player`로 이동한 후, **Api Compatibility Level**을 **.NET Standard**가 아닌 **.NET Framework**로 변경해야 합니다.

## 사용 방법
`WindowMessageUtility.Show(string title, string message, string iconPath = null)` 메서드를 호출하여 원하는 제목과 메시지를 가진 알림을 표시할 수 있습니다.

```csharp
WindowMessageUtility.Show("제목", "메시지 내용");
WindowMessageUtility.Show("제목", "메시지 내용", ico파일);
```
