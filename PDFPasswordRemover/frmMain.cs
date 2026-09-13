using System.Windows.Forms;

namespace PDFPasswordRemover;

public partial class frmMain : Form
{
    public frmMain()
    {
        InitializeComponent();
        Load += frmMain_Load;
    }

    private void frmMain_Load(object? sender, EventArgs e)
    {
        txtDirectory.Text = Properties.Settings.Default.LastDirectory;
        txtPassword.Text = LocalEnv.Get("DEFAULT_PASSWORD") ?? string.Empty;
    }

    private void btnSelect_Click(object? sender, EventArgs e)
    {
        DialogResult result = fbdMain.ShowDialog();
        if (result == DialogResult.OK)
        {
            txtDirectory.Text = fbdMain.SelectedPath;
            Properties.Settings.Default.LastDirectory = fbdMain.SelectedPath;
            Properties.Settings.Default.Save();
        }
    }

    private async void btnProcess_Click(object? sender, EventArgs e)
    {
        string directory = txtDirectory.Text;
        string password = txtPassword.Text;

        btnProcess.Enabled = false;
        btnSelect.Enabled = false;
        try
        {
            await Task.Run(() =>
            {
                string[] oFiles = Directory.GetFiles(directory, "*.pdf", SearchOption.AllDirectories);

                foreach (string oFile in oFiles)
                {
                    modiTextSharp.ProcessFile(oFile, password);
                }
            });
        }
        finally
        {
            btnProcess.Enabled = true;
            btnSelect.Enabled = true;
        }
    }
}
