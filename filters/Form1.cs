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
    public partial class Form : System.Windows.Forms.Form
    {
        Bitmap originalPhoto;

        int bc = 70;

        public Form()
        {
            InitializeComponent();

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            originalPhoto = Properties.Resources.Penguins;
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

                    if (color.R + bc > 255) r = 255;
                    else r = color.R + bc;

                    if (color.G + bc > 255) g = 255;
                    else g = color.G + bc;

                    if (color.B + bc > 255) b = 255;
                    else b = color.B + bc;

                    tmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox.Image = tmp;
        }

        private void original_Click(object sender, EventArgs e)
        {
            pictureBox.Image = originalPhoto;
        }
    }
}
