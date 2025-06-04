using System;

class Program
{
    static double F(double x)
    {
        return Math.Sin(x) - 2 * x - 1;
    }

    static void Bisection(double a, double b, double epsilon)
    {
        int n = 0;
        Console.WriteLine("\n--- Bisection Method ---");
        Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-15}", "N", "an", "bn", "bn - an");

        while ((b - a) >= epsilon)
        {
            Console.WriteLine("{0,-5}{1,-15:F6}{2,-15:F6}{3,-15:F6}", n, a, b, (b - a));

            double c = (a + b) / 2;
            if (F(c) == 0.0) break;
            else if (F(a) * F(c) < 0) b = c;
            else a = c;

            n++;
        }

        Console.WriteLine("{0,-5}{1,-15:F6}{2,-15:F6}{3,-15:F6}", n, a, b, (b - a));
        Console.WriteLine("Final root: {0:F6}\n", (a + b) / 2);
    }

    static void Newton(double x0, double epsilon)
    {
        int n = 0;
        double xn = x0;
        double xn_prev;

        Console.WriteLine("\n--- Newton's Method ---");
        Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-15}", "N", "xn", "xn+1", "xn+1 - xn");

        do
        {
            xn_prev = xn;
            double f_val = F(xn_prev);
            double df_val = Math.Cos(xn_prev) - 2;
            xn = xn_prev - f_val / df_val;

            Console.WriteLine("{0,-5}{1,-15:F6}{2,-15:F6}{3,-15:F6}", n, xn_prev, xn, Math.Abs(xn - xn_prev));
            n++;

        } while (Math.Abs(xn - xn_prev) >= epsilon);

        Console.WriteLine("Final root: {0:F6}\n", xn);
    }

    static void SimpleIteration(double x0, double epsilon)
    {
        int n = 0;
        double xn = x0;
        double xn_prev;

        Console.WriteLine("\n--- Simple Iteration Method ---");
        Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-15}", "N", "xn", "xn+1", "xn+1 - xn");

        do
        {
            xn_prev = xn;
            xn = (Math.Sin(xn_prev) - 1) / 2;

            Console.WriteLine("{0,-5}{1,-15:F6}{2,-15:F6}{3,-15:F6}", n, xn_prev, xn, Math.Abs(xn - xn_prev));
            n++;

        } while (Math.Abs(xn - xn_prev) >= epsilon);

        Console.WriteLine("Final root: {0:F6}\n", xn);
    }

    static void Main()
    {
        const double epsilon = 1e-4;

        Bisection(-1.0, 0.0, epsilon);

        Newton(-0.5, epsilon);

        SimpleIteration(-0.5, epsilon);
    }
}