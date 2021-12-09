using System.Drawing;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();
    var width = data[0].Length;
    var height = data.Length;

    var table = new int[height, width];

    for (var i = 0; i < data.Length; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
            table[i, j] = Int32.Parse(data[i][j].ToString());
    }

    var riskLevel = 0;
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            // Console.WriteLine(table[i, j]);

            var cptGreaterNumber = 0;
            var totalNumber = 0;

            if (j - 1 >= 0)
            {
                cptGreaterNumber += table[i, j - 1] > table[i, j] ? 1 : 0;
                totalNumber++;
            }
                // Console.WriteLine("gauche : " + table[i, j-1]);

            if (j + 1 < width)
            {
                cptGreaterNumber += table[i, j + 1] > table[i, j] ? 1 : 0;
                totalNumber++;
            }
                
            // Console.WriteLine("droite : " + table[i, j+1]);

            if (i - 1 >= 0)
            {
                cptGreaterNumber += table[i - 1, j] > table[i, j] ? 1 : 0;
                totalNumber++;
            }
                
            //Console.WriteLine("haut : " + table[i-1, j]);

            if (i + 1 < height)
            {
                cptGreaterNumber += table[i + 1, j] > table[i, j] ? 1 : 0;
                totalNumber++;
            }
                
            // Console.WriteLine("bas : " + table[i+1,j]);

            if (cptGreaterNumber == totalNumber)
            {
                Console.WriteLine(table[i, j]);
                riskLevel += table[i, j] + 1;
            }
        }            
    }

    Console.WriteLine($"Réponse 1 : {riskLevel}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var width = data[0].Length;
    var height = data.Length;

    var table = new int[height, width];

    for (var i = 0; i < data.Length; i++)
    {
        for (var j = 0; j < data[i].Length; j++)
            table[i, j] = Int32.Parse(data[i][j].ToString());
    }

    var lowPoints = new List<Point>();

    var riskLevel = 0;
    for (var i = 0; i < height; i++)
    {
        for (var j = 0; j < width; j++)
        {
            var cptGreaterNumber = 0;
            var totalNumber = 0;

            if (j - 1 >= 0)
            {
                cptGreaterNumber += table[i, j - 1] > table[i, j] ? 1 : 0;
                totalNumber++;
            }
            
            if (j + 1 < width)
            {
                cptGreaterNumber += table[i, j + 1] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (i - 1 >= 0)
            {
                cptGreaterNumber += table[i - 1, j] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (i + 1 < height)
            {
                cptGreaterNumber += table[i + 1, j] > table[i, j] ? 1 : 0;
                totalNumber++;
            }

            if (cptGreaterNumber == totalNumber)
            {
                Console.WriteLine(table[i, j]);
                lowPoints.Add(new Point(i, j));
            }
        }
    }

    var listBasinSizes = new List<int>();

    foreach(var lowPoint in lowPoints)
    {
        var basinPoint = new List<Point>();
        basinPoint.Add(lowPoint);

        

        var finished = false;
        while (!finished)
        {
            var addedPoint = new List<Point>();
            foreach (var point in basinPoint)
            {
                if (point.Y - 1 >= 0 && table[point.X, point.Y - 1] != 9 && basinPoint.Where(x => x.X == point.X && x.Y == point.Y - 1).Count() == 0
                                                                         && addedPoint.Where(x => x.X == point.X && x.Y == point.Y - 1).Count() == 0)
                {
                    addedPoint.Add(new Point(point.X, point.Y - 1));
                }

                if (point.Y + 1 < width && table[point.X, point.Y + 1] != 9 && basinPoint.Where(x => x.X == point.X && x.Y == point.Y + 1).Count() == 0
                                                                            && addedPoint.Where(x => x.X == point.X && x.Y == point.Y + 1).Count() == 0)
                {
                    addedPoint.Add(new Point(point.X, point.Y + 1));
                }

                if (point.X - 1 >= 0 && table[point.X - 1, point.Y] != 9 && basinPoint.Where(x => x.X == point.X - 1 && x.Y == point.Y).Count() == 0
                                                                         && addedPoint.Where(x => x.X == point.X - 1 && x.Y == point.Y).Count() == 0)
                {
                    addedPoint.Add(new Point(point.X - 1, point.Y));
                }

                if (point.X + 1 < height && table[point.X + 1, point.Y] != 9 && basinPoint.Where(x => x.X == point.X + 1 && x.Y == point.Y).Count() == 0
                                                                             && addedPoint.Where(x => x.X == point.X + 1 && x.Y == point.Y).Count() == 0)
                {
                    addedPoint.Add(new Point(point.X + 1, point.Y));
                }
            }

            if (addedPoint.Any())
                basinPoint.AddRange(addedPoint);
            else
                finished = true;
        }

        listBasinSizes.Add(basinPoint.Count());
        Console.WriteLine("BASSIN : " + basinPoint.Count());
    }

    var orderedList = listBasinSizes.OrderByDescending(x => x);
    var answer = orderedList.ElementAt(0) * orderedList.ElementAt(1) * orderedList.ElementAt(2);

    Console.WriteLine($"Réponse 2 : {answer}");
}

/*
 * 
 * 

                var finished = false;
                while(!finished)
                {
                    foreach(var point in pointList)
                    {
                        if (j - 1 >= 0 && table[i, j] != 9 && pointList.Where(x => x.X == i && x.Y == j).Count() == 0)
                        {
                            pointList.Add(new Point(i, j));
                        }

                        if (j + 1 < width && table[i, j] != 9)
                        {
                            pointList.Add(new Point(i, j));
                        }

                        if (i - 1 >= 0 && table[i, j] != 9)
                        {
                            pointList.Add(new Point(i, j));
                        }

                        if (i + 1 < height && table[i, j] != 9)
                        {
                            pointList.Add(new Point(i, j));
                        }
                    }
                }
 * 
 * */