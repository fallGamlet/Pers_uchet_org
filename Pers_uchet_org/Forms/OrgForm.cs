using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org.Forms
{
    public partial class OrgForm : Form
    {
        #region Поля
        string _connection;
        DataTable _orgTable;
        BindingSource _orgBS;
        SQLiteDataAdapter _orgAdapter;
        #endregion

        #region Конструктор и инициализация
        public OrgForm(string connection)
        {
            InitializeComponent();

            _connection = connection;
        }

        private void OrgForm_Load(object sender, EventArgs e)
        {
            // создание объеков таблиц
            _orgTable = Org.CreateTable();
            // создание объектов соединителей таблиц и отобразителей (GridView)
            _orgBS = new BindingSource();
            // создание обработчика событий смены выбранной (выделенной) огранизации
            _orgBS.CurrentChanged += new EventHandler(_orgBS_CurrentChanged);
            // отмена автогенерации столбцов в GridView-ерах
            this.orgView.AutoGenerateColumns = false;

            _orgAdapter = Org.CreateAdapter(_connection); //new SQLiteDataAdapter(Org.GetSelectCommandText(), _connection);
            _orgAdapter.Fill(_orgTable);

            // присоединяем GridView-еры к источникам данных (таблицам) через прослойку (BindingSource-ы)
            // соединяем GridView-еры с прослойками
            this.orgView.DataSource = _orgBS;
            // соединяем прослойки с таблицами
            _orgBS.DataSource = _orgTable;
        }
        #endregion

        #region Методы - обработчики событий
        void _orgBS_CurrentChanged(object sender, EventArgs e)
        {
            DataRowView row = _orgBS.Current as DataRowView;

            if (row != null)
            {
                this.regnumorgBox.Text = row[Org.regnum] as string;
                this.nameorgBox.Text = row[Org.name] as string;
                this.chiefpostorgBox.Text = row[Org.chief_post] as string;
                this.chieffioorgBox.Text = row[Org.chief_fio] as string;
                this.bookerfioorgBox.Text = row[Org.booker_fio] as string;
            }
        }

        private void addorgButton_Click(object sender, EventArgs e)
        {
            EditOrgForm tmpForm = new EditOrgForm();
            tmpForm.Owner = this;
            DialogResult dRes = tmpForm.ShowDialog(this);
            if (dRes == System.Windows.Forms.DialogResult.OK)
            {
                DataRowView row = _orgBS.AddNew() as DataRowView;
                if (row != null)
                {
                    row.BeginEdit();
                    row[Org.regnum] = tmpForm.RegnumOrg;
                    row[Org.name] = tmpForm.NameOrg;
                    row[Org.chief_post] = tmpForm.ChiefpostOrg;
                    row[Org.chief_fio] = tmpForm.ChieffioOrg;
                    row[Org.booker_fio] = tmpForm.BookerfioOrg;
                    row.EndEdit();

                    int pos = _orgBS.Position;
                    int orgCount = _orgAdapter.Update(_orgTable);
                    _orgAdapter.Fill(_orgTable);
                    _orgBS.Position = pos;
                    //MessageBox.Show(this, "Добавление записи об организации прошло успешно", "Добавление записи",
                    //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Не удалось произвести добавление!", "Добавление организации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void editorgButton_Click(object sender, EventArgs e)
        {
            DataRowView curOrg = (DataRowView)_orgBS.Current;
            if (curOrg == null)
            {
                MessageBox.Show("Сначала необходимо выбрать организацию");
                return;
            }
            EditOrgForm tmpForm = new EditOrgForm();
            tmpForm.Owner = this;
            tmpForm.RegnumOrg = (string)curOrg[Org.regnum];
            tmpForm.NameOrg = (string)curOrg[Org.name];
            tmpForm.ChiefpostOrg = (string)curOrg[Org.chief_post];
            tmpForm.ChieffioOrg = (string)curOrg[Org.chief_fio];
            tmpForm.BookerfioOrg = (string)curOrg[Org.booker_fio];
            DialogResult dRes = tmpForm.ShowDialog(this);
            if (dRes == DialogResult.OK)
            {
                curOrg.BeginEdit();
                curOrg[Org.regnum] = tmpForm.RegnumOrg;
                curOrg[Org.name] = tmpForm.NameOrg;
                curOrg[Org.chief_post] = tmpForm.ChiefpostOrg;
                curOrg[Org.chief_fio] = tmpForm.ChieffioOrg;
                curOrg[Org.booker_fio] = tmpForm.BookerfioOrg;
                curOrg.EndEdit();

                int pos = _orgBS.Position;
                int orgCount = _orgAdapter.Update(_orgTable);
                _orgAdapter.Fill(_orgTable);
                _orgBS.Position = pos;
                //MessageBox.Show(this, "Изменение записи об организации прошло успешно", "Изменение записи",
                //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void removeorgButton_Click(object sender, EventArgs e)
        {
            DataRowView curOrg = (DataRowView)_orgBS.Current;
            if (curOrg == null)
            {
                MessageBox.Show("Сначала необходимо выбрать организацию");
                return;
            }
            DialogResult dRes = MessageBox.Show(this,
                                            "Вместе с организацией произойдет удаление всех связей с этой организвцией!\nВы действительно желаете удалить организацию?",
                                            "Удаление организации",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dRes == DialogResult.Yes)
            {
                _orgBS.Remove(curOrg);
                _orgAdapter.Update(_orgTable);
                _orgAdapter.Fill(_orgTable);

                MessageBox.Show(this,
                                "Удаление зиписи об организации прошло успешно",
                                "Удаление организации",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _orgAdapter.Update(_orgTable);
                MessageBox.Show(this, "Данные были успешно сохранены", "Сохранение прошло успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, "Бали обнаружены ошибки при попытке сохранить данные в базу данных. Сообщение: " + err, "Сохранение не было осуществено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
