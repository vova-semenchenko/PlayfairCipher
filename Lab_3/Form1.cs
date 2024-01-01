using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class Form1 : Form
    {
        private string text;
        private string key;
        PlayfairCipher playfairCipher;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            typeSelector.SelectedIndex = 0; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = richTextBox.Text;

            if (typeSelector.SelectedIndex == 0)
            {       
                resultTextBox.Text = playfairCipher.Encrypt(text);
            }

            if(typeSelector.SelectedIndex == 1)
            {
                resultTextBox.Text = playfairCipher.Decrypt(text);
            }
        }

        private void EnableButton()
        {
            if (!string.IsNullOrEmpty(keyTextBox.Text) && !string.IsNullOrEmpty(richTextBox.Text))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void keyTextBox_TextChanged(object sender, EventArgs e)
        {
            key = keyTextBox.Text;
            playfairCipher = new PlayfairCipher(key);
            matrixTextBox.Text = playfairCipher.GetStringMatrix();
            EnableButton();
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableButton();
        }

        private void typeSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeSelector.SelectedIndex == 0)
            {
                button1.Text = "Encrypt";
                richTextBox.Text = string.Empty;
                resultTextBox.Text = string.Empty;
            }

            if (typeSelector.SelectedIndex == 1)
            {
                button1.Text = "Decrypt";
                richTextBox.Text = resultTextBox.Text;
                resultTextBox.Text = string.Empty;
            }
        }
    }
}
