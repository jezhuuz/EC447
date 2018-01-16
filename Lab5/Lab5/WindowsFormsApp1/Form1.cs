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
    public partial class Form1 : Form
    {
      
        //the amount of palindromes to be displayed in the list box
        //based off of textBox2 input, which asks for Enter count (1-100)
        private int countPalindromes;

        public Form1()
        {
            InitializeComponent();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int inputPalindrome = Int32.Parse(textBox1.Text);
        }
                
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            int input;
            int check1;
            int check2;

            if (Int32.TryParse(textBox1.Text, out check1) == true && Int32.TryParse(textBox2.Text, out check2) == true)
            {
                label1.Text = "";
                input = Convert.ToInt32(textBox1.Text);
                countPalindromes = Convert.ToInt32(textBox2.Text);

                if ((input >= 0) && (input <= 1000000000) && (countPalindromes >= 1) && (countPalindromes <= 100))
                {
                    for (int j = 0; j < countPalindromes; j++)
                    {
                        string inputOriginal = input.ToString();
                        char[] inputarray = inputOriginal.ToCharArray();
                        Array.Reverse(inputarray);
                        string inputReversed = new string(inputarray);

                        //make palindromes starting from input integer in textBox1 and add them to listBox1
                        //test for a palindrome by converting the number to a string, reversing the string and comparing to the original
                        if (inputOriginal == inputReversed)
                        {
                            //if it is a palindrome, add it to the list box
                            listBox1.Items.Add(inputOriginal);
                        }
                        else
                        {
                            j--;
                        }

                        input++;

                    }
                }
                else
                {
                    label1.Text = "Please enter a positive integer within range.";
                }
            }
            else
            {
                label1.Text = "Please enter a positive integer within range.";
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Font myFont = new Font("Arial MT", 24, FontStyle.Bold);
            e.Graphics.DrawString("Find Numeric Palindromes", myFont, Brushes.Black, 200, 30);
            myFont.Dispose();
        }
    }
}
