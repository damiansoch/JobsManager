namespace JobsManager.Helpers
{
    public static class ObjectExtentions
    {
        public static bool AreAllPropertiesNull(this object obj)
        {
            return obj.GetType().GetProperties().All(propertyInfo =>
            {
                var value = propertyInfo.GetValue(obj);
                return value switch
                {
                    null => true,
                    string str => string.IsNullOrWhiteSpace(str),
                    _ => false
                };
            });
        }
    }
}
