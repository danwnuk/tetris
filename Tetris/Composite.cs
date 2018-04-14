using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Tetris
{
    class Composite
    {
        protected List<Rectangle> list;
        protected Color color;
        protected int state;

        public Composite()
        {
            this.state = 1;
            list = new List<Rectangle>();
        }
        public Composite(Color color)
        {
            //this.color = color;
            this.state = 1;
            list = new List<Rectangle>();
        }

        public void add(Rectangle rect)
        {
            list.Add(rect);
        }

        public List<Rectangle> getChildren()
        {
            return list;
        }

        public Color getColor()
        {
            return color;
        }

        public int getMinX()
        {
            int min = 600;
            for (int x = 0; x < list.Count; x++)
            {
                if (list.ElementAt(x).X < min)
                {
                    min = list.ElementAt(x).X;
                }
            }
            return min;
        }
        public int getMinY()
        {
            int min = 0;
            for (int x = 0; x < list.Count; x++)
            {
                if (list.ElementAt(x).Y < min)
                {
                    min = list.ElementAt(x).Y;
                }
            }
            return min;
        }
        public int getMaxX()
        {
            int max = 0;
            for (int x = 0; x < list.Count; x++)
            {
                if ((list.ElementAt(x).X+30) > max)
                {
                    max = list.ElementAt(x).X+30;
                }
            }
            return max;
        }
        public int getMaxY()
        {
            int max = 0;
            for (int x = 0; x < list.Count; x++)
            {
                if ((list.ElementAt(x).Y+30) > max)
                {
                    max = list.ElementAt(x).Y+30;
                }
            }
            return max;
        }
        public virtual void increaseState()
        {
        }

        protected void checkConflicting(List<Rectangle> list2)
        {
            if (list != null)
            {
                Boolean isConflict = false;
                foreach (Rectangle rect1 in list2)
                {
                    if (GameManager.getInstance().checkBox(rect1) == true)
                    {
                        isConflict = true;
                    }
                    if (rect1.X > 390 || rect1.X < 0)
                    {
                        isConflict = true;
                    }
                    if (rect1.Y > 570)
                    {
                        isConflict = true;
                    }
                }
                if (isConflict == false)
                {
                    this.list = list2;
                }
                else
                {
                    state--;
                    if (state == 0) {
                        state = getNumStates();
                    }
                }
            }
        }
        protected virtual int getNumStates()
        {
            return 0;
        }
    }
}
