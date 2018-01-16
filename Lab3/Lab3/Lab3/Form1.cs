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

namespace Lab3
{
    public partial class Form1 : Form
    {
        private ArrayList coordinates = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_MouseClick(object sender, MouseEventArgs e) 
        {
            //if left button is pushed
            if (e.Button == MouseButtons.Left) 
            {
                //create and add coordinates to an array list
                CircleClass p = new CircleClass(e.X, e.Y, false); 
                this.coordinates.Add(p);
                this.Invalidate();
            }

            //if right button is pushed
            if (e.Button == MouseButtons.Right) 
            {
                int total = coordinates.Count;
                int index = total - 1;
                for (int i = index; i >= 0; i--)
                {
                    CircleClass p = (CircleClass)coordinates[i];
                    if ((Math.Abs(e.X - p.X) < 10) && (Math.Abs(e.Y - p.Y) < 10))
                    {
                        //if dot is black
                        if (p.rb == false) 
                        {  
                            //turn the black dot red on right click
                            p.rb = true;
                            this.Invalidate();
                        }
                        //if dot is red
                        else if (p.rb == true) 
                        {
                            //remove circles from array list on right click
                            coordinates.RemoveAt(i); 
                            this.Invalidate();
                        }
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            const int WIDTH = 20;
            const int HEIGHT = 20;
            Graphics g = e.Graphics;
            foreach (CircleClass p in this.coordinates)
            {
                if (!p.rb)
                {
                    //make a black circle if not on a circle
                    g.FillEllipse(Brushes.Black, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT);    
                }
                if (p.rb)
                {
                    //fill red if circle is present
                    g.FillEllipse(Brushes.Red, p.X - WIDTH / 2, p.Y - WIDTH / 2, WIDTH, HEIGHT);
                }
            }
        }

        private void clearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //clear if clear button on menu is clicked
            this.coordinates.Clear(); 
            this.Invalidate();
        }
    }

    //circle class for coordinates and color
    public class CircleClass
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool rb { get; set; }
        public CircleClass(int x_coord, int y_coord, bool color)
        {
            X = x_coord;
            Y = y_coord;
            rb = color;
        }
    }
}