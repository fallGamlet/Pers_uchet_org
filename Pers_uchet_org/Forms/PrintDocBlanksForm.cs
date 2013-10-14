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
            int[] counts = new int[] { (int)this.countADV1Box.Value, (int)MyXml.ReportType.ADV1,
                                       (int)this.countADV2Box.Value, (int)MyXml.ReportType.ADV2,
                                       (int)this.countADV3Box.Value, (int)MyXml.ReportType.ADV3,
                                       (int)this.countADV4Box.Value, (int)MyXml.ReportType.ADV4,
                                       (int)this.countADV6Box.Value,(int)MyXml.ReportType.ADV6,
                                       (int)this.countSZV1Box.Value, (int)MyXml.ReportType.SZV1,
                                       (int)this.countSZV2Box.Value, (int)MyXml.ReportType.SZV2,
                                       (int)this.countSZV1Box.Value,(int)MyXml.ReportType.SZV3,
                                       (int)this.countRDV1Box.Value, (int)MyXml.ReportType.RDV1,
                                       (int)this.countRDV21Box.Value, (int)MyXml.ReportType.RDV21,
                                       (int)this.countRDV22Box.Value, (int)MyXml.ReportType.RDV22,
                                       (int)this.countRDV3Box.Value, (int)MyXml.ReportType.RDV3
                                     };
            for (int i = 0; i < counts.Length-1; i += 2)
            {
                this.Print(new WebBrowser(), (MyXml.ReportType)counts[i + 1], counts[i]);
            }
        }

        private void Print(WebBrowser wb, MyXml.ReportType type, int count)
        {
            if (wb == null || count <= 0)
                return;
            string url = MyXml.GetReportUrl(type);
            if (url != null)
            {
                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
                wb.ScriptErrorsSuppressed = true;
                wb.Tag = count;
                wb.Navigate(url);
            }
        }

        static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
