using UnityEngine;

public interface ICursor
{
    CursorType GetCursorType();
}

public enum CursorType
{
    Default,
    Interact,
    Attack,
}

public class CursorController : MonoBehaviour
{
    private const float MaxRaycastDistance = 100f;
    
    private Camera _mainCamera;
    private CursorType _currentCursorType = CursorType.Default;

    private void Awake()
    {
        _mainCamera = Camera.main;
        SetCursor(_currentCursorType);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) return;
        UpdateCursorType();
    }

    private void UpdateCursorType()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, MaxRaycastDistance))
        {
            if (hit.collider.TryGetComponent(out ICursor interactable))
            {
                CursorType newType = interactable.GetCursorType();
                if (_currentCursorType != newType)
                {
                    SetCursor(newType);
                }
                return;
            }
        }

        if (_currentCursorType != CursorType.Default)
        {
            SetCursor(CursorType.Default);
        }
    }

    private void SetCursor(CursorType type)
    {
        if (!DataManager.Instance.CursorData.CursorDictionary.TryGetValue(type, out var cursorData)) return;
        _currentCursorType = type;
        Cursor.SetCursor(cursorData.texture, cursorData.hotspot, CursorMode.Auto);
    }
}