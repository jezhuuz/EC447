using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class DialogBox : Form
    {
        private bool outline;
        private bool fill;

        private int pencolor;
        private int fillcolor;
        private int penwidth;
        

        public DialogBox()
        {

            InitializeComponent();

            button1.Text = "OK";
            button2.Text = "Cancel";
            label1.Text = "Pen Color";
            label2.Text = "Fill Color";
            label3.Text = "Pen Width";
            checkBox1.Text = "Fill";
            checkBox2.Text = "Outline";

          /*  listBox1.Items.Add("Black");
            listBox1.Items.Add("Red");
            listBox1.Items.Add("Blue");
            listBox1.Items.Add("Green");

            listBox2.Items.Add("White");
            listBox2.Items.Add("Black");
            listBox2.Items.Add("Red");
            listBox2.Items.Add("Blue");
            listBox2.Items.Add("Green");

            listBox3.Items.Add("1");
            listBox3.Items.Add("2");
            listBox3.Items.Add("3");
            listBox3.Items.Add("4");
            listBox3.Items.Add("5");
            listBox3.Items.Add("6");
            listBox3.Items.Add("7");
            listBox3.Items.Add("8");
            listBox3.Items.Add("9");
            listBox3.Items.Add("10");*/

            this.checkBox2.Checked = true;

            this.listBox1.SelectedIndex = 0;
            this.listBox2.SelectedIndex = 0;
            this.listBox3.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Pen Color";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        //ok button
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        //cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.listBox1.SelectedIndex = this.pencolor;
            this.listBox2.SelectedIndex = this.fillcolor;
            this.listBox3.SelectedIndex = this.penwidth;

            this.checkBox1.Checked = this.fill;
            this.checkBox2.Checked = this.outline;

            this.DialogResult = DialogResult.Cancel;
        }

        protected override void OnShown(EventArgs e)
        {
            this.pencolor = this.listBox1.SelectedIndex;
            this.fillcolor = this.listBox2.SelectedIndex;
            this.penwidth = this.listBox3.SelectedIndex;

            this.fill = this.checkBox1.Checked;
            this.outline = this.checkBox2.Checked;
        }

        //listbox for pen color
        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        //listbox for fill color
        public void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //listbox for pen width
        public void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
