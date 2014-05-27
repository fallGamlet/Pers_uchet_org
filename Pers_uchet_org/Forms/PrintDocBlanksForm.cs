using System;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class PrintDocBlanksForm : Form
    {
        #region Поля

        #endregion

        #region Конструктор

        public PrintDocBlanksForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Методы - обработчики событий

        private void viewRDV1Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Rdv1);
        }

        private void viewRDV21Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Rdv21);
        }

        private void viewRDV22Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Rdv22);
        }

        private void viewRDV3Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Rdv3);
        }

        private void viewSZV1Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Szv1);
        }

        private void viewSZV2Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Szv2);
        }

        private void viewSZV3Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Szv3);
        }

        private void viewADV1Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv1);
        }

        private void viewADV2Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv2);
        }

        private void viewADV3Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv3);
        }

        private void viewADV4Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv4);
        }

        private void viewADV6Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv5);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            int[] counts =
            {
                (int) countADV1Box.Value, (int) XmlData.ReportType.Adv1,
                (int) countADV2Box.Value, (int) XmlData.ReportType.Adv2,
                (int) countADV3Box.Value, (int) XmlData.ReportType.Adv3,
                (int) countADV4Box.Value, (int) XmlData.ReportType.Adv4,
                (int) countADV6Box.Value, (int) XmlData.ReportType.Adv6,
                (int) countSZV1Box.Value, (int) XmlData.ReportType.Szv1,
                (int) countSZV2Box.Value, (int) XmlData.ReportType.Szv2,
                (int) countSZV1Box.Value, (int) XmlData.ReportType.Szv3,
                (int) countRDV1Box.Value, (int) XmlData.ReportType.Rdv1,
                (int) countRDV21Box.Value, (int) XmlData.ReportType.Rdv21,
                (int) countRDV22Box.Value, (int) XmlData.ReportType.Rdv22,
                (int) countRDV3Box.Value, (int) XmlData.ReportType.Rdv3
            };
            for (int i = 0; i < counts.Length - 1; i += 2)
            {
                Print(new WebBrowser(), (XmlData.ReportType)counts[i + 1], counts[i]);
            }
        }

        private void Print(WebBrowser wb, XmlData.ReportType type, int count)
        {
            if (wb == null || count <= 0)
                return;
            string url = XmlData.GetReportUrl(type);
            if (url != null)
            {
                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
                wb.ScriptErrorsSuppressed = true;
                wb.Tag = count;
                wb.Navigate(url);
            }
        }

        private static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (sender as WebBrowser);
            if (wb == null)
            {
                return;
            }
            int count = (int)wb.Tag;
            StringBuilder sb = new StringBuilder();
            for (; count > 0; count--)
                sb.AppendLine(wb.Document.Body.InnerHtml);
            wb.Document.Body.InnerHtml = sb.ToString();
            MyPrinter.PrintWebPage(wb);
        }

        #endregion
    }
}