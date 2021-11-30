using System;

namespace Jror.Backend.Libs.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Gigabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GB(this int value)
        {
            return value.MB() * 1024;
        }

        /// <summary>
        /// Checks whether a number is even
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>
        /// Finds if the int is the specified range
        /// </summary>
        /// <param name="target"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool IsInRange(this int target, int start, int end)
        {
            return target >= start && target <= end;
        }

        public static bool IsOdd(this int number)
        {
            return number % 2 != 0;
        }

        /// <summary>
        /// Returns true when a integer is a prime.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPrime(this int number)
        {
            if ((number % 2) == 0)
            {
                return number == 2;
            }
            int sqrt = (int)Math.Sqrt(number);
            for (int t = 3; t <= sqrt; t = t + 2)
            {
                if (number % t == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }

        /// <summary>
        /// Checks whether a number is odd
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <summary>
        /// Kilobytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int KB(this int value)
        {
            return value * 1024;
        }

        /// <summary>
        /// Megabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int MB(this int value)
        {
            return value.KB() * 1024;
        }

        /// <summary>
        /// Terabytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long TB(this int value)
        {
            return (long)value.GB() * (long)1024;
        }

        /// <summary>
        /// Converts an integer to a Guid. This could be used within a unit test to mock objects.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid ToGuid(this int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }
    }
}