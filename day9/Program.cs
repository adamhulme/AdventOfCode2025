public class Program
{
    static void Main(string[] args)
    {
        // Part1();
        Part2();
        Console.ReadLine();
    }

    public static void Part1()
    {
        var filename = "D:\\projects\\adventofcode2025\\day9\\in.txt";
        var lines = File.ReadAllLines(filename);
        var coordinates = new List<Coordinate>();
        foreach (var line in lines)
        {
            var split = line.Split(',');
            coordinates.Add(new Coordinate(long.Parse(split[0]), long.Parse(split[1])));
        }

        // brute force
        long currentMax = 0;
        for (int i = 0; i < coordinates.Count -1; i++)
        {
            for (int j = i + 1; j < coordinates.Count; j++)
            {
                var area = Coordinate.Area(coordinates[i], coordinates[j]);
                if (area > currentMax)
                {
                    currentMax = area;
                }
            }
        }

        Console.WriteLine(currentMax);

        // more efficient: work in from corners
        var xSize = coordinates.Max(c => c.X);
        var ySize = coordinates.Max(c => c.Y);
        var nw = coordinates.Min(c => c.X + c.Y);
        var ne = coordinates.Min(c => xSize - c.X + c.Y);
        var sw = coordinates.Max(c => xSize - c.X + c.Y);
        var se = coordinates.Max(c => c.X + c.Y);
        var nwCoord = coordinates.First(c => c.X + c.Y == nw);
        Console.WriteLine($"nw: {nwCoord.X}, {nwCoord.Y}");
        var neCoord = coordinates.First(c => xSize - c.X + c.Y == ne);
        Console.WriteLine($"ne: {neCoord.X}, {neCoord.Y}");
        var swCoord = coordinates.First(c => xSize - c.X + c.Y == sw);
        Console.WriteLine($"sw: {swCoord.X}, {swCoord.Y}");
        var seCoord = coordinates.First(c => c.X + c.Y == se);
        Console.WriteLine($"se: {seCoord.X}, {seCoord.Y}");
        var a1 = (1 + seCoord.X - nwCoord.X) * (1 + seCoord.Y - nwCoord.Y);
        var a2 = (1 + neCoord.X - swCoord.X) * (1 + swCoord.Y - neCoord.Y);
        Console.WriteLine($"Part1: {Math.Max(a1, a2)}, {a1}, {a2}");
    }

    public static void Part2()
    {
        var filename = "D:\\projects\\adventofcode2025\\day9\\in.txt";
        var lines = File.ReadAllLines(filename);
        var coordinates = new List<Coordinate>();
        foreach (var line in lines)
        {
            var split = line.Split(',');
            coordinates.Add(new Coordinate(long.Parse(split[0]), long.Parse(split[1])));
        }

        var xSize = coordinates.Max(c => c.X);
        var ySize = coordinates.Max(c => c.Y);
        var nwOrdered = coordinates.OrderBy(c => c.X + c.Y).ToArray();
        var neOrdered = coordinates.OrderBy(c => xSize - c.X + c.Y).ToArray();
        var swOrdered = coordinates.OrderByDescending(c => xSize - c.X + c.Y).ToArray();
        var seOrdered = coordinates.OrderByDescending(c => c.X + c.Y).ToArray();
        long currentMax = 0;
        for (int i = 0; i < 100; i++)
        {
            var nwCoord = nwOrdered[i];
            var seCoord = new Coordinate(94581, 48595);
            var swCoord = swOrdered[i];
            var neCoord = new Coordinate(94581, 50187);

            if (Coordinate.Area(nwCoord, seCoord) > currentMax)
            {
                //if (coordinates.Any(c => c.X >= nwCoord.X && c.X <= seCoord.X && c.Y >= nwCoord.Y && c.Y <= seCoord.Y))
                //{
                //    nwCoord.Print();
                //    seCoord.Print();
                //    Console.WriteLine($"Within");
                //    coordinates.First(c => c.X >= nwCoord.X && c.X <= seCoord.X && c.Y >= nwCoord.Y && c.Y <= seCoord.Y).Print();
                //}
                if (!coordinates.Where(c => c != nwCoord && c != seCoord).Any(c =>
                    c.X > nwCoord.X && c.X < seCoord.X && c.Y > nwCoord.Y && c.Y < seCoord.Y))
                {
                    nwCoord.Print();
                    seCoord.Print();
                    currentMax = Coordinate.Area(nwCoord, seCoord);
                    Console.WriteLine(currentMax);
                }
            }

            if (Coordinate.Area(swCoord, neCoord) > currentMax)
            {
                //if (coordinates.Any(c => c.X >= swCoord.X && c.X <= neCoord.X && c.Y <= swCoord.Y && c.Y >= neCoord.Y))
                //{
                //    nwCoord.Print();
                //    seCoord.Print();
                //    Console.WriteLine($"Within");
                //    coordinates.First(c => c.X >= swCoord.X && c.X <= neCoord.X && c.Y <= swCoord.Y && c.Y >= neCoord.Y).Print();
                //}
                if (!coordinates.Where(c => c != swCoord && c!= neCoord).Any(c => 
                    c.X > swCoord.X && c.X < neCoord.X && c.Y < swCoord.Y && c.Y > neCoord.Y))
                {
                    swCoord.Print();
                    neCoord.Print();
                    currentMax = Coordinate.Area(swCoord, neCoord);
                    Console.WriteLine(currentMax);
                }
            }
        }

        Console.Write(currentMax);
    }
}

public class Coordinate
{
    public Coordinate(long x, long y)
    {
        X = x;
        Y = y;
    }

    public long X { get; set; }
    public long Y { get; set; }

    public static long Area(Coordinate a, Coordinate b)
    {
        return (Math.Abs(a.X - b.X) + 1) * (Math.Abs(a.Y - b.Y) + 1);
    }
    public void Print()
    {
        Console.WriteLine($"X: {X}, Y: {Y}");
    }
}