using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var cpt = 0;
    var data = DataLoader.GetIntDataFromFile();

    for (var i = 1; i < data.Length; i++)
    {
        if (data[i] > data[i - 1])
            cpt++;
    }

    Console.WriteLine($"Réponse 1 : {cpt}");
}

void SolvePuzzleTwo()
{
    var cpt = 0;
    var data = DataLoader.GetIntDataFromFile();
    var previousSum = data[0] + data[1] + data[2];

    for (var i = 2; i < data.Length - 1; i++)
    {
        var sum = data[i - 1] + data[i] + data[i + 1];
        if (sum > previousSum)
            cpt++;

        previousSum = sum;
    }

    Console.WriteLine($"Réponse 2 : {cpt}");
}