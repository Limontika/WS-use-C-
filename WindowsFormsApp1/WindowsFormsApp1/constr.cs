using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class constr : Form
    {

        public constr()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            trackBar2.Value = 180;

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel) return;
            Bitmap img = new Bitmap(openFileDialog1.FileName.ToString());
            pictureBox1.Image = img;

            textBox1.Text = pictureBox1.Width.ToString();
            textBox2.Text = pictureBox1.Height.ToString();



        }

        private void constr_Load(object sender, EventArgs e)
        {
            trackBar2.Value = 180;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                var temp = new Bitmap(pictureBox1.Image);
                pictureBox1.Image.Dispose();

                temp.RotateFlip(RotateFlipType.Rotate90FlipX);

                pictureBox1.Image = temp;

                temp = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Изделие сохранено и направленно на утверждение",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);

            this.Close();
        }

        Control draggedPiece = null;
        bool resizing = false;
        private Point startDraggingPoint;
        private Size startSize;
        Rectangle rectProposedSize = Rectangle.Empty;

        int resizingMargin = 5;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draggedPiece = sender as Control;

            if ((e.X <= resizingMargin) || (e.X >= draggedPiece.Width - resizingMargin) ||
                (e.Y <= resizingMargin) || (e.Y >= draggedPiece.Height - resizingMargin))
            {
                resizing = true;

                this.Cursor = Cursors.SizeNWSE;

                this.startSize = new Size(e.X, e.Y);
                Point pt = this.PointToScreen(draggedPiece.Location);
                rectProposedSize = new Rectangle(pt, startSize);

                ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
            }
            else
            {
                resizing = false;
                this.Cursor = Cursors.SizeAll;
            }

            this.startDraggingPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPiece != null)
            {
                if (resizing)
                {
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                    rectProposedSize.Width = e.X - this.startDraggingPoint.X + this.startSize.Width;
                    rectProposedSize.Height = e.Y - this.startDraggingPoint.Y + this.startSize.Height;
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                else
                {
                    Point pt;
                    if (draggedPiece == sender)
                        pt = new Point(e.X, e.Y);
                    else
                        pt = draggedPiece.PointToClient((sender as Control).PointToScreen(new Point(e.X, e.Y)));

                    draggedPiece.Left += pt.X - this.startDraggingPoint.X;
                    draggedPiece.Top += pt.Y - this.startDraggingPoint.Y;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (resizing)
            {
                if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                {
                    ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                    textBox1.Text = rectProposedSize.Width.ToString();
                    textBox2.Text = rectProposedSize.Height.ToString();
                }
                if (rectProposedSize.Width > 10 && rectProposedSize.Height > 10)
                {
                    this.draggedPiece.Size = rectProposedSize.Size;
                    textBox1.Text = rectProposedSize.Width.ToString();
                    textBox2.Text = rectProposedSize.Height.ToString();
                }
                else
                {
                    this.draggedPiece.Size = new Size((int)Math.Max(10, rectProposedSize.Width), Math.Max(10, rectProposedSize.Height));
                    textBox1.Text = rectProposedSize.Width.ToString();
                    textBox2.Text = rectProposedSize.Height.ToString();
                }
            }

            this.draggedPiece = null;
            this.startDraggingPoint = Point.Empty;
            this.Cursor = Cursors.Default;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            var temp = new Bitmap(pictureBox1.Image);
            pictureBox1.Image.Dispose();

            temp.RotateFlip(RotateFlipType.Rotate270FlipX);

            pictureBox1.Image = temp;

            temp = null;
        }
    }
}
