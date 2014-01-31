using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using Pers_uchet_org.Properties;

namespace Pers_uchet_org
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            culture.DateTimeFormat.DateSeparator = ".";
            culture.DateTimeFormat.LongDatePattern = "d MMMM yyyy 'г.'";
            culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            culture.DateTimeFormat.ShortTimePattern = "H:mm";
            culture.DateTimeFormat.LongTimePattern = "H:mm:ss";
            culture.DateTimeFormat.TimeSeparator = ":";
            culture.NumberFormat.CurrencyDecimalDigits = 2;
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = " ";
            culture.NumberFormat.CurrencyNegativePattern = 5;
            culture.NumberFormat.CurrencyPositivePattern = 1;
            culture.NumberFormat.CurrencySymbol = "р.";
            culture.NumberFormat.DigitSubstitution = DigitShapes.None;
            culture.NumberFormat.NegativeInfinitySymbol = "-бесконечность";
            culture.NumberFormat.NegativeSign = "-";
            culture.NumberFormat.NumberDecimalDigits = 2;
            culture.NumberFormat.NumberGroupSeparator = " ";
            culture.NumberFormat.NumberGroupSizes = new int[] { 3 };
            culture.NumberFormat.NumberNegativePattern = 1;
            culture.NumberFormat.PercentDecimalDigits = 2;
            culture.NumberFormat.PercentDecimalSeparator = ",";
            culture.NumberFormat.PercentGroupSeparator = " ";
            culture.NumberFormat.PercentGroupSizes = new int[] { 3 };
            culture.NumberFormat.PercentNegativePattern = 1;
            culture.NumberFormat.PercentPositivePattern = 1;
            culture.NumberFormat.PercentSymbol = "%";
            culture.NumberFormat.PositiveInfinitySymbol = "бесконечность";
            culture.NumberFormat.PositiveSign = "+";
            culture.NumberFormat.NumberDecimalSeparator = ",";

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = culture;

            Process process = RunningInstance();
            if (process != null)
            {
                if (MainForm.ShowQuestionMessage("Другая копия программы уже запущена!\nЖелаете запустить еще одну копию?", "Предупреждение") != DialogResult.Yes)
                    return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.CurrentCulture = culture;
            Application.Run(new MainForm());

            CreateBackup();
        }

        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Просматриваем все процессы 
            return processes.Where(process => process.Id != current.Id).FirstOrDefault(process => Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName);
        }

        private static void CreateBackup()
        {
            try
            {
                bool isCreateBackup = Settings.Default.IsBackupEnabled;
                isCreateBackup = Backup.isBackupCreate;
                if (isCreateBackup)
                {
                    Backup.CreateBackup(Settings.Default.BackupPath, Settings.Default.DataBasePath, Backup.TypeBackup.Auto);
                    Backup.DeleteOldBackups(Settings.Default.BackupPath, (int)Settings.Default.BackupMaxCount, Backup.TypeBackup.Auto);
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorMessage(ex.Message, "Ошибка создания резервной копии");
            }
        }
    }
}
