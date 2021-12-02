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
        var input = data[i].Split(' ');
        var command = input[0];
        var commandValue = Int32.Parse(input[1]);

        switch (command)
        {
            case "forward":
                currentPosition += commandValue;
                break;
            case "down":
                currentDepth += commandValue;
                break;
            case "up":
                currentDepth -= commandValue;
                break;
        }
    }

    Console.WriteLine($"Réponse 1 : {currentPosition * currentDepth}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var currentPosition = 0;
    var currentDepth = 0;
    var currentAim = 0;

    for (var i = 0; i < data.Length; i++)
    {
        var input = data[i].Split(' ');
        var command = input[0];
        var commandValue = Int32.Parse(input[1]);

        switch (command)
        {
            case "forward":
                currentPosition += commandValue;
                currentDepth += currentAim * commandValue;
                break;
            case "down":
                currentAim += commandValue;
                break;
            case "up":
                currentAim -= commandValue;
                break;
        }
    }

    Console.WriteLine($"Réponse 2 : {currentPosition * currentDepth}");
}