class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        var filename = "D:\\projects\\adventofcode2025\\day5\\in.txt";
        StreamReader sr = new StreamReader(filename);
        var line = sr.ReadLine();
        var ranges = new List<Range>();
        while (line != "")
        {
            var range = line.Split('-');
            var low = long.Parse(range[0]);
            var high = long.Parse(range[1]);
            ranges.Add(new Range(low,high));
            line = sr.ReadLine();
        }
        
        for (int i = 0; i < ranges.Count; i++)
        {
            var r1 = ranges[i];
            for (int j = 0; j < ranges.Count; j++)
            {
                if (i == j) continue;
                var r2 = ranges[j];
                if (r2.Low <= r1.Low && r2.High >= r1.High)
                {
                    ranges.Remove(r1);
                    i = -1;
                    break;
                }

                if (r1.Low <= r2.Low && r2.Low <= r1.High)
                {
                    r1.High = Math.Max(r1.High, r2.High);
                    ranges.Remove(r2);
                    i = -1;
                    break;
                }

                if (r1.Low <= r2.High && r2.High <= r1.High)
                {
                    r1.Low = Math.Min(r1.Low, r2.Low);
                    ranges.Remove(r2);
                    i = -1;
                    break;
                }
            }
        }
        // P2
        long count = 0;
        ranges.ForEach(range => count += (range.High - range.Low + 1));
        line = sr.ReadLine();
        Console.WriteLine(count);
        // P1
        var freshCount = 0;
        while (line != null)
        {
            var toCheck = long.Parse(line);
            if (ranges.Any(r => r.Low <= toCheck && r.High >= toCheck))
                freshCount++;

            line = sr.ReadLine();
        }
        Console.WriteLine(freshCount);
        Console.ReadLine();
    }
}    

class Range
{
    public Range (long low, long high)
    {
        Low = low;
        High = high;
    }

    public long Low { get; set;}
    public long High { get; set;}
}