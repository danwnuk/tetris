using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading;

namespace Tetris
{
   class GameManager
    {
        private Composite activeShape;
        private List<Composite> shapes;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Timer timer;
        private Boolean gameEnd = false;
        private Boolean gameActive = false;
        private Boolean gamePaused = false;
        private int[] table = new int[281];
        private static GameManager gm = null;
        public Object locker = new Object();
        private ScoreManager sm;
        private Composite nextShape;

        public static GameManager getInstance()
        {
            return gm;
        }
        public static GameManager getInstance(System.Windows.Forms.Panel panel, ScoreManager sm)
        {
            if (gm == null)
            {
                gm = new GameManager(panel);
                gm.setScoreManager(sm);
            }
            return gm;
        }
        public void setScoreManager(ScoreManager sm)
        {
            this.sm = sm;
        }

        public void pause()
        {
            if (gamePaused == false)
            {
                timer.Enabled = false;
                gamePaused = true;
            }
            else
            {
                timer.Enabled = true;
                gamePaused = false;
            }
        }
        public Boolean isPaused()
        {
            if (gamePaused == false)
            {
                return false;
            }
            return true;
        }
        public GameManager(System.Windows.Forms.Panel panel)
        {
            this.panel = panel;
            shapes = new List<Composite>();
            timerSetup();
        }

        public Composite getNextShape()
        {
            return nextShape;
        }
        public void createShape()
        {
            if (nextShape == null)
            {
                activeShape = randomShape();
                nextShape = randomShape();
            }
            else
            {
                activeShape = nextShape;
                nextShape = randomShape();
            }
            panel.Refresh();
            timer.Enabled = true;
        }

        private Composite randomShape()
        {
            Composite returnShape = null;
            Random r = new Random();
            int x = r.Next(1, 8);
            switch (x)
            {
                case 1:
                    returnShape = new RedShape();
                    break;
                case 2:
                    returnShape = new GreenShape();
                    break;
                case 3:
                    returnShape = new CyanShape();
                    break;
                case 4:
                    returnShape = new YellowShape();
                    break;
                case 5:
                    returnShape = new BlueShape();
                    break;
                case 6:
                    returnShape = new MagentaShape();
                    break;
                case 7:
                    returnShape = new OrangeShape();
                    break;
            }
            return returnShape;
        }
        public int getMultiplier()
        {
            return timer.Interval;
        }

        public void changeMultiplier(int interval)
        {
            timer.Interval = interval;
        }
        private void timerSetup()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += new System.EventHandler(TimerTick);
            timer.Enabled = false;
            GC.KeepAlive(timer);
        }
        public void setDifficulty(int index)
        {
            if (index == 0)
            {
                timer.Interval = 200;
            }
            else if (index == 1)
            {
                timer.Interval = 100;
            }
            else if (index == 2)
            {
                timer.Interval = 25;
            }
        }
        public void TimerTick(object sender, EventArgs e)
        {

            if (activeShape != null)
            {
                List<Rectangle> children = activeShape.getChildren();
                if (hitCheckDown() != false)
                {

                    if (activeShape.getMaxY() < 600)
                    {
                        for (int x = 0; x < children.Count; x++)
                        {
                            Rectangle rect = children.ElementAt(x);
                            rect.Y += 5;
                            lock (locker)
                            {
                                children.Remove(children.ElementAt(x));
                                children.Insert(x, rect);
                            }
                        }
                        sm.increaseScore();
                    }
                    else
                    {
                        startNewShape();
                    }
                }
                else
                {
                    startNewShape();
                }
            }
            panel.Refresh();
           
        }

       public Boolean checkBox(Rectangle rectangle)
        {
            Boolean retVal = false;
            int[] table = new int[281];
            foreach (Composite comp in shapes)
            {
                foreach (Rectangle rect in comp.getChildren())
                {
                    int cellnum = (rect.X / 30) + (14 * (rect.Y / 30)) + 1;
                    table[cellnum] = 1;
                }
            }

            Boolean checkTwo = false;
            int boxNumber = (rectangle.X / 30) + (14 * (rectangle.Y / 30)) + 1;
            double boxNum = (rectangle.X / 30.0) + (14 * (rectangle.Y / 30.0)) + 1;
            int boxNumber2 = 0;
            if (boxNumber != boxNum)
            {
                checkTwo = true;
                if (boxNumber + 14 <= 280)
                { 
                    boxNumber2 = boxNumber + 14;
                }
                else 
                { 
                    boxNumber2 = boxNumber;
                }
            }
            if (boxNumber >= 0 && boxNumber <= 280)
            {
                if (table[boxNumber] == 1)
                {
                    retVal = true;
                }
                else if (checkTwo = true && table[boxNumber2] == 1)
                {
                    retVal = true;
                }
            }
            return retVal;
        }
        private void startNewShape()
        {
            timer.Enabled = false;
            shapes.Add(activeShape);
            //addToTable(activeShape);
            if (checkGameEnd() == false)
            {
                activeShape = null;
                checkRows();
                createShape();
            }
        }
        /*
        private void addToTable(Composite comp)
        {
            foreach (Rectangle rect in comp.getChildren())
            {
                int cellnum = (rect.X / 30) + (14 * (rect.Y / 30)) + 1;
                table[cellnum] = 1;
            }
        }
        private void addToTable(Rectangle rect)
        {
            int cellnum = (rect.X / 30) + (14 * (rect.Y / 30)) + 1;
            table[cellnum] = 1;
        }

        private void removeFromTable(Rectangle rect)
        {
            int cellnum = (rect.X / 30) + (14 * (rect.Y / 30)) + 1;
            table[cellnum] = 0;
        }*/
        private void checkRows()
        {
            int[] table = new int[281];
            foreach (Composite comp in shapes)
            {
                foreach (Rectangle rect in comp.getChildren())
                {
                    int cellnum = (rect.X / 30) + (14 * (rect.Y / 30)) + 1;
                    table[cellnum] = 1;
                }
            }
                Boolean rowErase = true;
                for (int x = 1; x <= 280; x++)
                {
                    if (table[x] == 0)
                    {
                        rowErase = false;
                    }
                    if (x % 14 == 0)
                    {
                        if (rowErase == true) 
                        {
                            eraseRow(x/14);
                        }
                        rowErase = true;
                    }
                }
        }

        private void eraseRow(int rowNumber)
        {
            foreach (Composite comp in shapes)
            {
               for (int x = 0; x < comp.getChildren().Count; x++) {
                   int thisRow = (comp.getChildren().ElementAt(x).Y / 30) + 1;
                    if (rowNumber == thisRow)
                    {
                        //removeFromTable(comp.getChildren().ElementAt(x));
                        comp.getChildren().RemoveAt(x);
                        x--;
                    }
                }
            }
            foreach (Composite comp in shapes)
            {
                for (int z = 0; z < comp.getChildren().Count; z++)
                {
                    int thisRow = (comp.getChildren().ElementAt(z).Y / 30) + 1;
                    if (thisRow < rowNumber)
                    {
                        int x = comp.getChildren().ElementAt(z).X;
                        int y = comp.getChildren().ElementAt(z).Y;
                        //removeFromTable(comp.getChildren().ElementAt(z));
                        comp.getChildren().RemoveAt(z);
                        Rectangle rect = new Rectangle(x, y+30, 30, 30);
                        //addToTable(rect);
                        comp.getChildren().Insert(z, rect);
                    }
                }
            }

            for (int t = 0; t < shapes.Count; t++)
            {
                if (shapes.ElementAt(t).getChildren().Count == 0)
                {
                    shapes.RemoveAt(t);
                    t--;
                }
            }

            sm.increaseScore(1000);
            if (shapes.Count == 0)
            {
                sm.increaseScore(10000);
            }
        }
        private Boolean hitCheckDown()
        {
            Boolean retVal = true;
            foreach (Rectangle rect in activeShape.getChildren())
            {
                int x = rect.X;
                int y = rect.Y;

                foreach (Composite comp in shapes)
                {
                    foreach (Rectangle rect2 in comp.getChildren())
                    {
                        if (rect2.Y == (y + 30) && rect2.X == x)
                        {
                            retVal = false;
                        }
                    }
                }
            }
            return retVal;
        }
        private Boolean hitCheckLeft()
        {
            Boolean retVal = true;
            foreach (Rectangle rect in activeShape.getChildren())
            {
                int x = rect.X;
                int y = rect.Y;

                foreach (Composite comp in shapes)
                {
                    foreach (Rectangle rect2 in comp.getChildren())
                    {
                        if (Math.Abs(rect2.Y-y) < 30 && rect2.X == (x - 30))
                        {
                            retVal = false;
                        }
                    }
                }
            }
            return retVal;
        }

        private Boolean hitCheckRight()
        {
            Boolean retVal = true;
            foreach (Rectangle rect in activeShape.getChildren())
            {
                int x = rect.X;
                int y = rect.Y;

                foreach (Composite comp in shapes)
                {
                    foreach (Rectangle rect2 in comp.getChildren())
                    {
                        if (Math.Abs(rect2.Y-y) < 30 && rect2.X == (x + 30))
                        {
                            retVal = false;
                        }
                    }
                }
            }
            return retVal;
        }
        public void changeShape()
        {
            activeShape.increaseState();
        }
        public void moveLeft()
        {
            if (activeShape != null)
            {
                if (hitCheckLeft() != false)
                {
                    List<Rectangle> children = activeShape.getChildren();
                    if (activeShape.getMinX() > 0)
                    {
                        for (int x = 0; x < children.Count; x++)
                        {
                            Rectangle rect = children.ElementAt(x);
                            rect.X -= 30;
                            lock (locker)
                            {
                                children.Remove(children.ElementAt(x));
                                children.Insert(x, rect);
                            }
                        }
                    }
                }
            }
            panel.Refresh();
        }

        public void moveRight()
        {
                if (activeShape != null)
                {
                    if (hitCheckRight() != false)
                    {
                        List<Rectangle> children = activeShape.getChildren();
                        if (activeShape.getMaxX() < 420)
                        {

                            for (int x = 0; x < children.Count; x++)
                            {
                                Rectangle rect = children.ElementAt(x);
                                rect.X += 30;
                                lock (locker)
                                {
                                    children.Remove(children.ElementAt(x));
                                    children.Insert(x, rect);
                                }
                            }
                        }
                    }
            }
            panel.Refresh();
        }

        public void moveDown()
        {
                if (activeShape != null)
                {
                    List<Rectangle> children = activeShape.getChildren();
                    if (hitCheckDown() != false)
                    {
                        if (activeShape.getMaxY() < 600)
                        {

                            for (int x = 0; x < children.Count; x++)
                            {
                                Rectangle rect = children.ElementAt(x);
                                rect.Y += 5;
                                lock (locker)
                                {
                                    children.Remove(children.ElementAt(x));
                                    children.Insert(x, rect);
                                }
                            }
                            sm.increaseScore2X();
                        }
                        else
                        {
                            startNewShape();
                        }
                    }
                    else
                    {
                        startNewShape();
                    }
            }
            panel.Refresh();
        }

        public Composite getActiveShape()
        {
            return activeShape;
        }

        public List<Composite> getShapes()
        {
            return shapes;
        }

        /*
         * Clears the shapes which in turn causes the redraw to not draw anything. Also stops the timer from creating new timers.
         * Refreshes the panel.
         * 
         */
        public void clear()
        {
            if (timer != null)
            {
                timer.Enabled = false;
            }
            gameEnd = false;
            activeShape = null;
            shapes = new List<Composite>();
            gamePaused = false;
            gameActive = true;
            sm.resetScore();
            panel.Refresh();
        }

        public Boolean gameStatus()
        {
            return gameActive;
        }
        /*
         * Checks if the game should be ending (if a shape reached the top.) 
         */
        private Boolean checkGameEnd()
        {
            Boolean retVal = false;
            foreach (Rectangle rect in activeShape.getChildren())
            {
                if (rect.Y <= 0)
                {
                    retVal = true;
                    gameEnd = true;
                    gameActive = false;
                }
            }
            return retVal;
        }

        /*
         * Gets whether or not the game has ended.
         */
        public Boolean getGameEnd()
        {
            return gameEnd;
        }
        /*
         * Sets whether the game has ended.
         */
        public void setGameEnd(Boolean isGameEnd)
        {
            gameEnd = isGameEnd;
        }
    }
}
