using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chapter10_SortingAndSearching
{
    public class SortingAndSearching
    {
        /// <summary>
        /// Sorts an array of int into peaks and valleys
        /// </summary>
        /// <param name="arr"></param>
        public static void SortValleyPeaks(int[] arr)
        {
            if (arr == null || arr.Length == 0)
                return;

            for (int i = 1; i < arr.Length; i += 2)
            {
                int biggestIndex = MaxIndex(arr, i - 1, i, i + 1);

                if (i == biggestIndex)
                {
                    // swap i and biggestIndex
                    int temp = arr[i];
                    arr[i] = arr[biggestIndex];
                    arr[biggestIndex] = temp;
                }
            }
        }

        private static int MaxIndex(int[] arr, int a, int b, int c)
        {
            int n = arr.Length;

            int aValue = (a >= 0 && a < n) ? arr[a] : int.MinValue;
            int bValue = (b >= 0 && b < n) ? arr[b] : int.MinValue;
            int cValue = (c >= 0 && c < n) ? arr[c] : int.MinValue;

            int max = Math.Max(a, Math.Max(b, c));

            if (aValue == max)
                return a;
            else if (bValue == max)
                return b;
            else
                return c;
        }

        /// <summary>
        /// Rank from Stream: Gets the number of values less than or equal to a given value
        /// </summary>
        public class RankNode
        {
            public int Data { get; set; }
            public RankNode Left { get; set; }
            public RankNode Right { get; set; }
            public int leftSize;

            public RankNode(int d)
            {
                Data = d;
                leftSize = 0;
            }

            public void Insert(int d)
            {
                if (d <= Data)
                {
                    if (Left == null)
                        Left = new RankNode(d);
                    else
                        Left.Insert(d);

                    leftSize++;
                } else
                {
                    if (Right == null)
                        Right = new RankNode(d);
                    else
                        Right.Insert(d);
                }
            }

            public int GetRank(int d)
            {
                if (d == Data)
                {
                    return leftSize;
                } else if (d < Data)
                {
                    return (Left == null) ? -1 : Left.GetRank(d);
                } else
                {
                    int rightRank = (Right == null) ? -1 : Right.GetRank(d);

                    if (rightRank == -1)
                        return -1;
                    else
                        return leftSize + 1 + rightRank;
                }
            }
        }

        /// <summary>
        /// Finds an element in a matrix in which each row and column is sorted
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Coord SortedMatrixSearch(int[,] matrix, int value)
        {
            if (matrix == null || matrix.Length == 0)
                return null;

            int row = 0;
            int col = matrix.Length - 1;

            while (row < matrix.Length - 1 && col >= 0)
            {
                if (matrix[row, col] == value)
                {
                    return new Coord(row, col);
                } else if (matrix[row, col] > value)
                {
                    col--;
                } else
                {
                    row++;
                }
            }

            return null;
        }

        public class Coord
        {
            public Coord(int r, int c)
            {
                Row = r;
                Column = c;
            }

            public int Row { get; set; }
            public int Column { get; set; }
        }

        /// <summary>
        /// Find duplicates in an array with only 4kb of memory available
        /// </summary>
        /// <param name="arr"></param>
        public static void FindDuplicates(int[] arr)
        {
            BitVector bv = new BitVector(32000);

            for (int i = 0; i < arr.Length; i++)
            {
                int num = arr[i];
                int num0 = num - 1;

                if (bv.Get(num0))
                {
                    Console.WriteLine("Duplicate: " + num);
                } else
                {
                    bv.Add(num0);
                }
            }
        }

        public class BitVector
        {
            private int[] vector;

            public BitVector(int totalSize)
            {
                vector = new int[(totalSize >> 5) + 1];
            }

            public bool Get(int num)
            {
                int wordNumber = num >> 5; // divide by 32
                int bitNumber = num & 0x1F;
                return ((vector[wordNumber] & (1 << bitNumber)) != 0);
            }

            public void Add(int num)
            {
                int wordNumber = num >> 5; // divide by 32
                int bitNumber = (num & 0x1F); // mod 32
                vector[wordNumber] |= 1 << bitNumber;
            }
        }

        public static void MissingInt(string fileName)
        {
            long numberInts = (long)int.MaxValue + 1;
            int[] bitField = new int[(int) (numberInts / 8) ];
            MissingInt(numberInts, bitField, fileName);
        }
        private static void MissingInt(long numberOfInts, int[] bitField, string fileName)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(fileName))) {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    int n = int.Parse(line);

                    bitField[n / 8] |= 1 << (n % 8);
                }
            }

            for (int i = 0; i < bitField.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((bitField[i] & (1 << j)) == 0)
                    {
                        Console.WriteLine(i * 8 + j);
                        return;
                    }
                }
            }
        }

        public static int SparseSearch(string[] arr, string value)
        {
            if (arr == null || arr.Length == 0 || value == null || value == "")
            {
                return -1;
            }
            
            return SparseSearch(arr, value, 0, arr.Length - 1);
        }

        public static int SparseSearch(string[] arr, string value, int first, int last)
        {
            if (first > last) return -1;

            int mid = (last + first) / 2;

            // if mid is empty, find closest non-empty string.
            if (arr[mid] == "")
            {
                int left = mid - 1;
                int right = mid + 1;

                while (true)
                {
                    if (left < first && right > last)
                        return -1;
                    else if (right <= last && arr[right] != "")
                    {
                        mid = right;
                        break;
                    } else if (left >= first && arr[left] != "")
                    {
                        mid = left;
                        break;
                    }

                    left--;
                    right++;
                }
            }

            if (value.Equals(arr[mid]))
            {
                return mid;
            } else if (arr[mid].CompareTo(value) < 0) // search right
            {
                return SparseSearch(arr, value, mid + 1, last);
            } else  // search left
            {
                return SparseSearch(arr, value, first, mid - 1);
            }
        }
        /// <summary>
        /// Searches an array-like data structure that has no size method or property
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int SearchListy(Listy<int> list, int key)
        {
            if (list == null || list.ElementAt(0) == -1)
                return -1;

            int index = 1;
            while (list.ElementAt(index) != -1 && list.ElementAt(index) < key)
            {
                index *= 2;
            }

            int low = index / 2;
            int high = index;

            while (low < high)
            {
                int mid = (low + high) / 2;

                if (list.ElementAt(mid) == key)
                {
                    return mid;
                } else if (list.ElementAt(mid) == -1)
                {
                    high = mid - 1;
                } else if (list.ElementAt(mid) < key)
                {
                    low = mid + 1;
                } else if (list.ElementAt(mid) > key)
                {
                    high = mid - 1;
                }


            }

            return -1;
        }

        public class Listy <T>
        {
            T[] list = null;

            public Listy(int size)
            {
                list = new T[size];
            }
            public int ElementAt(int i)
            {
                return -1;
            }
        }
        /// <summary>
        /// Search in a rotated array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int SearchRotatedArray(int[] arr, int left, int right, int key)
        {
            int mid = (left + right) / 2;
            if (key == arr[mid]) // found element
                return mid;

            if (right < left)
                return -1;

            if (arr[left] < arr[mid]) // left is ordered normally
            {
                if (key >= arr[left] && key < arr[mid])
                    return SearchRotatedArray(arr, left, mid - 1, key);
                else
                    return SearchRotatedArray(arr, mid + 1, right, key);
            } else if (arr[mid] < arr[right]) // right is ordered normally
            {
                if (key > arr[mid] && key <= arr[right])
                    return SearchRotatedArray(arr, mid + 1, right, key);
                else
                    return SearchRotatedArray(arr, left, mid - 1, key);
            } else if (arr[left] == arr[mid]) // left or right half is all repeats
            {
                if (arr[mid] != arr[right])
                {
                    return SearchRotatedArray(arr, mid + 1, right, key);
                } else
                {
                    int result = SearchRotatedArray(arr, left, mid - 1, key); // search left

                    if (result == -1)
                        SearchRotatedArray(arr, mid + 1, right, key);
                    else
                        return result;
                }
            }

            return -1;
        }

        /// <summary>
        /// Sorts an array of strings so that all the anagrams are next to each other
        /// </summary>
        /// <param name="arr"></param>
        public static void GroupAnagram(List<string> arr)
        {
            if (arr == null || arr.Count == 0)
                return;

            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

            foreach (var item in arr)
            {
                char[] s = item.ToCharArray();
                Array.Sort(s);

                string sa = new string(s);
                if (map[sa] == null)
                    map[sa] = new List<string>();

                map[sa].Add(item);
            }

            foreach (var key in map.Keys)
            {
                foreach (var value in key)
                {
                    Console.WriteLine(value);
                }
            }
        }

        /// <summary>
        /// Merges two sorted arrays where the larger array has 
        /// a larger enough buffer at the end to hold the smaller array
        /// </summary>
        /// <param name="a">First Array</param>
        /// <param name="b">Second Array</param>
        public static void SortedMerge(int[] a, int[] b)
        {
            if (a == null || a.Length == 0 || b == null || b.Length == 0)
                return;

            int[] smaller = (a.Length < b.Length) ? a : b;
            int[] bigger = (a.Length < b.Length) ? b : a;

            int i = bigger.Length - 1;
            int j = smaller.Length - 1;
            int current = i;

            while (i >= 0 && j >= 0)
            {
                if (bigger[i] < smaller[j])
                {
                    bigger[current] = smaller[j];
                    j--;
                } else
                {
                    bigger[current] = bigger[i];
                    i--;
                }
                current--;
            }

            int remaining = j;

            for (int x = remaining; x >= 0; x--)
            {
                bigger[current] = smaller[x];
                current--;
            }
        }
    }
}