using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab6
{
    public partial class Form1 : Form
    {

        private DialogBox settingspopup = new DialogBox();

        //1. Use an ArrayList to save each graphic object for painting
        private ArrayList gobjs = new ArrayList();
        

        //keep a state variable to determine whether a mouse click is the first or second
        //clickstate = true when it is the first mouse click
        //clickstate = false when it is not the first time the mouse has been clicked
        private bool clickstate = true;

        //object is added on the second click
        private Point fclick;
        private Point sclick;
        

        //private ToolStripMenuItem exitToolStripMenuItem;

        private void label1_Click(object sender, EventArgs e)
        {
           
        }


        public Form1()
        {
            InitializeComponent();
            label1.Text = "Draw";
        }

        //Undo - deletes last graphics object in list
        //***be sure that pogram doesnt crash if you undo an empty list***
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure if the list of graphic objects is not empty
            if(this.gobjs.Count > 0)
            {
                //delete last graphcis object in list
                this.gobjs.RemoveAt(this.gobjs.Count - 1);
            }

            this.panel2.Invalidate();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.gobjs.Clear();
            this.panel2.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        //panel with Draw options and Settings
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            radioButton1.Text = "Line";
            radioButton2.Text = "Rectangle";
            radioButton3.Text = "Ellipse";
            button1.Text = "Settings";

            panel1.Width = this.Width;
            panel1.Height = this.Height;
            panel1.BackColor = Color.Silver;

            Graphics g = e.Graphics;
            Rectangle r = new Rectangle(30, 35, 220, 160);
            Pen p = new Pen(Brushes.LightGray, 1);
            g.DrawRectangle(p, r);
            
        }

        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.Width = this.Width;
            panel2.Height = this.Height;
            panel2.BackColor = Color.White;

            Graphics g = e.Graphics;

            foreach(AllShapes sh in gobjs)
            {
                sh.Draw(g);
            }

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            //if the click state flag is true
            if (this.clickstate)
            {
                //save the location of this first mouse click
                this.fclick = e.Location;
                //since this is not the first time the mouse has been clicked now
                //set the click state flag to false now
                this.clickstate = false;
                //return so that the function restarts
                return;
            }
            else
            { 
                //this is reached when the mouse has already been clicked already
                this.sclick = e.Location;
                //reset the two mouse click count to true
                this.clickstate = true;

                //default drawing settings
                //pen for drawing
                Pen pen = (Pen) null;
                //fill color
                Brush fillBrush = (Brush) null;
                //pen color
                Brush penBrush = (Brush) null;

                //switch pen brush colors according to the index number of the listBox1 in DialogBox.cs which is for Pen Color
                switch (this.settingspopup.listBox1.SelectedIndex)
                {
                    case 0:
                        penBrush = Brushes.Black;
                        break;

                    case 1:
                        penBrush = Brushes.Red;
                        break;

                    case 2:
                        penBrush = Brushes.Blue;
                        break;

                    case 3:
                        penBrush = Brushes.Green;
                        break;
                }

                //if the fill button has been pressed on the Settings popup box
                if (this.settingspopup.checkBox1.Checked)
                {
                    //switch fill brush colors according to the index number of the listBox2 in DialogBox.cs which is for Fill Color
                    switch (this.settingspopup.listBox2.SelectedIndex)
                    {
                        case 0:
                            fillBrush = Brushes.White;
                            break;

                        case 1:
                            fillBrush = Brushes.Black;
                            break;

                        case 2:
                            fillBrush = Brushes.Red;
                            break;

                        case 3:
                            fillBrush = Brushes.Blue;
                            break;

                        case 4:
                            fillBrush = Brushes.Green;
                            break;
                    }
                }

                //if the line radio button is checked or if the outline check box in Settings is checked
                if (this.radioButton1.Checked || this.settingspopup.checkBox2.Checked)
                {
                    //change the pen width to the index of the listBox3 in DialogBox.cs which is for Pen Width
                    int newwidth = int.Parse((string)this.settingspopup.listBox3.SelectedItem);
                    pen = new Pen(penBrush, newwidth);
                }

                //if radioButton1.Checked == true (Draw Line)
                if (this.radioButton1.Checked)
                { 
                    this.gobjs.Add((object)new MyLine(pen, this.fclick, this.sclick));
                }

                //if the fill color or pen color is not null
                //draw the rectangle and ellipses
                if (fillBrush != null || pen != null)
                {
                    //if radioButton2.Checked == true (Draw Rectangle)
                    if (this.radioButton2.Checked)
                    {
                        this.gobjs.Add(new MyRectangle(pen, this.fclick, this.sclick, fillBrush));
                    }

                    //if radioButton3.Checked == true (Draw Ellipse)
                    if (this.radioButton3.Checked)
                    {
                        this.gobjs.Add(new MyEllipse(pen, this.fclick, this.sclick, fillBrush));
                    }
                }
                //else show an error mesage since fillBrush == null and pen == null means no fill or outline 
                else
                {
                    MessageBox.Show("Fill and or outline must be checked.");
                }

                   this.panel2.Invalidate();
        }
    }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.settingspopup.ShowDialog();
        }


        //Base Class for graphics elements
        //one Graphics object argument
        //virtual Draw method, overriden in each derived class
        public class AllShapes
        {
            public virtual void Draw(Graphics g)
            {

            }
        }

        public class MyLine : AllShapes
        {
            private Point firstpoint;
            private Point endpoint;
            private Pen pen;
            private Brush brush; 

            public MyLine(Pen pen, Point firstpoint, Point endpoint)
            {
                this.pen = pen;
                this.firstpoint = firstpoint;
                this.endpoint = endpoint;
            }

            public override void Draw(Graphics g)
            {
                g.DrawLine(this.pen, this.firstpoint, this.endpoint);
            }

        }

        public class MyRectangle : AllShapes
        {
            private Point firstpoint;
            private Point endpoint;
            private Pen pen;
            private Brush brush;

            public MyRectangle(Pen pen, Point firstpoint, Point endpoint, Brush brush)
            {
                this.pen = pen;
                this.firstpoint = firstpoint;
                this.endpoint = endpoint;
                this.brush = brush;
            }

            public override void Draw(Graphics g)
            {
                int rheight = Math.Abs(this.endpoint.Y - this.firstpoint.Y);
                int rwidth = Math.Abs(this.endpoint.X - this.firstpoint.X);
                int r_y = Math.Min(this.firstpoint.Y, this.endpoint.Y);
                int r_x = Math.Min(this.firstpoint.X, this.endpoint.X);

                //make sure pen is not null and that the rectangle will have an outline
                //so there won't be an error for DrawRectangle
                if (this.pen != null)
                {
                    g.DrawRectangle(this.pen, r_x, r_y, rwidth, rheight);
                }

                //if brush color is not null that means there is a fill color
                //so call FillRectangle
                if (this.brush != null)
                {
                    g.FillRectangle(this.brush, r_x, r_y, rwidth, rheight);
                }
            }
        }

        public class MyEllipse : AllShapes
        {
            private Point firstpoint;
            private Point endpoint;
            private Pen pen;
            private Brush brush;

            public MyEllipse(Pen pen, Point firstpoint, Point endpoint, Brush brush)
            {
                this.pen = pen;
                this.firstpoint = firstpoint;
                this.endpoint = endpoint;
                this.brush = brush;
            }

            public override void Draw(Graphics g)
            {
                int eheight = Math.Abs(this.endpoint.Y - this.firstpoint.Y);
                int ewidth = Math.Abs(this.endpoint.X - this.firstpoint.X);
                int e_y = Math.Min(this.firstpoint.Y, this.endpoint.Y);
                int e_x = Math.Min(this.firstpoint.X, this.endpoint.X);

                //make sure pen is not null and that the ellipse will have an outline
                //so there won't be an error for DrawEllipse
                if (this.pen != null)
                {
                    g.DrawEllipse(this.pen, e_x, e_y, ewidth, eheight);
                }

                //if brush color is not null that means there is a fill color
                //so call FillEllipse
                if (this.brush != null)
                {
                    g.FillEllipse(this.brush, e_x, e_y, ewidth, eheight);
                }

                

            }

        }


    }
}
