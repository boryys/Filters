using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filters
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            
            InitializeComponent();
            NewMatrix();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {

            NewMatrix();
        }

        private void NewMatrix()
        {
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            int SizeX = Int32.Parse(width.Text);
            int SizeY = Int32.Parse(height.Text);

            panel.ColumnCount = SizeX;
            panel.RowCount = SizeY;

            Point anchor = new Point(Int32.Parse(positionX.Text), Int32.Parse(positionY.Text));

            panel.Size = new Size(33*SizeX, 23*SizeY);

            for (int row = 0; row < SizeY; row++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 23));
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33));

                for (int column = 0; column < SizeX; column++)
                {
                    TextBox text = new TextBox();
                    text.Tag = new Point(column, row);
                    text.Size = new Size(30,20);
                    text.Margin = new Padding(0, 0, 3, 3);

                    if(column == anchor.X-1 && row == anchor.Y - 1)
                    {
                        text.BackColor = Color.Magenta;
                    }
                    
                    panel.Controls.Add(text);
                }
            }

            
        }

       
    }
}
