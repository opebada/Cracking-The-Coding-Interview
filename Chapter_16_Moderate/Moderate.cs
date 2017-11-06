using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter16_Moderate
{
    public class Moderate
    {
        public static bool TwoSum(int[] arr, int target)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();

            foreach (int key in arr)
            {
                if (!map.ContainsKey(key))
                    map[key] = target - key;
            }

            foreach (KeyValuePair<int, int> entry in map)
            {
                if (map.ContainsKey(entry.Value))
                    return true;
            }

            return false;
        }

        public static int ContiguousSequence(int[] arr)
        {
            if (arr == null || arr.Length == 0) return -1;

            int p1 = 0;
            int sum = 0;
            int largestSum = 0;

            while (p1 < arr.Length)
            {
                sum += arr[p1];
                largestSum = Math.Max(largestSum, sum);

                if (sum < 0)
                {
                    sum = 0; 
                }
                p1++;
            }

            return largestSum;
        }

        public static Tuple<int, int> SubSort(int[] arr)
        {
            // find left subsequence
            int endLeft = FindEndOfLeftSequence(arr);
            if (endLeft >= arr.Length - 1) return null; // already sorted

            // find right subsequence
            int startRight = FindEndOfRightSequence(arr);

            // get min and max
            int maxIndex = endLeft; // max of left side
            int minIndex = startRight; // min of right side
            for (int i = endLeft + 1; i < startRight; i++)
            {
                if (arr[i] < arr[minIndex]) minIndex = i;
                if (arr[i] > arr[maxIndex]) maxIndex = i;
            }

            int leftIndex = ShrinkLeft(arr, minIndex, endLeft);
            int rightIndex = ShrinkRight(arr, maxIndex, startRight);

            return new Tuple<int, int>(leftIndex, rightIndex);
        }

        static int FindEndOfLeftSequence(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < arr[i - 1]) return i - 1;
            }
            return arr.Length - 1;
        }

        static int FindEndOfRightSequence(int[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] > arr[i + 1]) return i + 1;
            }
            return 0;
        }

        static int ShrinkLeft(int[] arr, int minIndex, int start)
        {
            int comp = arr[minIndex];
            for (int i = start - 1; i >= 0; i--)
            {
                if (arr[i] <= comp) return i + 1;
            }
            return 0;
        }

        static int ShrinkRight(int[] arr, int maxIndex, int start)
        {
            int comp = arr[maxIndex];
            for (int i = start; i < arr.Length; i++)
            {
                if (arr[i] >= comp) return i - 1;
            }
            return arr.Length - 1;
        }

        public static MasterMindResult MasterMind(string solution, string guess)
        {
            if (solution.Length < 4 || solution.Length > 4) return null;
            if (guess.Length < 4 || guess.Length > 4) return null;

            Dictionary<char, int> map = new Dictionary<char, int>();

            int hits = 0;

            for (int i = 0; i < solution.Length; i++)
            {
                if (solution[i] == guess[i])
                {
                    hits++;
                    solution.Remove(i, 1);
                    guess.Remove(i, 1);
                } else
                {
                    if (!map.ContainsKey(solution[i]))
                        map[solution[i]] = 0;

                    map[solution[i]]++;
                }
            }

            int pseudoHits = 0;

            for (int i = 0; i < guess.Length; i++)
            {
                if (map.ContainsKey(guess[i]))
                {
                    if (map[guess[i]] > 0)
                    {
                        map[guess[i]]--;
                        pseudoHits++;
                    }
                }
            }

            return new MasterMindResult () { Hits = hits, PseudoHits = pseudoHits };
        }
        public class MasterMindResult
        {
            public int Hits { get; set; }
            public int PseudoHits { get; set; }
        }
        public static List<int> DivingBoard(int shorter, int longer, int k)
        {
            int shorterCount = 0;
            int longerCount = k;

            List<int> lengths = new List<int>();

            while (shorterCount < k)
            {
                int length = longerCount * longerCount + shorter * shorterCount;
                lengths.Add(length);
                shorterCount++;
                longerCount--;
            }

            return lengths;
        }

        public static int LivingPeople(Person[] people)
        {
            int[] births = new int[people.Length];
            int[] deaths = new int[people.Length];

            for (int i = 0; i < people.Length; i++)
            {
                births[i] = people[i].BirthYear;
                deaths[i] = people[i].DeathYear;
            }

            Array.Sort(births);
            Array.Sort(deaths);

            int birthIndex = 0;
            int deathIndex = 0;
            int maxAlive = 0;
            int maxAliveYear = births[birthIndex];
            int currentAlive = 0;

            while (birthIndex < births.Length)
            {
                if (births[birthIndex] <= deaths[deathIndex])
                {
                    currentAlive++;

                    if (currentAlive > maxAlive)
                    {
                        maxAlive = currentAlive;
                        maxAliveYear = births[birthIndex];
                    }
                    birthIndex++;
                } else
                {
                    currentAlive--;
                    deathIndex++;
                }
            }

            return maxAliveYear;
        }

        public class Person
        {
            public int BirthYear { get; set; }
            public int DeathYear { get; set; }
        }

        /// <summary>
        /// Performs a division operation with only the add operator
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int Division(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return int.MinValue;

            int absa = Abs(num1);
            int absb = Abs(num2);
            int product = 0;
            int x = 0;

            while (product + absb <= absa)
            {
                product += absb;
                x++;
            }

            if ((num1 < 0 && num2 < 0) || (num1 > 0 && num2 > 0))
            {
                return x;
            }

            return Negate(x);
        }

        static int Abs(int a)
        {
            if (a < 0)
                return Negate(a);
            
            return a;
        }
        public static int Subtraction(int num1, int num2)
        {
            return num1 + Negate(num2);
        }

        static int Negate(int num)
        {
            int ones = -1;

            int result = num ^ ones;
            result += 1;

            return result;
        }

        public static int Multiply(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return 0;

            int larger = Math.Max(num1, num2);
            int smaller = Math.Min(num1, num2);

            if (smaller == 1) return larger;

            int result = 0;

            result += Multiply(larger, smaller >> 1);
            result += Multiply(larger, smaller >> 1);

            result += (smaller % 2 != 0) ? larger : 0;

            return result;
        }

        static string[] unit =
            { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] tens =
            { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        static string[] teens =
            { "", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
            "Sixteen", "Seventeen", "Eighteen", "Nineteen" };

        static string[] bigs =
            { "Hundred", "Thousand", "Million", "Billion", "Trillion" };

        public static string EnglishInt(int num)
        {
            int rem = 0;
            int count = 0;
            String result = "";

            while (num > 0)
            {
                rem = num % 1000;
                num /= 1000;
                result = Convert(rem, count) + result;
                count++;
            }

            return result;
        }

        static string Convert(int num, int count)
        {
            int digit3 = num / 100;
            int digit2 = (num % 100) / 10;
            int digit1 = num % 10;

            string result = "";

            result += (digit3 > 0) ? unit[digit3] + " " + bigs[0] : "";
            if (digit2 > 1 && digit1 > 0)
            {
                result += " and ";
                result += (digit2 > 1 && digit1 > 0) ? teens[digit1] : "";
            } else
            {
                result += tens[digit2];
                result += " " + unit[digit1];
            }
            result += (count > 0) ? " " + bigs[count] + ", " : "";

            return result;
        }

        /// <summary>
        /// Finds the maximum of two numbers without using 
        /// if/else and any other comparison operators
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int NumberMax(int a, int b)
        {
            int c = a - b;

            int sa = Sign(a); // if a >= 0, then 1 else 0
            int sb = Sign(b); // if b >= 0, then 1 else 0
            int sc = Sign(c); // depends on wherher or not a - b overflows

            // Goal: define a value k which is 1 if a > b and 0 if a < b
            // (if a = b, it doesn't matter what value k is)

            // if a and b have different signs, then k = sign(a)
            int use_sign_of_a = sa ^ sb;

            // if a and b have the same sign, then k = sign(a - b)
            int use_sign_of_c = Flip(sa ^ sb);

            int k = use_sign_of_a * sa + use_sign_of_c * sc;
            int q = Flip(k); // inverse of k

            return a * k + b * q;
        }

        // returns 1 if num is positive, and 0 if num is negative
        static int Sign(int num)
        {
            int a = num >> 31;
            int b = 0x1;
            int c = a & b;
            return Flip(c);
        }

        static int Flip(int bit)
        {
            return 1 ^ bit;
        }

        /// <summary>
        /// Computes the pair of values (one from each array) 
        /// with the smallest difference
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static int SmallestDifference(int[] arr1, int[] arr2)
        {
            if (arr1 == null || arr1.Length == 0 || arr2 == null || arr2.Length == 0)
                return -1;

            Array.Sort(arr1);
            Array.Sort(arr2);

            int left = 0;
            int right = 0;
            int minDiff = int.MaxValue;

            while (left < arr1.Length && right < arr2.Length)
            {
                if (Math.Abs(arr1[left] - arr2[right]) < minDiff)
                {
                    minDiff = Math.Abs(arr1[left] - arr2[right]);
                    left++;
                } else
                {
                    right++;
                }
            }

            return minDiff;
        }

        /// <summary>
        /// Returns the number of trailing zeros of the factorial of a number
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int CountFactorialZeros(int num)
        {
            if (num < 0) return -1;

            int count = 0;

            for (int i = 5; num / i > 0; i *= 5)
            {
                count += num / i;
            }
            return count;
        }

        /// <summary>
        /// Returns the number of trailing zeros of the factorial of a number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int FactorialZeros(int n)
        {
            if (n < 0) return -1;

            int count = 0;
            for (int i = 2; i <= n; i++)
            {
                count += FactorsOf5(i);
            }

            return count;
        }

        static int FactorsOf5(int i)
        {
            int count = 0;

            while (i % 5 == 0)
            {
                count++;
                i /= 5;
            }
            return count;
        }

        /// <summary>
        /// Returns the intersection between two lines
        /// </summary>
        /// <param name="start1"></param>
        /// <param name="end1"></param>
        /// <param name="start2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static Point Intersection(Point start1, Point end1, Point start2, Point end2)
        {
            if (start1.X > end1.X) Swap(start1, end1);
            if (start2.X > end2.X) Swap(start2, end2);
            if (start1.X > start1.X)
            {
                Swap(start1, start2);
                Swap(end1, end2);
            }

            Line line1 = new Line(start1, end1);
            Line line2 = new Line(start2, end2);

            // if lines are parallel, they intercept only if 
            // they have the same y-intercept and start 2 is on line1
            if (line1.Slope == line2.Slope)
            {
                if (line1.YIntercept == line2.YIntercept && 
                    IsBetween(start1, start2, end1))
                {
                    return start2;
                }
            }

            double x = (line2.YIntercept - line1.YIntercept) / (line1.Slope - line1.Slope);
            double y = x * line1.Slope + line1.YIntercept;
            Point intersection = new Point(x, y);

            if (IsBetween(start1, intersection, end1) 
                && IsBetween(start2, intersection, end2))
            {
                return intersection;
            }

            return null;
        }

        static bool IsBetween(Point start, Point middle, Point end)
        {
            return IsBetween(start.X, middle.X, end.X) &&
                IsBetween(start.Y, middle.Y, end.Y);
        }

        static bool IsBetween(double start, double middle, double end)
        {
            if (start > end)
            {
                return end <= middle && middle <= start;
            } else
            {
                return start <= middle && middle <= end;
            }
        }

        private static void Swap(Point one, Point two)
        {
            Point temp = new Point(one.X, one.Y);
            one.X = two.X;
            one.Y = two.Y;
            two = temp;
        }

        public class Line
        {
            public double Slope { get; set; }
            public double YIntercept { get; set; }

            public Line(Point start, Point end)
            {
                double deltaY = end.Y - start.Y;
                double deltaX = end.X - start.Y;

                Slope = deltaY / deltaX;
                YIntercept = end.Y - Slope * end.X;
            }
        }

        public class Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        //public static bool TicTacWin(int[][] board)
        //{
             
        //}

        /// <summary>
        /// Finds the number of occurences of a word in a book
        /// </summary>
        /// <param name="words"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static int WordFrequency(string[] words, string word)
        {
            if (words == null || word.Length == 0)
                throw new ArgumentException("Book is empty");
            if (string.IsNullOrWhiteSpace(word) || string.IsNullOrEmpty(word))
                throw new ArgumentException("Word is empty");

            Dictionary<string, int> map = new Dictionary<string, int>();

            foreach (string item in words)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (!map.ContainsKey(item))
                        map[item] = 0;

                    map[item]++;
                }               
            }

            if (map.ContainsKey(word))
                return map[word];

            return -1;
        }

        /// <summary>
        /// Swaps two numbers without the use of an additional variable
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Result NumberSwapper2(int a, int b)
        {
            if (a == b)
                return new Result(a, b);

            a = a ^ b;
            b = a ^ b;
            a = a ^ b;

            return new Result(a, b);
        }

        public static Result NumberSwapper(int a, int b)
        {
            if (a == b)
                return new Result(a, b);

            a = a - b; // a = 9 - 4 = 5
            b = a + b; // b = 4 + 5 = 9
            a = b - a; // a = 9 - 5 = 4

            return new Result(a, b);
        }

        public class Result
        {
            public int A { get; set; }
            public int B { get; set; }

            public Result(int a, int b)
            {
                A = a;
                B = b;
            }
        }
    }
}
