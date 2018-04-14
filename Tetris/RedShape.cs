using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class RedShape : Composite
    {
        private int numStates = 2;
        public RedShape()
        {
            color = Color.Red;
            System.Drawing.Rectangle rect4 = new System.Drawing.Rectangle((420 / 2), -60, 30, 30);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle((420 / 2) - 30, -30, 30, 30);
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle((420 / 2), -30, 30, 30);
            System.Drawing.Rectangle rect3 = new System.Drawing.Rectangle((420 / 2) - 30, 0, 30, 30);
            add(rect4);
            add(rect);
            add(rect2);
            add(rect3);
        }
        public override void increaseState()
        {
            if (state == 1)
            {
                if (list.ElementAt(0).X != (420 - 30))
                {
                    state = 2;
                    List<Rectangle> list2 = new List<Rectangle>();
                    lock (GameManager.getInstance().locker)
                    {
                        list2.Add(new Rectangle(list.ElementAt(0).X + 30, list.ElementAt(0).Y + 60, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(1).X + 30, list.ElementAt(1).Y, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(2).X, list.ElementAt(2).Y + 30, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(3).X, list.ElementAt(3).Y - 30, 30, 30));
                    }
                    checkConflicting(list2);
                }
            }
            else if (state == 2)
            {
                state = 1;
                List<Rectangle> list2 = new List<Rectangle>();
                lock (GameManager.getInstance().locker)
                {
                    list2.Add(new Rectangle(list.ElementAt(0).X - 30, list.ElementAt(0).Y - 60, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(1).X - 30, list.ElementAt(1).Y, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(2).X, list.ElementAt(2).Y - 30, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(3).X, list.ElementAt(3).Y + 30, 30, 30));
                }
                checkConflicting(list2);
            }
        }
        protected override int getNumStates()
        {
            return numStates;
        }
    }
}
