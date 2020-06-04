using System;

namespace Chernyak_4
{
    class Program
    {
        static void Main(string[] args)
        {
            //В зависимости от языка системы дробные числа с точкой не конвертируются из строк
            //поэтому точки вручную заменяем на запятые
            //.Replace(".", ",")

            Console.WriteLine("Выполнила Черняк Оксана Александровна, АЭМ-111");

            Console.Write("Введите A: ");
            double a = double.Parse(Console.ReadLine().Replace(".", ","));
            Console.Write("Введите X: ");
            double x = double.Parse(Console.ReadLine().Replace(".", ","));
            Console.Write("Введите Z: ");
            double z = double.Parse(Console.ReadLine().Replace(".", ","));

            Console.WriteLine("Введите количество итераций: ");
            int iterations = int.Parse(Console.ReadLine());

            //На больших N увеличивается погрешность и теряется точность,
            //а значения мы всё равно проверить не можем
            //Проверить можно на WolframAlpha командой
            //Table[(0.8 k z^k - Sin[-x^(3 k - 2) + 3 (Pi/2) k])/((k + 1)! + a^k z^(k + 3))^(1/3), {k, 1, 5}]

            double U = 0; //результат вычисления U
            int sign = 1; //знак перед дробью
            double x_3_2 = x; //X в текущей степени * 3 - 2
            double fact_1 = 2; //факториал текущего числа + 1
            double ai = a; //A в текущей степени
            double zi = z; //A в текущей степени
            double zi_3 = z * z * z; //Z в текущей степени + 3
            for (int i = 1; i <= iterations; i++)
            {
                //1. -sin(a + 3 * pi / 2) = cos(a)
                //2. -sin(a + 3 * pi) = sin(a)
                //3. -sin(a + 9 * pi / 2) = cos(a)
                //4. -sin(a + 9 * pi) = sin(a)
                //...
                //i. -sin(a + 3 * i * pi / 2) = cos(a)..sin(a)..cos(a)...

                U += sign * (0.8 * i * zi - Math.Sin(x_3_2 + 3 * i * Math.PI / 2)) /
                     Math.Pow(fact_1 + ai * zi_3, 1.0 / 3);
                sign = -sign;
                x_3_2 *= x * x * x;
                fact_1 *= i + 2;
                ai *= a;
                zi *= z;
                zi_3 *= z;
            }

            Console.WriteLine("При N={0:00}, U={1:00.000}", iterations, U);

            Console.ReadKey(); //ожидание любой клавиши
        }
    }
}