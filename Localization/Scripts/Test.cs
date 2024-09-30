using UnityEngine;

public class Test : MonoBehaviour
{
    private Localization _localization;
    
    private void Awake()
    {
        _localization = new();
        _localization.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log(_localization.GetLocalizedText("test"));
    }
}
