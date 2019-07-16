using System;
using System.Text;
using System.Windows.Forms;

namespace StringConverter
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            this.cmbType.SelectedIndex = 1;

            this.toolStripStatusLabel4.Text = $"v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }

        private void btnConverter_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbType.SelectedIndex < 1 || string.IsNullOrEmpty(this.tbOrigin.Text) || this.tbOrigin.Text.Trim()=="") return;

                StringBuilder builder = new StringBuilder();

                if (this.cmbType.SelectedIndex == 1) // hex->ascii
                {
                    string[] array = this.tbOrigin.Text.Replace("\r", "0D ").Replace("\n", "0A ").Split(new string[] { "", " " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in array)
                    {
                        string ch = item.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? item : "0x" + item;
                        builder.Append((char)Convert.ToByte(item, 16));
                    }
                }
                else if (this.cmbType.SelectedIndex == 2) // hex<-ascii
                {
                    foreach (char item in this.tbOrigin.Text)
                    {
                        builder.Append($"{((int)item).ToString("X2")} ");
                    }

                    builder.Remove(builder.Length - 1, 1);
                }

                this.tbDestination.Text = builder.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Convert failed,Eception:{ex.Message}","Message box", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
