using AoC_Day_18;
using System.Text.RegularExpressions;
using Utilities;

SolvePuzzleOne();
//SolvePuzzleTwo();

//SolveOne();

void SolveOne()
{
    //var data = "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]";

    //var finished = false;
    //while(!finished)
    //{
    //    var index = new Regex("[[a-z0-9],[a-z0-9]]").Match(numberChain).Index;

    //    if (index != 0)
    //    {
    //        var preSub = data.Substring(0, index);
    //        var openCount = preSub.Count(x => x == '[');
    //        var closeCount = preSub.Count(x => x == ']');

    //        if (openCount - closeCount > 4)
    //        {

    //        }
    //    }
    //}
}

















void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile(true);

    var test = "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]";
    // var test2 = "[[[[0,7],4],[7,[[8,4],9]]],[1,1]]";
    // var index = new Regex("[[a-z0-9],[a-z0-9]]").Match(test).Index;

    var indice = 'a';
    var dict = new Dictionary<char, Pair>();

    var numberChain = test;
    var index = new Regex("[[a-z0-9],[a-z0-9]]").Match(numberChain).Index;
    Console.WriteLine(numberChain);

    Pair lastPair = null;
    while (index > 0)
    {
        var pair = new Pair();
        dict.Add(indice, pair);
        lastPair = pair;
        
        var sub = numberChain.Substring(index - 1, 5);
        
        var left = sub.Substring(1, 1);
        var right = sub.Substring(3, 1);

        if (dict.ContainsKey(left[0]))
        {
            pair.Left = dict[left[0]];
        }
        else
        {
            pair.Left = new Pair(Int32.Parse(left));
        }

        if (dict.ContainsKey(right[0]))
        {
            pair.Right = dict[right[0]];
        }
        else
        {
            pair.Right = new Pair(Int32.Parse(right));
        }

        pair.Left.Parent = pair;
        pair.Right.Parent = pair;

        numberChain = numberChain.Substring(0, index - 1) + indice + numberChain.Substring(index + 4);
        Console.WriteLine(numberChain);

        indice++;

        index = new Regex("[[a-z0-9],[a-z0-9]]").Match(numberChain).Index;
    }

    Console.WriteLine();
    PrintToScreen(lastPair);

    var finished = false;
    while (!finished)
    {
        finished = true;

        var explodingPair = GetPairToExplode(lastPair, 0);
        if (explodingPair != null)
        {
            finished = false;
            ExplodePair(explodingPair);
        }
        else
        {
            finished = false;
            var splittingPair = GetPairToSplit(lastPair);
            if (splittingPair != null)
                SplitPair(splittingPair);
        }

        Console.WriteLine();
        PrintToScreen(lastPair);
    }

    Console.WriteLine();
    PrintToScreen(lastPair);

    Console.WriteLine($"Réponse 1 : ");
}

void SplitPair(Pair pair)
{
    Console.WriteLine();
    Console.WriteLine($"Splitting Pair : {pair.Value}");

    var isImpair = pair.Value % 2 != 0;
    pair.Left = new Pair(pair.Value / 2);
    pair.Right = new Pair((pair.Value / 2) + (isImpair ? 1 : 0));

    pair.IsValue = false;

    Console.WriteLine();
    Console.WriteLine($"Splitted Pair : [{pair.Left.Value},{pair.Right.Value}]");
}
    

Pair GetPairToSplit(Pair pair)
{
    if (pair.Left != null)
    {
        if (pair.Left.IsValue && pair.Left.Value >= 10)
            return pair.Left;
        else
        {
            var leftPair = GetPairToSplit(pair.Left);
            if (leftPair != null)
                return leftPair;
        }
    }

    if (pair.Right != null)
    {
        if (pair.Right.IsValue && pair.Right.Value >= 10)
            return pair.Right;
        else
        {
            var rightPair = GetPairToSplit(pair.Right);
            if (rightPair != null)
                return rightPair;
        }
    }

    return null;
}

void ExplodePair(Pair explodingPair)
{
    Console.WriteLine($"Exploding Pair {explodingPair.Left.Value},{explodingPair.Right.Value}");

    if (explodingPair != null)
    {
        var parentPairLeft = explodingPair.Parent;
        while (parentPairLeft != null)
        {
            if (parentPairLeft.Left.IsValue)
            {
                parentPairLeft.Left.Value += explodingPair.Left.Value;
                break;
            }
            else
                parentPairLeft = parentPairLeft.Parent;
        }

        var parentPairRight = explodingPair.Parent;
        while (parentPairRight != null)
        {
            if (parentPairRight.Right.IsValue)
            {
                parentPairRight.Right.Value += explodingPair.Right.Value;
                break;
            }
            else
                parentPairRight = parentPairRight.Parent;                    
        }

        explodingPair.Value = 0;
        explodingPair.IsValue = true;

        Console.WriteLine();
        Console.WriteLine($"Exploded Pair : [{explodingPair.Left.Value},{explodingPair.Right.Value}]");
    }
}

void PrintToScreen(Pair pair)
{
    Console.Write("(");

    if (pair.Left.IsValue)
        Console.Write(pair.Left.Value);
    else
        PrintToScreen(pair.Left);

    Console.Write(",");

    if (pair.Right.IsValue)
        Console.Write(pair.Right.Value);
    else
        PrintToScreen(pair.Right);

    Console.Write(")");
}

Pair GetPairToExplode(Pair pair, int index)
{
    if (pair.Left.IsValue && pair.Right.IsValue && index >= 4)
        return pair;

    index++;

    if (!pair.Left.IsValue)
    {
        var leftPair = GetPairToExplode(pair.Left, index);
        if (leftPair != null)
            return leftPair;
    }
    
    if (!pair.Right.IsValue)
        return GetPairToExplode(pair.Right, index);

    return null;
}

// "[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]"
Pair OpenPair(Pair parentPair, string equation, int index, bool isRight = false)
{
    Pair pair = new Pair();
    var currentChar = equation[index].ToString();
    Console.WriteLine($"Current Char {currentChar}");

    int value = -1;
    if (Int32.TryParse(currentChar, out value))
    {
        pair = new Pair(value);
    }
    if (currentChar == ",")
    {
        index++;
        return OpenPair(parentPair, equation, index, true);
    }

    if (isRight)
        parentPair.Right = pair;
    else
        parentPair.Left = pair;

    index++;

    if (index < equation.Length)
        return OpenPair(pair, equation, index, isRight);
    else
        return parentPair;
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile(true);

    for (var i = 0; i < data.Length; i++)
    {

    }

    Console.WriteLine($"Réponse 2 : ");
}