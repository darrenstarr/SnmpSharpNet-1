﻿using System.Collections.Generic;

// From https://github.com/txdv/System.Extensions

namespace System
{
    public static class ArraySegmentExtensions
    {
        public static bool IsEmpty<T>(this ArraySegment<T> segment)
        {
            return segment == default;
        }

        public static ArraySegment<T> Skip<T>(this ArraySegment<T> segment, int count)
        {
            if (segment.Count - count == 0)
            {
                return default;
            }

            return new ArraySegment<T>(segment.Array, segment.Offset + count, segment.Count - count);
        }

        public static ArraySegment<T> SkipLast<T>(this ArraySegment<T> segment, int count)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset, segment.Count - count);
        }

        public static ArraySegment<T> Take<T>(this ArraySegment<T> segment, int count)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset, count);
        }

        public static ArraySegment<T> Take<T>(this ArraySegment<T> segment, int skip, int count)
        {
            return segment.Skip(skip).Take(count);
        }

        public static ArraySegment<T> TakeLast<T>(this ArraySegment<T> segment, int count)
        {
            return new ArraySegment<T>(segment.Array, segment.Offset + segment.Count - count, count);
        }

        public static ArraySegment<T> ToArraySegment<T>(this T[] array)
        {
            return new ArraySegment<T>(array);
        }

        public static bool IsEqual(this ArraySegment<byte> segment1, byte[] array)
        {
            return segment1.IsEqual(array.ToArraySegment());
        }

        public static bool IsEqual<T>(this ArraySegment<T> segment1, ArraySegment<T> segment2)
        {
            int n = segment1.Count;
            if (n != segment2.Count)
            {
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                var i1 = segment1.Offset + i;
                var i2 = segment2.Offset + i;
                if (!segment1.Array[i1].Equals(segment2.Array[i2]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsPrefixOf<T>(this ArraySegment<T> segment1, ArraySegment<T> segment2)
        {
            return segment2.Take(segment1.Count).IsEqual(segment1);
        }

        public static bool IsPrefixOf<T>(this T[] array, ArraySegment<T> segment)
        {
            return IsPrefixOf<T>(array.ToArraySegment<T>(), segment);
        }

        static void Swap<T>(ref T first, ref T second)
        {
            var tmp = first;
            first = second;
            second = tmp;
        }

        public static ArraySegment<T> Reverse<T>(this ArraySegment<T> segment)
        {
            for (int i = 0; i < segment.Count / 2; i++)
            {
                Swap(ref segment.Array[segment.Offset + i], ref segment.Array[segment.Offset + segment.Count - 1 - i]);
            }
            return segment;
        }

        public static IEnumerable<ArraySegment<T>> Slice<T>(this ArraySegment<T> segment, int size)
        {
            while (segment.Count >= size)
            {
                yield return segment.Take(size);
                segment = segment.Skip(size);
            }

            if (!segment.IsEmpty())
            {
                yield return segment;
            }
        }

        public static bool ContainsOnly(this ArraySegment<byte> segment, byte value)
        {
            foreach (var s in segment)
            {
                if (s != value)
                {
                    return false;
                }
            }
            return true;
        }

        public static int FindFirst(this ArraySegment<byte> segment, byte value)
        {
            for (int i = 0; i < segment.Count; i++)
            {
                if (segment.Array[segment.Offset + i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public static ArraySegment<T> ToArraySegment<T>(this T[] array, int offset = -1, int count = -1)
        {
            if (offset == -1)
            {
                offset = 0;
            }
            if (count == -1)
            {
                count = array.Length;
            }
            return new ArraySegment<T>(array, offset, count);
        }

        #region BitConverter

        public static bool ToBoolean(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToBoolean(segment.Array, segment.Offset + offset);
        }

        public static char ToChar(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToChar(segment.Array, segment.Offset + offset);
        }

        public static double ToDouble(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToDouble(segment.Array, segment.Offset + offset);
        }

        public static short ToInt16(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToInt16(segment.Array, segment.Offset + offset);
        }

        public static int ToInt32(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToInt32(segment.Array, segment.Offset);
        }

        public static long ToInt64(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToInt64(segment.Array, offset);
        }

        public static float ToSingle(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToSingle(segment.Array, segment.Offset + offset);
        }

        public static ushort ToUInt16(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToUInt16(segment.Array, segment.Offset + offset);
        }

        public static uint ToUInt32(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToUInt32(segment.Array, segment.Offset);
        }

        public static ulong ToUInt64(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToUInt64(segment.Array, offset);
        }

        public static float ToFloat(this ArraySegment<byte> segment, int offset = 0)
        {
            return BitConverter.ToSingle(segment.Array, segment.Offset + offset);
        }

        #endregion
    }
}
