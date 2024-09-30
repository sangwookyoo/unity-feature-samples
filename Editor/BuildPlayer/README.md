# BuildPlayer

`BuildPlayer` 클래스는 Unity 프로젝트를 다양한 플랫폼으로 빌드하기 위한 사용자 정의 빌드 기능을 제공합니다. 이 클래스는 Windows, macOS, Android 및 iOS 플랫폼에 대한 빌드 옵션을 포함하고 있습니다.

## 주요 기능
- **플랫폼별 빌드 메뉴**: Unity의 메뉴에서 Windows, macOS, Android 및 iOS에 대한 빌드 옵션을 제공합니다.
- **자동 활성화된 씬 검색**: 빌드에 포함할 활성화된 씬을 자동으로 검색합니다.
- **빌드 상태 로깅**: 빌드 결과에 따라 성공, 실패 및 취소 상태를 로깅합니다.

## 사용 방법
이 클래스를 Unity 프로젝트에 추가하면, Unity 메뉴의 "Build" 항목에서 각 플랫폼에 대한 빌드 옵션을 사용할 수 있습니다.