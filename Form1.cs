using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstWinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            convertCurrency();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            convertCurrency();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            convertCurrency();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedItem = "IDR";
            this.comboBox2.SelectedItem = "IDR";
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            convertCurrency();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void convertCurrency()
        {
            double inputNum = (double)(this.numericUpDown1.Value);
            string in_currency = this.comboBox1.SelectedItem.ToString();
            string out_currency = this.comboBox2.SelectedItem.ToString();

            double pembagi = 0.0;
            if (out_currency == "USD") pembagi = 0.000069;
            else if (out_currency == "IDR") pembagi = 1;
            else if (out_currency == "SGD") pembagi = 0.000093;
            else if (out_currency == "GBP") pembagi = 0.000050;
            else if (out_currency == "EUR") pembagi = 5.8 / 10000000;

            double pengali = 0.0;

            if (in_currency == "USD") pengali = 14391.45;
            else if (in_currency == "IDR") pengali = 1;
            else if (in_currency == "SGD") pengali = 10743.06;
            else if (in_currency == "GBP") pengali = 19949.14;
            else if (in_currency == "EUR") pengali = 17176.45;

            double outnum = inputNum * pengali / pembagi;

            if (in_currency == out_currency) outnum = inputNum;

            this.label2.Text = out_currency + " " + Math.Round(outnum, 6);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            convertCurrency();
        }
    }
}
