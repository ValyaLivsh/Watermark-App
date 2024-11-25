using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WatermarkApp
{
    public partial class AddWatermarkApp : Form
    {
        private Bitmap originalImage;
        public AddWatermarkApp()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Images|*.jpg; *.png; *.jpeg; *.bmp";
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    originalImage = new Bitmap(openFileDialog.FileName);
                    pictureBox1.Image = originalImage;
                }
            }
        }

        private void btnAddWatermark_Click(object sender, EventArgs e)
        {
            if(originalImage == null)
            {
                MessageBox.Show("Please load image again");
                return;
            }
            using(Graphics g = Graphics.FromImage(originalImage))
            {
                string watermarkText = "Watermark";
                Font font = new Font("Arial", 220, FontStyle.Bold, GraphicsUnit.Pixel);
                Color color = Color.FromArgb(128, 255, 255, 255);
                SolidBrush brush = new SolidBrush(color);
                SizeF textsize = g.MeasureString(watermarkText, font);
                Point position = new Point(originalImage.Width - (int)textsize.Width - 10, originalImage.Height - (int)textsize.Height - 10);
                g.DrawString(watermarkText, font, brush, position);
            }
            pictureBox1.Image = originalImage;
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (originalImage == null) { MessageBox.Show("No image to save"); return; }
            using(SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp";
                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    originalImage.Save(saveFileDialog.FileName);
                    MessageBox.Show("image is saved");
                }
            }
        }
    }
}
