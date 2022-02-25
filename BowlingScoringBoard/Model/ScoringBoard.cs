using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBowlingApp.BowlingScoringBoard.Model
{
    public class ScoringBoard
    {
        private BowlingFrame[] _GameCard;
        public BowlingFrame[] GetGameCard()
        {
            return _GameCard;
        }

        public ScoringBoard()
        {
            _GameCard = new BowlingFrame[10];
        }

        public FrameResultEnum AddFrame(int frameNo, IEnumerable<int> pins)
        {
            if (frameNo > _GameCard.Length && frameNo < 0)
                throw new ArgumentException("frameNo should be defined as 1-10 number. Current value: " + frameNo.ToString());
            _GameCard[frameNo - 1] = new BowlingFrame(pins);
            return _GameCard[frameNo - 1].FrameResult;
        }

        public BowlingFrame GetFrame(int frameNo)
        {
            if (frameNo > _GameCard.Length && frameNo < 0)
                throw new ArgumentException("frameNo should be defined as 1-10 number. Current value: " + frameNo.ToString());
            return _GameCard[frameNo - 1];
        }

        public void UpdateFrame(int frameNo, int bonus)
        {
            if (frameNo > _GameCard.Length && frameNo < 0)
                throw new ArgumentException("frameNo should be defined as 1-10 number. Current value: " + frameNo.ToString());
            _GameCard[frameNo - 1].Bonus = bonus;
        }

        public int TotalScore
        {
            get
            {
                int result=0;
                foreach(var item in _GameCard)
                if (item != null) result+=item.FrameScore;
                return result;
            }
        }

    }
}