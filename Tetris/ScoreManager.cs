using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    class ScoreManager
    {
        private int score = 0;
        private System.Windows.Forms.Panel panel;

        public ScoreManager(System.Windows.Forms.Panel panel)
        {
            this.panel = panel;
        }
        public void increaseScore()
        {
            score += 1;
        }

        public void increaseScore2X()
        {
            increaseScore();
            increaseScore();
        }

        public void increaseScore(int increase)
        {
            score += increase;
        }

        public String getScore()
        {
            return "" + score;
        }
        public void resetScore()
        {
            score = 0;
        }
    }
}
