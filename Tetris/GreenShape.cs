using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class GreenShape : Composite
    {
        private int numStates = 4;
        public GreenShape()
        {
            color = Color.Green;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle((420 / 2) - 30, -60, 30, 30);
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle((420 / 2) - 30, -30, 30, 30);
            System.Drawing.Rectangle rect3 = new System.Drawing.Rectangle((420 / 2) - 30, 0, 30, 30);
            System.Drawing.Rectangle rect4 = new System.Drawing.Rectangle((420 / 2), -30, 30, 30);
            add(rect);
            add(rect2);
            add(rect4);
            add(rect3);
        }

        public override void increaseState()
        {
            if (state == 1)
            {
                if (list.ElementAt(0).X != 420 - 60)
                {
                    state = 2;
                    List<Rectangle> list2 = new List<Rectangle>();
                    lock (GameManager.getInstance().locker)
                    {
                        list2.Add(new Rectangle(list.ElementAt(0).X + 60, list.ElementAt(0).Y, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(1).X + 30, list.ElementAt(1).Y - 30, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(2).X, list.ElementAt(2).Y, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(3).X, list.ElementAt(3).Y - 60, 30, 30));
                    }
                    checkConflicting(list2);
                }
            }
            else if (state == 2)
            {
                state = 3;
                List<Rectangle> list2 = new List<Rectangle>();
                lock (GameManager.getInstance().locker)
                {
                    list2.Add(new Rectangle(list.ElementAt(0).X - 30, list.ElementAt(0).Y + 60, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(1).X, list.ElementAt(1).Y + 30, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(2).X - 30, list.ElementAt(2).Y, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(3).X + 30, list.ElementAt(3).Y, 30, 30));
                }
                checkConflicting(list2);
            }
            else if (state == 3)
            {
                if (list.ElementAt(0).X != 420 - 30)
                {
                    state = 4;
                    List<Rectangle> list2 = new List<Rectangle>();
                    lock (GameManager.getInstance().locker)
                    {
                        list2.Add(new Rectangle(list.ElementAt(0).X - 30, list.ElementAt(0).Y - 30, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(1).X, list.ElementAt(1).Y, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(2).X + 30, list.ElementAt(2).Y - 30, 30, 30));
                        list2.Add(new Rectangle(list.ElementAt(3).X + 30, list.ElementAt(3).Y + 30, 30, 30));
                    }
                    checkConflicting(list2);
                }
            }
            else if (state == 4)
            {
                state = 1;
                List<Rectangle> list2 = new List<Rectangle>();
                lock (GameManager.getInstance().locker)
                {
                    list2.Add(new Rectangle(list.ElementAt(0).X, list.ElementAt(0).Y - 30, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(1).X - 30, list.ElementAt(1).Y, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(2).X, list.ElementAt(2).Y + 30, 30, 30));
                    list2.Add(new Rectangle(list.ElementAt(3).X - 60, list.ElementAt(3).Y + 30, 30, 30));
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
