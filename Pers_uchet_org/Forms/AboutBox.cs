using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    partial class AboutBox : Form
    {
        private Timer timerLogo;

        public AboutBox()
        {
            InitializeComponent();
            Text = String.Format("О {0}", AssemblyTitle);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format("Версия {0}", AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            textBoxDescription.Text = AssemblyDescription;
            timerLogo = new Timer { Interval = 1 };
            timerLogo.Tick += TimerLogoOnTick;
        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            timerLogo.Stop();
            logoPictureBox.Left = 0;
            logoPictureBox.Top = 0;
            logoPictureBox.Click -= logoPictureBox_Click;
        }

        private void logoPictureBox_DoubleClick(object sender, EventArgs e)
        {
            timerLogo.Start();
            logoPictureBox.Click += logoPictureBox_Click;
            logoPictureBox.Click -= logoPictureBox_DoubleClick;
        }

        private void TimerLogoOnTick(object sender, EventArgs eventArgs)
        {
            logoPictureBox.Top += 1;
            if (panelLogo.Height - logoPictureBox.Height < logoPictureBox.Top)
            {
                var timer = sender as Timer;
                if (timer != null) timer.Stop();
            }
        }
    }
}