using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class YellowShape : Composite
    {
        public YellowShape()
        {
            color = Color.Yellow;
            Rectangle rect = new Rectangle((420 / 2) - 30, -30, 30, 30);
            Rectangle rect2 = new Rectangle((420 / 2), -30, 30, 30);
            Rectangle rect3 = new Rectangle((420 / 2) - 30, 0, 30, 30);
            Rectangle rect4 = new Rectangle((420 / 2), 0, 30, 30);
            add(rect);
            add(rect2);
            add(rect3);
            add(rect4);
        }

        public override void increaseState()
        {
        }
    }
}
