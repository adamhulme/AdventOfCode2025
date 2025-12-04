class Program
{
    public int zeroCount = 0;
    static void Main(string[] args)
    {
        var filename = "C:\\projects\\aoc25\\day4\\in.txt";
        StreamReader sr = new StreamReader(filename);
        var line = sr.ReadLine();
        var gridWidth = line.Length;
        var gridHeight = File.ReadLines(filename).Count();
        var grid = new char[gridHeight, gridWidth];
        var accessible = 0;
        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                grid[i, j] = line[j];
            }
            line = sr.ReadLine();
        }

        bool removed = true; // p2
        while (removed)
        {
            removed = false; //p2
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    if (grid[i, j] == '@')
                    {
                        var adjacentPositions = GetAdjacentPositions(i, j, gridHeight, gridWidth);
                        if (CountRollsInPositions(grid, adjacentPositions) < 4)
                        {
                            accessible++;
                            grid[i, j] = '.'; // p2
                            removed = true; // p2
                        }
                    }
                }
            }
        }
        Console.WriteLine(accessible);
        Console.ReadLine();
    }

    public static List<(int, int)> GetAdjacentPositions(int row, int col, int gridHeight, int gridWidth)
    {
        var adjacentPositions = new List<(int, int)>();
        row--; // N
        if (row >= 0)
            adjacentPositions.Add((row, col));
        col++; // NE
        if (row >= 0 && col < gridWidth)
            adjacentPositions.Add((row, col));
        row++; // E
        if (col < gridWidth)
            adjacentPositions.Add((row, col));
        row++; // SE
        if (row < gridHeight && col < gridWidth)
            adjacentPositions.Add((row, col));
        col--; // S
        if (row < gridHeight)
            adjacentPositions.Add((row, col));
        col--; // SW
        if (row < gridHeight && col >= 0)
            adjacentPositions.Add((row, col));
        row--; // W
        if (col >= 0)
            adjacentPositions.Add((row, col));
        row--; // NW
        if (col >= 0 && row >= 0)
            adjacentPositions.Add((row, col));

        return adjacentPositions;
    }

    public static int CountRollsInPositions(char[,] grid, List<(int, int)> positions)
    {
        int count = 0;
        foreach (var (r, c) in positions)
        {
            if (grid[r, c] == '@')
                count++;
        }
        return count;
    }
}    

