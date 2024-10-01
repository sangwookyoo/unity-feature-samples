# SoundManager & AudioResourceManager

이 프로젝트는 Unity에서 사운드 관리 및 오디오 리소스 관리를 위한 클래스를 제공합니다. `SoundManager`는 게임 내 오디오를 관리하고, `AudioResourceManager`는 Unity 에디터에서 오디오 파일을 관리하는 기능을 제공합니다. 예시로 첨부된 사운드는 저작권에 주의하세요.

## SoundManager

### 기능
- **사운드 타입 정의**: BGM 및 효과음 사운드를 위한 enum 정의.
- **오디오 소스 관리**: BGM 및 효과음 재생을 위한 `AudioSource`를 자동으로 생성하고 관리.
- **오디오 클립 로드 및 캐싱**: 필요에 따라 오디오 클립을 로드하고 캐싱하여 성능을 최적화.

### 주요 메서드
- `Init()`: 오디오 루트 객체 및 소스 초기화.
- `Clear()`: 모든 오디오 소스를 정리하고 클립 캐시를 비움.
- `PlayBGM(string path, float pitch = 1.0f)`: BGM을 재생.
- `PlayEffect(string path, float pitch = 1.0f)`: 효과음을 재생.

### 사용 예시
```csharp
public class ExampleUsage : MonoBehaviour
{
    private SoundManager soundManager;

    void Start()
    {
        soundManager = new SoundManager();
        soundManager.Init();
        soundManager.PlayBGM("example_bgm");
        soundManager.PlayEffect("example_effect");
    }
}