using System;
using System.Configuration;
using System.Windows.Forms;

namespace AccountTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string[] args)
        {
            InitializeComponent();
            try
            {
                textBox1.Text = args[3].ToString();
                textBox2.Text = args[4].ToString();
                if (GetValue("AutoCopyPwd", "false").ToLower().Equals("true"))
                {
                    button2_Click(null, null);
                }
            } catch { }
        }

        public static string GetValue(string key, string def)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = string.Format("{0}\\Config.xml", System.Environment.CurrentDirectory);
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            return config.AppSettings.Settings[key] == null ? def : config.AppSettings.Settings[key].Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, textBox2.Text);
        }
    }
}
