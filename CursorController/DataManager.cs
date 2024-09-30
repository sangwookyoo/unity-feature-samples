using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region Singleton

    private static DataManager _instance;

    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(DataManager));
                    _instance = singletonObject.AddComponent<DataManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    public CursorData CursorData { get; private set; }

    public void Init()
    {
        CursorData = LoadScriptableObject("CursorData") as CursorData;
    }

    private ScriptableObject LoadScriptableObject(string path)
    {
        return Resources.Load<ScriptableObject>(path);
    }
}