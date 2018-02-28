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
        static Bitmap originalPhoto;

        int bc = 70;
        double cntr = 1.7;
        const int matrixSize = 3;
        int offset = 0;
        int offset2 = 127;
        Point anchorPoint = new Point(1, 1);

        public Form1()
        {
            InitializeComponent();

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            originalPhoto = Properties.Resources.widok;
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
            int[,] matrix =  {
                        { 1,1,1 },
                        { 1,1,1 },
                        { 1,1,1 }
                     };

            int divisor = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    divisor += matrix[i,j];

            if (divisor == 0) divisor = 1;

            Point anchor = new Point(1, 1);

            pictureBox.Image = convolutionFiler(matrixSize, matrixSize, matrix, anchor, divisor, offset);
        }

        private void gaussianBlur_Click(object sender, EventArgs e)
        {
            int[,] matrix =  {
                        { 0,1,0 },
                        { 1,4,1 },
                        { 0,1,0 }
                     };

            int divisor = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    divisor += matrix[i, j];

            if (divisor == 0) divisor = 1;

            Point anchor = new Point(1, 1);

            pictureBox.Image = convolutionFiler(matrixSize, matrixSize, matrix, anchor, divisor, offset);
        }

        private void sharpen_Click(object sender, EventArgs e)
        {
            int[,] matrix =  {
                        {  0,-1, 0 },
                        { -1, 5,-1 },
                        {  0,-1, 0 }
                     };

            int divisor = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    divisor += matrix[i, j];

            if (divisor == 0) divisor = 1;

            Point anchor = new Point(1, 1);

            pictureBox.Image = convolutionFiler(matrixSize, matrixSize, matrix, anchor, divisor, offset);
        }

        private void edgeDetection_Click(object sender, EventArgs e)
        {
            int[,] matrix =  {
                        {  0,-1,0 },
                        {  0,1,0 },
                        {  0,0,0 }
                     };

            int divisor = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    divisor += matrix[i, j];

            if (divisor == 0) divisor = 1;

            Point anchor = new Point(1, 1);

            pictureBox.Image = convolutionFiler(matrixSize, matrixSize, matrix, anchor, divisor, offset2);
        }

        private void emboss_Click(object sender, EventArgs e)
        {
            int[,] matrix =  {
                        { -1, 0, 1 },
                        { -1, 1, 1 },
                        { -1, 0, 1 }
                     };

            int divisor = 0;
            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    divisor += matrix[i, j];

            if (divisor == 0) divisor = 1;

            Point anchor = new Point(1, 1);

            pictureBox.Image = convolutionFiler(matrixSize, matrixSize, matrix, anchor, divisor, offset);
        }

        static int check(int c)
        {
            if (c > 255) c = 255;
            else
                if (c < 0) c = 0;

            return c;
        }

        static public Bitmap convolutionFiler(int matrixWidth, int matrixHeight, int[,] matrix, Point anchor, int div, int off)
        {
            Color color;
            int r, g, b, rr = 0, gg = 0, bb = 0, mx, my;

            Bitmap tmp = (Bitmap)originalPhoto.Clone();

            for (int x = 0; x < originalPhoto.Width; x++)
            {
                for (int y = 0; y < originalPhoto.Height; y++)
                {
                    rr = 0;
                    gg = 0;
                    bb = 0;
                    for (int j = 0; j < matrixHeight; j++)
                    {
                        for (int i = 0; i < matrixWidth; i++)
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
                           

                            color = originalPhoto.GetPixel(mx, my);
                            rr += matrix[i,j] * color.R;
                            gg += matrix[i,j] * color.G;
                            bb += matrix[i,j] * color.B;
                        }
                    }

                    r = off + rr / div;
                    g = off + gg / div;
                    b = off + bb / div;

                    r = check(r);
                    g = check(g);
                    b = check(b);

                    tmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return tmp;
        }

        private void editor_Click(object sender, EventArgs e)
        {
            form = new Form2();
            if(form.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = form.TMP;
            }
        }
    }
}
