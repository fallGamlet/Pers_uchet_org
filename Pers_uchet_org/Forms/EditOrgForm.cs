using System;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class EditOrgForm : Form
    {
        public static long _orgId;
        private string _connection;

        public EditOrgForm(string connection)
        {
            InitializeComponent();
            _orgId = -1;
            _connection = connection;
        }

        #region Свойства

        public long OrgId
        {
            get { return _orgId; }
            set { _orgId = value; }
        }

        public string RegnumOrg
        {
            get { return this.regNumOrgBox.Text.Trim(); }
            set { this.regNumOrgBox.Text = value; }
        }

        public string NameOrg
        {
            get { return this.nameorgBox.Text.Trim(); }
            set { this.nameorgBox.Text = value; }
        }

        public string ChiefpostOrg
        {
            get { return this.bosspostorgBox.Text.Trim(); }
            set { this.bosspostorgBox.Text = value; }
        }

        public string ChieffioOrg
        {
            get { return this.bossfioorgBox.Text.Trim(); }
            set { this.bossfioorgBox.Text = value; }
        }

        public string BookerfioOrg
        {
            get { return this.buhfioorgBox.Text.Trim(); }
            set { this.buhfioorgBox.Text = value; }
        }

        #endregion

        private void acceptButton_Click(object sender, EventArgs e)
        {
            TransformData();
            if (ValidateData())
                this.DialogResult = DialogResult.OK;
        }

        public bool ValidateData()
        {
            bool result = true;
            string err = "";

            if (!Org.IsCorrectRegNumber(regNumOrgBox.Text))
            {
                err += "\nНекорректный регистрационный номер (пример: У000123).";
                result = false;
            }

            if (String.IsNullOrEmpty(nameorgBox.Text.Trim()))
            {
                err += "\nНекорректное наименование.";
                result = false;
            }

            if (Org.IsDuplicate(regNumOrgBox.Text, _orgId, _connection))
            {
                err += "\nОрганизация с таким регистрационным номером уже существует.";
                result = false;
            }

            if (!result)
                MainForm.ShowWarningMessage("Были обнаружены следующие некорректные данные:" + err,
                    "Введены некорректные данные");

            return result;
        }

        private void TransformData()
        {
            regNumOrgBox.Text = Org.ChangeEnToRus(regNumOrgBox.Text);
            //bosspostorgBox.Text = FirstUpper(bosspostorgBox.Text);
            //bossfioorgBox.Text = FirstUpper(bossfioorgBox.Text);
            //buhfioorgBox.Text = FirstUpper(buhfioorgBox.Text);
        }


        public String FirstUpper(String str) /*Один символ после пробела Заглавный*/
        {
            str = str.ToLower();
            string[] s = str.Split(' ');
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Length > 1)
                    s[i] = s[i].Substring(0, 1).ToUpper() + s[i].Substring(1, s[i].Length - 1);
                else s[i] = s[i].ToUpper();
            }
            return string.Join(" ", s);
        }
    }
}