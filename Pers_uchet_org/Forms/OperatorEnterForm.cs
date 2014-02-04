using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org.Forms
{
    public partial class OperatorEnterForm : Form
    {
        #region Поля
        // строка соединения
        private string _mainConnection;
        private DataTable operatorTable;
        private Operator _operator;
        private int attemptsCount; //количество попыток входа
        private int maxAttemptsCount;
        #endregion

        #region Конструктор и инициализатор
        private OperatorEnterForm()
        {
            InitializeComponent();
        }

        public OperatorEnterForm(string connection)
            : this()
        {
            attemptsCount = 0;
            maxAttemptsCount = 10;
            this._mainConnection = connection;
        }

        private void OperatorEnterForm_Load(object sender, EventArgs e)
        {
            operatorTable = Operator.CreateTable();
            SQLiteDataAdapter adapter = Operator.CreateAdapter(_mainConnection);
            adapter.Fill(operatorTable);

            loginComboBox.DisplayMember = Operator.name;
            loginComboBox.ValueMember = Operator.id;
            loginComboBox.DataSource = operatorTable;

            foreach (DataRow dr in operatorTable.Rows)
            {
                this.loginComboBox.AutoCompleteCustomSource.Add(dr[Operator.name].ToString());
            }
        }
        #endregion

        #region Свойства
        public string Login
        {
            get { return this.loginComboBox.Text; }
            //set { this.loginBox.Text = value; }
        }

        public string Password
        {
            get { return this.passwordBox.Text; }
            set { this.passwordBox.Text = value; }
        }

        public Operator Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        #endregion

        #region Методы - обработчики событий
        private void loginComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passwordBox.Focus();
                //TODO: Удалить в конечно варианте
                acceptButton_Click(sender, e);
            }
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                acceptButton.Focus();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                string login = loginComboBox.Text.Trim();
                string password = passwordBox.Text;

                _operator = Operator.GetOperator(login, password, _mainConnection);
                if (_operator == null && attemptsCount <= maxAttemptsCount)
                {
                    MainForm.ShowWarningMessage("Указанные логин и пароль не существуют!",
                        "Не удалось выполнить авторизацию");
                    attemptsCount++;
                    passwordBox.Focus();
                    return;
                }

                if (attemptsCount > maxAttemptsCount)
                {
                    MainForm.ShowInfoMessage(string.Format("Вы {0} раз(а) неверно ввели логин или пароль.\r\nПрограмма будет закрыта!", attemptsCount),
                        "Не удалось выполнить авторизацию");
                    this.DialogResult = DialogResult.Abort;
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(exception.Message, "Ошибка");
                if (MainForm.ShowQuestionMessage("Файл базы данных не найден или поврежден!\nЖелаете попробовать восстановить базу из резервной копии?", "Ошибка") == DialogResult.Yes)
                {
                    RestoreDBForm tmpForm = new RestoreDBForm();
                    tmpForm.ShowDialog();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
