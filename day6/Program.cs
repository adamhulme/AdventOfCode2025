class Program
{
    static void Main(string[] args)
    {
        // Part1();
        Part2();
    }

    public static void Part1()
    {
        var filename = "C:\\projects\\aoc25\\day6\\ex.txt";
        StreamReader sr = new StreamReader(filename);
        var lines = File.ReadAllLines(filename);
        var numbersPerProblem = lines.Count() - 1;
        var problemCount = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Count();
        var operators = lines[numbersPerProblem].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var problemTotals = new long[problemCount];
        var line = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        for (int k = 0; k < problemCount; k++)
        {
            problemTotals[k] = int.Parse(line[k]);
        }

        string op;
        for (int i = 1; i < numbersPerProblem; i++)
        {
            line = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < problemCount; j++)
            {
                op = operators[j];
                if (op == "+")
                    problemTotals[j] += int.Parse(line[j]);
                else if (op == "*")
                    problemTotals[j] *= int.Parse(line[j]);
            }
        }

        Console.WriteLine($"Part 1: {problemTotals.Sum()}");
        Console.ReadLine();
    }

    public static void Part2()
    {
        var filename = "C:\\projects\\aoc25\\day6\\in.txt";
        StreamReader sr = new StreamReader(filename);
        var lines = File.ReadAllLines(filename);
        var numbersPerProblem = lines.Count() - 1;
        var problemCount = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Count();
        var operators = lines[numbersPerProblem].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var problemWidths = new int[problemCount];
        var problemIndex = -1;
        var width = 0;
        char current;
        for (int i = 0; i < lines[numbersPerProblem].Length; i++)
        {
            current = lines[numbersPerProblem][i];
            if (current == '+' || current == '*')
            {
                if (problemIndex != -1)
                {
                    problemWidths[problemIndex] = width;
                }
                problemIndex++;
                width = 0;
            }
            else
            {
                width++;
            }
        }
        problemWidths[problemCount-1] = width + 1;

        var problemNumbers = new string[problemCount, numbersPerProblem];
        var line = "";
        var lineIndex = 0;
        for (int i = 0; i < numbersPerProblem; i++)
        {
            line = lines[i];
            lineIndex = 0;
            for (int j = 0; j < problemCount; j++)
            {
                var num = line.Substring(lineIndex, problemWidths[j]);
                problemNumbers[j, i] = num; 
                lineIndex += problemWidths[j] + 1;
            }
        }

        long totaltotal = 0;
        for (int i = 0; i < problemCount; i++)
        {
            var op = operators[i];
            var probWidth = problemWidths[i];
            long problemTotal = 0;
            if (op == "*")
                problemTotal = 1;
            for (int j = 0; j < probWidth; j++)
            {
                var num = "";
                for (int k = 0; k < numbersPerProblem; k++)
                {
                    var c = problemNumbers[i, k][j];
                    if (c != ' ')
                        num += c;
                }
                if (op == "+")
                    problemTotal += long.Parse(num);
                else if (op == "*")
                    problemTotal *= long.Parse(num);
            }
            totaltotal += problemTotal;
        }

        Console.WriteLine(totaltotal);
        Console.ReadLine();
    }
}    
