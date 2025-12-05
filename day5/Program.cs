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
            if (ranges.Any(r => r.Low < low && r.High > high))
            {
                line = sr.ReadLine();
                continue;
            }
            ranges = ranges.Where(range => range.Low < low || range.High > high).ToList();
            var overlapped = false;
            var lowOverlap = ranges.Where(range => low < range.Low && range.Low < high && high < range.High);
            foreach (var r in lowOverlap)
            {
                r.Low = low;
                overlapped = true;
            }
            var highOverlap = ranges.Where(range =>  range.Low < low && low < range.High && high > range.High);
            foreach (var r in highOverlap)
            {
                r.High = low;
                overlapped = true;
            }
            if (!overlapped)
                ranges.Add(new Range(low,high));
            line = sr.ReadLine();
        }
        // Merge ranges
        for (int i = 0; i < ranges.Count; i++)
        {
            var r1 = ranges[i];
            if (ranges.Any(r2 => r2.Low == r1.High))
            {
                var toRemove = ranges.First(r2 => r2.Low == r1.High);
                r1.High = toRemove.High;
                ranges.Remove(toRemove);
            }

            if (ranges.Any(r2 => r2.High == r1.Low))
            {
                var toRemove = ranges.First(r2 => r2.High == r1.Low);
                r1.Low = toRemove.Low;
                ranges.Remove(toRemove);
            }
        }
        //ranges.ForEach(range => Console.WriteLine($"{range.Low}-{range.High}"));
        //Console.WriteLine("Middle hit");
        line = sr.ReadLine();
        // P1
        var freshCount = 0;
        while (line != null)
        {
            var toCheck = long.Parse(line);
            if (ranges.Any(r => r.Low <= toCheck && r.High >= toCheck))
                freshCount++;

            line = sr.ReadLine();
        }
        // P2
        // var freshCount = 0;
        // for (long i = 0; i <= max; i++)
        // {
        //     var toCheck = long.Parse(line);
        //     if (ranges.Any(r => r.Item1 <= toCheck && r.Item2 >= toCheck))
        //         freshCount++;
        // }
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