using System;
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
    }
}
