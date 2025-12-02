class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        StreamReader sr = new StreamReader("C:\\projects\\aoc25\\day2\\input.csv");
        var line = sr.ReadLine();
        var ranges = line.Split(',');
        var invalidList = new List<long>();
        foreach (var range in ranges)
        {
            var split = range.Split('-');
            var start = long.Parse(split[0]);
            var end = long.Parse(split[1]);
            for (long i = start; i <= end; i++)
            {
                if (CheckInvalid(i))
                    invalidList.Add(i);
            }
        }
        long count = 0;
        foreach (var num in invalidList)
        {
            count += num;
        }
        Console.WriteLine(count);
        sr.Close();
        Console.ReadLine();
    }

    // Part 1
    //public static bool CheckInvalid(long val)
    //{
    //    var valString = val.ToString();
    //    var length = valString.Length;
    //    if (length % 2 == 1)
    //    {
    //        return false;
    //    }
    //    var firstHalf = valString.Substring(0, length / 2);
    //    var secondHalf = valString.Substring(length / 2, length / 2);
    //    if (firstHalf == secondHalf)
    //    {
    //        Console.WriteLine($"Invalid number found: {val}");
    //        return true;
    //    }
    //    return false;
    //}

    // Part 2
    public static bool CheckInvalid(long val)
    {
        var length = val.ToString().Length;
        for (int i = 1; i <= length / 2; i++)
        {
            if (length % i != 0)
                continue;
            var sequence = val.ToString().Substring(0, i);
            for (int f = i; f < length; f += i)
            {
                var nextSequence = val.ToString().Substring(f, i);
                if (sequence != nextSequence)
                    break;
                if (f + i >= length)
                {
                    Console.WriteLine($"Invalid number found: {val}");
                    return true;
                }
            }

        }
        return false;
    }

}