#include <iostream>
#include <iomanip>
#include <cmath>

using namespace std;

double f(double x) {
    return sin(x) - 2 * x - 1;
}

void bisection(double a, double b, double epsilon) {
    int n = 0;
    cout << "\n--- Bisection Method ---\n";
    cout << left << setw(5) << "N" << setw(15) << "an" << setw(15) << "bn" << setw(15) << "bn - an" << endl;
    cout << fixed << setprecision(6);

    while ((b - a) >= epsilon) {
        cout << setw(5) << n << setw(15) << a << setw(15) << b << setw(15) << (b - a) << endl;

        double c = (a + b) / 2;
        if (f(c) == 0.0) break;
        else if (f(a) * f(c) < 0) b = c;
        else a = c;

        n++;
    }

    cout << setw(5) << n << setw(15) << a << setw(15) << b << setw(15) << (b - a) << endl;
    cout << "Final root: " << (a + b)/2 << "\n\n";
}

void newton(double x0, double epsilon) {
    int n = 0;
    double xn = x0;
    double xn_prev;

    cout << "\n--- Newton's Method ---\n";
    cout << left << setw(5) << "N" << setw(15) << "xn" << setw(15) << "xn+1" << setw(15) << "xn+1 - xn" << endl;
    cout << fixed << setprecision(6);

     do {
        xn_prev = xn;
        double f_val = f(xn_prev);
        double df_val = cos(xn_prev) - 2;
        xn = xn_prev - f_val / df_val;

        cout << setw(5) << n << setw(15) << xn_prev << setw(15) << xn << setw(15) << abs(xn - xn_prev) << endl;
        n++;

    } while (abs(xn - xn_prev) >= epsilon);

    cout << "Final root: " << xn << "\n\n";
}

void simple_iteration(double x0, double epsilon) {
    int n = 0;
    double xn = x0;
    double xn_prev;

    cout << "\n--- Simple Iteration Method ---\n";
    cout << left << setw(5) << "N" << setw(15) << "xn" << setw(15) << "xn+1" << setw(15) << "xn+1 - xn" << endl;
    cout << fixed << setprecision(6);

    do {
        xn_prev = xn;
        xn = (sin(xn_prev) - 1)/2;
        cout << setw(5) << n << setw(15) << xn_prev << setw(15) << xn << setw(15) << abs(xn - xn_prev) << endl;
        n++;

    } while (abs(xn - xn_prev) >= epsilon);

    cout << "Final root: " << xn << "\n\n";
}

int main() {
    const double epsilon = 1e-4;

    bisection(-1.0, 0.0, epsilon);

    newton(-0.5, epsilon);

    simple_iteration(-0.5, epsilon);

    return 0;
}
