using System;
using System.Runtime.ExceptionServices;
using System.Text;

public class Program
{
    static void Main(string[] args)
    {
        Part1();
        // Part2();
        Console.ReadLine();
    }

    public static void Part1()
    {
        var filename = "C:\\projects\\aoc25\\day8\\in.txt";
        var lines = File.ReadAllLines(filename);
        var coordinates = new Coordinate[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var coords = lines[i].Split(',');
            coordinates[i] = new Coordinate(long.Parse(coords[0]), long.Parse(coords[1]), long.Parse(coords[2]));
        }

        var circuits = new List<Circuit>();
        foreach (var coord in coordinates)
        {
            circuits.Add(new Circuit() { Elements = new List<Coordinate>() { coord } });
        }

        var distanceConnections = new SortedList<double, (Coordinate, Coordinate)>();
        for (int i = 0; i < coordinates.Length - 1; i++)
        {
            Console.WriteLine(i);
            for (int j = i + 1; j < coordinates.Length; j++)
            {
                distanceConnections.Add(coordinates[i].DistanceTo(coordinates[j]), (coordinates[i], coordinates[j]));
            }
        }

        for (int i = 0; i < 1000; i++)
        {
            var box1 = distanceConnections.GetValueAtIndex(i).Item1;
            var box2 = distanceConnections.GetValueAtIndex(i).Item2;

            if (circuits.Any(c => c.Contains(box1) && c.Contains(box2)))
            {
                continue;
            }
            else if (circuits.Any(c => c.Contains(box1)))
            {
                if (circuits.Any(c => c.Contains(box2)))
                {
                    var c2Index = circuits.IndexOf(circuits.First(c => c.Contains(box2)));
                    circuits.First(c => c.Contains(box1)).Elements.AddRange(circuits[c2Index].Elements);
                    circuits.RemoveAt(c2Index);
                }
                else
                    circuits.First(c => c.Contains(box1)).Elements.Add(box2);
            }
            else if (circuits.Any(c => c.Contains(box2)))
            {
                circuits.First(c => c.Contains(box2)).Elements.Add(box1);
            }
            else
            {
                circuits.Add(new Circuit() { Elements = new List<Coordinate>() { box1, box2 } });
            }
        }

        var totalInCircuit = 0;
        foreach (var circuit in circuits)
        {
            totalInCircuit += circuit.Elements.Count;
        }

        Console.WriteLine($"Total Circuits: {circuits.Count}");
        var ordered = circuits.OrderByDescending(c => c.Elements.Count).ToArray();
        Console.WriteLine($"{ordered[0].Elements.Count} x {ordered[1].Elements.Count} x {ordered[2].Elements.Count} = {ordered[0].Elements.Count * ordered[1].Elements.Count * ordered[2].Elements.Count}");
    }

    public static void Part2()
    {
        var filename = "C:\\projects\\aoc25\\day8\\in.txt";
        var lines = File.ReadAllLines(filename);
        var coordinates = new Coordinate[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var coords = lines[i].Split(',');
            coordinates[i] = new Coordinate(long.Parse(coords[0]), long.Parse(coords[1]), long.Parse(coords[2]));
        }

        var circuits = new List<Circuit>();
        foreach (var coord in coordinates)
        {
            circuits.Add(new Circuit() { Elements = new List<Coordinate>() { coord } });
        }

        var distanceConnections = new SortedList<double, (Coordinate, Coordinate)>();
        for (int i = 0; i < coordinates.Length - 1; i++)
        {
            Console.WriteLine(i);
            for (int j = i + 1; j < coordinates.Length; j++)
            {
                distanceConnections.Add(coordinates[i].DistanceTo(coordinates[j]), (coordinates[i], coordinates[j]));
            }
        }

        for (int i = 0; i < distanceConnections.Count; i++)
        {
            var box1 = distanceConnections.GetValueAtIndex(i).Item1;
            var box2 = distanceConnections.GetValueAtIndex(i).Item2;

            if (circuits.Any(c => c.Contains(box1) && c.Contains(box2)))
            {
                continue;
            }
            else if (circuits.Any(c => c.Contains(box1)))
            {
                if (circuits.Any(c => c.Contains(box2)))
                {
                    var c2Index = circuits.IndexOf(circuits.First(c => c.Contains(box2)));
                    circuits.First(c => c.Contains(box1)).Elements.AddRange(circuits[c2Index].Elements);
                    circuits.RemoveAt(c2Index);
                }
                else
                    circuits.First(c => c.Contains(box1)).Elements.Add(box2);
            }
            else if (circuits.Any(c => c.Contains(box2)))
            {
                circuits.First(c => c.Contains(box2)).Elements.Add(box1);
            }
            else
            {
                circuits.Add(new Circuit() { Elements = new List<Coordinate>() { box1, box2 } });
            }

            if (circuits.Count == 1)
            {
                box1.Print();
                box2.Print();
                Console.WriteLine($"Part 2: {box1.X * box2.X}");
                break;
            }
        }
    }
}

public class Coordinate
{
    public Coordinate(long x, long y, long z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }

    public double DistanceTo(Coordinate other)
    {
        return Math.Sqrt(Math.Pow((other.X - X), 2) + Math.Pow((other.Y - Y), 2) + Math.Pow((other.Z - Z), 2));
    }

    public void Print()
    {
        Console.WriteLine($"X: {X}, Y: {Y}, Z: {Z}");
    }

}

public class Circuit
{
    public List<Coordinate> Elements { get; set; }

    public bool Contains(Coordinate c) => Elements.Any(e => e == c);
}
