using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class OrangeShape : Composite
    {
        private int numStates = 2;
        public OrangeShape()
        {
            color = Color.Orange;
            Rectangle rect = new Rectangle((420 / 2) - 30, -60, 30, 30);
            Rectangle rect2 = new Rectangle((420 / 2) - 30, -30, 30, 30);
            Rectangle rect3 = new Rectangle((420 / 2), -30, 30, 30);
            Rectangle rect4 = new Rectangle((420 / 2), 0, 30, 30);
            add(rect);
            add(rect2);
            add(rect3);
            add(rect4);
        }

        public override void increaseState()
        {
            if (state == 1)
            {
                state = 2;
                List<Rectangle> list2 = new List<Rectangle>();
                list2.Add(new Rectangle(list.ElementAt(0).X+60, list.ElementAt(0).Y, 30, 30));
                list2.Add(new Rectangle(list.ElementAt(1).X+30, list.ElementAt(1).Y-30, 30, 30));
                list2.Add(new Rectangle(list.ElementAt(2).X, list.ElementAt(2).Y, 30, 30));
                list2.Add(new Rectangle(list.ElementAt(3).X-30, list.ElementAt(3).Y-30, 30, 30));
                checkConflicting(list2);
            }
            else if (state == 2)
            {
                state = 1;
                List<Rectangle> list2 = new List<Rectangle>();
                list2.Add(new Rectangle(list.ElementAt(0).X-60, list.ElementAt(0).Y, 30, 30));
                list2.Add(new Rectangle(list.ElementAt(1).X - 30, list.ElementAt(1).Y +30, 30, 30));
                list2.Add(new Rectangle(list.ElementAt(2).X, list.ElementAt(2).Y, 30, 30));
                list2.Add(new Rectangle(list.ElementAt(3).X + 30, list.ElementAt(3).Y +30, 30, 30));
                checkConflicting(list2);
            }
        }

        protected override int getNumStates()
        {
            return numStates;
        }
    }
}
