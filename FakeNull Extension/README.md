# FakeNull Extension

이 클래스는 C#에서 객체의 null 검사를 쉽게 수행할 수 있는 확장 메서드를 제공합니다. Unity 객체와 일반 객체에 대해 각각의 null 검사를 지원합니다.

## 주요 기능
- **확장 메서드**: `System.Object` 및 `UnityEngine.Object`에 대한 null 검사를 위한 `IsNull` 메서드를 제공합니다.
- **유연한 null 검사**: Unity와 비Unity 객체 모두에 대한 null 검사를 간편하게 수행할 수 있습니다.

## 사용 방법
`ObjectExtensions` 클래스를 프로젝트에 추가한 후, 아래와 같이 객체의 null 여부를 검사할 수 있습니다.

## 사용 예시
```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        GameObject myObject = null;
        if (myObject.IsNull())
        {
            Debug.Log("myObject is null.");
        }

        System.Object anotherObject = null;
        if (anotherObject.IsNull())
        {
            Debug.Log("anotherObject is null.");
        }
    }
}
