using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageInversionApp
{
    public class MainForm : Form
    {
        private PictureBox pictureBox;
        private Bitmap originalImage;
        private RadioButton fullInvertRadio;
        private RadioButton redInvertRadio;
        private RadioButton greenInvertRadio;
        private RadioButton blueInvertRadio;
        private Button openButton;
        private Button saveButton;
        private Button applyButton;

        public MainForm()
        {
            this.Text = "������� ���������� BMP";
            this.Width = 800;
            this.Height = 600;

            pictureBox = new PictureBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(10, 10),
                Size = new Size(500, 500),
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(pictureBox);

            openButton = new Button { Text = "³������", Location = new Point(520, 20), Width = 120 };
            openButton.Click += OpenButton_Click;
            this.Controls.Add(openButton);

            saveButton = new Button { Text = "��������", Location = new Point(520, 60), Width = 120 };
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            applyButton = new Button { Text = "�����������", Location = new Point(520, 100), Width = 120 };
            applyButton.Click += ApplyButton_Click;
            this.Controls.Add(applyButton);

            // ����� ����������
            GroupBox group = new GroupBox
            {
                Text = "����� ������",
                Location = new Point(520, 150),
                Size = new Size(220, 170) // �������� ������
            };

            fullInvertRadio = new RadioButton
            {
                Text = "����� �������",
                Location = new Point(10, 20),
                AutoSize = true
            };
            redInvertRadio = new RadioButton
            {
                Text = "������� ���������",
                Location = new Point(10, 50),
                AutoSize = true
            };
            greenInvertRadio = new RadioButton
            {
                Text = "������� ��������",
                Location = new Point(10, 80),
                AutoSize = true
            };
            blueInvertRadio = new RadioButton
            {
                Text = "������� �������",
                Location = new Point(10, 110),
                AutoSize = true
            };


            group.Controls.Add(fullInvertRadio);
            group.Controls.Add(redInvertRadio);
            group.Controls.Add(greenInvertRadio);
            group.Controls.Add(blueInvertRadio);
            this.Controls.Add(group);
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "BMP Files|*.bmp",
                Title = "������� BMP-����������"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFile.FileName);
                pictureBox.Image = new Bitmap(originalImage);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null) return;

            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "BMP Files|*.bmp",
                Title = "�������� ���������� ��"
            };

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image.Save(saveFile.FileName);
            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            Bitmap bmp = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    Color pixel = originalImage.GetPixel(x, y);
                    int r = pixel.R;
                    int g = pixel.G;
                    int b = pixel.B;

                    if (fullInvertRadio.Checked)
                        bmp.SetPixel(x, y, Color.FromArgb(255 - r, 255 - g, 255 - b));
                    else if (redInvertRadio.Checked)
                        bmp.SetPixel(x, y, Color.FromArgb(255 - r, g, b));
                    else if (greenInvertRadio.Checked)
                        bmp.SetPixel(x, y, Color.FromArgb(r, 255 - g, b));
                    else if (blueInvertRadio.Checked)
                        bmp.SetPixel(x, y, Color.FromArgb(r, g, 255 - b));
                }
            }

            pictureBox.Image = bmp;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
