using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Reflection;

namespace Pers_uchet_org.Forms
{
    public partial class OperatorEnterForm : Form
    {
        #region Поля

        // строка соединения
        private string _mainConnection;
        private DataTable _operatorTable;
        private Operator _operator;
        private int _attemptsCount; //количество попыток входа
        private int _maxAttemptsCount;

        #endregion

        #region Конструктор и инициализатор

        private OperatorEnterForm()
        {
            InitializeComponent();
        }

        public OperatorEnterForm(string connection)
            : this()
        {
            _attemptsCount = 0;
            _maxAttemptsCount = 10;
            _mainConnection = connection;
        }

        private void OperatorEnterForm_Load(object sender, EventArgs e)
        {
            _operatorTable = Operator.CreateTable();
            SQLiteDataAdapter adapter = Operator.CreateAdapter(_mainConnection);
            adapter.Fill(_operatorTable);

            loginComboBox.DisplayMember = Operator.name;
            loginComboBox.ValueMember = Operator.id;
            loginComboBox.DataSource = _operatorTable;

            foreach (DataRow dr in _operatorTable.Rows)
            {
                loginComboBox.AutoCompleteCustomSource.Add(dr[Operator.name].ToString());
            }

            labelVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        #endregion

        #region Свойства

        public string Login
        {
            get { return loginComboBox.Text; }
            //set { this.loginBox.Text = value; }
        }

        public string Password
        {
            get { return passwordBox.Text; }
            set { passwordBox.Text = value; }
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
                //TODO: Удалить в конечном варианте
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
                if (_operator == null && _attemptsCount <= _maxAttemptsCount)
                {
                    MainForm.ShowWarningMessage("Указанные логин и пароль не существуют!",
                        "Не удалось выполнить авторизацию");
                    _attemptsCount++;
                    passwordBox.Focus();
                    return;
                }

                if (_attemptsCount > _maxAttemptsCount)
                {
                    MainForm.ShowInfoMessage(
                        string.Format("Вы {0} раз(а) неверно ввели логин или пароль.\r\nПрограмма будет закрыта!",
                            _attemptsCount),
                        "Не удалось выполнить авторизацию");
                    DialogResult = DialogResult.Abort;
                    return;
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(exception.Message, "Ошибка");
                if (
                    MainForm.ShowQuestionMessage(
                        "Файл базы данных не найден или поврежден!\nЖелаете попробовать восстановить базу из резервной копии?",
                        "Ошибка") == DialogResult.Yes)
                {
                    RestoreDBForm tmpForm = new RestoreDBForm();
                    tmpForm.ShowDialog();
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void showPassButton_MouseDown(object sender, MouseEventArgs e)
        {
            passwordBox.UseSystemPasswordChar = false;
        }

        private void showPassButton_MouseUp(object sender, MouseEventArgs e)
        {
            passwordBox.UseSystemPasswordChar = true;
        }

        private void showPassButton_KeyDown(object sender, KeyEventArgs e)
        {
            passwordBox.UseSystemPasswordChar = false;
        }

        private void showPassButton_KeyUp(object sender, KeyEventArgs e)
        {
            passwordBox.UseSystemPasswordChar = true;
        }

        private void showPassButton_Leave(object sender, EventArgs e)
        {
            passwordBox.UseSystemPasswordChar = true;
        }

        #endregion
    }
}