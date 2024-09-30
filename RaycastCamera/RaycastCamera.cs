using UnityEngine;

public class RaycastCamera : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _mainCamera.nearClipPlane));
        Vector3 dir = mousePos - _mainCamera.transform.position;
        dir = dir.normalized;

        Debug.DrawRay(_mainCamera.transform.position, dir * 100, Color.red, 1f);

        if (Physics.Raycast(_mainCamera.transform.position, dir, out var hit, 100f))
        {
            Debug.Log($"Raycast Camera @{hit.collider.gameObject.name}");
        }
    }
}