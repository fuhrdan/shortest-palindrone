//*****************************************************************************
//** 214. Shortest Palindrome   leetcode                                     **
//*****************************************************************************


// Helper function to compute the KMP table (prefix table)
int* getTable(char* arr, int len) {
    int* table = (int*)malloc(len * sizeof(int));
    int index = 0;
    table[0] = 0;

    for (int i = 1; i < len; i++) {
        if (arr[i] == arr[index]) {
            index++;
            table[i] = index;
        } else {
            if (index != 0) {
                index = table[index - 1];
                i--; // Recheck with the new index
            } else {
                table[i] = 0;
            }
        }
    }

    return table;
}

// Entry function to compute the shortest palindrome
char* shortestPalindrome(char* s) {
    int len = strlen(s);
    
    // Create temp string: s + '#' + reverse(s)
    int temp_len = 2 * len + 1;
    char* temp = (char*)malloc((temp_len + 1) * sizeof(char));
    strcpy(temp, s);
    temp[len] = '#';
    
    for (int i = 0; i < len; i++) {
        temp[len + 1 + i] = s[len - 1 - i]; // reversed part
    }
    temp[temp_len] = '\0'; // Ensure null-termination

    // Compute KMP table for temp
    int* table = getTable(temp, temp_len);

    // Find the length of the longest palindromic prefix
    int palindrome_len = table[temp_len - 1];

    // Calculate the length of the resulting string
    int new_len = len + (len - palindrome_len);
    char* result = (char*)malloc((new_len + 1) * sizeof(char));

    // Add reversed part before s (only the necessary part)
    for (int i = 0; i < len - palindrome_len; i++) {
        result[i] = s[len - 1 - i];
    }

    // Add the original string s
    strcpy(result + (len - palindrome_len), s);
    result[new_len] = '\0';

    // Cleanup
    free(table);
    free(temp);

    return result;
}