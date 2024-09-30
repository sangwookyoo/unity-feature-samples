using UnityEngine;
using UnityEngine.EventSystems;

public static class TransformExtensions
{
    public static void SetPositionX(this Transform transform, float x)
    {
        var newPosition = new Vector3(x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
    
    public static void SetPositionY(this Transform transform, float y)
    {
        var newPosition = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = newPosition;
    }
    
    public static void SetPositionZ(this Transform transform, float z)
    {
        var newPosition = new Vector3(transform.position.x, transform.position.y, z);
        transform.position = newPosition;
    }

    public static void SetAsLastSiblingOnPointerDown(this Transform transform, PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}