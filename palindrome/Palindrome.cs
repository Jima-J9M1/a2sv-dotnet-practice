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
string str_val = Console.ReadLine() ?? "string";
string input_str = $"The string {str_val} is " + (palindrome(str_val) ? "a palindrome" : "not a palindrome");
Console.WriteLine($"{str_val} {palindrome(str_val)}");