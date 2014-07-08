using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using JR.Utils.GUI.Forms;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org.Forms
{
    public partial class MainForm : Form
    {
        #region Поля

        private const string ViewCol = "view_col";

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

        private WebProxy _proxy;
        private NetworkCredential _netCredential;

        #endregion

        #region Конструктор и инициализатор

        public MainForm()
        {
            InitializeComponent();

            Location = new Point(0, 0);
            RepYear = DateTime.Now.Year;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Settings.Default.DataBasePath))
            {
                _mainConnection = string.Format("data source = {0};", Settings.Default.DataBasePath);

                if (IsFirstLogin())
                    ShowInfoFlexMessage("Вы первый раз входите в программу.\n" +
                                        "По умолчанию пароль входа пустой.", "Первый вход в программу");

                changeoperatorMenuItem_Click(sender, e);
            }
            else
            {
                if (
                    ShowQuestionMessage(
                        "Файл базы данных не найден!\nЖелаете попробовать восстановить базу из резервной копии?",
                        "Ошибка") == DialogResult.Yes)
                {
                    vosstanovleniebdMenuItem_Click(sender, e);
                }
            }
        }

        #endregion

        #region Методы - свои

        public static void ShowInfoMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarningMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowErrorMessage(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorMessage(string err)
        {
            ShowErrorMessage("Возникла непредвиденная ошибка в работе программы.\n" + err, "Ошибка в работе программы");
        }

        public static DialogResult ShowQuestionMessage(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);
        }

        public static void ShowInfoFlexMessage(string message, string caption)
        {
            FlexibleMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarningFlexMessage(string message, string caption)
        {
            FlexibleMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowErrorFlexMessage(string message, string caption)
        {
            FlexibleMessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorFlexMessage(string err)
        {
            ShowErrorFlexMessage("Возникла непредвиденная ошибка в работе программы.\n" + err,
                "Ошибка в работе программы");
        }

        public static DialogResult ShowQuestionFlexMessage(string message, string caption)
        {
            return FlexibleMessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void SetPrivilege()
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            bool isAdmin = (_operator.candeleteVal == 0);
            adminMenu.Enabled = isAdmin;
            if (orgRow == null)
            {
                anketadataMenuItem.Enabled = false;
                stajidohodMenuItem.Enabled = false;
                svodvedomostMenuItem.Enabled = false;
                poiskfizlicaMenuItem.Enabled = false;
                elobmenMenuItem.Enabled = false;
            }
            else
            {
                string code = OperatorOrg.GetPrivilege(_operator.idVal, (long)orgRow[Org.id], _mainConnection);
                anketadataMenuItem.Enabled = isAdmin || int.Parse(code[0].ToString()) > 0;
                stajidohodMenuItem.Enabled = isAdmin || int.Parse(code[2].ToString()) > 0;
                svodvedomostMenuItem.Enabled = isAdmin || int.Parse(code[2].ToString()) > 0;
                poiskfizlicaMenuItem.Enabled = isAdmin || int.Parse(code[0].ToString()) > 0;
                elobmenMenuItem.Enabled = isAdmin || int.Parse(code[4].ToString()) > 0;
            }
        }

        private int Login()
        {
            OperatorEnterForm enterForm = new OperatorEnterForm(_mainConnection);
            enterForm.Owner = this;
            DialogResult dRes = enterForm.ShowDialog();

            switch (dRes)
            {
                case DialogResult.Cancel:
                    Backup.isBackupCreate = Backup.BackupCreate.DoNotCreate;
                    return 0; //Отмена входа
                case DialogResult.OK:
                    _operator = enterForm.Operator;
                    Backup.isBackupCreate = Backup.BackupCreate.None;
                    return 1; //Вход удачен
                case DialogResult.Abort:
                    Backup.isBackupCreate = Backup.BackupCreate.DoNotCreate;
                    return 2; //n раз(а) ввели неправильный логин или пароль
                default:
                    Backup.isBackupCreate = Backup.BackupCreate.DoNotCreate;
                    return -1;
            }
        }

        private bool IsFirstLogin()
        {
            int isFirstLogin =
                Convert.ToInt32(Options.GetKeyValue(Options.isFirstLoginKeyName, new SQLiteConnection(_mainConnection),
                    null));
            return isFirstLogin == 1;
        }

        private void ReloadData()
        {
            int position = -1;
            if (_orgBS != null)
                position = _orgBS.Position;

            _orgTable = Org.CreateTable();
            _orgTable.Columns.Add(ViewCol);

            _orgBS = new BindingSource();
            _orgBS.DataSource = _orgTable;

            string selectText = _operator.candeleteVal == 0
                ? Org.GetSelectCommandText()
                : Org.GetSelectTextByOperator(_operator.idVal);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectText, _mainConnection);
            adapter.Fill(_orgTable);
            foreach (DataRow rowItem in _orgTable.Rows)
            {
                rowItem[ViewCol] = string.Format("{0}    {1}", rowItem[Org.regnum], rowItem[Org.name]);
            }
            _orgTable.AcceptChanges();

            orgBox.DataSource = _orgBS;
            orgBox.DisplayMember = ViewCol;

            _orgBS.Position = position;

            SetPrivilege();

            statusLabel.Text = _operator.nameVal;
        }

        #endregion

        #region Методы - обработчики событий

        // сменить оператора
        private void changeoperatorMenuItem_Click(object sender, EventArgs e)
        {
            switch (Login())
            {
                case 1:
                    break;
                case 0:
                case 2:
                    Close();
                    return;
                default:
                    Close();
                    return;
            }
            ReloadData();

            if (IsFirstLogin())
            {
                ShowInfoFlexMessage("Рекомендуется для дальнейшего использования программы сменить пароль.\n" +
                                    "Если Вы желаете оставить пароль по умолчанию, просто зактройте следующие окно.",
                    "Первый вход в программу");

                smenaparoliaMenuItem_Click(sender, e);

                ShowInfoFlexMessage("Откройте справку и ознакомьтесь с инструкцией и возможностями программы.\n" +
                                    "Там же Вы узнаете, как добавить Вашу организацию в программу.",
                    "Первый вход в программу");

                Options.ChangeKeyValue(Options.isFirstLoginKeyName, "0", new SQLiteConnection(_mainConnection), null);
            }
            //MainForm.ShowInfoMessage(string.Format("Добро пожаловать, {0}!", _operator.nameVal), "Приветствие");
            checkUpdatesMenuItem_Click(null, null);
        }

        // выход из программы
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // открытие формы для работы с Анкетными данными сотрудников
        private void anketadataMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView orgRow = _orgBS.Current as DataRowView;
            if (orgRow == null)
            {
                ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
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
                ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
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
                ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
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
            ClassifierCategoriesForm tmpForm = new ClassifierCategoriesForm(ClasspercentTable, ClassificatorTable,
                ClassgroupTable, _mainConnection);
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
            if (_operator == null)
            {
                ShowInfoMessage("Сначала необходимо выбрать оператора", "Неопределен оператор");
                return;
            }

            tmpForm.PasswordOld = _operator.passwordVal;
            tmpForm.Owner = this;
            // отобразить диалоговое окно для пользователя
            DialogResult dRes = tmpForm.ShowDialog(this);
            // если диалог закрыт с утвердительным ответом
            if (dRes == DialogResult.OK)
            {
                // принять новый пароль
                _operator.passwordVal = tmpForm.Password;
                _operator.SaveNewPassword(_mainConnection);
            }
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
                ShowWarningMessage("Необходимо выбрать организацию!", "Не выбрана организация");
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
            tmpForm.ShowDialog();
        }

        // открыть форму представляющую краткую общую информацию о программе
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox tmpForm = new AboutBox();
            tmpForm.Owner = this;
            tmpForm.ShowDialog();
        }

        // открыть форму редактирования информации об организациях
        private void orgMenuItem_Click(object sender, EventArgs e)
        {
            OrgForm tmpForm = new OrgForm(_mainConnection);
            if (tmpForm.ShowDialog() == DialogResult.OK)
            {
                _operator = Operator.GetOperator(_operator.idVal, _mainConnection);
                ReloadData();
            }
        }

        // при смене организации поменять состояние активности вкладок в соответствии привилегиями пользователя
        private void orgBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPrivilege();
        }

        private void helpMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help.chm", HelpNavigator.Topic, "FirstRun.htm");
        }

        private void historyChangeMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("History.txt");
        }

        private void checkUpdatesMenuItem_Click(object sender, EventArgs e)
        {
            ReadProxySettings();

            string hostAddr = "http://ef-pmr.org/uploads/soft";

            WebClient webClient = new WebClient();
            webClient.Proxy = _proxy;
            webClient.Credentials = _netCredential;
            webClient.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            if (sender != null)
                webClient.DownloadStringCompleted +=
                    new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            else
                webClient.DownloadStringCompleted +=
                    new DownloadStringCompletedEventHandler(webClientAuto_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(hostAddr + "/Pers_uchet_org_update.xml"), 1);
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowInfoMessage("Не удалось проверить наличие обновлений!", "Обновление");
                return;
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.InnerXml = e.Result;

                Version v1 = new Version(xmlDocument.GetElementsByTagName("version")[0].InnerText);
                Version v2 = Assembly.GetExecutingAssembly().GetName().Version;
                switch (v1.CompareTo(v2))
                {
                    case -1:
                    case 0:
                        ShowInfoMessage("Установленная версия является самой последней", "Обновление");
                        break;
                    case 1:
                        if (
                            ShowQuestionFlexMessage(
                                string.Format(
                                    "Доступна новая версия {0} от {2}.\nВаша версия {1}.\nПерейти на сайт для скачивания новой версии?",
                                    v1.ToString(), v2.ToString(), xmlDocument.GetElementsByTagName("date")[0].InnerText),
                                "Обновление") == DialogResult.Yes)
                        {
                            Process.Start("http://ef-pmr.org/persuchet/soft/");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowInfoMessage("Не удалось проверить наличие обновлений!\n\n" + ex.Message, "Обновление");
            }
        }

        private void webClientAuto_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.InnerXml = e.Result;

                Version v1 = new Version(xmlDocument.GetElementsByTagName("version")[0].InnerText);
                Version v2 = Assembly.GetExecutingAssembly().GetName().Version;
                switch (v1.CompareTo(v2))
                {
                    case -1:
                    case 0:
                        break;
                    case 1:
                        if (
                            ShowQuestionFlexMessage(
                                string.Format(
                                    "Доступна новая версия {0} от {2}.\nВаша версия {1}.\nПерейти на сайт для скачивания новой версии?",
                                    v1.ToString(), v2.ToString(), xmlDocument.GetElementsByTagName("date")[0].InnerText),
                                "Обновление") == DialogResult.Yes)
                        {
                            Process.Start("http://ef-pmr.org/persuchet/soft/");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                //При автоматической проверке не пугаем пользователя сообщениями об ошибках

                //MainForm.ShowInfoMessage("Не удалось проверить наличие обновлений!\n\n" + ex.Message, "Обновление");
            }
        }

        private void ReadProxySettings()
        {
            string proxyAddr = Settings.Default.ProxyAddr;
            int proxyPort = Settings.Default.ProxyPort;
            string proxyLogin = Settings.Default.ProxyLogin;
            string proxyPass = Settings.Default.ProxyPass;
            bool proxyUseAuto = Settings.Default.ProxyUseAuto;
            bool bypassProxyOnLocal = Settings.Default.BypassProxyOnLocal;
            bool useDefaultCredentials = Settings.Default.UseDefaultCredentials;

            if (proxyUseAuto)
            {
                _proxy = WebProxy.GetDefaultProxy();
                if (_proxy != null)
                {
                    _proxy.BypassProxyOnLocal = bypassProxyOnLocal;
                    _proxy.UseDefaultCredentials = useDefaultCredentials;
                }
            }
            else
            {
                _proxy = new WebProxy(proxyAddr, proxyPort);
                _proxy.BypassProxyOnLocal = bypassProxyOnLocal;
                _proxy.UseDefaultCredentials = useDefaultCredentials;
            }

            _netCredential = useDefaultCredentials ? new NetworkCredential(proxyLogin, proxyPass) : null;
        }

        #endregion
    }
}