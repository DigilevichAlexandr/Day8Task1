using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dya8Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] iarray= new int[10,5];
            Random r = new Random(10);
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 5; j++)
                    iarray[i,j]=r.Next(10);
            Matrix<int> m = new Matrix<int>(10,5,iarray);
            Console.WriteLine(m);
            m.Register(m);
            m.ChangeSingleCell(4, 2,777);
            Console.WriteLine();            

            double[,] darray = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    darray[i, j] =r.Next(10);
            SquereMatrix<double> sqm = new SquereMatrix<double>(3,darray);
            Console.WriteLine(sqm);
            sqm.ChangeSingleCell(2, 2, 77.7);
            Console.WriteLine();
            
            //конструктор отзеркалит нижнюю половину
            string[,] strarray = new string[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    strarray[i, j] = r.Next(10).ToString();
            SymmetricMatrix<string> smm = new SymmetricMatrix<string>(3, strarray);
            smm.ChangeSingleCell(2, 2, "777");
            Console.WriteLine(smm);

            DateTime[] dtarray = new DateTime[3];
            for (int i = 0; i < 3; i++)
                    dtarray[i] = new DateTime((long)(i+1)*1000000000000000000);
            DiagonalMatrix<DateTime> dm = new DiagonalMatrix<DateTime>(3, dtarray);
            dm.ChangeSingleCell(2, 2, new DateTime((long)7770000000000000));
            Console.WriteLine(dm);

            Console.Read();

        }
    }
}
