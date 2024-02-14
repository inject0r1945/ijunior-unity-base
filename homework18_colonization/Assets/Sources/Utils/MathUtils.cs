using UnityEngine;

public static class MathUtils
{
    public static float GetRandomSign()
    {
        float minValue = -1f;
        float maxValue = 1f;

        return Mathf.Sign(Random.Range(minValue, maxValue));
    }

    public static Vector3 GetRandomSquareOutPosition(float squareSize, float minShift, float maxShift)
    {
        return GetRandomRectangleOutPosition(squareSize, squareSize, minShift, maxShift);
    }

    public static Vector3 GetRandomRectangleOutPosition(float fistSideSize, float secondSideSize, float minShift, float maxShift)
    {
        int squareSidesCount = 4;
        int side = Random.Range(0, squareSidesCount);

        float delimiter = 2f;
        float firstSideHalfSize = fistSideSize / delimiter;
        float secondSideHalfSize = secondSideSize / delimiter;
        float randomX = 0f;
        float randomZ = 0f;

        float shift = Random.Range(minShift, maxShift);

        switch (side)
        {
            case (int)SquareSide.Top:
                randomX = Random.Range(-firstSideHalfSize - shift, firstSideHalfSize + shift);
                randomZ = secondSideHalfSize + shift;
                break;

            case (int)SquareSide.Right:
                randomX = firstSideHalfSize + shift;
                randomZ = Random.Range(-secondSideHalfSize - shift, secondSideHalfSize + shift);
                break;

            case (int)SquareSide.Bottom:
                randomX = Random.Range(-firstSideHalfSize - shift, firstSideHalfSize + shift);
                randomZ = -secondSideHalfSize - shift;
                break;

            case (int)SquareSide.Left:
                randomX = -firstSideHalfSize - shift;
                randomZ = Random.Range(-secondSideHalfSize - shift, secondSideHalfSize + shift);
                break;
        }

        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

        return randomPosition;
    }

    public static Quaternion GetRandomRotation(Vector3 direction)
    {
        float minValue = 0f;
        float maxValue = 360f;

        return Quaternion.AngleAxis(Random.Range(minValue, maxValue), direction);
    }

    public enum SquareSide
    {
        Top, Right, Bottom, Left
    }
}
