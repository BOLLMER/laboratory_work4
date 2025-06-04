#include <iostream>
#include <vector>
#include <algorithm>

class RC4 {
private:
    int n;
    std::vector<unsigned int> S;
    unsigned int i, j;
    unsigned int mod_mask;

    void ksa(const std::vector<unsigned int>& key) {
        const unsigned int size = 1 << n;
        const unsigned int key_len = key.size();

        S.resize(size);
        for(unsigned int k = 0; k < size; ++k) {
            S[k] = k;
        }

        unsigned int j = 0;
        for(unsigned int k = 0; k < size; ++k) {
            j = (j + S[k] + key[k % key_len]) & mod_mask;
            std::swap(S[k], S[j]);
        }
    }

public:
    RC4(int word_size, const std::vector<unsigned int>& key)
        : n(word_size), mod_mask((1 << word_size) - 1)
    {
        i = j = 0;
        ksa(key);
    }

    unsigned int generate() {
        const unsigned int size = 1 << n;

        i = (i + 1) & mod_mask;
        j = (j + S[i]) & mod_mask;
        std::swap(S[i], S[j]);

        const unsigned int t = (S[i] + S[j]) & mod_mask;
        return S[t];
    }
};

int main() {
    setlocale(LC_ALL, "Russian");
    std::string key_str = "SecretKey";
    std::vector<unsigned int> key;
    for(char c : key_str) {
        key.push_back(static_cast<unsigned char>(c));
    }

    RC4 rc4_8bit(8, key);

    std::cout << "RC4-8 (10 чисел): ";
    for(int k = 0; k < 10; ++k) {
        std::cout << std::hex << rc4_8bit.generate() << " ";
    }
    std::cout << "\n\n";

    std::vector<unsigned int> small_key = {0x3, 0x7, 0xB, 0xF};
    RC4 rc4_4bit(4, small_key);

    std::cout << "RC4-4 (10 чисел): ";
    for(int k = 0; k < 10; ++k) {
        std::cout << rc4_4bit.generate() << " ";
    }
    std::cout << "\n";

    return 0;
}
