using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShapesApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Form1());
        }
    }

    public class Form1 : Form
    {
        private PictureBox pictureBox1;
        private Button btnGenerate;
        private List<Figure> figures = new List<Figure>();
        private Random random = new Random();

        public Form1()
        {
            this.Text = "Фігури";
            this.Size = new Size(650, 500);

            pictureBox1 = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(600, 400),
                BackColor = Color.White
            };
            this.Controls.Add(pictureBox1);

            btnGenerate = new Button
            {
                Text = "Згенерувати фігури",
                Location = new Point(10, 420),
                Size = new Size(150, 30)
            };
            btnGenerate.Click += BtnGenerate_Click;
            this.Controls.Add(btnGenerate);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            figures.Clear();
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);

            for (int i = 0; i < 10; i++)
            {
                int x = random.Next(0, pictureBox1.Width - 100);
                int y = random.Next(0, pictureBox1.Height - 100);
                Point position = new Point(x, y);
                Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                int type = random.Next(4);

                Figure fig;
                switch (type)
                {
                    case 0:
                        fig = new Square(color, position, random.Next(30, 70));
                        break;
                    case 1:
                        fig = new RectangleFigure(color, position, random.Next(40, 90), random.Next(30, 60));
                        break;
                    case 2:
                        fig = new Triangle(color, position, random.Next(40, 70));
                        break;
                    case 3:
                        fig = new Circle(color, position, random.Next(20, 40));
                        break;
                    default:
                        continue;
                }

                fig.Draw(g);
                figures.Add(fig);
            }

            pictureBox1.Refresh();
        }
    }

    public abstract class Figure
    {
        public Color Color { get; set; }
        public Point Position { get; set; }

        public Figure(Color color, Point position)
        {
            Color = color;
            Position = position;
        }

        public abstract void Draw(Graphics g);
        public abstract void Move(int dx, int dy);
    }

    public class Square : Figure
    {
        public int Side { get; set; }

        public Square(Color color, Point position, int side)
            : base(color, position)
        {
            Side = side;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, Position.X, Position.Y, Side, Side);
            }
        }

        public override void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }

    public class RectangleFigure : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public RectangleFigure(Color color, Point position, int width, int height)
            : base(color, position)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, Position.X, Position.Y, Width, Height);
            }
        }

        public override void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }

    public class Circle : Figure
    {
        public int Radius { get; set; }

        public Circle(Color color, Point position, int radius)
            : base(color, position)
        {
            Radius = radius;
        }

        public override void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, Position.X, Position.Y, Radius * 2, Radius * 2);
            }
        }

        public override void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }

    public class Triangle : Figure
    {
        public int Size { get; set; }

        public Triangle(Color color, Point position, int size)
            : base(color, position)
        {
            Size = size;
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[]
            {
                new Point(Position.X, Position.Y + Size),
                new Point(Position.X + Size / 2, Position.Y),
                new Point(Position.X + Size, Position.Y + Size)
            };

            using (Brush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, points);
            }
        }

        public override void Move(int dx, int dy)
        {
            Position = new Point(Position.X + dx, Position.Y + dy);
        }
    }
}
