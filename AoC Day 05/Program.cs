using Utilities;
using System.Drawing;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var pointMap = new Dictionary<string, int>();
    for (var i = 0; i < data.Length; i++)
    {
        var points = data[i].Split(" -> ");
        var x1Points = points[0].Split(',');
        var x2Points = points[1].Split(',');

        var x1 = new Point(Int32.Parse(x1Points[0]), Int32.Parse(x1Points[1]));
        var x2 = new Point(Int32.Parse(x2Points[0]), Int32.Parse(x2Points[1]));

        UpdateIfLine(x1, x2, pointMap);
    }

    var cpt = 0;
    foreach(var key in pointMap.Keys)
    {
        if (pointMap[key] > 1)
            cpt++;
    }

    Console.WriteLine($"Réponse 1 : {cpt}");
}

void SolvePuzzleTwo()
{
    var pointMap = new Dictionary<string, int>();
    var data = DataLoader.GetStringDataFromFile();

    for (var i = 0; i < data.Length; i++)
    {
        var points = data[i].Split(" -> ");
        var x1Points = points[0].Split(',');
        var x2Points = points[1].Split(',');

        var x1 = new Point(Int32.Parse(x1Points[0]), Int32.Parse(x1Points[1]));
        var x2 = new Point(Int32.Parse(x2Points[0]), Int32.Parse(x2Points[1]));

        if (!UpdateIfLine(x1, x2, pointMap))
            UpdateIfDiagonal(x1, x2, pointMap);
    }
    
    var cpt = 0;
    foreach (var key in pointMap.Keys)
    {
        if (pointMap[key] > 1)
            cpt++;
    }

    Console.WriteLine($"Réponse 2 : {cpt}");
}

bool UpdateIfLine(Point point1, Point point2, Dictionary<string, int> map)
{
    if ((point1.X == point2.X && point1.Y != point2.Y) || (point1.X != point2.X && point1.Y == point2.Y))
    {
        if (point1.X > point2.X)
        {
            for (var j = point2.X; j <= point1.X; j++)
            {
                UpdateMap($"{j},{point1.Y}", map);
            }
        }

        if (point1.X < point2.X)
        {
            for (var j = point1.X; j <= point2.X; j++)
            {
                UpdateMap($"{j},{point1.Y}", map);
            }
        }

        if (point1.Y > point2.Y)
        {
            for (var j = point2.Y; j <= point1.Y; j++)
            {
                UpdateMap($"{point1.X},{j}", map);
            }
        }

        if (point1.Y < point2.Y)
        {
            for (var j = point1.Y; j <= point2.Y; j++)
            {
                UpdateMap($"{point1.X},{j}", map);
            }
        }

        return true;
    }

    return false;
}

bool UpdateIfDiagonal(Point point1, Point point2, Dictionary<string, int> map)
{
    if (Math.Abs(point1.X - point2.X) == Math.Abs(point1.Y - point2.Y))
    {
        if (point1.X > point2.X)
        {
            for (var j = 0; j <= point1.X - point2.X; j++)
            {
                if (point1.Y > point2.Y)
                {
                    UpdateMap($"{point1.X - j},{point1.Y - j}", map);
                }

                if (point1.Y < point2.Y)
                {
                    UpdateMap($"{point1.X - j},{point1.Y + j}", map);
                }
            }
        }

        if (point1.X < point2.X)
        {
            for (var j = 0; j <= point2.X - point1.X; j++)
            {
                if (point1.Y > point2.Y)
                {
                    UpdateMap($"{point1.X + j},{point1.Y - j}", map);
                }

                if (point1.Y < point2.Y)
                {
                    UpdateMap($"{point1.X + j},{point1.Y + j}", map);
                }
            }
        }

        return true;
    }

    return false;
}

void UpdateMap(string key, Dictionary<string, int> map)
{
    var mapValue = 0;
    map.TryGetValue(key, out mapValue);

    mapValue++;
    map[key] = mapValue;
}