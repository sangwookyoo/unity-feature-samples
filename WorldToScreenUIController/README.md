# WorldToScreenUIController

이 스크립트는 Unity에서 3D 월드 좌표에 위치한 UI 요소(닉네임)의 화면 표시를 관리합니다. 카메라와의 거리에 따라 UI의 크기와 가시성을 조정하여 자연스러운 사용자 경험을 제공합니다.

## 주요 기능
- **UI 위치 설정**: 닉네임 UI는 3D 월드 좌표(`nickNamePos`)에 따라 위치가 설정됩니다.
- **거리 기반 가시성**: 카메라와의 거리가 설정된 최대 거리(`maxDistance`)를 초과하거나 카메라와 닉네임 사이의 방향이 반대일 경우 UI를 비활성화합니다.
- **스케일 조정**: 닉네임 UI의 스케일은 카메라와의 거리(`sqrDistance`)에 따라 동적으로 조정됩니다. 이는 UI가 카메라에 가까워질수록 더 크게 보이도록 합니다.

## 사용 방법
1. **Canvas 연결**: Unity 에디터에서 `canvas` 필드에 사용할 Canvas를 연결합니다.
2. **RectTransform 설정**: `nickNameRect` 필드에 닉네임 UI의 RectTransform을 연결합니다.
3. **위치 설정**: `nickNamePos` 필드에 닉네임이 표시될 위치의 Transform을 연결합니다.
4. **최대 거리 조정**: `maxDistance` 값을 설정하여 UI가 가시화되는 최대 거리를 정의합니다.
5. **스케일 배율 설정**: `scaleMultiplier` 값을 설정하여 UI 크기를 조정하는 배율을 설정합니다.