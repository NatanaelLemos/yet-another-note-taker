using System;
using System.Collections.Generic;
using System.Text;

namespace YetAnotherNoteTaker.Common.Helpers
{
    public static class ValidationHelpers
    {
        public static T AsNotNull<T>(this T objectToTest, string objectName = "")
        {
            if (objectToTest == null)
            {
                throw new ArgumentNullException(
                    string.IsNullOrWhiteSpace(objectName)
                    ? typeof(T).FullName
                    : objectName);
            }

            return objectToTest;
        }
    }
}
