using AoC_Day_11;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var octopuses = GenerateOctopuses(data);

    var cptFlash = 0;
    for (var i = 0; i < 100; i++)
    {
        foreach (var octo in octopuses)
            octo.Tick();

        while (octopuses.Any(x => x.MustFlash && !x.HasFlashed))
        {
            foreach (var octo in octopuses)
                cptFlash += octo.Flash() ? 1 : 0;
        }

        foreach (var octo in octopuses)
            octo.Reset();
    }

    Console.WriteLine($"Réponse 1 : {cptFlash}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();
    
    var octopuses = GenerateOctopuses(data);

    var cptRound = 0;
    var cptFlash = 0;
    while(cptFlash != 100)
    {
        cptRound++;
        cptFlash = 0;

        foreach (var octo in octopuses)
            octo.Tick();

        while (octopuses.Any(x => x.MustFlash && !x.HasFlashed))
        {
            foreach (var octo in octopuses)
                cptFlash += octo.Flash() ? 1 : 0;
        }

        foreach (var octo in octopuses)
            octo.Reset();
    }

    Console.WriteLine($"Réponse 2 : {cptRound}");
}

List<Octopus> GenerateOctopuses(string[] data)
{
    var octos = new Octopus[data.Length, data[0].Length];

    for (var i = 0; i < data.Length; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
        {
            var octo = new Octopus(Int32.Parse(data[i][j].ToString()));
            octos[i, j] = octo;
        }
    }

    var octopuses = new List<Octopus>();
    var height = data.Length;
    for (var i = 0; i < height; i++)
    {
        var width = data[i].Length;
        for (var j = 0; j < width; j++)
        {
            var left = j - 1;
            var right = j + 1;
            var up = i - 1;
            var down = i + 1;

            if (left >= 0)
                octos[i, j].AdjacentOctopuses.Add(octos[i, left]);

            if (left >= 0 && up >= 0)
                octos[i, j].AdjacentOctopuses.Add(octos[up, left]);

            if (up >= 0)
                octos[i, j].AdjacentOctopuses.Add(octos[up, j]);

            if (up >= 0 && right < width)
                octos[i, j].AdjacentOctopuses.Add(octos[up, right]);

            if (right < width)
                octos[i, j].AdjacentOctopuses.Add(octos[i, right]);

            if (right < width && down < height)
                octos[i, j].AdjacentOctopuses.Add(octos[down, right]);

            if (down < height)
                octos[i, j].AdjacentOctopuses.Add(octos[down, j]);

            if (down < height && left >= 0)
                octos[i, j].AdjacentOctopuses.Add(octos[down, left]);

            octopuses.Add(octos[i, j]);
        }
    }

    return octopuses;
}