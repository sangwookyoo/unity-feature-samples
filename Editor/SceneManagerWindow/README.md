# Scene Manager 에디터 툴

이 프로젝트는 Unity 에디터에서 씬을 쉽게 선택하고 로드할 수 있도록 도와주는 간단한 에디터 툴입니다.

## 주요 기능
- **씬 목록 조회**: 빌드 세팅에 추가된 씬 중 활성화된 씬들을 조회할 수 있습니다.
- **씬 선택 및 로드**: 드롭다운 메뉴에서 씬을 선택하고 버튼을 눌러 해당 씬을 로드할 수 있습니다.
- **현재 씬 저장**: 씬을 로드하기 전에 변경사항을 저장할 수 있도록 사용자에게 묻는 창이 표시됩니다.

## 사용 방법
1. `Assets/Editor` 폴더 아래에 스크립트를 추가합니다.
2. Unity 에디터 상단 메뉴에서 **Tools > Scene Manager**를 클릭하여 창을 엽니다.
3. 드롭다운 메뉴에서 원하는 씬을 선택한 후 `Load Scene` 버튼을 눌러 씬을 로드합니다.