using System;
using System.Collections.Generic;
using System.Text;

namespace Session_09
{
    public static class StringHelper
    {
        //public static bool IsLongerThan(string value, int length)
        //{
        //    if (value.Length > length)
        //        return true;

        //    return false;
        //}

        /// <summary>
        /// Determines whether the source string's length is greater than the specified length.
        /// </summary>
        /// <remarks>Throws NullReferenceException if the source string is null.</remarks>
        /// <param name="value">Source string.</param>
        /// <param name="length">Length to compare against.</param>
        /// <returns>true if the source string's length is greater than the specified length; otherwise, false.</returns>
        public static bool IsLongerThan(this string value, int length)
        {
            if (value.Length > length)
                return true;

            return false;
        }
    }
}
