using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile(true);

    var cpt = 0;
    var dict = new Dictionary<string, int>();
    var polymer = data[0];

    for (var k = 0; k < 3; k++)
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

        Console.WriteLine(polymer);
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
    var data = DataLoader.GetStringDataFromFile();

    // On construit une table de reference, ex : CH -> B devient CB,HB
    var reference = new Dictionary<string, string>();
    for (var i = 2; i < data.Length; i++)
    {
        var find = data[i].Split(" -> ")[0];
        var replace = data[i].Split(" -> ")[1];

        reference.Add(find, $"{find[0]}{replace},{replace}{find[1]}");
    }

    // On crée le dicitonnaire de départ, ex. pour le data de test : NNCB devient NN, NC, CB
    var startingDict = new Dictionary<string, long>();
    for(var i = 0; i < data[0].Length - 1; i++)
    {
        var pair = $"{data[0][i]}{data[0][i+1]}";

        if (!startingDict.ContainsKey(pair))
            startingDict.Add(pair, 0);

        startingDict[pair]++;
    }

    // Dans un itération, on remplace les éléments du dictionnaire de polymere par les chaines construites en reference.
    // Ex. NN, NC, CB devient NC : 1, CN : 1, NB : 1, BC : 1, CH : 1, HB : 1
    // Puis on remplace ces maillons un nombre X de fois.
    for(var i = 0; i < 40; i++)
    {
        var tempDict = new Dictionary<string, long>();
        foreach (var pair in startingDict)
        {
            var text = reference[pair.Key];
            var pair1 = text.Split(',')[0];
            var pair2 = text.Split(',')[1];

            if (!tempDict.ContainsKey(pair1))
                tempDict.Add(pair1, 0);

            if (!tempDict.ContainsKey(pair2))
                tempDict.Add(pair2, 0);

            tempDict[pair1] += pair.Value;
            tempDict[pair2] += pair.Value;
        }

        startingDict = tempDict;
    }

    // Puis que les lettres sont comptés en double (nous transformons NB en NC et CB), 
    // nous allons éliminer la partie de droite dans notre décompte.
    var letterDict = new Dictionary<char, long>();
    foreach(var pair in startingDict)
    {
        var letter = pair.Key[0];

        if (!letterDict.ContainsKey(letter))
            letterDict.Add(letter, 0);

        letterDict[letter] += pair.Value;
    }

    // On ajoute la derniere lettre, car elle sera éliminé par notre algo mais ne devrait pas.
    var lastLetter = data[0].Last();
    letterDict[lastLetter]++;

    foreach (var letter in letterDict)
        Console.WriteLine($"{letter.Key} : {letter.Value}");

    var max = letterDict.Max(x => x.Value);
    var min = letterDict.Min(x => x.Value);

    Console.WriteLine($"Réponse 2 : {max - min}");
}