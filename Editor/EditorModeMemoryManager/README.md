# Editor Mode Memory Manager

이 프로젝트는 Unity 에디터에서 Play 모드 종료 시 자동으로 메모리를 정리해주는 에디터 유틸리티입니다.

## 주요 기능
- **메모리 정리**: Play 모드에서 에디터 모드로 전환될 때 사용되지 않는 에셋과 메모리를 자동으로 정리합니다.
- **자동 실행**: Unity 에디터의 `InitializeOnLoad`와 `ExecuteInEditMode` 속성을 통해 에디터가 로드될 때 자동으로 활성화됩니다.

## 사용 방법
1. `Assets/Editor` 폴더 아래에 스크립트를 추가합니다.
2. Play 모드를 종료하면 자동으로 사용되지 않는 메모리와 리소스를 정리합니다.
# unity-feature-sample