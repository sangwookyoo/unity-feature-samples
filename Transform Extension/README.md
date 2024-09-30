# Transform Extensions

`TransformExtensions` 클래스는 Unity의 `Transform` 객체에 대한 유용한 확장 메서드를 제공합니다. 이 메서드는 특정 축의 위치를 쉽게 설정하고, PointerEventData에 따라 Transform을 최하위 자식으로 설정할 수 있는 기능을 포함합니다.

## 주요 기능
- **위치 설정 메서드**: Transform의 X, Y, Z 축 위치를 개별적으로 설정할 수 있는 메서드를 제공합니다.
- **PointerEventData 처리**: PointerEventData를 통해 Transform을 최하위 자식으로 설정할 수 있는 메서드를 제공합니다.

## 사용 방법
`TransformExtensions` 클래스를 프로젝트에 추가한 후, 아래와 같이 Transform 객체의 위치를 설정하거나 PointerEventData를 처리할 수 있습니다.

## 사용 예시
```csharp
using UnityEngine;
using UnityEngine.EventSystems;

public class ExampleUsage : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    private void Start()
    {
        targetTransform.SetPositionX(5f);  // X축 위치를 5로 설정
        targetTransform.SetPositionY(3f);  // Y축 위치를 3으로 설정
        targetTransform.SetPositionZ(1f);  // Z축 위치를 1로 설정
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetTransform.SetAsLastSiblingOnPointerDown(eventData);
    }
}
