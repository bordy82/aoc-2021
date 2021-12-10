using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var points = 0;
    for (var i = 0; i < data.Length; i++)
    {
        var openingList = new List<string>();   
        for(var j = 0; j < data[i].Length; j++)
        {
            if ("([{<".Contains(data[i][j]))
            {
                openingList.Add(data[i][j].ToString());
            }
            else
            {
                var closingChar = data[i][j] == '}' ? '{' : data[i][j] == ')' ? '(' : data[i][j] == '>' ? '<' : data[i][j] == ']' ? '[' : ' ';
                if (openingList.Last() == closingChar.ToString())
                    openingList.RemoveAt(openingList.Count - 1);
                else
                {
                    Console.WriteLine(data[i][j].ToString());
                    points += data[i][j] == '}' ? 1197 : data[i][j] == ')' ? 3 : data[i][j] == '>' ? 25137 : data[i][j] == ']' ? 57 : 0;
                    break;
                }
            }
        }
    }

    Console.WriteLine($"Réponse 1 : {points}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var listPoints = new List<long>();
    for (var i = 0; i < data.Length; i++)
    {
        long points = 0;
        var isCorrupted = false;
        var openingList = new List<string>();
        for (var j = 0; j < data[i].Length; j++)
        {
            if ("([{<".Contains(data[i][j]))
            {
                openingList.Add(data[i][j].ToString());
            }
            else
            {
                var closingChar = data[i][j] == '}' ? '{' : data[i][j] == ')' ? '(' : data[i][j] == '>' ? '<' : data[i][j] == ']' ? '[' : ' ';
                if (openingList.Last() == closingChar.ToString())
                    openingList.RemoveAt(openingList.Count - 1);
                else
                {
                    isCorrupted = true;
                    break;
                }
            }
        }

        if (!isCorrupted)
        {
            for(var j = openingList.Count - 1; j >= 0; j--)
            {
                var current = openingList[j];
                var point = current == "(" ? 1 : current == "[" ? 2 : current == "{" ? 3 : current == "<" ? 4 : 0;
                points = (points * 5) + point;
                Console.Write(current.ToString());
            }
        }

        if (points != 0)
            listPoints.Add(points);

        Console.WriteLine();
    }

    var orderedList = listPoints.OrderBy(x => x);
    var middle = orderedList.Count() / 2;

    Console.WriteLine(orderedList.Count() + " : " + orderedList.Count() / 2 + " : " + orderedList.Count() % 2);

    Console.WriteLine($"Réponse 2 : {orderedList.ElementAt(middle)}");
}