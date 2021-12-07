using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();
    var crabs = data[0].Split(',').Select(x => Int32.Parse(x)).ToList<int>();

    var minValue = crabs.Min();
    var maxValue = crabs.Max();

    var lowestCost = int.MaxValue;
    for(var i = minValue; i <= maxValue; i++)
    {
        var cost = 0;
        foreach (var c in crabs)
            cost += Math.Abs(i - c);

        if (cost < lowestCost)
            lowestCost = cost;
    }

    Console.WriteLine($"Réponse 1 : {lowestCost}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();
    var crabs = data[0].Split(',').Select(x => Int32.Parse(x)).ToList<int>();

    var minValue = crabs.Min();
    var maxValue = crabs.Max();

    var lowestCost = int.MaxValue;
    for (var i = minValue; i <= maxValue; i++)
    {
        var cost = 0;
        foreach (var c in crabs)
        {
            var steps = Math.Abs(i - c);
            for (var j = 1; j <= steps; j++)
                cost += j;
        }

        if (cost < lowestCost)
            lowestCost = cost;
    }

    Console.WriteLine($"Réponse 2 : {lowestCost}");
}