string phrase = "";
string[] names = phrase.Split(' ');

Dictionary<string, int> WordFreq= new Dictionary<string, int>();


foreach(var name in names)
{
    if(WordFreq.ContainsKey(name))
    {
        WordFreq[name] += 1;
    }else{
        WordFreq.Add(name, 1);
    }

}

foreach(var frq in WordFreq)
{
    Console.WriteLine($"Key:{frq.Key}, value:{frq.Value}");
}

/*
void fibonacci(int num){
    var fibonacci_number = new List<int>{1,1};
    int indx = 2;

    while(indx < num)
    {   
        int fib = fibonacci_number[indx - 1] + fibonacci_number[indx - 2];
        fibonacci_number.Add(fib);
        indx += 1;
    }

   foreach(var fib_num in fibonacci_number)
   {
       Console.Write(fib_num);
       Console.Write(" ");
   }
   
}

fibonacci(10);


*/
