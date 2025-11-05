public static class ExtensionMethods
{
    public static int ToIntMinusPlusOne(this bool value)
    {
        return value ? 1 : -1;
    }
}
