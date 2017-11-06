using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter1ArraysAndStrings
{
    public class ArraysAndStrings
    {
        /// <summary>
        /// Checks if a given string is a rotation of another given string
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool IsRotationOf(string s1, string s2)
        {
            if (s1 == null || s2 == null) return false;
            if (s1.Length == 0 || s2.Length == 0) return false;

            string s = s2 + s2;

            if (s.Contains(s1))
                return true;

            return false; 
        }
        /// <summary>
        /// Zeros the rows and columns that have a zero in them
        /// </summary>
        /// <param name="matrix"></param>
        public static void ZeroMatrix2(int[][] matrix)
        {
            if (matrix == null || matrix.Length == 0) return;

            bool firstRowHasZero = false;
            bool firstColHasZero = false;

            for (int j = 0; j < matrix[0].Length; j++)
            {
                if (matrix[0][j] == 0)
                {
                    firstRowHasZero = true;
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i][0] == 0)
                {
                    firstColHasZero = true;
                }
            }

            for (int i = 1; i < matrix.Length; i++)
            {
                for (int j = 1; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        matrix[i][0] = 0;
                        matrix[0][j] = 0;
                    }
                }
            }

            // clear rows based on first column
            for (int i = 1; i < matrix.Length; i++)
            {
                if (matrix[i][0] == 0)
                {
                    ZeroRow(matrix, 0);
                }
            }

            // clear columns based on first row
            for (int i = 1; i < matrix[0].Length; i++)
            {
                if (matrix[0][i] == 0)
                {
                    ZeroCol(matrix, 0);
                }
            }

            if (firstRowHasZero)
                ZeroRow(matrix, 0);

            if (firstColHasZero)
                ZeroCol(matrix, 0);
        }

        
        /// <summary>
        /// Zeros the rows and columns that have a zero in them
        /// </summary>
        /// <param name="matrix"></param>
        public static void ZeroMatrix(int[][] matrix)
        {
            if (matrix == null || matrix.Length == 0) return;

            int[] storage = new int[matrix.Length];
            for (int i = 0; i < storage.Length; i++)
                storage[i] = -1;

            // find where the 0s are and store their locations
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] == 0)
                        storage[i] = j;
                }
            }

            // begin zeroing
            for (int i = 0; i < storage.Length; i++)
            {
                if (storage[i] > -1)
                {
                    Zero(matrix, i, storage[i]);
                }
            }
        }

        private static void Zero(int[][] matrix, int row, int col)
        {
            ZeroRow(matrix, row);
            ZeroCol(matrix, col);
        }

        private static void ZeroRow(int[][] matrix, int row)
        {
            for (int x = 0; x < matrix.Length; x++)
            {
                matrix[row][x] = 0;
            }
        }

        private static void ZeroCol(int[][] matrix, int col)
        {
            for (int x = 0; x < matrix[0].Length; x++)
            {
                matrix[x][col] = 0;
            }
        }

        /// <summary>
        /// Rotates a matrix clockwisely by 90 degress
        /// </summary>
        /// <param name="matrix"></param>
        public static void RotateMatrix(int[][] matrix)
        {
            if (matrix == null || matrix.Length == 0) return;
            if (matrix.Length != matrix[0].Length) return;

            int layersCount = matrix.Length / 2;

            for (int layer = 0; layer < layersCount; layer++)
            {
                int first = layer;
                int last = matrix[0].Length - 1 - layer; ;

                for (int i = first; i < last; i++)
                {
                    int offset = i - first;

                    int top = matrix[first][i];

                    // top <- left
                    matrix[first][i] = matrix[last - offset][first];

                    // left <- bottom
                    matrix[last - offset][first] = matrix[last][last - offset];

                    // bottom <- right
                    matrix[last][last - offset] = matrix[i][last];

                    // right <- top
                    matrix[i][last] = top;
                }
            }
        }

        /// <summary>
        /// Compresses a given string in the form 'aabcccccaaa' -> 'a2b1c5a3'
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StringCompression(string s)
        {
            if (s == null || s.Length == 0)
                return String.Empty;

            int charCount = 1;

            int p1 = 0;
            int p2 = 1;

            StringBuilder sb = new StringBuilder();

            while (p1 < s.Length && p2 < s.Length)
            {
                if (s[p1] == s[p2])
                {
                    charCount++;
                    p2++;
                } else
                {
                    sb.Append(s[p1].ToString() + charCount);

                    charCount = 1; // p2 encountered a new character
                    p1 = p2; // set p1 to the new character index
                    p2++; // set p2 to get next character
                }
            }

            sb.Append(s[p1].ToString() + charCount);

            string result = sb.ToString();

            return (result.Length < s.Length) ? result : s;
        }

        /// <summary>
        /// Checks if two strings are one edit away
        /// </summary>
        /// <param name="s1">First string</param>
        /// <param name="s2">Second string</param>
        /// <returns></returns>
        public static bool OneAway(string s1, string s2)
        {
            if (s1.Length - s2.Length > 1)
                return false;

            int diffCount = 0;
            int index1 = 0;
            int index2 = 0;

            while (index1 < s1.Length && index2 < s2.Length)
            {
                if (s1[index1] != s2[index2])
                {
                    diffCount++;

                    if (diffCount > 1) // they are more than one edit away
                        return false;

                    int result = s1.Length.CompareTo(s2.Length);

                    if (result == 0)
                    {
                        index1++;
                        index2++;
                    } else if (result < 0)
                    {
                        index2++;
                    } else if (result > 0)
                    {
                        index1++;
                    }
                } else {
                    index1++;
                    index2++;
                }
            }

            return true;
        }

        public static bool isPalindrome(string s)
        {
            if (s == null || s.Length == 0)
                return false;

            Dictionary<char, int> map = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (!map.ContainsKey(c))
                    map[c] = 0;

                map[c] += 1;
            }

            int odds = 0;

            foreach (int value in map.Values)
            {
                if (value % 2 == 1)
                    odds++;

                if (odds > 1)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Replaces all the spaces in the given string with "%20"
        /// </summary>
        /// <param name="s">Input char array</param>
        /// <param name="trueLength">The length of the actual string in the array</param>
        /// <returns></returns>
        public static string urlify(char[] s, int trueLength)
        {
            if (s == null || s.Length == 0)
                throw new ArgumentException("Please enter a non-empty char array.");

            int length = s.Length - 1;

            for (int i = trueLength - 1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    s[length] = '0';
                    s[length - 1] = '2';
                    s[length - 2] = '%';
                    length -= 3;
                }
                else
                {
                    s[length] = s[i];
                    length--;
                }

            }

            return new string(s);
        }

        /// <summary>
        /// Checks two strings to determine if they are permutations of each other
        /// </summary>
        /// <param name="s1">First string</param>
        /// <param name="s2">Second string</param>
        /// <returns>True if they are permutations of each other, otherwise, false</returns>
        public static bool IsPermutation(string s1, string s2)
        {
            if (String.IsNullOrWhiteSpace(s1) || String.IsNullOrWhiteSpace(s2))
                throw new ArgumentException("Please enter a non-empty string.");

            if (s1.Length != s2.Length) return false;

            Dictionary<char, int> map = new Dictionary<char, int>();

            for (int i = 0; i < s1.Length; i++)
            {
                if (!map.ContainsKey(s1[i]))
                    map[s1[i]] = 0;

                if (!map.ContainsKey(s2[i]))
                    map[s2[i]] = 0;

                map[s1[i]] += 1;
                map[s2[i]] += 1;
            }

            // if any value in the hash map is odd, 
            // then they are not permutations of one another
            foreach (int value in map.Values)
            {
                if (value % 2 == 1)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if a string has a unique set of characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>True if input is unique, otherwise it returns false</returns>
        public static bool IsUnique2(string s)
        {
            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
                throw new ArgumentException("Please enter a non-empty string.");

            int bitSet = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int val = s[i] - 'a';
                if ((bitSet & (1 << val)) > 0)
                    return false;

                // set bit
                bitSet |= (1 << val);
            }
            return true;
        }

        /// <summary>
        /// Determines if a string has a unique set of characters
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>True if input is unique, otherwise it returns false</returns>
        public static bool IsUnique(string s)
        {
            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s))
                throw new ArgumentException("Please enter a non-empty string.");
            Dictionary<char, int> map = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (!map.Keys.Contains(c))
                    map[c] = 0;

                map[c] += 1;

                if (map[c] > 1)
                    return false;
            }

            return true;
        }
    }
}
