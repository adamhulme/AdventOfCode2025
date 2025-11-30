StreamReader sr = new StreamReader("D:\\projects\\adventofcode2025\\day1\\to-update.csv");
var line = sr.ReadLine();
var toWrite = new List<string>();
while (line != null)
{
    Console.WriteLine(line);
    toWrite.Add(line);
    line = sr.ReadLine();
}
sr.Close();

File.WriteAllLines("D:\\projects\\adventofcode2025\\day1\\updated.csv", toWrite);

Console.ReadLine();