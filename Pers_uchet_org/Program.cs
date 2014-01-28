using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace Pers_uchet_org
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.CurrentCulture = culture;
            Application.Run(new MainForm());
        }
    }
}
