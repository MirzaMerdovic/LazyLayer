using System;

namespace LazyLayer.Core
{
    public class Guard
    {
        public static void ThrowIfNull<T>(T argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}