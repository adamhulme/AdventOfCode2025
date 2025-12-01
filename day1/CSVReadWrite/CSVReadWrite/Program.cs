using CSVReadWrite;

class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        StreamReader sr = new StreamReader("D:\\projects\\adventofcode2025\\day1\\to-update.csv");
        var line = sr.ReadLine();
        var dial = new DialMover();
        while (line != null)
        {
            var direction = line[0];
            var amountToMove = int.Parse(line.Substring(1));

            dial.MoveDial(direction, amountToMove);
            if (dial.DialPos == 100)
            {
                dial.DialPos = 0;
                dial.HitZeroCount++;
            }
            Console.WriteLine($"Current dial pos: {dial.DialPos}");
            Console.WriteLine($"Finish zero: {dial.HitZeroCount}");
            line = sr.ReadLine();
        }
        sr.Close();
        Console.WriteLine(dial.HitZeroCount);

        Console.ReadLine();
    }
}