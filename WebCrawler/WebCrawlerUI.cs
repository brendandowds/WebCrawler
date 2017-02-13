using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCrawler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisableControls();

            lstResults.Items.Clear();
            foreach (string s in WebCrawler.Crawl(txtUrl.Text))
            {
                lstResults.Items.Add(s);
            }

            EnableControls();
            Cursor = Cursors.Default;
        }

        private void EnableControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Enabled = true;
        }


        private void DisableControls()
        {
            foreach (Control ctrl in this.Controls)
                ctrl.Enabled = false;
        }
    }
}
