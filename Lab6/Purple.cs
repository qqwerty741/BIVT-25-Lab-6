using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab6
{
    public class Purple
    {
        private (int, int, int, int) getDef(int[,] A, int[,] B)
        {
            return (A.GetLength(0), A.GetLength(1), B.GetLength(0), B.GetLength(1));
        }
        private (int, int) getDef(int[,] A)
        {
            return (A.GetLength(0), A.GetLength(1));
        }
        private int getDef(int[] A) { return A.Length; }
        public void Print(char end = '\n')
        {
            Console.Write("----------------" + end);
        }
        public void Print(int[,] array, char end = '\n')
        {
            for(int i = 0; i < array.GetLength(0); i++)
            {
                for(int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i,j], 3} ");
                }
                Console.WriteLine();
            }
            Print(end);
        }
        public void Print(int[][] array, char end = '\n')
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write($"{array[i][j],3} ");
                }
                Console.WriteLine();
            }
            Print(end);
        }
        public void Print(int[] array, char end = '\n')
        {
            for(int i = 0; i < array.GetLength(0); i++) Console.Write($"{array[i], 3}");
            Console.WriteLine();
            Print(end);
        }
        public void Print(int num, char end = '\n')
        {
            Console.Write($"{num}{end}");
        }
        public void Print(string s)
        {
            Console.WriteLine(s);
        }

        public int FindDiagonalMaxIndex(int[,] matrix)
        {
            int ans = 0;
            for (int i = 0; i < matrix.GetLength(0); i++) ans = (matrix[i, i] > matrix[ans, ans] ? i : ans);
            return ans;
        }
        public void SwapRowColumn(int[,] matrix, int rowIndex, int[,] B, int columnIndedx)
        {
            for(int i = 0; i < B.GetLength(0); i++)
            {
                (matrix[rowIndex, i], B[i, columnIndedx]) = (B[i, columnIndedx], matrix[rowIndex, i]);
            }
        }
        public void Task1(int[,] A, int[,] B)
        {
            int n1 = A.GetLength(0), m1 = A.GetLength(1);
            int n2 = B.GetLength(0), m2 = B.GetLength(1);
            if (n1 != m1 || n2 != m2) return;
            if (n1 != n2) return;
            // code here
            int r1 = FindDiagonalMaxIndex(A);
            int r2 = FindDiagonalMaxIndex(B);

            SwapRowColumn(A, r1, B, r2);
            // end

        }
        //-------------------------------------------------------------------------------------------------------
        public int CountPositiveElementsInRow(int[,] matrix, int row)
        {
            int n = matrix.GetLength(0), m = matrix.GetLength(1);
            int ans = 0;
            for (int i = 0; i < m; i++) if (matrix[row, i] > 0) ans += 1;
            return ans;
        }
        public int CountPositiveElementsInColumn(int[,] matrix, int col)
        {
            int n = matrix.GetLength(0), m = matrix.GetLength(1);
            int ans = 0;
            for (int i = 0; i < n; i++) if (matrix[i, col] > 0) ans += 1;
            return ans;
        }
        public void InsertColumn(ref int[,] A, int rowIndex, int columnIndex, int[,] B)
        {
            int n1 = A.GetLength(0), m1 = A.GetLength(1);
            int n2 = B.GetLength(0), m2 = B.GetLength(1);
            if (m1 != n2) return;
            int[,] temp = new int[n1 + 1, m1];
            for(int i = 0; i <= rowIndex; i++)
            {
                for(int j = 0; j < m1; j++)
                {
                    temp[i, j] = A[i, j];
                }
            }
            for(int i = 0; i < n2; i++)
            {
                temp[rowIndex + 1, i] = B[i, columnIndex];
            }
            for(int i = rowIndex + 2; i <= n1; i++)
            {
                for(int j = 0; j < m1; j++)
                {
                    temp[i,j] = A[i - 1, j];
                }
            }
            A = temp;
            return;
        }

        public void Task2(ref int[,] A, int[,] B)
        {
            int n1 = A.GetLength(0), m1 = A.GetLength(1);
            int n2 = B.GetLength(0), m2 = B.GetLength(1);
            if (m1 != n2) return;
            // code here
            Print(B);

            int r = 0, rm = 0,  c = 0, cm = 0;
            for(int i = 0; i < n1; i++)
            {
                int n = CountPositiveElementsInColumn(B, i);
                if (n > rm) (r, rm) = (i, n); 
            }
            for (int i = 0; i < n2; i++)
            {
                int n = CountPositiveElementsInRow(A, i);
                if (n > cm) (c, cm) = (i, n);
            }
            InsertColumn(ref A, c, r, B);
            // end

        }
        //-------------------------------------------------------------------------------------------------------
        private int[] toArray(int[,] matrix)
        {
            int n = matrix.GetLength(0), m = matrix.GetLength(1);
            int[] ans = new int[n * m];
            int c = 0;
            for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) ans[c++] = matrix[i, j];
            return ans;
        }
        public void ChangeMatrixValues(int[,] matrix)
        {
            int n = matrix.GetLength(0), m = matrix.GetLength(1);
            int[] arr = toArray(matrix);
            if(arr.Length < 5)
            {
                for (int i = 0; i < n; i++) for (int j = 0; j < m; j++) matrix[i, j] *= 2;
                return;
            }
            Array.Sort(arr);
            int a1 = arr[^1], a2 = arr[^2], a3 = arr[^3], a4 = arr[^4], a5 = arr[^5];
            int max = a1 + 1;
            for(int i = 0; i < n; i++) for(int j = 0; j < m; j++)
                {
                    if (matrix[i, j] == a1) (matrix[i, j], a1) = (matrix[i, j] * 2, max);
                    else if (matrix[i, j] == a2) (matrix[i, j], a2) = (matrix[i, j] * 2, max);
                    else if (matrix[i, j] == a3) (matrix[i, j], a3) = (matrix[i, j] * 2, max);
                    else if (matrix[i, j] == a4) (matrix[i, j], a4) = (matrix[i, j] * 2, max);
                    else if (matrix[i, j] == a5) (matrix[i, j], a5) = (matrix[i, j] * 2, max);
                    else matrix[i, j] /= 2;
                }
            return;
        }
        public void Task3(int[,] matrix)
        {

            // code here
            ChangeMatrixValues(matrix);
            // end

        }
        //-------------------------------------------------------------------------------------------------------
        public int[] CountNegativesPerRow(int[,] matrix)
        {
            int n, m;
            (n, m) = getDef(matrix);
            int[] ans = new int[n];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (matrix[i, j] < 0) ans[i]++;
                }
            }
            return ans;
        }
        public int FindMaxIndex(int[] array)
        {
            int n = getDef(array);
            int ans = 0;
            bool flag = false;
            for(int i = 0; i < n; i++)
            {
                if (array[ans] < array[i]) { ans = i; flag = true; }
            }
            if (!flag) return -1;
            return ans; 
        }
        public void Task4(int[,] A, int[,] B)
        {

            // code here
            int n1, n2, m1, m2;
            (n1, m1, n2, m2) = getDef(A, B);
            if (m1 != m2) return;
            int r1 = FindMaxIndex(CountNegativesPerRow(A)), r2 = FindMaxIndex(CountNegativesPerRow(B));
            if (r1 == -1 || r2 == -1) return;
            for(int i = 0; i < m1; i++)
            {
                (A[r1, i], B[r2, i]) = (B[r2, i], A[r1, i]);
            }
            // end

        }
        //-------------------------------------------------------------------------------------------------------
        private int[] getNegativeSorted(int[] A)
        {
            int n = A.Length;
            int c = 0;
            for(int i = 0; i < n; i++)
            {
                if (A[i] < 0) c++;
            }
            int[] ans = new int[c];
            c = 0;
            for (int i = 0; i < n; i++) if (A[i] < 0) ans[c++] = A[i];
            Array.Sort(ans);
            return ans;
        }
        
        public void SortNegativeAscending(int[] matrix)
        {
            int n = matrix.Length;
            int[] neg = getNegativeSorted(matrix);
            int c = 0;
            for (int i = 0; i < n; i++) if (matrix[i] < 0) matrix[i] = neg[c++];
            return;
        }
        public void SortNegativeDescending(int[] matrix)
        {
            int n = matrix.Length;
            int[] neg = getNegativeSorted(matrix);
            int c = neg.Length - 1;
            for (int i = 0; i < n; i++) if (matrix[i] < 0) matrix[i] = neg[c--];
            return;
        }
        public delegate void Sorting(int[] array);
        public void Task5(int[] matrix, Sorting sort)
        {

            // code here
            sort(matrix);
            // end

        }
        //-------------------------------------------------------------------------------------------------------
        public void SortRowsByMaxAscending(int[,] matrix)
        {
            int n, m;
            (n, m) = getDef(matrix);
            int[][] arr = new int[n][];
            for(int i = 0; i < n; i++)
            {
                int[] temp = new int[2];
                temp[0] = GetRowMax(matrix, i);
                temp[1] = i;
                arr[i] = temp;
            }
            Array.Sort(arr, (a, b) => a[0].CompareTo(b[0]));
            int[,] ans = new int[n, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    ans[i,j] = matrix[arr[i][1], j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    matrix[i,j] = ans[i,j];
                }
            }
            return;
        }
        public void SortRowsByMaxDescending(int[,] matrix)
        {
            int n, m;
            (n, m) = getDef(matrix);
            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
            {
                int[] temp = new int[2];
                temp[0] = GetRowMax(matrix, i);
                temp[1] = i;
                arr[i] = temp;
            }
            Array.Sort(arr, (a, b) =>
            {
                int comp = b[0].CompareTo(a[0]);
                if (comp != 0) return comp;
                return a[1].CompareTo(b[1]);
            });
            int[,] ans = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    ans[i, j] = matrix[arr[i][1], j];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = ans[i, j];
                }
            }
            return;
        }
        public int GetRowMax(int[,] matrix, int row)
        {
            int n, m;
            (n, m) = getDef(matrix);
            int ans = 0;
            for(int i = 0; i < m; i++)
            {
                if (matrix[row, ans] < matrix[row, i]) ans = i;
            }
            return matrix[row, ans];
        }
        public delegate void SortRowsByMax(int[,] matrix); 
        public void Task6(int[,] matrix, SortRowsByMax sort)
        {

            // code here
            sort(matrix);
            // end

        }
        //-------------------------------------------------------------------------------------------------------
        public int[] FindNegativeCountPerRow(int[,] matrix)
        {
            int n, m;
            (n, m) = getDef(matrix);
            int[] ans = new int[n];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (matrix[i, j] < 0) ans[i]++;
                }
            }
            return ans; 
        }
        public int[] FindMaxNegativePerColumn(int[,] matrix)
        {
            int n, m;
            (n, m) = getDef(matrix);
            int[] ans = new int[m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (ans[j] == 0 && matrix[i, j] < 0) ans[j] = matrix[i, j];
                    else if (ans[j] < matrix[i, j] && matrix[i, j] < 0) ans[j] = matrix[i, j];
                }
            }
            return ans;
        }
        public delegate int[] FindNegatives(int[,] matrix); 
        public int[] Task7(int[,] matrix, FindNegatives find)
        {
            int[] negatives = find(matrix);

            // code here

            // end

            return negatives;
        }
        //-------------------------------------------------------------------------------------------------------
        private void Add(ref int[,] matrix, int[] elem)
        {
            int n, m;
            (n,m) = getDef(matrix);
            if (m != elem.Length) return;
            int[,] ans = new int[n + 1, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    ans[i, j] = matrix[i, j];
                }
            }
            for(int i = 0; i < m; i++)
            {
                ans[n, i] = elem[i];
            }
            matrix = ans;
        }

        
        public int[,] DefineSeq(int[,] matrix)
        {
            int[,] temp = FindAllSeq(matrix);
            if (matrix.GetLength(1) == 1) return new int[0, 0];
            if(temp.GetLength(0) == 1)
            {
                if (matrix[1, 0] < matrix[1, 1]) return new int[1, 1] { { 1 } };
                return new int[1,1] { { -1 } };
            }

            return new int[1, 1] { { 0 } };
        }
        public int[,] FindAllSeq(int[,] matrix)
        {
            int n = 2, m = matrix.GetLength(1);
            if(m == 1) return new int[0,0];
            bool isGreater = matrix[1, 0] < matrix[1, 1];
            int[,] ans = new int[0, 2];
            int start = matrix[0,0];
            for(int i = 0; i < m - 1; i++)
            {
                if ((isGreater && matrix[1, i] <= matrix[1, i + 1]) || (!isGreater && matrix[1,i] >= matrix[1,i+1])) continue;
                else
                {
                    isGreater = !isGreater;
                    Add(ref ans, new int[]{ start, matrix[0,i] });
                    start = matrix[0,i];
                    i--;
                }
            }
            Add(ref ans, new int[]{ start, matrix[0,m - 1] });
            return ans;
        }
        public int[,] FindLongestSeq(int[,] matrix)
        {
            int[,] temp = FindAllSeq(matrix);
            if (matrix.GetLength(1) == 1) return new int[0, 0];
            int intl = 0;
            for(int i = 0; i < temp.GetLength(0); i++)
            {
                if (temp[intl, 1] - temp[intl, 0] < temp[i, 1] - temp[i, 0]) intl = i;
            }
            return new int[1,2] { { temp[intl, 0], temp[intl, 1] } }; 
        }
        public delegate int[,] MathInfo(int[,] matrix); 
        public int[,] Task8(int[,] matrix, MathInfo info)
        {
            int[,] answer = info(matrix);
            Print(matrix);
            Print(answer);
            Print();
            // code here

            // end

            return answer;
        }
        //-------------------------------------------------------------------------------------------------------
        public int CountSignFlips(double a, double b, double h, Func<double, double> func)
        {
            int counter = 0;
            // Хочется написать if(func(1) == FuncA(1)) и просто пойти анализировать...
            // ...ибо там только 1/2 точки смены знаков, а тесты не подразумевают такие погрешности...
            // ...но я добропорядочный человек, поэтому решу нормально
            bool isPositive = func(a) > 0;
            int ans = 0;
            for(double i = a + h; i <= b; i += h)
            {
                if (func(i - h) >= 0 && func(i) < 0) ans++;
                else if (func(i - h) <= 0 && func(i) > 0) ans++;
                double x = i;
            }

            return ans; 
        }
        public double FuncA(double x)
        {
            return (double)(x * x) - Math.Sin(x); 
        }
        public double FuncB(double x)
        {
            return Math.Pow(double.E, x) - (double)1;
        }
        public int Task9(double a, double b, double h, Func<double, double> func)
        {
            int answer = CountSignFlips(a, b, h, func);
            Print(answer);
            Print();
            // code here

            // end

            return answer;
        }
        //-------------------------------------------------------------------------------------------------------
        public void SortInCheckersOrder(int[][] array)
        {
            int n = array.Length;
            for(int i = 0; i < n; i++)
            {
                Array.Sort(array[i]);
                if(i % 2 == 1) Array.Reverse(array[i]);
            }
            return;
        }
        public int Sum(int[] a)
        {
            int ans = 0;
            for(int i = 0; i < a.Length; i++) ans += a[i];
            return ans;
        }
        public void SortBySumDesc(int[][] array)
        {
            int n = array.Length;
            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
            {
                int[] temp = new int[2];
                temp[0] = Sum(array[i]);
                temp[1] = i;
                arr[i] = temp;
            }
            Array.Sort(arr, (a, b) =>
            {
                int comp = b[0].CompareTo(a[0]);
                if (comp != 0) return comp;
                return a[1].CompareTo(b[1]);
            });
            int[][] ans = new int[n][];
            for(int i = 0; i < n; i++)
            {
                ans[i] = array[arr[i][1]];
            }
            for(int i = 0; i < n; i++)
            {
                array[i] = ans[i];
            }
            return; 
        }
        public void TotalReverse(int[][] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                Array.Reverse(array[i]);
            }
            Array.Reverse(array);
            return;
        }
        public void Task10(int[][] array, Action<int[][]> func)
        {

            // code here
            func(array);
            // end

        }
    }
}
