using System;
using System.Collections.Generic;
using System.Linq;
using MyBowlingApp.BowlingScoringBoard.Model;

namespace MyBowlingApp.BowlingScoringBoard.ViewModel
{
    public class BowlingGameplay
    {
        private IDictionary<string, ScoringBoard> _Players;
        public IDictionary<string, ScoringBoard> Players
        {
            get
            {
                if (_Players == null) _Players = new Dictionary<string, ScoringBoard>();
                return _Players;
            }
            set
            {
                _Players = value;
            }
        }
        private bool _gameStarted;

        public void AddNewPlayer(string playerName)
        {
            if (!_gameStarted)
                Players.Add(playerName, new ScoringBoard());
        }

        public void AddPlayerFrame(string playerName, int frameNo, IEnumerable<int> pins)
        {
            if (Players.ContainsKey(playerName))
            {
                Players[playerName].AddFrame(frameNo, pins);
                if (frameNo > 1)
                {
                    var prevFrame = _Players[playerName].GetFrame(frameNo - 1);
                    if (prevFrame.FrameResult == FrameResultEnum.Strike) _Players[playerName].UpdateFrame(frameNo - 1, Enumerable.Sum(pins));
                    if (prevFrame.FrameResult == FrameResultEnum.Spare) _Players[playerName].UpdateFrame(frameNo - 1, pins.First());
                }
            }
        }

        public void StartGame()
        {
            if (_Players.Count() > 0)
            {
                _gameStarted = true;

            }
            else
            {
                Console.WriteLine("Add new player to play a game");
            }
        }


        public void EndGame(){
            _gameStarted=false;
        }
    }
}