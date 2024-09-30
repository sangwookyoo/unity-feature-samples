using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CursorMapping
{
    public CursorType type;
    public Texture2D texture;
    [Tooltip("일반 포인터 커서: 일반적으로 커서의 클릭 지점은 이미지의 왼쪽 상단 모서리이므로 (0, 0)으로 설정하는 것이 적합합니다." +
             "<br>십자형 커서: 이미지가 십자형이면 중앙을 클릭 지점으로 설정하는 것이 일반적입니다." +
             "<br>예를 들어, 이미지 크기가 32x32라면 핫스팟은 (16, 16)이 됩니다.")]
    public Vector2 hotspot;
}

[CreateAssetMenu(fileName = "CursorData", menuName = "Cursor System/Cursor Data")]
public class CursorData : ScriptableObject, ISerializationCallbackReceiver
{
    public CursorMapping[] cursors;
    public readonly Dictionary<CursorType, CursorMapping> CursorDictionary = new();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        CursorDictionary.Clear();
        for (int i = 0; i < cursors.Length; i++)
        {
            CursorDictionary.TryAdd(cursors[i].type, cursors[i]);
        }
    }
}