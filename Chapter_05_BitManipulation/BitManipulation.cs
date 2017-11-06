using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter5_BitManipulation
{
    public class BitManipulation
    {
        public static int ComputeByteNum(int width, int x, int y)
        {
            return (width * y + x) / 8;
        }

        public static void DrawLine(byte[] screen, int width, int x1, int x2, int y)
        {
            var startOffset = x1 % 8;
            var firstFullByte = x1 / 8;

            if (startOffset != 0)
            {
                firstFullByte++;
            }

            var endOffset = x2 % 8;
            var lastFullByte = x2 / 8;

            if (endOffset != 7)
            {
                lastFullByte--;
            }

            // Set full bytes
            for (var b = firstFullByte; b <= lastFullByte; b++)
            {
                screen[(width / 8) * y + b] = (byte)0xFF;
            }

            var startMask = (byte)(0xFF >> startOffset);
            var endMask = (byte)~(0xFF >> (endOffset + 1));

            // Set start and end of line
            if ((x1 / 8) == (x2 / 8))
            {
                // If x1 and x2 are in the same byte
                var mask = (byte)(startMask & endMask);
                screen[(width / 8) * y + (x1 / 8)] |= mask;
            }
            else
            {
                if (startOffset != 0)
                {
                    var byteNumber = (width / 8) * y + firstFullByte - 1;
                    screen[byteNumber] |= startMask;
                }
                if (endOffset != 7)
                {
                    var byteNumber = (width / 8) * y + lastFullByte + 1;
                    screen[byteNumber] |= endMask;
                }
            }
        }

        public static void PrintByte(byte b)
        {
            for (var i = 7; i >= 0; i--)
            {
                Console.Write((b >> i) & 1);
            }
        }

        public static void PrintScreen(byte[] screen, int width)
        {
            var height = screen.Length * 8 / width;

            for (var r = 0; r < height; r++)
            {
                for (var c = 0; c < width; c += 8)
                {
                    var b = screen[ComputeByteNum(width, c, r)];
                    PrintByte(b);
                }

                Console.WriteLine("");
            }
        }

        public static void Run()
        {
            const int width = 8 * 4;
            const int height = 15;
            var screen = new byte[width * height / 8];
            //screen[1] = 13;

            DrawLine(screen, width, 8, 10, 2);

            PrintScreen(screen, width);
        }

        /// <summary>
        /// Swaps the odd and even bits in an integer
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int PairwiseSwap(int a, int b)
        {
            return ((a & 0xaaaaaaa) >> 1) | ((b & 0x5555555) << 1);
        }

        /// <summary>
        /// Determines the number of bits you would need to flip to convert integer a to integer b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Conversion(int a, int b)
        {
            if (a == 0 && b == 0) return 0;

            int c = a ^ b;
            int count = 0;

            while (c > 0)
            {
                count++;
                c >>= 1; // or c = c & (c - 1)
            }

            return count;
        }
        
        /// <summary>
        /// Gets the next biggest number after a given number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetNext(int num)
        {
            int c = num;
            int c1 = 0;
            int c0 = 0;

            while ((c & 1) == 0 && c != 0)
            {
                c0++;
                c >>= 1;
            }

            while ((c & 1) == 1)
            {
                c1++;
                c >>= 1;
            }

            if (c0 + c1 == 31 || c0 + c1 == 0)
                return -1;

            // set rightmost non-trailing zero to 1
            int p = c0 + c1;
            num |= 1 << p;

            // clear all the bits to the right of p
            int mask = ~((1 << p) - 1);
            num &= mask;

            // set c1 - 1 ones as far right as possible
            num |= (1 << (c1 - 1)) - 1;

            return num;
        }
         
        /// <summary>
        /// Gets the next smallest number after a given number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetPrev(int num)
        {
            int c = num;
            int c0 = 0;
            int c1 = 0;

            while ((c & 1) == 1)
            {
                c1++;
                c >>= 1;
            }

            if (c == 0)
                return -1;

            while ((c & 1) == 0)
            {
                c0++;
                c >>= 1;
            }

            // set rightmost non-trailing one to zero
            int p = c0 + c1;
            num &= (1 << (p + 1)) - 1; // clears all the bits from p through 0

            // set c1 + 1 bits to ones right after p
            int mask = (1 << (c1 + 1)) - 1;
            num |= mask << (c0 - 1);

            return num;
        }

        /// <summary>
        /// Returns the length longest sequence of 1s, if you can only flip one bit
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int LongestSequenceOfOnes(int num)
        {
            if (num == 0) return 0;
            if (~num == 0) return 32;

            int maxLength = 1;
            int currentLength = 0;
            int prevLength = 0;

            while (num != 0)
            {
                if ((num & 1) == 1)
                {
                    currentLength++;
                } else if ((num & 1) == 0) {
                    prevLength = (num & 2) == 0 ? 0 : currentLength;
                    currentLength = 0;
                }

                maxLength = Math.Max(maxLength, prevLength + currentLength + 1);
                num >>= 1;
            }

            return maxLength;
        }
        /// <summary>
        /// Prints the binary string representation of a number between 0 and 1
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string BinaryToString(double n) {
            if (n < 0 || n > 1) return "ERROR";

            StringBuilder binary = new StringBuilder();
            binary.Append(".");

            while (n > 0)
            {
                if (binary.Length >= 32)
                {
                    return "ERROR";
                }

                double r = n * 2;
                if (r >= 1)
                {
                    binary.Append(1);
                    n = r - 1;
                } else
                {
                    binary.Append(0);
                    n = r;
                }
            }

            return binary.ToString();
        }

        /// <summary>
        /// Inserts a binary number m into a binary number n in positions j through i
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public static int Insertion(int n, int m, int i, int j)
        {
            int leftMask = -1 << (j + 1);
            int rightMask = (1 << i) - 1;
            int mask = leftMask | rightMask;

            // clear bits j through i
            n &= mask;

            // set bits j through i
            n |= (m << i);

            return n;
        }
    }
}
