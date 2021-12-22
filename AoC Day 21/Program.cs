using AoC_Day_21;
using Utilities;

SolvePuzzleOne();
SolvePuzzleTwo();

void SolvePuzzleOne()
{
    var data = DataLoader.GetStringDataFromFile();

    var player1 = new Player(Int32.Parse(data.First().Last().ToString()));
    var player2 = new Player(Int32.Parse(data.Last().Last().ToString()));

    var dice = new Dice();
    var board = new Board(10);

    var cptTurn = 0;
    var finished = false;
    var losingScore = 0;
    while(!finished)
    {
        cptTurn++;

        if (cptTurn % 2 != 0)
        {
            player1.MoveTo(board.Move(player1.Position, dice.RollThreeTimes()));
            if (player1.Score >= 1000)
            {
                finished = true;
                losingScore = player2.Score;
            }

            Console.WriteLine($"Player 1 score {player1.Score} at position {player1.Position}");
        }
        else
        {
            player2.MoveTo(board.Move(player2.Position, dice.RollThreeTimes()));
            if (player2.Score >= 1000)
            {
                finished = true;
                losingScore = player1.Score;
            }

            Console.WriteLine($"Player 2 score {player2.Score} at position {player2.Position}");
        }
    }

    Console.WriteLine($"Réponse 1 : {losingScore * dice.DieRoll}");
}

void SolvePuzzleTwo()
{
    var data = DataLoader.GetStringDataFromFile(true);

    for (var i = 0; i < data.Length; i++)
    {

    }

    Console.WriteLine($"Réponse 2 : ");
}