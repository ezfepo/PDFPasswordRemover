using System;
using System.IO;
using System.Windows.Forms;

namespace PDFPasswordRemover
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdMain.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDirectory.Text = fbdMain.SelectedPath;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string[] oFiles = Directory.GetFiles(txtDirectory.Text, "*.pdf", SearchOption.AllDirectories);

            foreach (string oFile in oFiles)
            {
                modiTextSharp.ProcessFile(oFile, txtPassword.Text);
            }
        }
    }
}