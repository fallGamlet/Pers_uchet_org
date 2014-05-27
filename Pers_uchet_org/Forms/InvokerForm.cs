using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Cache;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Ionic.Zip;
using Ionic.Zlib;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org.Forms
{
    public partial class InvokerForm : Form
    {
        #region Поля

        private Org _organization;
        private int _repYear;
        private string _directoryPath;
        private string _containerName;
        private string _mdcName;
        private string _archiveName;
        private BackgroundWorker _worker;
        private WebProxy _proxy;
        private NetworkCredential _netCredential;

        private string _hostAddr;
        private bool _abortSending;

        #region Для запросов значения параметров

        private string _userAgent;
        private string _accept;
        private string _contentTypeUrlencoded;
        private string _contentTypeMultipart;
        private string _acceptEncoding;
        private string _acceptLanguage;
        private int _timeout;
        private string _boundary;

        #endregion

        #endregion

        private InvokerForm()
        {
            InitializeComponent();
        }

        public InvokerForm(Org organization, int repYear, string directoryPath)
            : this()
        {
            _organization = organization;
            _repYear = repYear;
            _directoryPath = directoryPath;

            _containerName = "edatacon.pfs";
            _mdcName = "mdc";
            _hostAddr = "http://www.gpfpmr.idknet.com";
            _abortSending = true;
            _archiveName = "edatacon.zip";

            _userAgent = "Invoker_Cli";
            _accept = "image/gif, image/x-xbitmap, image/jpeg," +
                      "image/pjpeg, application/x-shockwave-flash, " +
                      "application/vnd.ms-excel, " +
                      "application/vnd.ms-powerpoint, " +
                      "application/msword, */*";
            _contentTypeUrlencoded = "application/x-www-form-urlencoded";
            _contentTypeMultipart = "multipart/form-data, boundary=";
            _acceptEncoding = "gzip, deflate";
            _acceptLanguage = "ru-RU";
            _timeout = 10000;
            _boundary = "--irrona";
        }

        private void InvokerForm_Load(object sender, EventArgs e)
        {
            regNumOrgBox.Text = _organization.regnumVal;
            yearBox.Value = _repYear;
            pathTextBox.Text = _directoryPath;
            reportTypeComboBox.SelectedIndex = 0;

            regNumOrgBox.Enabled = false;
            yearBox.Enabled = false;
            pathTextBox.ReadOnly = true;
            edataBrowseButton.Enabled = false;

            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            _worker.WorkerSupportsCancellation = true;
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            cancelButton.Enabled = false;
            ReadSettings();

            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(lanSettingsButton, "Настройки прокси-сервера");
            toolTip1.SetToolTip(helpButton, "Справка");
            toolTip1.SetToolTip(edataBrowseButton, "Обзор..");
            toolTip1.SetToolTip(showPassButton, "Показать пароль");
            toolTip1.SetToolTip(sendButton, "Отправить данные на хостинг");
            toolTip1.SetToolTip(cancelButton, "Прервать отправку данных");
            toolTip1.SetToolTip(passwordTextBox, "Пароль указан на дополнительном соглашении");
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> param = e.Argument as List<string>;
            MyHttpUtility httpUtility = new MyHttpUtility();
            int orgId = 0;
            try
            {
                string result;
                httpUtility.Proxy = _proxy;
                httpUtility.NetCredential = _netCredential;
                httpUtility.UserAgent = _userAgent;
                httpUtility.Accept = _accept;
                httpUtility.ContentType = _contentTypeUrlencoded;
                httpUtility.AcceptLanguage = _acceptLanguage;
                httpUtility.AllowAutoRedirect = false;
                httpUtility.Timeout = _timeout;
                httpUtility.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                httpUtility.KeepAlive = true;
                httpUtility.Expect100Continue = false;

                ToLog(">Проверка доступности хостинга. Ожидайте...");
                if (httpUtility.PingHost(_hostAddr, out result))
                    ToLog("Успешно");
                else
                {
                    throw new Exception(
                        "Нет доступа к сети Интернет. Укажите параметры прокси-сервера(если есть). Ошибка: " + result);
                }

                if (_abortSending)
                    throw new Exception(">Отправка данных прервана!");

                //////
                orgId = IsRegisteredOrg(httpUtility, param[0], param[3]);
                if (orgId > 0)
                    ToLog("Успешно");
                else
                {
                    throw new Exception(
                        "Организация не найдена: Вы неверно ввели пароль или Вы не заключали дополнительное соглашение.");
                }

                if (_abortSending)
                    throw new Exception(">Отправка данных прервана!");

                //////
                int mdc = SendMdc(httpUtility, orgId, Int32.Parse(param[1]), Int32.Parse(param[2]));
                if (mdc > 0)
                    ToLog("Успешно");
                else
                {
                    throw new Exception("Произошла ошибка при отправке контрольной суммы!");
                }

                if (_abortSending)
                    throw new Exception(">Отправка данных прервана!");

                //////
                string archivePath = Path.Combine(_directoryPath, _archiveName);
                if (File.Exists(archivePath))
                    File.Delete(archivePath);
                ToLog(">Сжатие контейнера. Ожидайте...");
                using (ZipFile zip = new ZipFile())
                {
                    zip.CompressionLevel = CompressionLevel.BestCompression;
                    zip.AddDirectoryWillTraverseReparsePoints = false;
                    zip.Comment = "Arhiv kontei`nera.";
                    zip.AddFile(Path.Combine(_directoryPath, _containerName), "");
                    zip.Save(archivePath);
                }
                ToLog("Успешно");
                long length = (new FileInfo(archivePath)).Length;
                ToLog("Размер сжатого контейнера: " + length + " байт (" +
                      String.Format("{0:###0.00}", length / 1048576.0) + " Мб)");

                if (_abortSending)
                    throw new Exception(">Отправка данных прервана!");

                int blob = SendBlob(httpUtility, orgId, Int32.Parse(param[1]));
                if (_abortSending)
                    throw new Exception(">Отправка данных прервана!");
                if (blob > 0)
                    ToLog("Успешно");
                else
                {
                    throw new Exception("Произошла ошибка при отправке контейнера!");
                }

                int update = UpdateMDC(httpUtility, orgId, Int32.Parse(param[1]), mdc, 1);
                if (update > 0)
                    ToLog(
                        ">Передача успешно завершена! В течение 30-60 минут Вам на почту прийдет отчет о проверке данных.");
                else
                {
                    throw new Exception("Данные не отправлены из-за ошибки передачи");
                }

                //////
                MainForm.ShowInfoMessage(
                    "Передача успешно завершена! В течение 30-60 минут Вам на почту прийдет отчет о проверке данных.",
                    "Внимание");
            }
            catch (Exception ex)
            {
                ToLog(ex.Message);
                DeleteAll(httpUtility, orgId, Int32.Parse(param[1]));
                MainForm.ShowErrorFlexMessage(ex.Message, "Ошибка отправки данных");
            }
            finally
            {
                _abortSending = false;
            }
        }

        private int UpdateMDC(MyHttpUtility httpUtility, int orgId, int repYear, int mdc, int status)
        {
            ToLog(">Фиксация результатов. Ожидайте...");
            string queryString = "pid=" + orgId +
                                 "&ry=" + repYear +
                                 "&mid=" + mdc +
                                 "&stat=" + status;
            httpUtility.ContentType = _contentTypeUrlencoded;
            httpUtility.AcceptEncoding = String.Empty;
            string result =
                httpUtility.CreateGetRequestAndExec("http://www.gpfpmr.idknet.com/php_scr/umdc.php?" + queryString);
            return Convert.ToInt32(result);
        }

        private int SendBlob(MyHttpUtility httpUtility, int orgId, int repYear)
        {
            byte[] buf = File.ReadAllBytes(Path.Combine(_directoryPath, _archiveName));
            int offset = 0;
            const int blobSize = 65535;
            string b64String = Convert.ToBase64String(buf);
            int lengh = b64String.Length;
            int ret = 0;

            ToLog(">Отправка контейнера. Ожидайте...");
            ToLog(">Отправлено 0%...");

            while (offset < lengh)
            {
                httpUtility.ContentType = _contentTypeMultipart + _boundary;
                httpUtility.AcceptEncoding = _acceptEncoding;

                int lengthToSend = (lengh - offset > blobSize) ? blobSize : lengh - offset;
                string queryString =
                    "--" + _boundary + "\n" +
                    "Content-Type: text/plain" + "\n" +
                    "Content-Disposition: form-data; name=\"pid\"" + "\n" + "\n" + orgId.ToString() + "\n" + "--" +
                    _boundary + "--" + "\n" + "\n" +
                    "--" + _boundary + "\n" +
                    "Content-Type: text/plain" + "\n" +
                    "Content-Disposition: form-data; name=\"ry\"" + "\n" + "\n" + repYear.ToString() + "\n" + "--" +
                    _boundary + "--" + "\n" + "\n" +
                    "--" + _boundary + "\n" +
                    "Content-Type: application/octet-stream; filename=\"" + _containerName + "\"" + "\n" +
                    "Content-Transfer-Encoding: base64" + "\n" +
                    "Content-Disposition: form-data; name=\"data\"" + "\n" + "\n" +
                    b64String.Substring(offset, lengthToSend) + "\n" + "--" + _boundary + "--" + "\n" + "\n";

                string result = httpUtility.CreatePostRequestAndExec("http://www.gpfpmr.idknet.com/php_scr/wrblob.php",
                    queryString);
                ret = Convert.ToInt32(result);
                if (ret < 1)
                {
                    ToLog("Произошла ошибка при отправке контейнера!");
                    DeleteAll(httpUtility, orgId, repYear);
                    return 0;
                }

                offset += lengthToSend;
                UndoLog();
                ToLog(String.Format(">Отправлено {0:0.0%}...", (offset * 1.0) / lengh));

                if (_abortSending)
                    return 0;
            }
            return ret;
        }

        private int SendMdc(MyHttpUtility httpUtility, int orgId, int repYear, int reportType)
        {
            ToLog(">Отправка контрольной суммы. Ожидайте...");

            byte[] buf = File.ReadAllBytes(Path.Combine(_directoryPath, _mdcName));
            //int length = buf.Length;

            if (_abortSending)
                throw new Exception(">Отправка данных прервана!");

            string b64String = Convert.ToBase64String(buf);

            string queryString =
                "--" + _boundary + "\n" +
                "Content-Type: text/plain" + "\n" +
                "Content-Disposition: form-data; name=\"pid\"" + "\n" + "\n" + orgId.ToString() + "\n" + "--" +
                _boundary + "--" + "\n" + "\n" +
                "--" + _boundary + "\n" +
                "Content-Type: text/plain" + "\n" +
                "Content-Disposition: form-data; name=\"ry\"" + "\n" + "\n" + repYear.ToString() + "\n" + "--" +
                _boundary + "--" + "\n" + "\n" +
                "--" + _boundary + "\n" +
                "Content-Type: text/plain" + "\n" +
                "Content-Disposition: form-data; name=\"dt\"" + "\n" + "\n" + reportType.ToString() + "\n" + "--" +
                _boundary + "--" + "\n" + "\n" +
                "--" + _boundary + "\n" +
                "Content-Type: application/octet-stream; filename=\"" + _mdcName + "\"" + "\n" +
                "Content-Transfer-Encoding: base64" + "\n" +
                "Content-Disposition: form-data; name=\"mdc\"" + "\n" + "\n" +
                b64String + "\n" + "--" + _boundary + "--" + "\n" + "\n";

            httpUtility.ContentType = _contentTypeMultipart + _boundary;
            httpUtility.AcceptEncoding = _acceptEncoding;
            string result = httpUtility.CreatePostRequestAndExec("http://www.gpfpmr.idknet.com/php_scr/wrmdc.php",
                queryString);
            return Convert.ToInt32(result);
        }

        private int IsRegisteredOrg(MyHttpUtility httpUtility, string regNumOrg, string password)
        {
            ToLog(">Проверка наличия организации. Ожидайте...");
            string queryString = "reg=" + regNumOrg +
                                 "&pass=" + password;
            string result = httpUtility.CreatePostRequestAndExec("http://www.gpfpmr.idknet.com/php_scr/getorg.php",
                queryString);
            return Convert.ToInt32(result);
        }

        private void DeleteAll(MyHttpUtility httpUtility, int orgId, int repYear)
        {
            httpUtility.ContentType = _contentTypeUrlencoded;
            httpUtility.AcceptEncoding = String.Empty;
            string queryString = "pid=" + orgId + "&ry=" + repYear + "&stat=0";
            httpUtility.CreateGetRequestAndExec("http://www.gpfpmr.idknet.com/php_scr/delall.php?" + queryString);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _abortSending = true;
            sendButton.Enabled = true;
            cancelButton.Enabled = false;
        }

        private void ReadSettings()
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

        private void showPassButton_MouseDown(object sender, MouseEventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = false;
        }

        private void showPassButton_MouseUp(object sender, MouseEventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = true;
        }

        private void edataBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.Description = "Выберите папку c файлами электронных данных";
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderDialog.SelectedPath;
            }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            regNumOrgBox.Text = Org.ChangeEnToRus(regNumOrgBox.Text);
            if (!ValidateData())
                return;

            _abortSending = false;
            sendButton.Enabled = false;
            cancelButton.Enabled = true;
            logRichTextBox.Text = "";

            List<string> param = new List<string>();
            param.Add(regNumOrgBox.Text);
            param.Add(yearBox.Value.ToString());
            param.Add(reportTypeComboBox.SelectedIndex.ToString());
            param.Add(passwordTextBox.Text);

            _worker.RunWorkerAsync(param);
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

            if (String.IsNullOrEmpty(yearBox.Value.ToString().Trim()))
            {
                err += "\nНекорректный отчетный год.";
                result = false;
            }

            if (String.IsNullOrEmpty(pathTextBox.Text.Trim()) || !Directory.Exists(_directoryPath))
            {
                err += "\nНекорректный путь к папке с ЭДО.";
                result = false;
            }
            else
            {
                _directoryPath = pathTextBox.Text.Trim();
            }

            if (String.IsNullOrEmpty(passwordTextBox.Text.Trim()))
            {
                err += "\nНе указан пароль.";
                result = false;
            }

            if (!File.Exists(Path.Combine(_directoryPath, _mdcName)))
            {
                err += "\nФайл с контрольной суммой контейнера отсутствует в каталоге " + _directoryPath + ".";
                result = false;
            }

            string containerFullPath = Path.Combine(_directoryPath, _containerName);

            try
            {
                var fi = new FileInfo(containerFullPath);
                if (!fi.Exists)
                {
                    err += "\nФайл контейнера отсутствует в каталоге " + _directoryPath + ".";
                    result = false;
                }
                else if (fi.Length <= 0)
                {
                    err += "\nФайл контейнера пуст - " + containerFullPath + ".";
                    result = false;
                }
                else
                {
                    byte[] buf = new byte[8];
                    var fileStream = fi.OpenRead();
                    fileStream.Read(buf, 0, 8);
                    fileStream.Close();
                    byte[] testBuf = { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };

                    if (Encoding.Default.GetString(buf) != Encoding.Default.GetString(testBuf))
                    {
                        err += "\nУказанный файл не является контейнером - " + containerFullPath + ".";
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //err += "\nУказанный файл не является контейнером - " + containerFullPath + ".\n" + ex.Message;
                err += "\n" + ex.Message;
                result = false;
            }

            if (!result)
                MainForm.ShowErrorFlexMessage("Были обнаружены следующие некорректные данные:" + err,
                    "Введены некорректные данные");

            return result;
        }

        public void ToLog(string str)
        {
            if (logRichTextBox.InvokeRequired)
            {
                Invoke(new Action(() => ToLog(str)));
            }
            else
            {
                logRichTextBox.AppendText(str + "\n");
                logRichTextBox.SelectionStart = logRichTextBox.Text.Length;
                logRichTextBox.ScrollToCaret();
            }
        }

        public void UndoLog()
        {
            if (logRichTextBox.InvokeRequired)
            {
                Invoke(new Action(UndoLog));
            }
            else
            {
                logRichTextBox.Undo();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _abortSending = true;
            sendButton.Enabled = true;
            cancelButton.Enabled = false;
            _worker.CancelAsync();
        }

        private void lanSettingsButton_Click(object sender, EventArgs e)
        {
            SettingsLanForm settingsLanForm = new SettingsLanForm();
            if (settingsLanForm.ShowDialog() == DialogResult.OK)
            {
                ReadSettings();
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help.chm", HelpNavigator.Topic, "ExchangeInternet.htm");
        }
    }
}