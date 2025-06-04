#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

int main() {
    setlocale(LC_ALL, "Russian");
    int n, k;

    cout << "Введите количество кандидатов (n): ";
    cin >> n;

    cout << "Введите количество избирателей (k): ";
    cin >> k;

    vector<vector<int>> rankings(k);
    vector<vector<int>> positions(k, vector<int>(n));

    for (int i = 0; i < k; ++i) {
        vector<int> ranking(n);
        cout << "\nВведите предпочтения избирателя " << i+1 << " (через пробел, "
             << n << " чисел от 1 до " << n << ")\n"
             << "Порядок: от самого предпочтительного к наименее: ";

        for (int j = 0; j < n; ++j) {
            int num;
            cin >> num;
            ranking[j] = num - 1;
        }
        rankings[i] = ranking;

        for (int pos = 0; pos < n; ++pos) {
            int candidate = ranking[pos];
            positions[i][candidate] = pos;
        }
    }

    vector<int> borda_scores(n, 0);
    for (const auto& ranking : rankings) {
        for (int pos = 0; pos < n; ++pos) {
            int candidate = ranking[pos];
            borda_scores[candidate] += (n - 1 - pos);
        }
    }

    int max_score = *max_element(borda_scores.begin(), borda_scores.end());
    vector<int> borda_winners;
    for (int i = 0; i < n; ++i) {
        if (borda_scores[i] == max_score) {
            borda_winners.push_back(i);
        }
    }

    vector<vector<int>> condorcet(n, vector<int>(n, 0));
    for (int i = 0; i < k; ++i) {
        const auto& pos = positions[i];
        for (int a = 0; a < n; ++a) {
            for (int b = 0; b < n; ++b) {
                if (a != b && pos[a] < pos[b]) {
                    condorcet[a][b]++;
                }
            }
        }
    }

    int condorcet_winner = -1;
    for (int a = 0; a < n; ++a) {
        bool wins_all = true;
        for (int b = 0; b < n; ++b) {
            if (a == b) continue;
            if (condorcet[a][b] <= k / 2) {
                wins_all = false;
                break;
            }
        }
        if (wins_all) {
            condorcet_winner = a;
            break;
        }
    }

    cout << "Победитель по Борду: ";
    for (int winner : borda_winners) {
        cout << winner + 1 << " ";
    }
    cout << endl;

    if (condorcet_winner != -1) {
        cout << "Победитель по Кондорсе: " << condorcet_winner + 1 << endl;
    } else {
        cout << "Нет победителя по Кондорсе" << endl;
    }

    bool borda_includes_condorcet = false;
    if (condorcet_winner != -1) {
        for (int w : borda_winners) {
            if (w == condorcet_winner) {
                borda_includes_condorcet = true;
                break;
            }
        }
        if (!borda_includes_condorcet) {
            cout << "Note: Different winners detected. The methods can produce different results based on voting specifics." << endl;
        }
    } else if (!borda_winners.empty()) {
        cout << "Note: Borda method selected a winner while Condorcet method did not." << endl;
    }

    return 0;
}
