using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var currentPosition = 0;
    var currentDepth = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var command = data[i].Split(' ');

        switch (command[0])
        {
            case "forward":
                currentPosition += Int32.Parse(command[1]);
                break;
            case "down":
                currentDepth += Int32.Parse(command[1]);
                break;
            case "up":
                currentDepth -= Int32.Parse(command[1]);
                break;

        }
    }

    Console.WriteLine($"Réponse 1 : {currentPosition*currentDepth}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var currentPosition = 0;
    var currentDepth = 0;
    var currentAim = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var command = data[i].Split(' ');

        switch (command[0])
        {
            case "forward":
                currentPosition += Int32.Parse(command[1]);
                currentDepth += (currentAim * Int32.Parse(command[1]));
                break;
            case "down":
                currentAim += Int32.Parse(command[1]);
                break;
            case "up":
                currentAim -= Int32.Parse(command[1]);
                break;
        }
    }

    Console.WriteLine($"Réponse 2 : {currentPosition * currentDepth}");
}