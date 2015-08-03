using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dya8Task1
{
    class SquereMatrix<T> : Matrix<T>
    {
        public SquereMatrix()
        {
            _matrix = new T[1, 1];
            _matrix[0, 0] = default(T);
        }

        public SquereMatrix(int n, T[,] arr)
        {
            try
            {
                if (n == 0)
                {
                    throw new Exception("n cann't be 0");
                }
                N = n;
                M = n;
                _matrix = new T[N, N];
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < M; j++)
                        _matrix[i, j] = arr[i, j];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
