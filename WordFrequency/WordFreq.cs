Dictionary<string, int> WordFreq (string phrase)
{
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
    return WordFreq;
}




int Main(){
    Console.WriteLine("Enter a string: ");
    string str = Console.ReadLine();
    Dictionary<string, int> resultWordFreq = WordFreq(str);

    foreach(var frq in resultWordFreq)
    {
        Console.WriteLine($"Key:{frq.Key}, value:{frq.Value}");
    }
    return 0;
}

Main();