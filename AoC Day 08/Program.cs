using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var cpt = 0;
    for (var i = 0; i < data.Length; i++)
    {
        var signalValues = data[i].Split(" | ")[0];
        var outputValues = data[i].Split(" | ")[1];

        var digitValues = outputValues.Split();
        foreach(var value in digitValues)
            cpt += value.Length == 2 | value.Length == 3 | value.Length == 4 | value.Length == 7 ? 1 : 0;
    }

    Console.WriteLine($"Réponse 1 : {cpt}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var answer = 0;
    for (var i = 0; i < data.Length; i++)
    {
        var chart = new char[7];
        var signalValues = data[i].Split(" | ")[0].Split();
        var outputValues = data[i].Split(" | ")[1].Split();

        var valueOne = signalValues.First(x => x.Length == 2);

        var rightSideBar = new List<char>();
        var valueSeven = signalValues.First(x => x.Length == 3);
        foreach(var value in valueSeven)
        {
            if (!valueOne.Contains(value))
                chart[0] = value;
            else
                rightSideBar.Add(value);
        }

        var valueFour = signalValues.First(x => x.Length == 4);

        // to find the 9, you add the top bar to the 4.
        var valueNine = string.Empty;
        var valueNineCandidates = signalValues.Where(x => x.Length == 6);
        foreach (var value in valueNineCandidates)
        {
            var found = true;
            var tempValue = valueFour + chart[0];
            foreach (var letter in tempValue)
                if (!value.Contains(letter))
                    found = false;

            if (found)
            {
                chart[6] = value.First(x => !tempValue.Contains(x));
                valueNine = value;
            }
        }

        var valueEight = signalValues.First(x => x.Length == 7);
        foreach (var value in valueEight)
            if (!valueNine.Contains(value))
                chart[4] = value;

        var valueTwo = signalValues.First(x => x.Length == 5 && x.Contains(chart[0]) && x.Contains(chart[4]) && x.Contains(chart[6]));
        var valueThree = signalValues.First(x => x.Length == 5 && x.Contains(valueOne[0]) && x.Contains(valueOne[1]));
        var valueFive = signalValues.First(x => x.Length == 5 && x != valueTwo && x != valueThree);

        var valueZero = signalValues.First(x => x.Length == 6 && x != valueNine && x.Contains(valueSeven[0]) && x.Contains(valueSeven[1]) && x.Contains(valueSeven[2]));
        var valueSix = signalValues.First(x => x.Length == 6 && x != valueZero && x != valueNine);

        var reference = new Dictionary<int, string>();
        reference[0] = valueZero;
        reference[1] = valueOne;
        reference[2] = valueTwo;
        reference[3] = valueThree;
        reference[4] = valueFour;
        reference[5] = valueFive;
        reference[6] = valueSix;
        reference[7] = valueSeven;
        reference[8] = valueEight;
        reference[9] = valueNine;

        var concatValue = string.Empty;
        foreach(var value in outputValues)
        {
            foreach (var element in reference)
            {
                if (value.Length == element.Value.Length && value.All(x => element.Value.Contains(x)))
                {
                    concatValue += element.Key;
                }
            }
        }

        Console.WriteLine(concatValue);

        answer += Int32.Parse(concatValue);
    }

    Console.WriteLine($"Réponse 2 : {answer}");
}