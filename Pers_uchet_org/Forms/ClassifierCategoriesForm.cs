using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org
{
    public partial class ClassifierCategoriesForm : Form
    {
        #region Поля
        string _conection; // строка подключение к БД
        // объекты таблиц для хранения записей
        DataTable _classgroupTable;         // таблица Classgroup
        DataTable _classificatorTable;   // таблица Classificator
        DataTable _classpercentTable;     // таблица Classpercent
        // объекты BindingSource для синхронизации таблиц и отображателей
        BindingSource _classgroupBS;
        BindingSource _classificatorBS;
        BindingSource _classpercentBS;
        #endregion

        #region Конструкторы и инициализатор
        public ClassifierCategoriesForm(string connection)
        {
            InitializeComponent();
            // определить строку подключения к БД
            _conection = connection;
        }

        public ClassifierCategoriesForm(DataTable classpercentTable, DataTable classificatorTable, DataTable classgroupTable, string connection)
        {
            InitializeComponent();
            // определить строку подключения к БД
            _conection = connection;
            // определение таблиц
            _classgroupTable = classgroupTable;
            _classpercentTable = classpercentTable;
            _classificatorTable = classificatorTable;
        }

        private void ClassifierCategoriesForm_Load(object sender, EventArgs e)
        {
            // инициализация сомманды для считывания
            SQLiteCommand command = new SQLiteCommand();
            // определение подключения к БД
            command.Connection = new SQLiteConnection(_conection);
            // инициализация адаптера для считывания
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            adapter.SelectCommand = command;

            #region Определение и заполнение таблиц, при необходимости
            // если таблице не присвоенно значение, то создать новую таблицу
            if (_classgroupTable == null)
            {
                _classgroupTable = Classgroup.CreateTable();
            }
            // если таблица новая (не имеет записей), то заполнить эту таблицу
            if(_classgroupTable.Rows.Count <=0)
            {
                // назначить текст комманды считывания
                command.CommandText = Classgroup.GetSelectText(); 
                // заполнить таблицу
                adapter.Fill(_classgroupTable);
            }
            // если таблице не присвоенно значение, то создать новую таблицу
            if (_classificatorTable == null)
            {
                _classificatorTable = Classificator.CreateTable();
            }
            // если таблица новая (не имеет записей), то заполнить эту таблицу
            if (_classificatorTable.Rows.Count <= 0)
            {
                // назначить текст комманды считывания
                command.CommandText = Classificator.GetSelectText();
                // заполнить таблицу
                adapter.Fill(_classificatorTable);
            }
            // если таблице не присвоенно значение, то создать новую таблицу
            if (_classpercentTable == null)
            {
                _classpercentTable = Classpercent.CreateTable();
            }
            // если таблица новая (не имеет записей), то заполнить эту таблицу
            if (_classpercentTable.Rows.Count <= 0)
            {
                // назначить текст комманды считывания
                command.CommandText = Classpercent.GetSelectText();
                // заполнить таблицу
                adapter.Fill(_classpercentTable);
            }
            #endregion
            
            // создание объектов BindingSource
            _classgroupBS = new BindingSource();
            _classificatorBS = new BindingSource();
            _classpercentBS = new BindingSource();

            // Привязываем BindingSource-ы к таблицам
            _classgroupBS.DataSource = _classgroupTable;
            _classificatorBS.DataSource = _classificatorTable;
            _classpercentBS.DataSource = _classpercentTable;
            //_privilegeBS.DataSource = _privilegeTable;

            // отключаем автогенерацию столбцов в GridView-ерах
            this.classgroupView.AutoGenerateColumns = false;
            this.classificatorView.AutoGenerateColumns = false;
            this.classpercentView.AutoGenerateColumns = false;

            // Привязываем BindingSource-ы к отображателями
            this.classgroupView.DataSource = _classgroupBS;
            this.classificatorView.DataSource = _classificatorBS;
            this.classpercentView.DataSource = _classpercentBS;
            // назначение обработчиков событий при смене активных (выделенных) записей
            _classgroupBS.CurrentChanged += new EventHandler(_classgroupBS_CurrentChanged);
            _classificatorBS.CurrentChanged += new EventHandler(_classificatorBS_CurrentChanged);
            // сделать активной первую запись
            _classificatorBS.MoveFirst();
        }
        #endregion

        // вызывается при выборе текущей записи в таблице Classgroup (смене текущего элемента)
        void _classgroupBS_CurrentChanged(object sender, EventArgs e)
        {
            // получение текущей (выбранной) записи таблицы Classgroup
            DataRowView curRow = (DataRowView)_classgroupBS.Current;
            // если строка не выбрана (пустое значение), то ничего не делать
            if (curRow == null)
                return;
            // установить фильтр для таблицы Classificator по ключу выбранной записи
            _classificatorBS.Filter = string.Format("{0} = {1}", Classificator.classgroupID, curRow[Classgroup.id]);
        }
        // вызывается при выборе текущей записи в таблице Classificator (смене текущего элемента)
        void _classificatorBS_CurrentChanged(object sender, EventArgs e)
        {
            // получение текущей (выбранной) записи таблицы Classificator
            DataRowView curRow = (DataRowView)_classificatorBS.Current;
            // очистить поле комментария
            this.commentBox.Clear();
            // если строка пустая, то ничего не делать
            if (curRow == null)
            {
                return;
            }
            // получаем значение описания
            string comment = (string)curRow[Classificator.description];
            // если описание отлично от 'NULL', то отображаем его для пользователя
            if(comment.ToUpper() != "NULL")
                this.commentBox.Text = comment;
            // устанавливаем фильтр для таблицы Classpercent
            _classpercentBS.Filter = string.Format("{0} = {1}", Classpercent.classificatorID, curRow[Classificator.id]);
            // если после фильтра нет строк, то скрываем таблицу Classpercent от пользователя
            this.classpersonGroupBox.Visible = (_classpercentBS.Count > 0);
        }
    }
}
