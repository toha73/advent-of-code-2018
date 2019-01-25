using System.Collections;

namespace Advent_of_Code_2018.Extensions
{
    public static class BitArrayExtensions
    {
        public static BitArray CopySlice(this BitArray source, int offset, int length)
        {
            var ret = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                ret[i] = source[offset + i];
            }
            return ret;
        }

        public static bool AreFirstFiveBitsEqual(this BitArray source, BitArray target)
        {
            return source[0] == target[0]
                && source[1] == target[1]
                && source[2] == target[2]
                && source[3] == target[3]
                && source[4] == target[4];
        }
    }
}
