using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile(true);

    //var scannerLines = data.Where(x => x.Contains("scanner"));
    //var index = Int32.Parse(scannerLines.Last().Split(" ")[2]);
    //var scanners = new List<int>[];

    var scanners = new Dictionary<int, List<string>>();

    var scannerId = 0;
    for (var i = 0; i < data.Length; i++)
    {
        var line = data[i];
        if (line.Contains("scanner"))
        {
            scannerId = Int32.Parse(line.Split(" ")[2]);
            scanners.Add(scannerId, new List<string>());
        }
        else if (!String.IsNullOrEmpty(line))
        {
            scanners[scannerId].Add(line);
        }
    }

    var valueDict = new Dictionary<string, int>();
    foreach(var scanner in scanners[0])
    {
        foreach(var innerScan in scanners[1])
        {
            var scan0 = scanner.Split(",");
            var scan1 = innerScan.Split(",");

            var distanceValue = string.Empty;

            for (var i = 0; i <= 2; i++)
            {
                var num0 = Int32.Parse(scan0[i]);
                var num1 = Int32.Parse(scan1[i]);

                var diff = Math.Abs(Math.Abs(num0) - Math.Abs(num1));

                if (!string.IsNullOrEmpty(distanceValue))
                    distanceValue += ",";

                distanceValue += diff;
            }

            if (!valueDict.ContainsKey(distanceValue))
                valueDict.Add(distanceValue, 0);

            valueDict[distanceValue]++;
        }
    }

    foreach (var value in valueDict)
        if (value.Value >= 2)
            Console.WriteLine(value.Value + " : " + value.Key);

    Console.WriteLine($"Réponse 1 : ");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile(true);

    for (var i = 0; i < data.Length; i++)
    {

    }

    Console.WriteLine($"Réponse 2 : ");
}