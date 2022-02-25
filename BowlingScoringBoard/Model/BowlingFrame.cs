using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBowlingApp.BowlingScoringBoard.Model
{
    public class BowlingFrame
    {
        private IEnumerable<int> _ThrowedDownPins;

        public BowlingFrame(IEnumerable<int> throwedDownPins)
        {
            _ThrowedDownPins = throwedDownPins;
        }

        public int Bonus
        {
            get; set;
        }

        public FrameResultEnum FrameResult{
            get{
                if (_ThrowedDownPins.First()==10) return FrameResultEnum.Strike;
                var score = Enumerable.Sum(_ThrowedDownPins);
                if (score == 10) return FrameResultEnum.Spare;
                if (score == 0) return FrameResultEnum.Empty;
                return FrameResultEnum.Failed;
            }
        }

        public int FrameScore
        {
            get
            {
                return Bonus + Enumerable.Sum(_ThrowedDownPins);
            }
        }

    }
}