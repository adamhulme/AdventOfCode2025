class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        StreamReader sr = new StreamReader("D:\\projects\\adventofcode2025\\day3\\input.txt.txt");
        var line = sr.ReadLine();
        var totalDigits = 12;
        long sum = 0;
        while (line != null)
        {
            var digits = new int[totalDigits];
            var previousDigitIndex = -1;
            for (int i = 0; i < totalDigits; i++)
            {
                var currentMax = -1;
                var toSearch = line.Substring(previousDigitIndex + 1, line.Length - (totalDigits-i) - previousDigitIndex);
                // Console.WriteLine($"Searching in {toSearch} for digit {i+1}");
                var tempPreviousDigitIndex = -1;
                for (int j = 0; j < toSearch.Length; j++)
                {
                    var num = int.Parse(toSearch[j].ToString());
                    if (num > currentMax)
                    {
                        currentMax = num;
                        tempPreviousDigitIndex = previousDigitIndex + j + 1;
                    }
                }
                previousDigitIndex = tempPreviousDigitIndex;
                digits[i] = currentMax;
            }
            var digitString = "";
            foreach (var digit in digits)
            {
                digitString += digit;
            }
            //Console.WriteLine(digitString);
            sum += long.Parse(digitString);
            line = sr.ReadLine();
        }
        Console.WriteLine($"Sum is {sum}");
        sr.Close();
        Console.ReadLine();
    }
}