class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        var filename = "C:\\projects\\aoc25\\day5\\in.txt";
        StreamReader sr = new StreamReader(filename);
        var line = sr.ReadLine();
        var ranges = new List<Range>();
        while (line != "")
        {
            var range = line.Split('-');
            var low = long.Parse(range[0]);
            var high = long.Parse(range[1]);
            ranges = ranges.Where(range => range.Low < low || range.High > high).ToList();
            var lowOverlap = ranges.Any(range => low <= range.Low && range.Low <= high && high <= range.High);
            if (lowOverlap)
                high = ranges.Where(range => low <= range.Low && range.Low <= high && high <= range.High).Min(r => r.Low);
            var highOverlap = ranges.Any(range =>  range.Low <= low && low <= range.High && high >= range.High);
            if (highOverlap)
                low = ranges.Where(range =>  range.Low <= low && low <= range.High && high >= range.High).Max(r => r.High);

            if (ranges.Any(r => r.Low <= low && r.High >= high))
            {
                line = sr.ReadLine();
                continue;
            }
            ranges.Add(new Range(low,high));
            line = sr.ReadLine();
        }
        // Merge ranges
        // too low: 354143734113765 too high: 354143734113774 wrong 354143734113767
        for (int i = 0; i < ranges.Count; i++)
        {
            var r1 = ranges[i];
            if (ranges.Where(r2 => r1 != r2).Any(r2 => r2.Low <= r1.Low && r2.High >= r1.High))
            {
                Console.WriteLine("hi");
                ranges.Remove(r1);
                continue;
            }

            if (ranges.Any(r2 => r2.Low == r1.High))
            {
                Console.WriteLine("hi1");
                var toRemove = ranges.First(r2 => r2.Low == r1.High);
                r1.High = toRemove.High;
                ranges.Remove(toRemove);
            }

            if (ranges.Any(r2 => r2.High == r1.Low))
            {
                Console.WriteLine("hi2");
                var toRemove = ranges.First(r2 => r2.High == r1.Low);
                r1.Low = toRemove.Low;
                ranges.Remove(toRemove);
            }
        }

        for (int i = 0; i < ranges.Count; i++)
        {
            var r1 = ranges[i];
            for (int j = 0; j < ranges.Count; j++)
            {
                if (i == j) continue;
                var r2 = ranges[j];
                if (ranges.Where(r2 => r1 != r2).Any(r2 => r2.Low <= r1.Low && r2.High >= r1.High))
                {
                    Console.WriteLine("hi");
                    ranges.Remove(r1);
                    continue;
                }

                if (r1.Low <= r2.Low && r2.Low <= r1.High)
                {
                    Console.WriteLine("hi3");
                    r1.High = Math.Max(r1.High, r2.High);
                    ranges.Remove(r2);
                    i = -1;
                    break;
                }
                if (r1.Low <= r2.High && r2.High <= r1.High)
                {
                    Console.WriteLine("hi4");
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