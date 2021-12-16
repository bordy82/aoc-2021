using AoC_Day_16;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile()[0];

    // var data = "D2FE28";
    // var data = "38006F45291200";
    // var data = "EE00D40C823060";
    // var data = "8A004A801A8002F478"; // 16
    // var data = "620080001611562C8802118E34"; // 12
    // var data = "C0015000016115A2E0802F182340"; // 23
    // var data = "A0016C880162017C3686B18A3D4780"; // 31

    var bits = CreateBitArray(data);

    var results = Process(bits, 0);

    Console.WriteLine($"Réponse 1 : {results.VersionSum}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile()[0];

    // var data = "C200B40A82"; // 3
    //var data = "04005AC33890"; // 54
    //var data = "880086C3E88112"; // 7
    // var data = "CE00C43D881120"; // 9
    // var data = "D8005AC2A8F0"; // 1
    // var data = "F600BC2D8F"; // 0
    // var data = "9C005AC2F8F0"; // 0
    // var data = "9C0141080250320F1802104A08"; // 1

    var bits = CreateBitArray(data);

    var results = Process(bits, 0);

    Console.WriteLine($"Réponse 2 : {results.Value}");
}

ProcessResults Process(string bits, int index, bool isSubpacket = false)
{
    var version = Convert.ToInt32(bits.Substring(index, 3), 2);
    index += 3;

    var typeId = Convert.ToInt32(bits.Substring(index, 3), 2);
    index += 3;

    var processValue = 0L;
    var versionSum = version;

    //Console.WriteLine($"Process, version {version} dans type {typeId}");

    if (typeId == 4)
    {
        var results = ProcessLiteralValue(bits, index, isSubpacket);
        index = results.Index;
        versionSum += results.VersionSum;
        processValue = results.Value;
    }
    else
    {
        var results = ProcessOperator(bits, index, typeId, isSubpacket);
        index = results.Index;
        versionSum += results.VersionSum;
        processValue = results.Value;
    }

    var processResults = new ProcessResults(index);
    processResults.VersionSum += versionSum;
    processResults.Value = processValue;

    return processResults;
}

ProcessResults ProcessOperator(string bits, int index, int typeId, bool isSubpacket = false)
{
    var length = bits.Substring(index, 1) == "1" ? 11 : 15;
    index++;

    var versionSum = 0;
    var resultsForOperator = new List<ProcessResults>();

    if (length == 11)
    {
        var numberOfPackets = Convert.ToInt32(bits.Substring(index, length), 2);
        index += length;

        //Console.WriteLine($"Operator with {numberOfPackets} packets");

        for (var i = 0; i < numberOfPackets; i++)
        {
            var results = Process(bits, index, true);
            index = results.Index;
            versionSum += results.VersionSum;

            resultsForOperator.Add(results);
        }
    }
    else if (length == 15)
    {
        var lengthOfSubpacket = Convert.ToInt32(bits.Substring(index, length), 2);
        index += length;

        //Console.WriteLine($"Operator with a size of {lengthOfSubpacket} bits");

        var subBits = bits.Substring(index, lengthOfSubpacket);

        var currentIndex = 0;
        while (currentIndex < subBits.Length)
        {
            var results = Process(subBits, currentIndex, true);
            versionSum += results.VersionSum;
            currentIndex = results.Index;

            resultsForOperator.Add(results);
        }

        index += lengthOfSubpacket;
    }

    var operatorResults = new ProcessResults(index);
    operatorResults.VersionSum += resultsForOperator.Sum(x => x.VersionSum);

    if (typeId == 0)
    {
        operatorResults.Value = resultsForOperator.Sum(x => x.Value);
    }
    else if (typeId == 1)
    {
        var product = 1L;
        foreach (var res in resultsForOperator)
            product *= res.Value;

        operatorResults.Value = product;
    }
    else if (typeId == 2)
    {
        operatorResults.Value = resultsForOperator.Min(x => x.Value);
    }
    else if (typeId == 3)
    {
        operatorResults.Value = resultsForOperator.Max(x => x.Value);
    }
    else if (typeId == 5)
    {
        if (resultsForOperator.Count != 2)
            throw new Exception("Devrais avoir 2 éléments seulement.");

        operatorResults.Value = resultsForOperator[0].Value > resultsForOperator[1].Value ? 1 : 0;
    }
    else if (typeId == 6)
    {
        if (resultsForOperator.Count != 2)
            throw new Exception("Devrais avoir 2 éléments seulement.");

        operatorResults.Value = resultsForOperator[0].Value < resultsForOperator[1].Value ? 1 : 0;
    }
    else if (typeId == 7)
    {
        if (resultsForOperator.Count != 2)
            throw new Exception("Devrais avoir 2 éléments seulement.");

        operatorResults.Value = resultsForOperator[0].Value == resultsForOperator[1].Value ? 1 : 0;
    }

    return operatorResults;
}

ProcessResults ProcessLiteralValue(string bits, int index, bool isSubpacket = false)
{
    var currentIndex = 6;

    var isLast = false;
    var value = string.Empty;
    while (!isLast)
    {
        var bitValue = bits.Substring(index, 5);

        if (bitValue[0] == '0')
            isLast = true;

        value += bitValue.Substring(1);

        index += 5;
        currentIndex += 5;
    }

    if (!isSubpacket)
    {
        while (currentIndex % 4 != 0)
        {
            currentIndex++;
            index++;
        }
    }

    //Console.WriteLine($"Literal Value with a value of {Convert.ToInt64(value, 2)}");

    var result = new ProcessResults(index, Convert.ToInt64(value, 2));
    result.TypeId = 4;

    return result;
}

string CreateBitArray(string data)
{
    var refHex = new Dictionary<string, string>();
    refHex.Add("0", "0000");
    refHex.Add("1", "0001");
    refHex.Add("2", "0010");
    refHex.Add("3", "0011");
    refHex.Add("4", "0100");
    refHex.Add("5", "0101");
    refHex.Add("6", "0110");
    refHex.Add("7", "0111");
    refHex.Add("8", "1000");
    refHex.Add("9", "1001");
    refHex.Add("A", "1010");
    refHex.Add("B", "1011");
    refHex.Add("C", "1100");
    refHex.Add("D", "1101");
    refHex.Add("E", "1110");
    refHex.Add("F", "1111");

    var bits = "";
    for (var i = 0; i < data.Length; i++)
    {
        bits += refHex[data[i].ToString()];
    }

    return bits;
}