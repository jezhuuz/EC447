using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab7
{
    public partial class Form1 : Form
    {
        private string keygen;
        private string source;
        private string encrypted;

        public Form1()
        {
            InitializeComponent();
        }

        //open file dialog button
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //this is to test if there is a key generated in the key textbox
        //if blank, show Please enter a key error message
        //if true, make keygen = whatever text is generated in the key textbo
        private bool ValidKey()
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter a key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            keygen = textBox2.Text;
            return true;
        }

        //encrypt button
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidKey())
            {
                 source = textBox1.Text;
                 encrypted = source + ".enc";

                //if the file exists already
                if(File.Exists(encrypted) && MessageBox.Show("Output file exists. Overwrite", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                EncryptThis();
            }
          else
            {
               return;
            }
       }

        //decrypt button
        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidKey())
            {
                
                source = textBox1.Text;

                if(!source.EndsWith(".enc"))
                {
                    MessageBox.Show("Not a .enc file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    encrypted = source.Substring(0, source.Length - 3);

                    if (File.Exists(encrypted) && MessageBox.Show("Output file exists. Overwrite?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }

                    EncryptThis();
                }
            }
          else
          {
               return;
          }

        }
      
        private void EncryptThis()
        {
            FileStream instream = null;
            FileStream outstream = null;

            try
            {
                instream = new FileStream(source, FileMode.Open, FileAccess.Read);
                outstream = new FileStream(encrypted, FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch
            {
                MessageBox.Show("Could not open source or destination file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                if (instream != null)
                {
                    instream.Close();
                }

                if (outstream != null)
                {
                    outstream.Close();
                }

                return;
            }
                /*
                 * The encryption is done by performing an exclusive OR (^ operator) on each byte of the file and a corresponding character in the key cast to a byte. 
                 * Byte 1 of the file is XORed with byte 1 of the key. 
                 * Byte 2 of the file is XORed with byte 2 of the key etc. 
                 * After using the last byte of the key we go back to the first byte of the key wrapping around as many times as needed. 
                 * The character in the Unicode key string is cast to a byte (byte).
               */

                int length = keygen.Length;
                int byte1;
                int index = 0;

                while( (byte1 = instream.ReadByte()) != -1)
                {
                    byte byte2 = (byte) keygen[index];
                    byte keybyte = (byte) (byte1 ^ byte2);
                    outstream.WriteByte(keybyte);
                    index++;

                    if(index == length)
                    {
                        index = 0;
                    }

                }

               outstream.Close();
               instream.Close();

            MessageBox.Show("Operation completed successfully.");

            }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
