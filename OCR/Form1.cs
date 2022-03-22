using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.Util;


namespace OCR
{
    public partial class Form1 : Form
    {
        private string path = string.Empty;
        private string lang = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if(res==DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(path);
            }
            else
            {
                MessageBox.Show("Картинка не выбрана", "Необходимо выбрать картинку", MessageBoxButtons.OK);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(path) || String.IsNullOrWhiteSpace(path))
                    throw new Exception("Картинка не выбрана!");
                else if (toolStripComboBox1.SelectedItem == null)
                    throw new Exception("Не выбран язык!");
                else
                {
                    Tesseract tess = new Tesseract(@"F:\D\tessdata", 
                        lang, OcrEngineMode.TesseractLstmCombined);
                    tess.SetImage(new Image<Bgr, byte>(path));
                    tess.Recognize();
                    richTextBox1.Text = tess.GetUTF8Text();

                    tess.Dispose();

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
                lang = "rus";
            else if (toolStripComboBox1.SelectedIndex == 1)
                lang = "eng";
        }
    }
}
