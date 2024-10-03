using UnityEngine;

public static class RandomHelper
{
    // 실수 범위 내의 임의 값 반환 (최대 값 포함)
    public static float RangeInclusive(float min, float max)
    {
        return Random.Range(min, max);
    }

    // 정수 범위 내의 임의 값 반환 (최대 값 포함)
    public static int RangeInclusive(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    // true 또는 false 무작위 반환
    public static bool RandomBool()
    {
        return Random.value > 0.5f;
    }

    // 특정 확률로 true 반환 (확률을 0.0 ~ 1.0 사이 값으로 입력)
    public static bool Chance(float probability)
    {
        return Random.value < Mathf.Clamp01(probability);
    }
}