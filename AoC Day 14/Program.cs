using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var cpt = 0;
    var dict = new Dictionary<string, int>();
    var polymer = data[0];

    for (var k = 0; k < 10; k++)
    {
        for (var i = 2; i < data.Length; i++)
        {
            var find = data[i].Split(" -> ")[0];
            var replace = data[i].Split(" -> ")[1];

            if (!dict.ContainsKey(replace))
                dict[replace] = cpt++;

            while(polymer.Contains(find))
                polymer = polymer.Replace(find, find[0] + dict[replace].ToString() + find[1]);
        }

        foreach (var value in dict)
            polymer = polymer.Replace(value.Value.ToString(), value.Key.ToString());
    }

    var answerDict = new Dictionary<char, int>();
    foreach (var letter in polymer)
    {
        if (!answerDict.ContainsKey(letter))
            answerDict.Add(letter, 0);

        answerDict[letter]++;
    }

    var max = answerDict.Max(x => x.Value);
    var min = answerDict.Min(x => x.Value); 

    Console.WriteLine($"Réponse 1 : {max - min}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile(true);

    for (var i = 0; i < data.Length; i++)
    {

    }

    Console.WriteLine($"Réponse 2 : ");
}