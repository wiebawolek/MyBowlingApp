using System;
using System.Collections.Generic;
using System.Linq;
using BowlingScoringBoardMVVM.Model;
using System.Windows.Input;
using BowlingScoringBoardMVVM.ViewModel.Commands;

namespace BowlingScoringBoardMVVM.ViewModel
{
    public class BowlingGameplayVM : ViewModelBase
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
        public bool GameStarted
        {
            get
            {
                return _gameStarted;
            }
            set
            {
                _gameStarted = value;
                OnPropertyChange(nameof(GameStarted));
            }
        }

        private ICommand _AddPlayerCommand;
        public ICommand AddPlayerCommand
        {
            get
            {
                if (_AddPlayerCommand == null)
                {
                    _AddPlayerCommand = new CleanCommand(
                        p => { return Players.Count() != 4; },
                        p => AddNewPlayer($"Player{Players.Count() + 1}"));
                }
                return _AddPlayerCommand;
            }
        }

        private ICommand _StartGameCommand;
        public ICommand StartGameCommand
        {
            get
            {
                if (_StartGameCommand == null)
                {
                    _StartGameCommand = new CleanCommand(
                        p => { return !GameStarted && Players.Count > 0; },
                        p => { GameStarted = true; });
                }
                return _StartGameCommand;
            }
        }
        private ICommand _EndGameCommand;
        public ICommand EndGameCommand
        {
            get
            {
                if (_EndGameCommand == null)
                {
                    _EndGameCommand = new CleanCommand(
                        p => { return _gameStarted; },
                        p => { _gameStarted = false; });
                }
                return _EndGameCommand;
            }
        }

        public void AddNewPlayer(string playerName)
        {
            if (!_gameStarted)
                Players.Add(playerName, new ScoringBoard());
            OnPropertyChange(nameof(Players));
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
    }
}