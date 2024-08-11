using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIVE1970
{
    public partial class Form1 : Form
    {
        // старт, продолжить - 2
        // стоп, сброс - 1
        // 
        int mode, cells_x, cells_y, cells_size = 16, speed = 1;
        bool isMouse;
        Life universe;
        Graphics graphics;
        Bitmap buf;
        Pen pen;
        SolidBrush brush_lime, brush_black;
        Rectangle rect, rect_bl, rect_gr;
        Point []point;
        public Form1()
        {
            InitializeComponent();

            universe = new Life();
            cells_size = 16;
            cells_x = 50;
            cells_y = 50;

            isMouse = false;
            mode = 1;

            buf = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(buf);
            rect = new Rectangle(0, 0, cells_size-1, cells_size-1); rect_bl = new Rectangle(0, 0, cells_size, cells_size);
            pen = new Pen(Color.DarkGray, 0);
            brush_lime = new SolidBrush(Color.Lime);
            brush_black = new SolidBrush(Color.Black);
            point = new Point[2];
            graphics.Clear(Color.Black);
            pictureBox1.Image = buf;

            universe.world.universe[10, 11].state = state_of_cell.ALIVE;
            universe.world.universe[10, 12].state = state_of_cell.ALIVE;
            universe.world.universe[10, 13].state = state_of_cell.ALIVE;
        }



        private void выходToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /*e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle bbox1 = new Rectangle(30, 40, 50, 50);
            e.Graphics.DrawEllipse(new Pen(Color.Purple), bbox1);*/
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            SolidBrush brush;
            if (mode == 2) return;
            if (e.Button == MouseButtons.Left) brush = brush_lime;
            else brush = brush_black;
            point[0].X = (int) System.Math.Round(e.X / (double) cells_size);
            point[0].Y = (int) System.Math.Round(e.Y / (double) cells_size);
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    universe.world.universe[point[0].X, point[0].Y].state = state_of_cell.ALIVE;
                }
                else { universe.world.universe[point[0].X, point[0].Y].state = state_of_cell.DEAD; }
            }
            catch (System.Exception exp) {
                return;
            }
            point[0].X = point[0].X * 16;
            point[0].Y = 1+point[0].Y * 16;
            rect.X = point[0].X;
            rect.Y = point[0].Y;
            graphics.FillRectangle(brush, rect);
            pictureBox1.Image = buf;
        }

        private void toolStripMenuItem4_Click(object sender, System.EventArgs e)
        {
            speed = 20;
        }

        private void toolStripMenuItem5_Click(object sender, System.EventArgs e)
        {
            speed = 19;
        }

        private void toolStripMenuItem6_Click(object sender, System.EventArgs e)
        {
            speed = 18;
        }

        private void toolStripMenuItem7_Click(object sender, System.EventArgs e)
        {
            speed = 17;
        }

        private void toolStripMenuItem8_Click(object sender, System.EventArgs e)
        {
            speed = 16;
        }

        private void toolStripMenuItem9_Click(object sender, System.EventArgs e)
        {
            speed = 15;
        }

        private void toolStripMenuItem10_Click(object sender, System.EventArgs e)
        {
            speed = 14;
        }

        private void toolStripMenuItem11_Click(object sender, System.EventArgs e)
        {
            speed = 13;
        }

        private void toolStripMenuItem12_Click(object sender, System.EventArgs e)
        {
            speed = 12;
        }

        private void toolStripMenuItem13_Click(object sender, System.EventArgs e)
        {
            speed = 11;
        }

        private void toolStripMenuItem14_Click(object sender, System.EventArgs e)
        {
            speed = 5;
        }

        private void toolStripMenuItem15_Click(object sender, System.EventArgs e)
        {
            speed = 1;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            SolidBrush brush;
            if (!isMouse || mode == 2) return;
            if (e.Button == MouseButtons.Left) brush = brush_lime;
            else brush = brush_black;

            point[0].X = (int)System.Math.Round(e.X / (double)cells_size);
            point[0].Y = (int)System.Math.Round(e.Y / (double)cells_size);
            try {
                if (e.Button == MouseButtons.Left)
                {
                    universe.world.universe[point[0].X, point[0].Y].state = state_of_cell.ALIVE;
                }
                else { universe.world.universe[point[0].X, point[0].Y].state = state_of_cell.DEAD; }
            } catch (System.Exception) {
                return;
            }
            
            point[0].X =  point[0].X * 16;
            point[0].Y =  point[0].Y * 16;
            rect.X = point[0].X;
            rect.Y = 1+point[0].Y;
            graphics.FillRectangle(brush, rect);
            pictureBox1.Image = buf;
        }

        private void сбросToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            mode = 2;
            universe.clear_space();
            graphics.Clear(Color.Black);
            pictureBox1.Image = buf;
            mode = 1;
        }

        private void toolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            mode = 2;
        }

        private async void toolStripMenuItem2_Click(object sender, System.EventArgs e)
        {
            mode = 1;
            go();
        }

        private async void стартToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            mode = 1;
            go();
        }
        private void drawUniverse(Life lf) {
            SolidBrush brush;
            rect.X = 1;
            rect.Y = 1;
            for (int i = 0; i < cells_y; i++) {
                for (int j = 0; j < cells_x; j++)
                {
                    if (lf.world.universe[j, i].state == state_of_cell.ALIVE)
                    {
                        graphics.FillRectangle(brush_lime, rect);
                    }
                    else {
                        graphics.FillRectangle(brush_black, rect);
                    }
                    rect.X = rect.X + cells_size;
                }
                rect.X = 0;
                rect.Y = rect.Y + 16;
            }
            pictureBox1.Image = buf;
        }
        private async void go() {
            while (true) {
                if (mode == 2) return;
                universe.Gen();
                drawUniverse(universe);
                await Task.Delay(10 * speed);
            }
        }
    }
}
