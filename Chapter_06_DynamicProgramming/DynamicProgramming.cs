using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter6_DynamicProgramming
{
    public class DynamicProgramming
    {

        public static void EightQueens()
        {
            bool[,] board = new bool[8, 8];
            int[] columns = new int[8];
            List<int[]> results = new List<int[]>();

            EightQueens(columns, 0, results);
        }
        private static void EightQueens(int[] columns, int row, List<int[]> results)
        {
            if (row >= 8)
            {
                results.Add((int[]) columns.Clone());
            } else
            {
                for (int col = 0; col < 8; col++)
                {
                    if (CanPlace(columns, row, col))
                    {
                        columns[row] = col;
                        EightQueens(columns, row + 1, results);
                    }
                }
            }
        }

        private static bool CanPlace(int[] columns, int row1, int col1)
        {
            for (int row2 = 0; row2 < row1; row2++)
            {
                int col2 = columns[row1];

                if (col1 == col2)
                {
                    return false;
                }

                int colDistance = Math.Abs(col1 - col2);
                int rowDistance = row1 - row2;

                if (colDistance == rowDistance)
                {
                    return false;
                }
            }
            return true;
        }

        private static int CoinWays(int[] denoms, int index, int amount, int[,] map)
        {
            if (map[amount, index] > 0)
                return map[amount, index];

            if (index >= denoms.Length - 1)
                return 1;
 
            int denom = denoms[index];
            int ways = 0;
            for (int i = 0; i * denom <= amount; i++)
            {
                int amountRemaining = amount - i * denom;
                ways += CoinWays(denoms, index + 1, amountRemaining);
            }

            map[amount, index] = ways;
            return ways;
        }

        public static int CoinWays(int amount)
        {
            int[] denoms = new int[] { 25, 10, 5, 1 };
            int[,] map = new int[amount + 1, denoms.Length];
            return CoinWays(denoms, 0, amount, map);
            //return CoinWays(denoms, 0, amount);
        }
        private static int CoinWays(int[] denoms, int index, int amount)
        {
            if (index >= denoms.Length - 1)
                return 1;

            int denom = denoms[index];
            int ways = 0;
            for (int i = 0; i * denom <= amount; i++)
            {
                int amountRemaining = amount - i * denom;
                ways += CoinWays(denoms, index + 1, amountRemaining);
            }

            return ways;
        }

        public static void PaintFill(byte[,] arr, int r, int c, byte oColor, byte nColor)
        {
            if (r < 0 || c < 0 || r > arr.GetLength(0) || c > arr.GetLength(1))
                return;

            if (arr[r, c] == oColor) // look for black outline
            {
                arr[r, c] = nColor;

                PaintFill(arr, r + 1, c, oColor, nColor); // go up
                PaintFill(arr, r - 1, c, oColor, nColor); // go down
                PaintFill(arr, r, c + 1, oColor, nColor); // go right
                PaintFill(arr, r, c - 1, oColor, nColor); // go left
            }
        }

        public static List<string> GenerateParens(int count)
        {
            char[] str = new char[count * 2];
            List<string> results = new List<string>();
            Parens(results, 0, str, count, count);
            return results;
        }
        static void Parens(List<string> results, int index, char[] str, int left, int right)
        {
            if (left < 0 || right < left)
                return;

            if (left == 0 && right == 0)
            {
                results.Add( new string(str));
            } else
            {
                str[index] = '(';
                Parens(results, index + 1, str, left - 1, right);

                str[index] = ')';
                Parens(results, index + 1, str, left, right - 1);
            }
        }

        public static HashSet<string> Parens(int n)
        {
            HashSet<string> results = new HashSet<string>();

            if (n == 0)
            {
                results.Add("");
                //return results;
            }
            else
            {
                HashSet<string> partials = Parens(n - 1);

                foreach (string s in partials)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        if (s[i] == '(')
                        {
                            string newString = InsertStringAt(i, s, "()");
                            results.Add(newString);
                        }

                    }

                    results.Add("()" + s);
                }
            }
            return results;
        }

        static string InsertStringAt(int i, string word, string value)
        {
            string start = word.Substring(0, i + 1);
            string end = (i + 1 < word.Length) ? word.Substring(i + 1) : "";

            return start + value + end;
        }

        public static List<string> PermutationsWithDups(string s)
        {
            List<string> results = new List<string>();
            Dictionary<char, int> map = BuildFreqTable(s);
            PrintPerms(map, "", s.Length, results);
            return results;
        }

        static void PrintPerms(Dictionary<char, int> map, string prefix, int remaining, List<string> results)
        {
            if (remaining == 0)
            {
                results.Add(prefix);
                return;
            }

            foreach (char c in map.Keys.ToArray())
            {
                int count = map[c];
                if (count > 0)
                {
                    map[c]--;
                    PrintPerms(map, prefix + c, remaining - 1, results);
                    map[c]++;
                }
            }
        }

        static Dictionary<char, int> BuildFreqTable(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (!map.ContainsKey(c))
                    map[c] = 0;

                map[c]++;
            }

            return map;
        }

        public static List<string> Permutations(string s)
        {
            int len = s.Length;
            List<string> results = new List<string>();

            if (len == 0)
            {
                results.Add("");
                return results;
            }

            for (int i = 0; i < len; i++)
            {
                string start = s.Substring(0, i);
                string end = s.Substring(i + 1);

                List<string> perms = Permutations(start + end);

                foreach (string word in perms)
                {
                    results.Add(word + s[i]);
                }
            }

            return results;
        }

        public static List<string> Permutation(string s)
        {
            List<string> allPerms = new List<string>();
            if (s.Length == 0)
            {
                allPerms.Add("");
                return allPerms;
            } else
            {
                char first = s[0];
                string rem = s.Substring(1);

                List<string> perms = Permutation(rem);
                foreach (string word in perms)
                {
                    for (int k = 0; k <= word.Length; k++)
                    {
                        string newPerm = InsertCharAt(k, word, first);
                        allPerms.Add(newPerm);
                    }
                }
            }

            return allPerms;
        }

        private static string InsertCharAt(int i, string word, char c)
        {
            string start = word.Substring(0, i);
            string end = word.Substring(i);
            return start + c + end;
        }

        public static void TowerOfHanoi()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            Stack<int> source = new Stack<int>();
            Stack<int> temp = new Stack<int>();
            Stack<int> destination = new Stack<int>();

            foreach (int item in list)
            {
                source.Push(item);
            }

            int n = list.Count;
            MoveDisks(n, source, destination, temp);
        }

        private static void MoveDisks(int disks, Stack<int> source, Stack<int> dest, Stack<int> temp)
        {
            if (disks <= 0)
                return;

            MoveDisks(disks - 1, source, temp, dest);
            // move top from source to destination
            dest.Push(source.Pop());

            MoveDisks(disks - 1, temp, dest, source);
        }

        /// <summary>
        /// Solves multiplication without using the * operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Multiply1(int a, int b)
        {
            int smaller = (a < b) ? a : b;
            int bigger = (a < b) ? b : a;
            int[] memo = new int[smaller + 1];
            return MultiplyHelper1(bigger, smaller, memo);
        }
        public static int MultiplyHelper1(int bigger, int smaller, int[] memo)
        {
            if (bigger == 0 || smaller == 0) return 0;
            if (smaller == 1) return bigger;
            else if (memo[smaller] > 0) return memo[smaller];
            
            int c = (smaller % 2 == 0) ? 0 : bigger;
            int s = smaller >> 1;

            memo[smaller] = MultiplyHelper1(bigger, s, memo) + MultiplyHelper1(bigger, s, memo) + c;
            return memo[smaller];
        }

        public static int Multiply(int a, int b)
        {
            int smaller = (a < b) ? a : b;
            int bigger = (a < b) ? b : a;

            return MultiplyHelper(bigger, smaller);
        }
        public static int MultiplyHelper(int a, int b)
        {
            if (a == 0 || b == 0) return 0;
            if (b == 1) return a;

            int i = b % 2;
            int c = (i == 0) ? 0 : a;
            int s = b >> 1;
            return MultiplyHelper(a, s) + MultiplyHelper(a, s) + c;
        }
        public static List<List<int>> PowerSet(List<int> list)
        {
            if (list == null || list.Count == 0) return null;

            int n = list.Count;

            List<List<int>> allSubsets = new List<List<int>>();

            for (int i = 0; i < Math.Pow(2, n); i++)
            {
                int a = i;

                List<int> set = new List<int>();
                int index = 0;

                while (a > 0)
                {
                    if ((a & 1) == 1)
                    {
                        set.Add(list[index]);
                    }

                    index++;
                    a >>= 1;
                }

                allSubsets.Add(set);
            }

            return allSubsets;
        }

        public static List<List<int>> getSubsets(List<int> set, int index)
        {
            List<List<int>> allSubsets;

            if (set.Count == index)
            {
                allSubsets = new List<List<int>>();
                allSubsets.Add(new List<int>());
            } else
            {
                allSubsets = getSubsets(set, index + 1);
                int item = set[index];

                List<List<int>> moreSubsets = new List<List<int>>();

                foreach (List<int> subset in allSubsets)
                {
                    List<int> newSubset = new List<int>(subset);
                    newSubset.Add(item);
                    moreSubsets.Add(newSubset);
                }

                allSubsets.AddRange(moreSubsets);
            }

            return allSubsets;
        }

        public static List<List<int>> AllSubsets(List<int> list)
        {
            if (list.Count == 0)
            {
                List<List<int>> powerSet = new List<List<int>>();
                powerSet.Add(new List<int>());
                return powerSet;
            }

            List<List<int>> allSubsets = null;

            for (int i = 0; i < list.Count; i++)
            {
                List<int> listClone = new List<int>(list);
                listClone.RemoveAt(i);
                allSubsets = AllSubsets(listClone);

                List<List<int>> newSubsets = new List<List<int>>();
                foreach (var set in allSubsets)
                {
                    List<int> newSet = new List<int>(set);
                    newSet.Add(list[i]);
                    newSubsets.Add(newSet);
                }

                allSubsets.AddRange(newSubsets);
            }

            return allSubsets;
        }

        public static int MagicIndex(int[] arr, int start, int end)
        {
            if (end < start) return -1;

            int midIndex = (start + end) / 2;
            int midValue = arr[midIndex];

            if (midValue == midIndex)
            {
                return midIndex;
            }

            int leftIndex = Math.Min(midIndex - 1, midValue);
            int left = MagicIndex(arr, start, leftIndex);
            if (left >= 0) return left;

            int rightIndex = Math.Max(midIndex + 1, midValue);
            int right = MagicIndex(arr, rightIndex, end);

            return right;
        }
        public static List<Point> FindPath(bool[,] board)
        {
            if (board == null || board.Length == 0) return null;
            List<Point> path = new List<Point>();
            HashSet<Point> failedPoints = new HashSet<Point>();

            // if there is a route to the bottom corner
            if (FindPath(board, board.GetLength(0) - 1, board.GetLength(1) - 1, path, failedPoints))
            {
                return path;
            }

            return null;
        }

        private static bool FindPath(bool[,] board, int r, int c, List<Point> path, HashSet<Point> failedPoints)
        {
            if (r < 0 || c < 0 || !board[r,c]) return false;

            Point p = new Point(r, c);

            if (failedPoints.Contains(p))
            {
                return false;
            }

            bool isOrigin = (r == 0) && (c == 0);

            //bool canGoLeft = FindPath(board, r, c - 1, path, failedPoints);
            //bool canGoRight = FindPath(board, r - 1, c, path, failedPoints);

            if (isOrigin || FindPath(board, r, c - 1, path, failedPoints) || FindPath(board, r - 1, c, path, failedPoints))
            {
                path.Add(p);
                return true;
            }

            failedPoints.Add(p);
            return false;
        }

        public class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }
        }

        /// <summary>
        /// Counts the possible number of ways the child can run up the stairs
        /// </summary>
        /// <param name="n"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static int TripleStep(int n, int[] memo)
        {
            if (n < 0) return 0;
            if (n == 0 || n == 1) return 1;

            if (memo[n] == 0)
            {
                memo[n] = TripleStep(n - 1, memo) + TripleStep(n - 2, memo) + TripleStep(n - 3, memo);
            }

            return memo[n];
        }
        public static int TripleStep(int n)
        {
            if (n < 0) return 0;
            if (n == 0 || n == 1) return 1;

            return TripleStep(n - 1) + TripleStep(n - 2) + TripleStep(n - 3);
        }
    }
}
