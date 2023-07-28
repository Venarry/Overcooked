public static class Calculator
{
    public static float GetCenterdStartPoint(float objectCount, float spacing)
    {
        float normalizedValue = objectCount - 1;
        float numberOfSides = 2;

        return normalizedValue / numberOfSides * -spacing;
    }
}
