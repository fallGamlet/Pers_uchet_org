using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org
{
    public partial class PrintDocBlanksForm : Form
    {
        #region Поля
        WebBrowser wbrowser;

        #endregion

        public PrintDocBlanksForm()
        {
            InitializeComponent();

            wbrowser = new WebBrowser();
            //Stack<HtmlDocument
            
        }

        private void viewRDV1Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.RDV1);
        }

        private void viewRDV21Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.RDV21);
        }

        private void viewRDV22Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.RDV22);
        }

        private void viewRDV3Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.RDV3);
        }

        private void viewSZV1Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.SZV1);
        }

        private void viewSZV2Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.SZV2);
        }

        private void viewSZV3Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.SZV3);
        }

        private void viewADV1Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.ADV1);
        }

        private void viewADV2Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.ADV2);
        }

        private void viewADV3Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.ADV3);

        }

        private void viewADV4Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.ADV4);
        }

        private void viewADV6Button_Click(object sender, EventArgs e)
        {
            MyPrinter.ShowWebPage(null, MyXml.ReportType.ADV5);
        }

        private void printButton_Click(object sender, EventArgs e)
        {

        }

        static public void Print(IEnumerable<DataRow> printRows)
        {
            //string file = Path.GetFullPath(Properties.Settings.Default.report_adv1);
            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Tag = printRows;
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            webBrowser.ScriptErrorsSuppressed = true;
            //webBrowser.Navigate(file);
            
            
        }

        static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (sender as WebBrowser);
            if (wb == null)
            {
                return;
            }
            List<string> htmlDivList = new List<string>();
            HtmlDocument htmlDoc = wb.Document;
            IEnumerable<DataRow> PrintRows = wb.Tag as IEnumerable<DataRow>;
            foreach (DataRow personRow in PrintRows)
            {
                string xmlStr = MyXml.Adv1Xml(personRow).InnerXml;
                htmlDoc.InvokeScript("setAllData", new object[] { xmlStr });
                htmlDivList.Add(htmlDoc.Body.InnerHtml);
            }
            if (htmlDivList.Count > 0)
            {
                StringBuilder sb = new StringBuilder(htmlDivList.Count * htmlDivList[0].Length);
                //string htmlBody = "";
                foreach (string div in htmlDivList)
                    //htmlBody += div;
                    sb.Append(div);
                //htmlDoc.Body.InnerHtml = htmlBody;
                htmlDoc.Body.InnerHtml = sb.ToString();
            }

            MyPrinter.SetPrintSettings();
            //Form webForm = new Form();
            //webForm.Width = 700;
            //webForm.Height = 600;
            //webForm.Controls.Add(wb);
            //wb.Dock = DockStyle.Fill;
            //wb.Show();
            //webForm.Show();
            wb.ShowPrintPreviewDialog();
        }
    }
}
