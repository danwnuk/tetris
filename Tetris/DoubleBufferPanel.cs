using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class DoubleBufferPanel : Panel
{
    public DoubleBufferPanel()
    {
        this.DoubleBuffered = true;
        // Set the value of the double-buffering style bits to true.
        this.SetStyle(ControlStyles.UserPaint,
        true);

        this.UpdateStyles();
    }
}
