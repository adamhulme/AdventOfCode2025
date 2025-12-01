using CSVReadWrite;

class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        StreamReader sr = new StreamReader("D:\\projects\\adventofcode2025\\day1\\to-update.csv");
        var line = sr.ReadLine();
        var dial = new DialMoverBruteForce();
        var zeroCount = 0;
        while (line != null)
        {
            var direction = line[0];
            var amountToMove = int.Parse(line.Substring(1));

            dial.MoveDial(direction, amountToMove);
            Console.WriteLine($"Current dial pos: {dial.DialPos}");
            Console.WriteLine($"Hit zero: {dial.HitZeroCount}");
            line = sr.ReadLine();
        }
        sr.Close();
        Console.WriteLine(dial.HitZeroCount);

        Console.ReadLine();
    }
}