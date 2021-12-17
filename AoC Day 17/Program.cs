using System.Drawing;
using Utilities;

SolvePuzzleOneAndTwo();

void SolvePuzzleOneAndTwo()
{
    var data = DataLoader.GetStringDataFromFile()[0];
    var valuesX = data.Split(",")[0].Split("x=")[1].Split("..").Select(x => Int32.Parse(x));
    var valuesY = data.Split(",")[1].Split("y=")[1].Split("..").Select(x => Int32.Parse(x));

    var initialVelocityCount = 0;
    var maxHeight = Int32.MinValue;
    var maxHeightStartingVelocity = new Point(0, 0);

    for (var i = -1000; i < 1000; i++)
    {
        for(var j = -1000; j < 1000; j++)
        {
            var velocity = new Point(i, j);
            var position = new Point(0, 0);
            var scopedMaxHeight = Int32.MinValue;

            while (position.X < valuesX.First() || position.Y > valuesY.Last())
            {
                if (position.X > valuesX.Last() || position.Y < valuesY.First())
                    break;

                position.X += velocity.X;
                position.Y += velocity.Y;

                if (position.Y > scopedMaxHeight)
                {
                    scopedMaxHeight = position.Y;
                }

                if (velocity.X > 0)
                    velocity.X--;

                if (velocity.X < 0)
                    velocity.X++;

                velocity.Y--;
            }

            if (position.X >= valuesX.First() && position.X <= valuesX.Last() && position.Y >= valuesY.First() && position.Y <= valuesY.Last())
            {
                if (scopedMaxHeight > maxHeight)
                {
                    maxHeight = scopedMaxHeight;
                    maxHeightStartingVelocity = new Point(i, j);
                }

                initialVelocityCount++;
            }
        }
    }
    
    Console.WriteLine($"Réponse 1 : {maxHeightStartingVelocity.X},{maxHeightStartingVelocity.Y} for a height of {maxHeight}");
    Console.WriteLine($"Réponse 2 : {initialVelocityCount} initial velocities");
}