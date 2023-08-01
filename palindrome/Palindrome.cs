bool palindrome(string str){
    int left = 0;
    int right = str.Length - 1;
    bool flag = true;
    while(left < right){
        if(str[left] != str[right]){
            flag = false;
        }
        left++;
        right--;
    }
    return flag;
}

Console.Write("Enter a string: ");
string str_val = Console.ReadLine();
Console.WriteLine(palindrome(str_val));