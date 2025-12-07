using System;
using System.Runtime.ExceptionServices;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Part1();
        Part2();
        Console.ReadLine();
    }

    public static void Part1()
    {
        var filename = "C:\\projects\\aoc25\\day7\\ex.txt";
        var lines = File.ReadAllLines(filename);
        for (int i = 0; i < lines[0].Length; i++)
            if (lines[0][i] == 'S')
                lines[1] = ReplaceWithBeam(lines[1], i);
        Console.WriteLine(lines[0]);
        Console.WriteLine(lines[1]);
        var splitCount = 0;
        for (int row = 1; row < lines.Length-1; row++)
        {
            var line = lines[row];
            for (int col = 0; col < line.Length; col++)
            {
                if (line[col] == '|')
                {
                    if (lines[row + 1][col] == '^')
                    {
                        lines[row + 1] = ReplaceWithBeam(lines[row + 1], col - 1);
                        lines[row + 1] = ReplaceWithBeam(lines[row + 1], col + 1);
                        splitCount++;
                    }
                    else
                    {
                        lines[row + 1] = ReplaceWithBeam(lines[row + 1], col);
                    }
                }
            }
            Console.WriteLine(line);
        }

        Console.WriteLine($"Part 1: {splitCount}");
    }

    public static int NUM_TIMELINES = 0;

    public static void Part2()
    {
        var filename = "C:\\projects\\aoc25\\day7\\in.txt";
        var lines = File.ReadAllLines(filename);

        // Convert to int array then use dynamic programming
        var grid = new long[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                if (lines[i][j] == '^')
                    grid[i, j] = -1;
            }
        }


        for (int i = 0; i < lines[0].Length; i++)
            if (lines[0][i] == 'S')
                grid[1, i]++;

        for (int row = 1; row < lines.Length - 1; row++)
        {
            for (int col = 0; col < lines[0].Length; col++)
            {
                //if (grid[row, col] == -1)
                //    Console.Write("^ ");
                //else
                //    Console.Write(grid[row, col] + " ");
                if (grid[row, col] > 0)
                {
                    if (grid[row + 1, col] == -1)
                    {
                        grid[row + 1, col - 1] += grid[row, col];
                        grid[row + 1, col + 1] += grid[row, col];
                    }
                    else
                    {
                        grid[row + 1, col] += grid[row, col];
                    }
                }
            }
            // Console.WriteLine();
        }

        long sumRoutes = 0;
        var finalRow = lines.Length - 1;
        for (int col = 0; col < lines[0].Length; col++)
        {
            sumRoutes += grid[finalRow, col];
        }

        // Recursion: too slow
        //for (int i = 0; i < lines[0].Length; i++)
        //    if (lines[0][i] == 'S')
        //    {
        //        NUM_TIMELINES = 1;
        //        TraceBeam(lines, 1, i);
        //    }

        Console.WriteLine($"Part 2: {sumRoutes}");
    }

    private static string ReplaceWithBeam(string s, int col)
    {
        var sb = new StringBuilder(s);
        sb[col] = '|';
        return sb.ToString();
    }

    public static void TraceBeam(string[] lines, int row, int col)
    {
        while (row < lines.Length - 1)
        {
            if (lines[row + 1][col] == '^')
            {
                NUM_TIMELINES++;
                TraceBeam(lines, row, col + 1);
                col--;
            }
            row++;
        }

        // Too slow
        //if (lines[row][col] == '^')
        //{
        //    NUM_TIMELINES++;
        //    TraceBeam(lines, row+1, col - 1);
        //    TraceBeam(lines, row+1, col + 1);
        //}
        //else
        //{
        //    if (row < lines.Length-1)
        //        TraceBeam(lines, row + 1, col);
        //}
    }
}    
