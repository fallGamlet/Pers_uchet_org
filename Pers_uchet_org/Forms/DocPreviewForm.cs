using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Pers_uchet_org.Forms
{
    public partial class DocPreviewForm : Form
    {
        DataRow _person;

        public DocPreviewForm(DataRow person)
        {
            InitializeComponent();

            _person = person;
            string file = Application.StartupPath + @"\static\template\report_(форма АДВ-1).html";
            this.webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            this.webBrowser.Navigate(file);
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_person != null)
            {
                this.webBrowser.Document.InvokeScript("setFName", new object[] { _person[PersonView.fName] });
                this.webBrowser.Document.InvokeScript("setMName", new object[] {_person[PersonView.mName]});
                this.webBrowser.Document.InvokeScript("setLName", new object[] { _person[PersonView.lName] });
                object sexObj = _person[PersonView.sex];
                string sexStr = sexObj is DBNull ? "не определено" : (int)sexObj == 1 ? "м" : "ж";
                this.webBrowser.Document.InvokeScript("setSex", new object[] { sexStr });
                this.webBrowser.Document.InvokeScript("setBornDate", new object[] { _person[PersonView.birthday].ToString() });
                this.webBrowser.Document.InvokeScript("setCitizen1", new object[] { _person[PersonView.citizen1].ToString() });
                this.webBrowser.Document.InvokeScript("setCitizen2", new object[] { _person[PersonView.citizen2].ToString() });
                this.webBrowser.Document.InvokeScript("setRegAddress", new object[] { _person[PersonView.regAdress].ToString() });
            }
        }

        private void DocPreviewForm_Load(object sender, EventArgs e)
        {

        }
    }
}
