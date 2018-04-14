using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private GameManager gm;
        private ScoreManager sm;
        private Boolean outline;
        private Boolean labels;
        private Composite nextShape;
        public Form1()
        {
            InitializeComponent();
            sm = new ScoreManager(panel2);
            gm = GameManager.getInstance(panel1, sm);
            this.KeyPreview = true;
            easyToolStripMenuItem.Checked = true;
            outline = true;
            labels = false;
            outlineToolStripMenuItem.Checked = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics formGraphics = e.Graphics;
           SolidBrush brush = new SolidBrush(Color.Black) ;
            if (gm.getActiveShape() != null)
            {
                Composite c = gm.getActiveShape();
                brush = new SolidBrush(c.getColor());

                Font drawFont = null;
                SolidBrush drawBrush = null;
                string drawString;
                StringFormat drawFormat = null;
                float x, y;
                if (labels == true)
                {
                    drawString = "";
                    drawFont = new Font("Arial", 9);
                    if (c.getColor() != Color.Yellow && c.getColor() != Color.Cyan)
                    {
                        drawBrush = new SolidBrush(Color.White);
                    }
                    else
                    {
                        drawBrush = new SolidBrush(Color.Black);
                    }
                    x = 0;
                    y = 0;
                    drawFormat = new StringFormat();
                }

                foreach (Rectangle rect in c.getChildren())
                {
                    formGraphics.FillRectangle(brush, rect);
                    if (outline == true)
                    {
                        formGraphics.DrawRectangle(new Pen(Color.Black), rect);
                    }
                    if (labels == true)
                    {
                        int column = (rect.X / 30) + 1;
                        drawString = "" + column;
                        x = rect.X + 10;
                        y = rect.Y + 10;
                        if (column >= 10)
                        {
                            x -= 3;
                        }
                        formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                    }
                }
            }
            foreach (Composite comp in gm.getShapes())
            {
                Font drawFont = null;
                SolidBrush drawBrush = null;
                string drawString;
                StringFormat drawFormat = null;
                float x, y;
                if (labels == true)
                {
                    drawString = "";
                    drawFont = new Font("Arial", 9);
                    if (comp.getColor() != Color.Yellow && comp.getColor() != Color.Cyan)
                    {
                        drawBrush = new SolidBrush(Color.White);
                    }
                    else
                    {
                        drawBrush = new SolidBrush(Color.Black);
                    }
                    x = 0;
                    y = 0;
                    drawFormat = new StringFormat();
                }
                brush = new SolidBrush(comp.getColor());
                foreach (Rectangle rect in comp.getChildren())
                {
                    formGraphics.FillRectangle(brush, rect);
                    if (outline == true)
                    {
                        formGraphics.DrawRectangle(new Pen(Color.Black), rect);
                    }
                    if (labels == true)
                    {
                        int column = (rect.X / 30) + 1;
                        drawString = "" + column;
                        x = rect.X + 10;
                        y = rect.Y + 10;
                        if (column >= 10)
                        {
                            x -= 3;
                        }
                        formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                    }
                }
            }

            if (gm.isPaused() == true)
            {
                string drawString = "P A U S E D";
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 24);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                float x = 120;
                float y = 200;
                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();
            }
            if (gm.getGameEnd() == true)
            {
                string drawString = "G A M E  O V E R";
                System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 24);
                System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
                float x = 80;
                float y = 200;
                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                formGraphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                drawFont.Dispose();
                drawBrush.Dispose();
            }
            panel2.Refresh();
            if (nextShape != gm.getNextShape())
            {
                panel3.Refresh();
            }
            brush.Dispose();
            //formGraphics.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gm.isPaused() == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        gm.moveLeft();
                        break;
                    case Keys.D:
                        gm.moveRight();
                        break;
                    case Keys.S:
                        for (int i = 0; i < 5; i++)
                        {
                            gm.moveDown();
                        }
                        break;
                    case Keys.W:
                        gm.changeShape();
                        panel1.Refresh();
                        break;
                }
            }

            if (e.KeyCode == Keys.P)
            {
                if (gm.gameStatus() == true)
                {
                    gm.pause();
                    if (gm.isPaused() == true)
                    {
                        pauseToolStripMenuItem.Checked = true;
                    }
                    else
                    {
                        pauseToolStripMenuItem.Checked = false;
                    }
                    panel1.Refresh();
                }
            }
        }

        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gm.moveLeft();
                    break;
                case Keys.Right:
                    gm.moveRight();
                    break;
                case Keys.Down:
                    for (int i = 0; i < 5; i++)
                    {
                        gm.moveDown();
                    }
                    break;
                case Keys.Up:
                    gm.changeShape();
                    panel1.Refresh();
                    break;
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gm.clear();
            gm.createShape();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Draw score on
            System.Drawing.Graphics formGraphics = e.Graphics;
            string drawScore = sm.getScore();
            System.Drawing.Font drawScoreFont = new System.Drawing.Font("Arial", 24);
            System.Drawing.SolidBrush drawScoreBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            float scoreX = 130;
            float scoreY = 20;
            System.Drawing.StringFormat drawScoreFormat = new System.Drawing.StringFormat(StringFormatFlags.DirectionRightToLeft);
            drawScoreFormat.LineAlignment = StringAlignment.Center;
            formGraphics.DrawString(drawScore, drawScoreFont, drawScoreBrush, scoreX, scoreY, drawScoreFormat);
            drawScoreFont.Dispose();
            drawScoreBrush.Dispose();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gm.setDifficulty(0);
            easyToolStripMenuItem.Checked = true;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = false;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gm.setDifficulty(1);
            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = true;
            hardToolStripMenuItem.Checked = false;
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gm.setDifficulty(2);
            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gm.gameStatus() == true)
            {
                gm.pause();
                if (gm.isPaused() == true)
                {
                    pauseToolStripMenuItem.Checked = true;
                }
                else
                {
                    pauseToolStripMenuItem.Checked = false;
                }
                panel1.Refresh();
            }
        }

        private void outlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (outline == true)
            {
                outline = false;
                outlineToolStripMenuItem.Checked = false;
            }
            else
            {
                outline = true;
                outlineToolStripMenuItem.Checked = true;
            }
            panel1.Refresh();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            if (gm.getNextShape() != null)
            {
                System.Drawing.Graphics formGraphics = e.Graphics;
                nextShape = gm.getNextShape();
                Color color = nextShape.getColor();
                SolidBrush brush = new SolidBrush(color);
                Composite c = gm.getActiveShape();
                int xOffset = 0;
                int yOffset = 0;
                if (color == Color.Cyan)
                {
                    yOffset = -25;
                    xOffset = -2;
                }
                else if (color == Color.Blue || color == Color.Magenta)
                {
                    yOffset = -10;
                    xOffset = 15;
                }
                else if (color == Color.Orange || color == Color.Red || color == Color.Green)
                {
                    yOffset = 5;
                }
                else if (color == Color.Yellow)
                {
                    yOffset = -10;
                }
                foreach (Rectangle rect in nextShape.getChildren())
                {
                    Rectangle rect1 = new Rectangle(rect.X - 145 + xOffset, rect.Y + 70 + yOffset, 30, 30);
                    formGraphics.FillRectangle(brush, rect1);
                    if (outline == true)
                    {
                        formGraphics.DrawRectangle(new Pen(Color.Black), rect1);
                    }
                }
                brush.Dispose();
                formGraphics.Dispose();
            }
        }

        private void columnLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labels == true)
            {
                columnLabelsToolStripMenuItem.Checked = false;
                labels = false;
            }
            else
            {
                columnLabelsToolStripMenuItem.Checked = true;
                labels = true;
            }
            panel1.Refresh();
        }

    }
}
