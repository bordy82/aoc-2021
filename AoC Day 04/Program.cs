using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();
    
    var bingoCards = CreateBingoCards(data);
    var drawNumbers = data.First().Split(',');

    var winningCardValue = 0;
    var lastDraw = string.Empty;

    foreach(var draw in drawNumbers)
    {
        lastDraw = draw;

        MarkDraw(draw, bingoCards);

        foreach (var card in bingoCards)
        {
            if (ValidateCard(card))
            {
                winningCardValue = GetCardValue(card);
                break;
            }
        }

        if (winningCardValue > 0)
            break;
    }

    Console.WriteLine($"Réponse 1 : { winningCardValue * Int32.Parse(lastDraw) }");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile();

    var bingoCards = CreateBingoCards(data);
    var drawNumbers = data.First().Split(',');

    var lastDraw = string.Empty;
    var lastBoardToWin = new string[5,5];
    
    foreach (var draw in drawNumbers)
    {
        lastDraw = draw;

        MarkDraw(draw, bingoCards);

        var winning = new Dictionary<string[,], bool>();
        foreach (var card in bingoCards)
        {
            winning[card] = ValidateCard(card);
        }

        // We keep the reference to the last board to win.
        if (winning.Count(x => x.Value) == winning.Count() - 1)
            lastBoardToWin = winning.First(x => !x.Value).Key;

        if (winning.All(x => x.Value))
            break;
    }

    var cardValue = GetCardValue(lastBoardToWin);

    Console.WriteLine($"Réponse 2 : { cardValue * Int32.Parse(lastDraw) }");
}

List<string[,]> CreateBingoCards(string[] data)
{
    var index = 2;
    List<string[,]> bingoCards = new List<string[,]>();
    while (index < data.Length)
    {
        var bingoCard = new string[5, 5];
        for (var i = index; i < index + 5; i++)
        {
            var numbers = data[i].Trim().Replace("  ", " ").Split();
            for (var j = 0; j < 5; j++)
                bingoCard[i - index, j] = numbers[j];
        }

        bingoCards.Add(bingoCard);
        index += 6;
    }

    return bingoCards;
}

void MarkDraw(string draw, List<string[,]> cards)
{
    foreach (var card in cards)
    {
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                if (card[i, j] == draw)
                    card[i, j] = "x";
            }
        }
    }
}

bool ValidateCard(string[,] card)
{
    var win = false;
    for (var i = 0; i < 5; i++)
    {
        var rowWins = true;
        var colWins = true;
        for (var j = 0; j < 5; j++)
        {
            if (card[i, j] != "x")
                rowWins = false;

            if (card[j, i] != "x")
                colWins = false;
        }

        if (rowWins || colWins)
        {
            win = true;
            break;
        }
    }

    return win;
}

int GetCardValue(string[,] card)
{
    var cardValue = 0;
    for (var i = 0; i < 5; i++)
    {
        for (var j = 0; j < 5; j++)
        {
            var square = card[i, j];
            if (square != "x")
                cardValue += Int32.Parse(square);
        }
    }

    return cardValue;
}