using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using Pers_uchet_org.Forms;
using JR.Utils.GUI.Forms;

namespace Pers_uchet_org
{
    public partial class MainForm : Form
    {
        #region Поля
        const string viewCol = "view_col";

        // строка соединения
        private string _mainConnection;
        private Operator _operator;
        private DataTable _orgTable;
        private BindingSource _orgBS;

        // справочные таблицы БД, используются здесь как статика, для уменьшения обращения к БД
        public static DataTable CountryTable = Country.CreateTable();
        public static DataTable IDocTypeTable = IDocType.CreateTable();
        public static DataTable ClassificatorTable = Classificator.CreateTable();
        public static DataTable ClassgroupTable = Classgroup.CreateTable();
        public static DataTable ClasspercentTable = Classpercent.CreateTable();
        public static DataTable ClasspercentViewTable = ClasspercentView.CreateTable();

        // переменная содержит текущий используемый год
        public static int RepYear;
        #endregion

        #region Конструктор и инициализатор
        public MainForm()
        {
            InitializeComponent();

            _mainConnection = @"data source = //SRV3-STATEPF/e$/Programmers Archive/Db_for_orgs/orgDB.db;";
            //_mainConnection = "data source = SRV3-STATEPF\\e$\\Programmers Archive\\Db_for_orgs\\orgDB.db ;";
            this.Location = new Point(0, 0);

            MainForm.RepYear = DateTime.Now.Year;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _orgTable = Org.CreateTable();
            _orgTable.Columns.Add(viewCol);
            _orgBS = new BindingSource();
            _orgBS.DataSource = _orgTable;
            this.orgBox.DataSource = _orgBS;
            this.orgBox.DisplayMember = viewCol;
            int isLogedin = 0;
            for (int i = 0; i < 3 && isLogedin == 0; i++) 
            {
                isLogedin = this.Login();
            }
            if (isLogedin != 2)
                this.Close();
        }
        #endregion

        #region Методы - свои
        static public void ShowInfoMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static public void ShowWarningMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public void ShowErrorMessage(string err)
        {
            ShowErrorMessage("Возникла непредвиденная ошибка в работе программы.\n" + err, "Ошибка в работе программы");
        }

        static public DialogResult ShowQuestionMessage(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        static public void ShowInfoFlexMessage(string message, string caption)
        {
            FlexibleMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static public void ShowWarningFlexMessage(string message, string caption)
        {
            FlexibleMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static public void ShowErrorFlexMessage(string message, string caption)
        {
            FlexibleMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static public void ShowErrorFlexMessage(string err)
        {
            ShowErrorFlexMessage("Возникла непредвиденная ошибка в работе программы.\n" + err, "Ошибка в работе программы");
        }

        static public DialogResult ShowQuestionFlexMessage(string message, string caption)
        {
            return FlexibleMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void SetPrivilege()
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            bool isAdmin = (_operator.candeleteVal == 0);
            this.adminMenu.Enabled = isAdmin;
            if (orgRow == null)
            {
                this.anketadataMenuItem.Enabled = false;
                this.stajidohodMenuItem.Enabled = false;
                this.svodvedomostMenuItem.Enabled = false;
                this.poiskfizlicaMenuItem.Enabled = false;
                this.elobmenMenuItem.Enabled = false;
            }
            else
            {
                string code = OperatorOrg.GetPrivilege(_operator.idVal, (long)orgRow[Org.id], _mainConnection);
                this.anketadataMenuItem.Enabled = isAdmin || int.Parse(code[0].ToString()) > 0;
                this.stajidohodMenuItem.Enabled = isAdmin || int.Parse(code[2].ToString()) > 0;
                this.svodvedomostMenuItem.Enabled = isAdmin || int.Parse(code[2].ToString()) > 0;
                this.poiskfizlicaMenuItem.Enabled = isAdmin || int.Parse(code[0].ToString()) > 0;
                this.elobmenMenuItem.Enabled = isAdmin || int.Parse(code[4].ToString()) > 0;
            }
        }

        private int Login()
        {
            OperatorEnterForm enterForm = new OperatorEnterForm();
            enterForm.Owner = this;
            DialogResult dRes = enterForm.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                string login = enterForm.Login;
                string password = enterForm.Password;

                Operator oper = Operator.GetOperator(login, password, _mainConnection);
                if (oper == null)
                {
                    MainForm.ShowWarningMessage("Указанные логин и пароль не существуют!", "Не удалось выполнить авторизацию");
                    return 0;
                }

                _operator = oper;

                string selectText;
                if (oper.candeleteVal == 0)
                {
                    selectText = Org.GetSelectCommandText();
                }
                else
                {
                    selectText = Org.GetSelectTextByOperator(oper.idVal);
                }

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectText, _mainConnection);
                _orgTable.Rows.Clear();

                adapter.Fill(_orgTable);
                foreach (DataRow rowItem in _orgTable.Rows)
                {
                    rowItem[viewCol] = string.Format("{0}    {1}", rowItem[Org.regnum], rowItem[Org.name]);
                }
                _orgTable.AcceptChanges();
                this.SetPrivilege();
                this.statusLabel.Text = oper.nameVal;
                MainForm.ShowInfoMessage(string.Format("Добро пожаловать {0}!", oper.nameVal), ";)");
                return 2;
            }
            else
            {
                return 1;
            }
        }
        #endregion

        #region Методы - обработчики событий
        // сменить оператора
        private void changeoperatorMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: в случае ввода неправильных данных текущий пользователь остается в программе, возможно нужно сделать запрос пароля заново. ???
            this.Login();
        }
        // выход из программы
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // открытие формы для работы с Анкетными данными сотрудников
        private void anketadataMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            if (orgRow == null)
            {
                MainForm.ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
                return;
            }
            Org org = new Org();
            org.idVal = (long)orgRow[Org.id];
            org.regnumVal = orgRow[Org.regnum] as string;
            org.nameVal = orgRow[Org.name] as string;
            org.chiefpostVal = orgRow[Org.chief_post] as string;
            org.chieffioVal = orgRow[Org.chief_fio] as string;
            org.bookerfioVal = orgRow[Org.booker_fio] as string;

            AnketadataForm tmpForm = new AnketadataForm(_operator, org, _mainConnection);
            tmpForm.Show();
        }
        // открытие формы для работы со стажем и доходом сотрудников
        private void stajidohodMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            if (orgRow == null)
            {
                MainForm.ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
                return;
            }
            Org org = new Org();
            org.idVal = (long)orgRow[Org.id];
            org.regnumVal = orgRow[Org.regnum] as string;
            org.nameVal = orgRow[Org.name] as string;
            org.chiefpostVal = orgRow[Org.chief_post] as string;
            org.chieffioVal = orgRow[Org.chief_fio] as string;
            org.bookerfioVal = orgRow[Org.booker_fio] as string;
            StajDohodForm tmpForm = new StajDohodForm(_operator, org, _mainConnection);
            tmpForm.Show();
        }
        // открытие формы для работы со сводными ведомостями
        private void svodvedomostMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            if (orgRow == null)
            {
                MainForm.ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
                return;
            }
            Org org = new Org();
            org.idVal = (long)orgRow[Org.id];
            org.regnumVal = orgRow[Org.regnum] as string;
            org.nameVal = orgRow[Org.name] as string;
            org.chiefpostVal = orgRow[Org.chief_post] as string;
            org.chieffioVal = orgRow[Org.chief_fio] as string;
            org.bookerfioVal = orgRow[Org.booker_fio] as string;

            SvodVedomostForm tmpForm = new SvodVedomostForm(org, _operator, _mainConnection);
            tmpForm.Show();
        }
        // открыть форму для печати бланков
        private void pechatblankMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocBlanksForm tmpForm = new PrintDocBlanksForm();
            tmpForm.Show();
        }
        // открыть форму для просмотра справочника по классификациям льгот
        private void klassificatorMenuItem_Click(object sender, EventArgs e)
        {
            ClassifierCategoriesForm tmpForm = new ClassifierCategoriesForm(ClasspercentTable, ClassificatorTable, ClassgroupTable, _mainConnection);
            tmpForm.Show();
        }
        // открыть форму для просмотра справочника типов документов
        private void typedocumentMenuItem_Click(object sender, EventArgs e)
        {
            DoctypeForm tmpForm = new DoctypeForm(_mainConnection);
            tmpForm.Show();
            tmpForm.DoctypeTable = IDocTypeTable;
        }
        // открыть форму для работы с настройками программы
        private void nastroikiMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm tmpForm = new SettingsForm(_mainConnection);
            tmpForm.Show();
        }
        // открыть форму для поиска сотрудников из общего списка вне зависимости от привязки к организациям
        private void poiskfizlicaMenuItem_Click(object sender, EventArgs e)
        {
            SearchIndividualForm tmpForm = new SearchIndividualForm(_mainConnection);
            tmpForm.Show();
        }
        // открыть форму для смены пароля текущего оператора
        private void smenaparoliaMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm tmpForm = new ChangePasswordForm();
            tmpForm.ShowDialog();
        }
        // открыть форму для восстановления БД из резервной копии
        private void vosstanovleniebdMenuItem_Click(object sender, EventArgs e)
        {
            RestoreDBForm tmpForm = new RestoreDBForm();
            tmpForm.ShowDialog();
        }
        // открыть форму для электронного обмена данными с ЕГФСС
        private void elobmenMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            if (orgRow == null)
            {
                MainForm.ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
                return;
            }
            Org org = new Org();
            org.idVal = (long)orgRow[Org.id];
            org.regnumVal = orgRow[Org.regnum] as string;
            org.nameVal = orgRow[Org.name] as string;
            org.chiefpostVal = orgRow[Org.chief_post] as string;
            org.chieffioVal = orgRow[Org.chief_fio] as string;
            org.bookerfioVal = orgRow[Org.booker_fio] as string;

            ExchangeForm tmpForm = new ExchangeForm(_operator, org, _mainConnection);
            tmpForm.ShowDialog();
        }
        // открыть форму для редактирования информации об операторах и их уровни доступа
        private void operatoriMenuItem_Click(object sender, EventArgs e)
        {
            OperatorsForm tmpForm = new OperatorsForm(_mainConnection);
            tmpForm.Show();
        }
        // открыть форму представляющую краткую общую информацию о программе
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox tmpForm = new AboutBox();
            tmpForm.Owner = this;
            tmpForm.ShowDialog(this);
        }
        // открыть форму редактирования информации об организациях
        private void orgMenuItem_Click(object sender, EventArgs e)
        {
            OrgForm tmpForm = new OrgForm(_mainConnection);
            tmpForm.Show();
        }
        // при смене организации поменять состояние активности вкладок в соответствии привилегиями пользователя
        private void orgBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetPrivilege();
        }
        #endregion
    }
}
