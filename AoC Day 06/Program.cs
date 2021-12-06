using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();
    var fishes = data[0].Split(',').Select(x => Int32.Parse(x)).ToList<int>();

    for(var i = 0; i < 80; i++)
    {
        var fishesToAdd = fishes.Count(x => x == 0);
        for (var j = 0; j < fishesToAdd; j++)
            fishes.Add(9);

        for (var j = 0; j < fishes.Count; j++)
            if (fishes[j] == 0)
                fishes[j] = 7;

        for (var j = 0; j < fishes.Count; j++)
            fishes[j]--;
    }

    Console.WriteLine($"Réponse 1 : {fishes.Count()}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var fishTable = new Dictionary<int, long>();
    for(var i = 0; i <= 8; i++)
        fishTable[i] = 0;

    foreach (var fish in data[0].Split(',').Select(x => Int32.Parse(x)))
        fishTable[fish]++;

    for (var i = 0; i < 256; i++)
    {
        var temp0Table = fishTable[0];

        for (var j = 0; j <= 7; j++)
            fishTable[j] = fishTable[j+1];
        
        // 0 deviennent 6 et spawn des 8;
        fishTable[8] = temp0Table;
        fishTable[6] += temp0Table;
    }

    Console.WriteLine($"Réponse 2 : {fishTable.Sum(x => x.Value)}");
}