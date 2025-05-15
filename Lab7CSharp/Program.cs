using System;
using System.Windows.Forms;

namespace ComboBoxEditor
{
    public class MainForm : Form
    {
        private ComboBox comboBox;
        private TextBox inputBox;
        private Button addButton;
        private Button removeButton;

        public MainForm()
        {
            // Заголовок форми
            this.Text = "Редактор списку";
            this.Width = 350;
            this.Height = 200;

            // Випадаючий список
            comboBox = new ComboBox();
            comboBox.Location = new System.Drawing.Point(20, 20);
            comboBox.Width = 200;
            this.Controls.Add(comboBox);

            // Поле введення
            inputBox = new TextBox();
            inputBox.Location = new System.Drawing.Point(20, 60);
            inputBox.Width = 200;
            this.Controls.Add(inputBox);

            // Кнопка "Додати"
            addButton = new Button();
            addButton.Text = "Додати";
            addButton.Location = new System.Drawing.Point(230, 20);
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            // Кнопка "Видалити"
            removeButton = new Button();
            removeButton.Text = "Видалити";
            removeButton.Location = new System.Drawing.Point(230, 60);
            removeButton.Click += RemoveButton_Click;
            this.Controls.Add(removeButton);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string item = inputBox.Text.Trim();
            if (!string.IsNullOrEmpty(item))
            {
                if (!comboBox.Items.Contains(item))
                {
                    comboBox.Items.Add(item);
                    inputBox.Clear();
                }
                else
                {
                    MessageBox.Show("Елемент уже є у списку.");
                }
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            string item = inputBox.Text.Trim();
            if (comboBox.Items.Contains(item))
            {
                comboBox.Items.Remove(item);
                inputBox.Clear();
            }
            else
            {
                MessageBox.Show("Елемента не знайдено.");
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
