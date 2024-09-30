public static class ObjectExtensions
{
    /// <summary>
    /// 직렬화가 가능한 필드에 사용주의!
    /// </summary>
    public static bool IsNull(this System.Object obj)
    {
        return ReferenceEquals(obj, null);
    }

    public static bool IsNull(this UnityEngine.Object obj)
    {
        return obj == null;
    }
}
