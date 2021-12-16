using Utilities;
using AoC_Day_15;

// SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();
    var paths = GeneratePaths(data);

    // Current list of cavernPaths
    List<List<CavernPath>> ongoingPaths = new List<List<CavernPath>>();

    var minValueFound = new List<int>();
    minValueFound.Add(Int32.MaxValue);

    // We are going to create our first cavernPaths
    var firstNode = paths.First();
    foreach(var adjacentPath in firstNode.AdjacentPaths)
    {
        var newPath = new List<CavernPath>();
        newPath.Add(firstNode);
        newPath.Add(adjacentPath);

        ongoingPaths.Add(newPath);
    }

    while (ongoingPaths.Count() > 0)
    {
        var tempOngoingPaths = new List<List<CavernPath>>();
        tempOngoingPaths.AddRange(ongoingPaths);

        ongoingPaths = new List<List<CavernPath>>();

        foreach (var ongoingPath in tempOngoingPaths)
        {
            foreach (var adjacentPath in ongoingPath.Last().AdjacentPaths)
            {
                if (!ongoingPath.Contains(adjacentPath))
                {
                    var newPath = new List<CavernPath>();
                    newPath.AddRange(ongoingPath);
                    newPath.Add(adjacentPath);

                    if (adjacentPath.Index == "!99,99!")
                    {
                        minValueFound.Add(newPath.Sum(x => x.Value) - firstNode.Value);
                    }
                    else
                    {
                        ongoingPaths.Add(newPath);
                    }
                }
            }
        }

        // There should be only one best path to a point at a certain moment
        var tempPaths = ongoingPaths.Where(x => x.Sum(y => y.Value) < minValueFound.Min()).OrderBy(y => y.Sum(z => z.Value));
        var filteredPaths = new List<List<CavernPath>>();
        foreach(var path in tempPaths)
            if (!filteredPaths.Any(x => x.Last().Index == path.Last().Index))
                filteredPaths.Add(path);

        ongoingPaths = new List<List<CavernPath>>();
        ongoingPaths.AddRange(filteredPaths);

        if (ongoingPaths.Any())
            Console.WriteLine(ongoingPaths.First().Count() + " : " + ongoingPaths.Count() + " : " + minValueFound.Min());
    }

    Console.WriteLine($"Réponse 1 : {minValueFound.Min()}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();
    var paths = GenerateMegaPaths(data);

    // Current list of cavernPaths
    List<List<CavernPath>> ongoingPaths = new List<List<CavernPath>>();

    var minValueFound = new List<int>();
    minValueFound.Add(Int32.MaxValue);

    // We are going to create our first cavernPaths
    var firstNode = paths.First();
    foreach (var adjacentPath in firstNode.AdjacentPaths)
    {
        var newPath = new List<CavernPath>();
        newPath.Add(firstNode);
        newPath.Add(adjacentPath);

        ongoingPaths.Add(newPath);
    }

    var scoreSheet = new int[500, 500];
    for (var i = 0; i < 500; i++)
        for (var j = 0; j < 500; j++)
            scoreSheet[i, j] = Int32.MaxValue;

    while (ongoingPaths.Count() > 0)
    {
        var newOngoingPaths = new List<List<CavernPath>>();

        foreach (var ongoingPath in ongoingPaths)
        {
            var adjacentPaths = ongoingPath.Last().AdjacentPaths;
            foreach (var adjacentPath in adjacentPaths)
            {
                if (!ongoingPath.Contains(adjacentPath))
                {
                    var sum = ongoingPath.Sum(x => x.Value) + adjacentPath.Value - firstNode.Value;

                    if (adjacentPath.Index == "!499,499!")
                    {
                        minValueFound.Add(sum);
                    }
                    else
                    {
                        if (sum < scoreSheet[adjacentPath.X, adjacentPath.Y])
                        {
                            var newPath = new List<CavernPath>();
                            newPath.AddRange(ongoingPath);
                            newPath.Add(adjacentPath);

                            newOngoingPaths.Add(newPath);
                            scoreSheet[adjacentPath.X, adjacentPath.Y] = sum;
                        }
                    }
                }
            }
        }

        ongoingPaths = new List<List<CavernPath>>();
        foreach (var path in newOngoingPaths)
        {
            var lastElement = path.Last();
            var sum = path.Sum(x => x.Value) - firstNode.Value;

            if (scoreSheet[lastElement.X, lastElement.Y] == sum)
                ongoingPaths.Add(path);
        }

        if (ongoingPaths.Any())
            Console.WriteLine(ongoingPaths.First().Count() + " : " + ongoingPaths.Count() + " : " + minValueFound.Min());
    }

    Console.WriteLine($"Réponse 2 : {minValueFound.Min()}");
}

List<CavernPath> GenerateMegaPaths(string[] data)
{
    var paths = new CavernPath[data.Length * 5, data[0].Length * 5];

    for (var ii = 0; ii < 5; ii++)
    {
        for (var jj = 0; jj < 5; jj++)
        {
            for (var i = 0 + (data.Length * ii); i < data.Length + (data.Length * ii); i++)
            {
                for (var j = 0 + (data[0].Length * jj); j < data[0].Length + (data[0].Length * jj); j++)
                {
                    var index = ii + jj;
                    var trueValue = Int32.Parse(data[i - (data.Length * ii)][j - (data[0].Length * jj)].ToString()) + index;
                    if (trueValue > 9)
                        trueValue -= 9;

                    var path = new CavernPath(trueValue, i, j);
                    paths[i, j] = path;
                }
            }
        }
    }

    return GenerateObjects(paths, data.Length * 5, data[0].Length * 5);
}

List<CavernPath> GeneratePaths(string[] data)
{
    var paths = new CavernPath[data.Length, data[0].Length];

    for (var i = 0; i < data.Length; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
        {
            var path = new CavernPath(Int32.Parse(data[i][j].ToString()), i, j);
            paths[i, j] = path;
        }
    }

    return GenerateObjects(paths, data.Length, data[0].Length);
}

List<CavernPath> GenerateObjects(CavernPath[,] paths, int height, int width)
{
    var octopuses = new List<CavernPath>();
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            var left = j - 1;
            var right = j + 1;
            var up = i - 1;
            var down = i + 1;

            if (left >= 0)
                paths[i, j].AdjacentPaths.Add(paths[i, left]);

            if (up >= 0)
                paths[i, j].AdjacentPaths.Add(paths[up, j]);

            if (right < width)
                paths[i, j].AdjacentPaths.Add(paths[i, right]);

            if (down < height)
                paths[i, j].AdjacentPaths.Add(paths[down, j]);

            octopuses.Add(paths[i, j]);
        }
    }

    return octopuses;
}