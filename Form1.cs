﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filters
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создаём диалог для открытия файла
            OpenFileDialog dialog = new OpenFileDialog();

            // фильтр для удобства открытия только изображений (других файлов не видно)
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All files (*.*) | *.*";

            // условие для проверки "выбрал ли пользователь файл?"
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // в случ. выполнения инициализируем переменную image выбранным изображением
                image = new Bitmap(dialog.FileName);

                // после загрузки картинки в программу нужно визуализировать её на форме. Для этого:
                // 1) image присвоим свойству pictureBox.Image
                pictureBox1.Image = image;
                // 2) обновим pictureBox
                pictureBox1.Refresh();
                pictureBox2.Image = null;
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Отмена_Click(object sender, EventArgs e)
        {
            // остановка выполнения фильтра
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox2.Image = image;
                pictureBox2.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void размытиеГаусаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрШаараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ShalyaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void черноБелоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Sepia();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new Brithness();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharpnessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GreyWorld();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PryttFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void линейноеРастяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new LinearStreching();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйФильтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void фильтрМаксимумToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MaxFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void очиститьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBox2.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBox2.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBox2.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }

        }

        private void тестToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new CircleFilter();

            backgroundWorker1.RunWorkerAsync(filter);
        }
    }

    
}
