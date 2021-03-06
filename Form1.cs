using System;
using System.Globalization;
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
            init();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            convertCurrency();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
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
        private Dictionary<string, float> dict = new Dictionary<string, float>();
        private void init()
        {
            dict["usd"] = GetCurrencyRateInEuro("usd");
            dict["idr"] = GetCurrencyRateInEuro("idr");
            dict["sgd"] = GetCurrencyRateInEuro("sgd");
            dict["gbp"] = GetCurrencyRateInEuro("gbp");
            dict["eur"] = GetCurrencyRateInEuro("eur");
            this.label2.Text = "READY";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        public static float GetCurrencyRateInEuro(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("Invalid Argument! currency parameter cannot be empty!");
            if (currency.ToLower() == "eur") return 1;

            try
            {
                // Create valid RSS url to european central bank
                string rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.ToLower() + ".html");

                // Create & Load New Xml Document
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(rssUrl);

                // Create XmlNamespaceManager for handling XML namespaces.
                System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
                nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

                // Get list of daily currency exchange rate between selected "currency" and the EURO
                System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr);

                // Loop Through all XMLNODES with daily exchange rates
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    // Create a CultureInfo, this is because EU and USA use different sepperators in float (, or .)
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";

                    try
                    {
                        // Get currency exchange rate with EURO from XMLNODE
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any,
                            ci);

                        return exchangeRate;
                    }
                    catch { }
                }
                return 0;
            }
            catch { return 0;}
        }


        private void convertCurrency()
        {
            double inputNum = (double)(this.numericUpDown1.Value);
            string in_currency = this.comboBox1.SelectedItem.ToString().ToLower();
            string out_currency = this.comboBox2.SelectedItem.ToString().ToLower();
            double outnum = inputNum;

            if (in_currency == out_currency) outnum = inputNum;
            // Convert Euro to Other Currency
            else if (in_currency == "eur") outnum = inputNum * dict[out_currency];
            // Convert Other Currency to Euro
            else if (out_currency == "eur") outnum = inputNum / dict[in_currency];
            else
            {
                // Get the exchange rate of both currencies in euro
                float toRate = dict[out_currency];
                float fromRate = dict[in_currency];

                // Calculate exchange rate From A to B
                outnum = (inputNum * toRate) / fromRate;
            }

            this.label2.Text = out_currency.ToUpper() + " " + Math.Round(outnum, 6);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            convertCurrency();
        }
    }
}
