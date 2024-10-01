using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    private static T _instance;
    private static readonly object _lock = new();
    private static bool _shuttingDown;

    protected static T Instance
    {
        get
        {
            if (_shuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance @{typeof(T)} already destroyed. Returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        var singletonObject = new GameObject($"@{typeof(T)}");
                        _instance = singletonObject.AddComponent<T>();
                        DontDestroyOnLoad(singletonObject);
                        Debug.Log($"[Singleton] An instance of @{typeof(T)} is created.");
                    }
                }

                return _instance;
            }
        }
    }

    private void OnDestroy()
    {
        _shuttingDown = true;
    }
}