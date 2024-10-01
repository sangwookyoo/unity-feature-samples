# Missing Script Remover 에디터 툴

이 프로젝트는 선택된 게임 오브젝트와 그 하위 계층에서 누락된 스크립트(Missing Script) 컴포넌트를 자동으로 제거하는 Unity 에디터 툴입니다.

## 주요 기능
- **Missing Script 제거**: 선택된 게임 오브젝트와 그 자식 오브젝트에서 누락된 스크립트 컴포넌트를 찾아 제거합니다.
- **Undo 지원**: 스크립트 제거 작업은 Undo 시스템에 등록되므로, 작업 후에도 복구할 수 있습니다.

## 사용 방법
1. `Assets/Editor` 폴더 아래에 스크립트를 추가합니다.
2. Unity 에디터 상단 메뉴에서 **Tools > Missing Script Remover**를 클릭하여 창을 엽니다.
3. `Remove Missing Scripts` 버튼을 눌러 선택된 게임 오브젝트와 그 자식들에서 누락된 스크립트를 제거합니다.