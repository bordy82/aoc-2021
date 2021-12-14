using Utilities;
using AoC_Day_12;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var caves = LoadCaves(data);

    var startingCave = caves.First(x => x.Name == "start");

    var paths = new List<string>();
    foreach (var cave in startingCave.ConnectedCaves)
        PathFindFor1(cave, startingCave.Name, paths);

    Console.WriteLine($"Réponse 1 : {paths.Count}");
}

void PathFindFor1(Cave cave, string path, List<string> paths)
{
    var visitedCaves = path.Split(',');
    var newPath = path + "," + cave.Name;

    if (cave.Name != "end")
    {
        foreach (var innerCave in cave.ConnectedCaves)
        {
            if (!innerCave.IsSmallCave || !visitedCaves.Any(x => x == innerCave.Name))
            {
                PathFindFor1(innerCave, newPath, paths);
            }
        }
    }
    else
    {
        paths.Add(newPath);
    }
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var caves = LoadCaves(data);

    var startingCave = caves.First(x => x.Name == "start");

    var paths = new List<string>();
    foreach (var cave in startingCave.ConnectedCaves)
        PathFindFor2(cave, startingCave.Name, paths);

    Console.WriteLine($"Réponse 2 : {paths.Count}");
}

void PathFindFor2(Cave cave, string path, List<string> paths)
{
    var visitedCaves = path.Split(',');
    var newPath = path + "," + cave.Name;

    if (cave.Name != "end")
    {
        foreach (var innerCave in cave.ConnectedCaves.Where(x => x.Name != "start"))
        {
            // HERE
            if (!innerCave.IsSmallCave || !visitedCaves.Any(x => x == innerCave.Name) || CanVisitInnerCaveTwice(newPath))
            {
                PathFindFor2(innerCave, newPath, paths);
            }
        }
    }
    else
    {
        paths.Add(newPath);
    }
}

bool CanVisitInnerCaveTwice(string path)
{
    var visitedCaves = path.Split(',');
    var visitedSmallCaveTwice = false;

    foreach (var visitedCave in visitedCaves.Where(x => x == x.ToLower()))
        if (visitedCaves.Count(x => x == visitedCave) >= 2)
            visitedSmallCaveTwice = true;

    if (visitedSmallCaveTwice)
        return false;
    else
        return true;
}

List<Cave> LoadCaves(string[] data)
{
    var caves = new List<Cave>();
    for (var i = 0; i < data.Length; i++)
    {
        var caveAName = data[i].Split('-')[0];
        var caveBName = data[i].Split('-')[1];

        var caveA = new Cave(caveAName);
        if (!caves.Any(x => x.Name == caveAName))
            caves.Add(caveA);
        else
            caveA = caves.First(x => x.Name == caveAName);

        var caveB = new Cave(caveBName);
        if (!caves.Any(x => x.Name == caveBName))
            caves.Add(caveB);
        else
            caveB = caves.First(x => x.Name == caveBName);

        caveA.ConnectedCaves.Add(caveB);
        caveB.ConnectedCaves.Add(caveA);
    }

    return caves;
}