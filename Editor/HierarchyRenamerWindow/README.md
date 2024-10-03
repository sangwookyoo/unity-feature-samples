# Hierarchy Rename Tool

이 프로젝트는 Unity의 Hierarchy 창에서 여러 GameObject 이름을 일괄적으로 검색하여 변경할 수 있는 에디터 유틸리티입니다.

## 주요 기능
- **이름 검색 및 교체**: 사용자가 입력한 검색 패턴을 기반으로 여러 GameObject 이름을 일괄적으로 교체합니다.
- **프리뷰 기능**: 변경될 이름을 미리 확인하고, 적용 여부를 선택할 수 있습니다.
- **색상 사용자 정의**: 이름 변경 전후에 대한 색상을 사용자가 직접 설정할 수 있습니다.

## 사용 방법
1. `Assets/Editor` 폴더에 스크립트를 추가합니다.
2. Unity 에디터 상단 메뉴에서 **Tools > Hierarchy Rename Tool**을 선택해 창을 엽니다.
3. 이름을 변경하려는 패턴을 입력한 후 `Preview Rename` 버튼을 눌러 미리보기를 확인합니다.
4. 적용하고 싶은 변경 사항을 선택한 후 `Apply Rename` 버튼을 클릭해 변경을 완료합니다.