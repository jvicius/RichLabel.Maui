namespace RichLabel.Maui.Helpers
{
    public static class ResponsiveSize
    {
        public static int Get(int baseValue)
        {
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
                return baseValue;

            return (int)Math.Round((baseValue / 2.0) * 3);
        }
    }
}