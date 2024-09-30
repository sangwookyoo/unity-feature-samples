using UnityEngine;

public class WorldToScreenUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform nickNameRect;
    [SerializeField] private Transform nickNamePos;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float scaleMultiplier = 5f;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 nickNamePosition = nickNamePos.position;
        Vector3 cameraPosition = _mainCamera.transform.position;
        Vector3 directionToNickName = nickNamePosition - cameraPosition;
        float sqrDistance = directionToNickName.sqrMagnitude;
        
        if (sqrDistance > maxDistance * maxDistance || Vector3.Dot(directionToNickName, _mainCamera.transform.forward) <= 0)
        {
            canvas.enabled = false;
            return;
        }
        
        nickNameRect.position = _mainCamera.WorldToScreenPoint(nickNamePosition);
        nickNameRect.localScale = Vector3.one * (scaleMultiplier / Mathf.Sqrt(sqrDistance)); 
        canvas.enabled = true;
    }
}