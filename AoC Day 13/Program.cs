using System.Drawing;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var pointList = new List<Point>();
    var instructions = new List<Tuple<char, int>>();
    FillData(data, pointList, instructions);

    var newPoints = Fold(pointList, instructions.First());

    Console.WriteLine($"Réponse 1 : {newPoints.Count}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var pointList = new List<Point>();
    var instructions = new List<Tuple<char, int>>();
    FillData(data, pointList, instructions);

    foreach (var instruction in instructions)
        pointList = Fold(pointList, instruction);

    var height = pointList.Max(x => x.Y) + 1;
    var width = pointList.Max(y => y.X) + 1;

    var outputArray = new string[height, width];
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            outputArray[i, j] = ".";
        }
    }

    foreach (var point in pointList)
        outputArray[point.Y, point.X] = "#";

    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
            Console.Write(outputArray[i, j]);

        Console.WriteLine();
    }
}

List<Point> Fold(List<Point> pointList, Tuple<char, int> instruction)
{
    var newPoints = new List<Point>();
    foreach (var point in pointList)
    {
        var newPoint = point;

        if (instruction.Item1 == 'x')
        {
            if (newPoint.X > instruction.Item2)
            {
                var distance = newPoint.X - instruction.Item2;
                newPoint.X -= distance * 2;
            }
        }
        else
        {
            if (newPoint.Y > instruction.Item2)
            {
                var distance = newPoint.Y - instruction.Item2;
                newPoint.Y -= distance * 2;
            }
        }

        if (!newPoints.Any(x => x.X == newPoint.X && x.Y == newPoint.Y))
            newPoints.Add(newPoint);
    }

    return newPoints;
}

void FillData(string[] data, List<Point> pointList, List<Tuple<char, int>> instructions)
{
    var isInstructions = false;
    foreach (var item in data)
    {
        if (isInstructions)
        {
            instructions.Add(new Tuple<char, int>(item.Split('=')[0].Last(), Int32.Parse(item.Split('=')[1])));
        }
        else if (!String.IsNullOrEmpty(item))
        {
            pointList.Add(new Point(Int32.Parse(item.Split(',')[0]), Int32.Parse(item.Split(',')[1])));
        }
        else
            isInstructions = true;
    }
}