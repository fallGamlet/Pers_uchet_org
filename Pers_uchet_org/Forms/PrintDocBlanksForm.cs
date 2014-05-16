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
        WebBrowser wb;
        #endregion

        #region Конструктор
        public PrintDocBlanksForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(otherPage);
            wb = new WebBrowser();
            wb.Visible = false;
            wb.Parent = this;
        }
        #endregion

        #region Методы - обработчики событий
        private void viewSZV1Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Szv1);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Szv1);
        }

        private void viewSZV2Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Szv2);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Szv2);
        }

        private void viewSZV3Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Szv3);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Szv3);
        }

        private void viewADV1Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv1);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Adv1);
        }

        private void viewADV2Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv2);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Adv2);
        }

        private void viewADV3Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv3);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Adv3);

        }

        private void viewADV4Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv4);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Adv4);
        }

        private void viewADV6Button_Click(object sender, EventArgs e)
        {
            //MyPrinter.ShowWebPage(null, XmlData.ReportType.Adv6);
            MyPrinter.ShowPrintPreviewWebPage(wb, XmlData.ReportType.Adv6);
        }

        //private void printButton_Click(object sender, EventArgs e)
        //{
        //    this.Print(new WebBrowser(), (XmlData.ReportType)counts[i + 1], counts[i]);
        //}

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
