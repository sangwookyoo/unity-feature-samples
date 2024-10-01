using UnityEngine;

public class Test : MonoBehaviour
{
    private SoundManager _soundManager;
    
    private void Awake()
    {
        _soundManager = new();
        _soundManager.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _soundManager.PlayBGM("SA_Channel");
    }
}
