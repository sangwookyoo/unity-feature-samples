using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class IconController : MonoBehaviour
{
    [Header("Icon Settings")]
    [SerializeField] private GameObject iconCanvasPrefab;
    [SerializeField] private Transform iconPosition;

    [Header("Events")]
    public UnityEvent onClickEvent;
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerExitEvent;

    private Camera _mainCamera;
    private Canvas _iconCanvas;
    private bool _isIconVisible;

    private void Start()
    {
        if (TryGetComponent(out BoxCollider boxCollider))
        {
            boxCollider.isTrigger = true;
        }
        
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = false;
        }
        
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!_isIconVisible) return;
        UpdateIconRotation();
    }

    private void OnDrawGizmos()
    {
        if (TryGetComponent(out BoxCollider boxCollider))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other != _playerCollider) return;// 다른 사람이 들어갔을 경우에 대한 처리 필요.
        ShowIcon();
        onTriggerEnterEvent?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        // if (other != _playerCollider) return;// 다른 사람이 들어갔을 경우에 대한 처리 필요.
        HideIcon();
        onTriggerExitEvent?.Invoke();
    }

    public void OnClickIcon()
    {
        onClickEvent?.Invoke();
    }
    
    public void ShowIcon()
    {
        if (_iconCanvas == null) CreateIcon();
        _iconCanvas.enabled = true;
        _isIconVisible = true;
    }

    public void HideIcon()
    {
        if (_iconCanvas == null) return;
        _iconCanvas.enabled = false;
        _isIconVisible = false;
    }

    private void CreateIcon()
    {
        if (iconCanvasPrefab == null) return;
        if (iconPosition == null) return;
        
        GameObject canvasObj = Instantiate(iconCanvasPrefab, iconPosition);
        if (canvasObj.TryGetComponent(out Canvas canvas))
        {
            _iconCanvas = canvas;
            _iconCanvas.transform.position = iconPosition.position;
            _iconCanvas.worldCamera = _mainCamera;
        }
    }

    private void UpdateIconRotation()
    {
        _iconCanvas.transform.LookAt(_mainCamera.transform);
        _iconCanvas.transform.Rotate(0f, 180f, 0f);
    }
}
