using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var readingAlgo = true;
    var algorithm = string.Empty;
    var rawMap = new List<string>();
    for (var i = 0; i < data.Length; i++)
    {
        if (readingAlgo)
        {
            if (!string.IsNullOrEmpty(data[i]))
                algorithm += data[i];
            else
                readingAlgo = false;
        }
        else
        {
            rawMap.Add(data[i]);
        }
    }

    var height = rawMap.Count;
    var width = rawMap[0].Length;
    var map = new char[height,width];

    for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
            map[i,j] = rawMap[i][j];

    for (var i = 0; i < height; i++)
    {
        Console.WriteLine();
        for (var j = 0; j < width; j++)
            Console.Write(map[i, j]);
    }

    Console.WriteLine();

    var enhancedMap = map;

    for (var i = 0; i < 50; i++)
        enhancedMap = Enhance(enhancedMap, algorithm, i % 2 == 0);

    //var enhancedMap = Enhance(map, algorithm);
    //enhancedMap = Enhance(enhancedMap, algorithm);

    // À cause de l'infini, tout est devenu 1 puis tout est redevenu 0, influençant les cotés. On peut jsute garder le centre.

    //for (var i = 0; i < enhancedMap.GetLength(0); i++)
    //{
    //    Console.WriteLine();
    //    for (var j = 0; j < enhancedMap.GetLength(1); j++)
    //    {
    //        Console.Write(enhancedMap[i, j]);
    //    }
    //}

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine();

    var finalMap = new char[enhancedMap.GetLength(0) - 12, enhancedMap.GetLength(1) - 12];
    for(var i = 15;i < enhancedMap.GetLength(0) - 15; i++)
        for (var j = 15;j < enhancedMap.GetLength(1) - 15; j++)
            finalMap[i,j] = enhancedMap[i,j];


    var cpt = 0;
    for (var i = 0; i < finalMap.GetLength(0); i++)
    {
        Console.WriteLine();
        for (var j = 0; j < finalMap.GetLength(1); j++)
        {   cpt += finalMap[i, j] == '#' ? 1 : 0;
            Console.Write(finalMap[i, j]);
        }
    }

    Console.WriteLine();

    Console.WriteLine($"Réponse 1 : {cpt}");
}

char[,] Enhance(char[,] map, string algorithm, bool isPair)
{
    var height = map.GetLength(0) + 24;
    var width = map.GetLength(1) + 24;
    var referenceMap = new char[height, width];

    for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
            referenceMap[i, j] = isPair ? '.' : '#';

    for (var i = 0; i < height; i++)
        for (var j = 0; j < width; j++)
        {
            if (i > 11 && i < height - 12 && j > 11 && j < width - 12)
                referenceMap[i, j] = map[i - 12, j - 12];
        }

    //for (var i = 0; i < height; i++)
    //{
    //    Console.WriteLine();
    //    for (var j = 0; j < width; j++)
    //        Console.Write(referenceMap[i, j]);
    //}

    //Console.WriteLine();

    var newMap = new char[height - 2, width - 2];
    for (var i = 1; i < height - 1; i++)
        for (var j = 1; j < width - 1; j++)
        {
            var pixelValue = string.Empty;

            pixelValue += referenceMap[i - 1, j - 1];
            pixelValue += referenceMap[i - 1, j];
            pixelValue += referenceMap[i - 1, j + 1];
            pixelValue += referenceMap[i, j - 1];
            pixelValue += referenceMap[i, j];
            pixelValue += referenceMap[i, j + 1];
            pixelValue += referenceMap[i + 1, j - 1];
            pixelValue += referenceMap[i + 1, j];
            pixelValue += referenceMap[i + 1, j + 1];

            pixelValue = pixelValue.Replace('#', '1').Replace('.', '0');
            var pixelIntValue = Convert.ToInt32(pixelValue, 2);

            newMap[i - 1, j - 1] = algorithm[pixelIntValue];
        }

    return newMap;
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile(true);

    for (var i = 0; i < data.Length; i++)
    {

    }

    Console.WriteLine($"Réponse 2 : ");
}