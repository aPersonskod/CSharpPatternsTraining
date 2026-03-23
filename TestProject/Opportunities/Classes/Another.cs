namespace TestProject.Opportunities.Classes;

public class Another
{
    [Test]
    public void FindCountOfBinarySymbols()
    {
        int n = 25;
        var x = Convert.ToString(n, 2);
        var res = x.Count(l => l == '1');
        Console.WriteLine(x);
        Console.WriteLine(res);
    }

    [Test]
    public void tesssdd()
    {
        var test = "AAAAABC";
        Console.WriteLine(Encode(test));
    }
    
    private static string Encode(string str) {
        var newStr = "";
        var counter = 1;
        char letter = str[0];
        for (int i = 0; i < str.Length; i++)
        {
            if(i == 0){
                letter = str[i];
                newStr += letter;
                continue;
            }
            if(letter == str[i]){
                counter++;
                if(i == str.Length - 1){
                    newStr += counter;
                }
            }
            else
            {
                letter = str[i];
                if (counter == 1)
                {
                    newStr += letter;
                }
                else
                {
                    newStr += $"{counter}{letter}";
                }
                counter = 1;
            }
        }
        return newStr;
    }
}