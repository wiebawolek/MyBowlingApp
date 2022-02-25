using System;
using Xunit;
using MyBowlingApp.BowlingScoringBoard;
using MyBowlingApp.BowlingScoringBoard.ViewModel;
using System.Collections.ObjectModel;
using Xunit.Abstractions;

namespace MyBowlingApp.BowlingScoringBoard.test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test1()
        {
            var game = new BowlingGameplay();
            string playerName = "piotr";
            var rnd = new Random();
            game.AddNewPlayer(playerName);
            for (int i = 1; i <= 10; i++)
            {
                int[] pinsDown = new int[3];
                pinsDown[0] = rnd.Next(11);
                if (pinsDown[0] != 10)
                {
                    pinsDown[1] = rnd.Next(11 - pinsDown[0]);
                }
                game.AddPlayerFrame(playerName, i, pinsDown);
                output.WriteLine($"Pins throw 1: {pinsDown[0]} throw 2: {pinsDown[1]}. Score after {i} frames {game.Players[playerName].TotalScore}");
            }

            for (int i = 1; i <= 10; i++)
            {
                output.WriteLine($"final score after {i} frame {game.Players[playerName].GetFrame(i).FrameScore}");
            }
        }
    }
}
