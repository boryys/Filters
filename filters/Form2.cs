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

        private Bitmap tmp;
        public Bitmap TMP
        {
            get { return tmp; }
            set { TMP = value; }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            int mSizeWidth = Int32.Parse(width.Text);
            int mSizeHeight = Int32.Parse(height.Text);
            Point anchor = new Point(Int32.Parse(positionX.Text)-1, Int32.Parse(positionY.Text) - 1);
            int div = Int32.Parse(division.Text);
            int off = Int32.Parse(offset.Text);

            int[,] matrix = new int[mSizeWidth, mSizeHeight];

            for (int y = 0; y < mSizeHeight; y++)
            {
                for (int x = 0; x < mSizeWidth; x++)
                {
                    Control control = panel.GetControlFromPosition(x, y);

                    matrix[x, y] = Int32.Parse(control.Text);
                }
            }

            tmp = Form1.convolutionFiler(mSizeWidth, mSizeHeight, matrix, anchor, div, off);
        }

        private void blurButton_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            int[,] matrix =  {
                        { 1, 1, 1 },
                        { 1, 1, 1 },
                        { 1, 1, 1 }
                     };

            int divisor = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    divisor += matrix[i, j];
                }

            if (divisor == 0) divisor = 1;

            width.Text = "3";
            height.Text = "3";
            positionX.Text = "2";
            positionY.Text = "2";
            division.Text = divisor.ToString();
            offset.Text = "0";

            Point anchor = new Point(Int32.Parse(positionX.Text), Int32.Parse(positionY.Text));

            panel.Size = new Size(33 * 3, 23 * 3);

            NewMatrix();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Control control = panel.GetControlFromPosition(i, j);
                    control.Text = matrix[i, j].ToString();
                }
            }
        }

        private void gBlurButton_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            int[,] matrix =  {
                        { 0,1,0 },
                        { 1,4,1 },
                        { 0,1,0 }
                     };

            int divisor = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    divisor += matrix[i, j];
                }

            if (divisor == 0) divisor = 1;

            width.Text = "3";
            height.Text = "3";
            positionX.Text = "2";
            positionY.Text = "2";
            division.Text = divisor.ToString();
            offset.Text = "0";

            Point anchor = new Point(Int32.Parse(positionX.Text), Int32.Parse(positionY.Text));

            panel.Size = new Size(33 * 3, 23 * 3);

            NewMatrix();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Control control = panel.GetControlFromPosition(i, j);
                    control.Text = matrix[i, j].ToString();
                }
            }
        }

        private void sharpenButton_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            int[,] matrix =  {
                        {  0,-1, 0 },
                        { -1, 5,-1 },
                        {  0,-1, 0 }
                     };

            int divisor = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    divisor += matrix[i, j];
                }

            if (divisor == 0) divisor = 1;

            width.Text = "3";
            height.Text = "3";
            positionX.Text = "2";
            positionY.Text = "2";
            division.Text = divisor.ToString();
            offset.Text = "0";

            Point anchor = new Point(Int32.Parse(positionX.Text), Int32.Parse(positionY.Text));

            panel.Size = new Size(33 * 3, 23 * 3);

            NewMatrix();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Control control = panel.GetControlFromPosition(i, j);
                    control.Text = matrix[i, j].ToString();
                }
            }
        }

        private void edgeButton_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            int[,] matrix =  {
                        {  0,-1,0 },
                        {  0,1,0 },
                        {  0,0,0 }
                     };

            int divisor = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    divisor += matrix[i, j];
                }

            if (divisor == 0) divisor = 1;

            width.Text = "3";
            height.Text = "3";
            positionX.Text = "2";
            positionY.Text = "2";
            division.Text = divisor.ToString();
            offset.Text = "127";

            Point anchor = new Point(Int32.Parse(positionX.Text), Int32.Parse(positionY.Text));

            panel.Size = new Size(33 * 3, 23 * 3);

            NewMatrix();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Control control = panel.GetControlFromPosition(i, j);
                    control.Text = matrix[i, j].ToString();
                }
            }
        }

        private void embossButton_Click(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();

            int[,] matrix =  {
                        { -1, 0, 1 },
                        { -1, 1, 1 },
                        { -1, 0, 1 }
                     };

            int divisor = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    divisor += matrix[i, j];
                }

            if (divisor == 0) divisor = 1;

            width.Text = "3";
            height.Text = "3";
            positionX.Text = "2";
            positionY.Text = "2";
            division.Text = divisor.ToString();
            offset.Text = "0";

            Point anchor = new Point(Int32.Parse(positionX.Text), Int32.Parse(positionY.Text));

            panel.Size = new Size(33 * 3, 23 * 3);

            NewMatrix();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Control control = panel.GetControlFromPosition(i,j);
                    control.Text = matrix[i,j].ToString();
                }
            }
        }
    }
}
