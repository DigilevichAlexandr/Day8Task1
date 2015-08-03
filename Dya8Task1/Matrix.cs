using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dya8Task1
{
    #region NewChangeNMEventArgs
    internal sealed class NewChangeNMEventArgs<T> : EventArgs
    {
        private readonly int _i;
        private readonly int _j;
        private readonly T _v;
        public NewChangeNMEventArgs(int i, int j,T val)
        {
            _i = i;
            _j = j;
            _v = val;
        }

        public int I { get { return _i; } }
        public int J { get { return _j; } }
        public T V { get { return _v; } }
    }
    #endregion

    static class MatrixExtentor
    {
        public static Matrix<T> Addition<T>(this Matrix<T> m1, Matrix<T> m2)
            //where T : double
        {
            try
            {                
                    Matrix<T> m3 = new Matrix<T>(m1.N,m1.M,new T[m1.N,m1.M]);
                    for (int i = 0; i < m1.N; i++)
                        for (int j = 0; j < m1.M; j++)
                        {
                            dynamic dtemp1 = m1[i, j];
                            dynamic dtemp2 = m2[i, j];
                            m3[i,j] =(T) dtemp1 + dtemp2;
                        }
                    return m3;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return default(Matrix<T>);
        }

    }

    class Matrix<T>
    {
        public event EventHandler<NewChangeNMEventArgs<T>> NewValue;

        protected virtual void OnValueChanging(NewChangeNMEventArgs<T> e)
        {
            ////для синхронизации потоков
            //EventHandler<NewChangeNMEventArgs<T>> temp = NewValue;//возможна оптимизация компилятора!

            //Потокобезопасный вызов события оставлен на усмотрение разработчиков
            if (NewValue != null)
            {
                // может быть вызвано исключение NullReferenceException в том случае,
                // если обработчик был удален из списка уже после проверки
                NewValue(this, e);
            }
        }

        public void ChangeSingleCell(int i,int j,T v)
        {
            this[i, j] = v;

        }

        protected T[,] _matrix;
        public T this[int indi, int indj]
        {
            get { return _matrix[indi, indj]; }
            set 
            {
                OnValueChanging(new NewChangeNMEventArgs<T>(indi,indj, value));
                _matrix[indi, indj] = value;                
            }
        }

        public void Register()
        {
            this.NewValue += MatrixValue;
        }

        public void MatrixValue(Object sender,NewChangeNMEventArgs<T> eventArgs)
        {
            Console.WriteLine("changing value");
            Console.WriteLine("i = {0}, j {1} old value was {2}, this value is {3}",eventArgs.I,eventArgs.J,this[eventArgs.I,eventArgs.J],eventArgs.V);
        }

        int _n;
        int _m;

        public int N { get { return _n; } protected set { _n = value; } }
        public int M { get { return _m; } protected set { _m = value; } }

        public Matrix()
        {
            _matrix = new T[1, 1];
            _matrix[0, 0] = default(T);
        }

        /// <summary>
        /// первый аргумент количество строк, 
        /// второй столбцов
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <param name="arr"></param>
        public Matrix(int n, int m, T[,] arr)
        {
            try
            {
                if (n == 0)
                {
                    throw new Exception("n cann't be 0");
                }
                N = n;
                M = m;
                _matrix = new T[N, M];
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < M; j++)
                        _matrix[i, j] = arr[i, j];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Transpose()
        {
            T[,] arr = new T[M, N];
            for (int i = 0; i < N; i++)
                for (int j = i; j < M; j++)
                {
                    arr[i, j] = this[j, i];
                }
            _matrix = arr;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < N; i++)
            { 
                for (int j = 0; j < M; j++)
                {
                    str+=this[i, j].ToString()+" ";
                }
                str += "\n";
            }
            return str;   
        }

        //public static Matrix<double> operator +(Matrix<T> m1, Matrix<double> m2)
        //{
        //    try
        //    {
        //        if (m1 as new Matrix<double> == null)
        //            throw new TypeAccessException();
        //        if (m1.N != m2.N || m1.M != m2.M)
        //            throw new IndexOutOfRangeException();
        //        Matrix<double> m3 = new Matrix<double>();
        //        for (int i = 0; i < m1.N; i++)
        //            for (int j = i; j < m1.M; j++)
        //            {
        //                m3[i, j] = m1[i, j] + m2[i, j];
        //            }
        //        return m3;
        //    }
        //    catch (IndexOutOfRangeException e)
        //    {
        //        Console.WriteLine("Matrixes have different sizes. " + e.Message);
        //    }
        //    catch (TypeAccessException e)
        //        Console.WriteLine("Wrong type. " + e.Message);
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return default(Matrix<double>);
        //}
    }
}
