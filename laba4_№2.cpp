#include <iostream>
#include <vector>
#include <random>
#include <cmath>
#include <algorithm>
#include <map>
#include <numeric>

using namespace std;

vector<double> generateDoubleArray(int n) {
    random_device rd;
    mt19937 gen(rd());
    uniform_real_distribution<> dis(100.0, 900.0);
    vector<double> arr(n);
    for (int i = 0; i < n; ++i) {
        arr[i] = dis(gen);
    }
    return arr;
}

pair<int, double> task2(const vector<double>& arr, double A) {
    int count = 0;
    double product =1.0;
    for (double num : arr) {
        if (abs(num) > A)
        {
            count++;
            product = product * abs(num);
        }
    }
    return {count, product};
}

void task3(vector<double>& arr) {
    for (int i = 1; i < arr.size(); i += 2) {
        int num = static_cast<int>(arr[i]);
        int hundreds = (num / 100) % 10;
        int tens = (num / 10) % 10;
        if (tens > hundreds) {
            swap(arr[i], arr[i - 1]);
        }
    }
}

vector<int> generateIntArray(int n) {
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> dis(10, 20);
    vector<int> arr(n);
    for (int i = 0; i < n; ++i) {
        arr[i] = dis(gen);
    }
    return arr;
}

int findMostFrequent(const vector<int>& arr) {
    map<int, int> freq;
    for (int num : arr) freq[num]++;

    int maxCount = 0, result = 0;
    for (auto& [num, count] : freq) {
        if (count > maxCount) {
            maxCount = count;
            result = num;
        }
    }
    return result;
}

int reverseDigits(int num) {
    string s = to_string(abs(num));
    sort(s.rbegin(), s.rend());
    int res = stoi(s);
    return num < 0 ? -res : res;
}

void processTask5(vector<int>& arr) {
    for (int& num : arr) num = reverseDigits(num);
    sort(arr.rbegin(), arr.rend());
}

int main() {
    setlocale(LC_ALL, "Russian");
    int n = 10;
    vector<double> arr1 = generateDoubleArray(n);
    cout << "Массив 1: ";
    for (double num : arr1) cout << num << " ";
    cout << "\n\n";

    double A;
    cout << "Введите A: ";
    cin >> A;
    auto [count, product] = task2(arr1, A);
    cout << "Элементов > A: " << count << "\n";
    cout << "Произведение после максимума: " << product << "\n\n";

    task3(arr1);
    cout << "После обмена: ";
    for (double num : arr1) cout << num << " ";
    cout << "\n\n";

    int m = 15;
    vector<int> arr4 = generateIntArray(m);
    cout << "Массив 4: ";
    for (int num : arr4) cout << num << " ";
    cout << "\nЧастый элемент: " << findMostFrequent(arr4) << "\n\n";

    vector<int> arr5 = generateIntArray(n);

    cout << "Исходный массив 5: ";
    for (int num : arr5) cout << num << " ";
    int sumOrig = accumulate(arr5.begin(), arr5.end(), 0);
    cout << "\nСумма исходная: " << sumOrig << "\n";

    processTask5(arr5);
    int sumNew = accumulate(arr5.begin(), arr5.end(), 0);

    cout << "Обработанный массив 5: ";
    for (int num : arr5) cout << num << " ";
    cout << "\nСумма после обработки: " << sumNew << "\n";
    if (sumOrig == sumNew)
        cout << "Суммы равны";
    if (sumOrig > sumNew)
        cout << "Сумма первого больше на "<<sumOrig-sumNew;
    if (sumOrig < sumNew)
        cout << "Сумма второго больше на "<<sumNew-sumOrig;

    return 0;
}
