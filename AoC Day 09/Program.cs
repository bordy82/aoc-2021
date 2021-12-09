using Utilities;
using System.Drawing;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();
    var width = data[0].Length;
    var height = data.Length;

    var table = new int[height, width];

    for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
            table[i, j] = Int32.Parse(data[i][j].ToString());

    var lowPoints = GetLowPoints(table, height, width);

    var riskLevel = 0;
    foreach (var lowPoint in lowPoints)
        riskLevel += table[lowPoint.X, lowPoint.Y] + 1;

    Console.WriteLine($"Réponse 1 : {riskLevel}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var width = data[0].Length;
    var height = data.Length;

    var table = new int[height, width];

    for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
            table[i, j] = Int32.Parse(data[i][j].ToString());

    var listBasinSizes = new List<int>();
    var lowPoints = GetLowPoints(table, height, width);

    foreach(var lowPoint in lowPoints)
    {
        var basinPoint = new List<Point>() { lowPoint };

        var finished = false;
        while (!finished)
        {
            var addedPoint = new List<Point>();
            foreach (var point in basinPoint)
            {
                if (point.Y - 1 >= 0 && 
                    table[point.X, point.Y - 1] != 9 && 
                    !basinPoint.Any(x => x.X == point.X && x.Y == point.Y - 1) && 
                    !addedPoint.Any(x => x.X == point.X && x.Y == point.Y - 1))
                {
                    addedPoint.Add(new Point(point.X, point.Y - 1));
                }

                if (point.Y + 1 < width && 
                    table[point.X, point.Y + 1] != 9 && 
                    !basinPoint.Any(x => x.X == point.X && x.Y == point.Y + 1) && 
                    !addedPoint.Any(x => x.X == point.X && x.Y == point.Y + 1))
                {
                    addedPoint.Add(new Point(point.X, point.Y + 1));
                }

                if (point.X - 1 >= 0 && 
                    table[point.X - 1, point.Y] != 9 && 
                    !basinPoint.Any(x => x.X == point.X - 1 && x.Y == point.Y) && 
                    !addedPoint.Any(x => x.X == point.X - 1 && x.Y == point.Y))
                {
                    addedPoint.Add(new Point(point.X - 1, point.Y));
                }

                if (point.X + 1 < height && 
                    table[point.X + 1, point.Y] != 9 && 
                    !basinPoint.Any(x => x.X == point.X + 1 && x.Y == point.Y) && 
                    !addedPoint.Any(x => x.X == point.X + 1 && x.Y == point.Y))
                {
                    addedPoint.Add(new Point(point.X + 1, point.Y));
                }
            }

            if (addedPoint.Any())
                basinPoint.AddRange(addedPoint);
            else
                finished = true;
        }

        listBasinSizes.Add(basinPoint.Count());
    }

    var orderedList = listBasinSizes.OrderByDescending(x => x);
    var answer = orderedList.ElementAt(0) * orderedList.ElementAt(1) * orderedList.ElementAt(2);

    Console.WriteLine($"Réponse 2 : {answer}");
}

List<Point> GetLowPoints(int[,] table, int height, int width)
{
    var lowPoints = new List<Point>();

    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            var totalNumber = 0;
            var cptGreaterNumber = 0;

            if (j - 1 >= 0)
            {
                cptGreaterNumber += table[i, j - 1] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (j + 1 < width)
            {
                cptGreaterNumber += table[i, j + 1] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (i - 1 >= 0)
            {
                cptGreaterNumber += table[i - 1, j] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (i + 1 < height)
            {
                cptGreaterNumber += table[i + 1, j] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (cptGreaterNumber == totalNumber)
                lowPoints.Add(new Point(i, j));
        }
    }

    return lowPoints;
}