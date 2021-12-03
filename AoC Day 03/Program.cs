using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var gammaRate = "";
    var epsilonRate = "";

    var countTable = new int[data[0].Length];
    
    for (var i = 0; i < data.Length; i++)
    {
        for(var j = 0; j < data[i].Length; j++)
            countTable[j] += data[i][j] == '1' ? 1 : 0;
            
    }

    foreach(var count in countTable)
    {
        var oneCount = count;
        var zeroCount = data.Length - count;

        gammaRate += count > zeroCount ? "1" : "0";
        epsilonRate += count > zeroCount ? "0" : "1";
    }

    var gammaValue = Convert.ToInt32(gammaRate, 2);
    var epsilonValue = Convert.ToInt32(epsilonRate, 2);

    Console.WriteLine($"Réponse 1 : { gammaValue * epsilonValue }");
}

List<string> GetNewListOxygen(int index, List<string> data)
{
    var cpt = 0;
    var newData = new List<string>();
    foreach (var item in data)
    {
        cpt += item[index] == '1' ? 1 : 0;
    }

    foreach (var item in data.Where(x => x[index] == (cpt >= (data.Count - cpt)? '1' : '0')))
        newData.Add(item);

    return newData;
}

List<string> GetNewListCO2(int index, List<string> data)
{
    var cpt = 0;
    var newData = new List<string>();
    foreach (var item in data)
    {
        cpt += item[index] == '1' ? 1 : 0;
    }

    foreach (var item in data.Where(x => x[index] == (cpt >= (data.Count - cpt) ? '0' : '1')))
        newData.Add(item);

    return newData;
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var finished = true;
    var dataList = data.ToList();

    for(var i = 0; i < data[0].Length; i++)
    {
        dataList = GetNewListOxygen(i, dataList);

        if (dataList.Count == 1)
            break;
    }

    var oxygenValue = Convert.ToInt32(dataList.First(), 2);

    dataList = data.ToList();

    for (var i = 0; i < data[0].Length; i++)
    {
        dataList = GetNewListCO2(i, dataList);

        if (dataList.Count == 1)
            break;
    }

    var CO2Value = Convert.ToInt32(dataList.First(), 2);

    Console.WriteLine($"Réponse 2 : { oxygenValue * CO2Value }");
}