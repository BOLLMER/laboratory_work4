#include <iostream>
#include <vector>
#include <functional>
#include <random>

using namespace std;

struct GameResult {
    int score1;
    int score2;
};

bool alwaysCooperate(int round_number, const vector<bool>& self_choices, const vector<bool>& enemy_choices) {
    return true;
}

bool alwaysBetray(int round_number, const vector<bool>& self_choices, const vector<bool>& enemy_choices) {
    return false;
}

bool titForTat(int round_number, const vector<bool>& self_choices, const vector<bool>& enemy_choices) {
    if (enemy_choices.empty()) {
        return true;
    }
    return enemy_choices.back();
}

GameResult playGame(bool (*algo1)(int, const vector<bool>&, const vector<bool>&),
                    bool (*algo2)(int, const vector<bool>&, const vector<bool>&)) {
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> distrib(100, 200);
    int rounds = distrib(gen);

    vector<bool> choices1;
    vector<bool> choices2;
    int score1 = 0;
    int score2 = 0;

    for (int round = 1; round <= rounds; ++round) {
        bool choice1 = algo1(round, choices1, choices2);
        bool choice2 = algo2(round, choices2, choices1);

        if (choice1 && choice2) {
            score1 += 24;
            score2 += 24;
        } else if (!choice1 && !choice2) {
            score1 += 4;
            score2 += 4;
        } else if (choice1 && !choice2) {
            score2 += 20;
        } else {
            score1 += 20;
        }

        choices1.push_back(choice1);
        choices2.push_back(choice2);
    }

    return {score1, score2};
}

int main() {
    setlocale(LC_ALL, "Russian");
    GameResult result = playGame(titForTat, alwaysBetray);
    cout << "Счет алгоритма 1 (Tit-for-Tat): " << result.score1 << endl;
    cout << "Счет алгоритма 2 (Всегда предавать): " << result.score2 << endl;

    return 0;
}
