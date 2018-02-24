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
    public partial class Form1 : System.Windows.Forms.Form
    {
        Form2 form;
        Bitmap originalPhoto;

        int bc = 70;
        double cntr = 1.7;
        const int matrixSize = 3;
        int divisor = 1;
        int offset = 0;
        Point anchorPoint = new Point(1, 1);

        public Form1()
        {
            InitializeComponent();

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            originalPhoto = Properties.Resources.pingwiny;
            pictureBox.Image = originalPhoto;
        }

        private void inversion_Click(object sender, EventArgs e)
        {
            Color color;
            int r, g, b;
            Bitmap tmp = (Bitmap)originalPhoto.Clone();

            for (int x = 0; x < originalPhoto.Width; x++)
            {
                for(int y = 0; y < originalPhoto.Height; y++)
                {
                    color = originalPhoto.GetPixel(x, y);

                    r = 255 - color.R;
                    g = 255 - color.G;
                    b = 255 - color.B;

                    tmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image = tmp;
        }

        private void brightness_Click(object sender, EventArgs e)
        {
            Color color;
            int r, g, b;
            Bitmap tmp = (Bitmap)originalPhoto.Clone();

            for (int x = 0; x < originalPhoto.Width; x++)
            {
                for (int y = 0; y < originalPhoto.Height; y++)
                {
                    color = originalPhoto.GetPixel(x, y);

                    r = check(color.R + bc);
                    g = check(color.G + bc);
                    b = check(color.B + bc);

                    tmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image = tmp;
        }

        private void original_Click(object sender, EventArgs e)
        {
            pictureBox.Image = originalPhoto;
        }


        private void contrast_Click(object sender, EventArgs e)
        {
            Color color;
            int r, g, b;
            Bitmap tmp = (Bitmap)originalPhoto.Clone();

            for (int x = 0; x < originalPhoto.Width; x++)
            {
                for (int y = 0; y < originalPhoto.Height; y++)
                {
                    color = originalPhoto.GetPixel(x, y);

                    r = check((int)contrastFormula(color.R));
                    g = check((int)contrastFormula(color.G));
                    b = check((int)contrastFormula(color.B));

                    tmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image = tmp;
        }

        private double contrastFormula(double c)
        {
            c = cntr * (c - 127) + 127;
            return c;
        }

        private void blur_Click(object sender, EventArgs e)
        {
            //int[,] matrix = new int[matrixSize, matrixSize];

            int[,] matrix =  {
                        { 1,1,1 },
                        { 1,1,1 },
                        { 1,1,1 }
                     };

            divisor = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    divisor += matrix[i,j];

            Point anchor = new Point(1, 1);
            Color color;
            int r, g, b, rr = 0, gg = 0, bb = 0, mx, my;
            
            Bitmap tmp = (Bitmap)originalPhoto.Clone();

            for (int y = 0; y < originalPhoto.Height; y++)
            {
                for (int x = 0; x < originalPhoto.Width; x++)
                {
                    rr = 0;
                    gg = 0;
                    bb = 0;
                    for (int j = 0; j < matrixSize; j++)
                    {
                        for(int i = 0; i < matrixSize; i++)
                        {
                            mx = x + i - anchor.X;
                            my = y + j - anchor.Y;

                            if (mx < 0) mx = 0;
                            else
                            {
                                if (mx >= originalPhoto.Width) mx = originalPhoto.Width - 1;
                            }
                            
                            if (my < 0) my = 0;
                            else
                            {
                                if (my >= originalPhoto.Height) my = originalPhoto.Height - 1;
                            }
                            if (my >= originalPhoto.Height) my = originalPhoto.Height - 1;

                            color = originalPhoto.GetPixel(mx, my);
                            rr += matrix[i, j] * color.R;
                            gg += matrix[i, j] * color.G;
                            bb += matrix[i, j] * color.B;
                        }
                    }

                    r = offset + rr / divisor;
                    g = offset + gg / divisor;
                    b = offset + bb / divisor;

                    r = check(r);
                    g = check(g);
                    b = check(b);

                    tmp.SetPixel(x, y, Color.FromArgb(r,g,b));
                }
            }

            pictureBox.Image = tmp;
        }

        private int check(int c)
        {
            if (c > 255) c = 255;
            else
                if (c < 0) c = 0;

            return c;
        }

        private void convolutionFiler(int[,] matrix, Point anchor, int div, int off)
        {

        }

        private void editor_Click(object sender, EventArgs e)
        {
            form = new Form2();
            form.ShowDialog();
        }
    }
}
