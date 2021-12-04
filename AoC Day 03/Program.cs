using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var gammaRate = "";
    var epsilonRate = "";

    var data = DataLoader.GetStringDataFromFile();
    var countTable = new int[data[0].Length];
    
    for (var i = 0; i < data.Length; i++)
    {
        for(var j = 0; j < data[i].Length; j++)
            countTable[j] += data[i][j] == '1' ? 1 : 0;
    }

    foreach(var oneCount in countTable)
    {
        var zeroCount = data.Length - oneCount;

        gammaRate += oneCount > zeroCount ? "1" : "0";
        epsilonRate += oneCount > zeroCount ? "0" : "1";
    }

    var gammaValue = Convert.ToInt32(gammaRate, 2);
    var epsilonValue = Convert.ToInt32(epsilonRate, 2);

    Console.WriteLine($"Réponse 1 : { gammaValue * epsilonValue }");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();
    var dataList = data.ToList();

    for(var i = 0; i < data[0].Length; i++)
    {
        dataList = GetNewList(i, dataList, '1', '0');

        if (dataList.Count == 1)
            break;
    }

    var oxygenValue = Convert.ToInt32(dataList.First(), 2);

    dataList = data.ToList();

    for (var i = 0; i < data[0].Length; i++)
    {
        dataList = GetNewList(i, dataList, '0', '1');

        if (dataList.Count == 1)
            break;
    }

    var co2Value = Convert.ToInt32(dataList.First(), 2);

    Console.WriteLine($"Réponse 2 : { oxygenValue * co2Value }");
}

/// <summary>
/// Get a new list based on the '1' count.
/// </summary>
/// <param name="valueOne">The value to look for at the index if there are more or equalt 1's.</param>
/// <param name="valueZero">The value to look for at the index if there are more 0's.</param>
List<string> GetNewList(int index, List<string> data, char valueOne, char valueZero)
{
    var cpt = 0;
    var newData = new List<string>();
    foreach (var item in data)
    {
        cpt += item[index] == '1' ? 1 : 0;
    }

    newData.AddRange(data.Where(x => x[index] == (cpt >= (data.Count - cpt) ? valueOne : valueZero)));

    return newData;
}