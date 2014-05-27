using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Pers_uchet_org.Forms
{
    public partial class OrgForm : Form
    {
        #region Поля

        private string _connection;
        private DataTable _orgTable;
        private BindingSource _orgBS;
        private SQLiteDataAdapter _orgAdapter;

        private DialogResult _dialogResult = DialogResult.Cancel;

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
            orgView.AutoGenerateColumns = false;

            _orgAdapter = Org.CreateAdapter(_connection);
                //new SQLiteDataAdapter(Org.GetSelectCommandText(), _connection);
            _orgAdapter.Fill(_orgTable);

            // соединяем прослойки с таблицами
            _orgBS.DataSource = _orgTable;

            // присоединяем GridView-еры к источникам данных (таблицам) через прослойку (BindingSource-ы)
            // соединяем GridView-еры с прослойками
            orgView.DataSource = _orgBS;
        }

        #endregion

        #region Методы - обработчики событий

        private void _orgBS_CurrentChanged(object sender, EventArgs e)
        {
            regnumorgBox.Text = "";
            nameorgBox.Text = "";
            chiefpostorgBox.Text = "";
            chieffioorgBox.Text = "";
            bookerfioorgBox.Text = "";

            DataRowView row = _orgBS.Current as DataRowView;

            if (row != null)
            {
                regnumorgBox.Text = row[Org.regnum] as string;
                nameorgBox.Text = row[Org.name] as string;
                chiefpostorgBox.Text = row[Org.chief_post] as string;
                chieffioorgBox.Text = row[Org.chief_fio] as string;
                bookerfioorgBox.Text = row[Org.booker_fio] as string;
            }
        }

        //private void saveButton_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _orgAdapter.Update(_orgTable);
        //        MessageBox.Show(this, "Данные были успешно сохранены", "Сохранение прошло успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(this, "Были обнаружены ошибки при попытке сохранить данные в базу данных. Сообщение: " + err, "Сохранение не было осуществено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        //private void closeButton_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        private void OrgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = _dialogResult;
        }

        #endregion

        private void addOrgStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                EditOrgForm tmpForm = new EditOrgForm(_connection);
                tmpForm.Owner = this;
                DialogResult dRes = tmpForm.ShowDialog(this);
                if (dRes == DialogResult.OK)
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
                        _orgAdapter.Update(_orgTable);
                        _orgTable.Clear();
                        _orgAdapter.Fill(_orgTable);
                        _orgBS.Position = pos;
                        MainForm.ShowInfoMessage("Организация успешно добавлена!", "Добавление организации");
                        _dialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MainForm.ShowErrorMessage(ex.Message, "Ошибка добавления организации");
            }
        }

        private void editOrgStripButton_Click(object sender, EventArgs e)
        {
            DataRowView curOrg = (DataRowView) _orgBS.Current;
            if (curOrg == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора организации");
                return;
            }
            EditOrgForm tmpForm = new EditOrgForm(_connection);
            tmpForm.Owner = this;
            tmpForm.OrgId = (long) curOrg[Org.id];
            tmpForm.RegnumOrg = (string) curOrg[Org.regnum];
            tmpForm.NameOrg = (string) curOrg[Org.name];
            tmpForm.ChiefpostOrg = (string) curOrg[Org.chief_post];
            tmpForm.ChieffioOrg = (string) curOrg[Org.chief_fio];
            tmpForm.BookerfioOrg = (string) curOrg[Org.booker_fio];
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
                _orgAdapter.Update(_orgTable);
                _orgTable.Clear();
                _orgAdapter.Fill(_orgTable);
                _orgBS.Position = pos;
                MainForm.ShowInfoMessage("Изменения успешно сохранены!", "Изменение организации");
                _dialogResult = DialogResult.OK;
            }
        }

        private void delOrgStripButton_Click(object sender, EventArgs e)
        {
            DataRowView curOrg = (DataRowView) _orgBS.Current;
            if (curOrg == null)
            {
                MainForm.ShowInfoMessage("Необходимо выбрать запись!", "Ошибка выбора организации");
                return;
            }

            long countPersonId = PersonOrg.GetCountPersonId((long) (_orgBS.Current as DataRowView)[Org.id], _connection);
            if (countPersonId != -1 && countPersonId > 0)
            {
                MainForm.ShowInfoMessage(
                    "Нельзя удалить организацию,\nтак как в данной организации имеются работники!", "Предупреждение");
                return;
            }

            DialogResult dRes = MainForm.ShowQuestionMessage("Вы действительно желаете удалить организацию?",
                "Удаление организации");
            if (dRes == DialogResult.Yes)
            {
                _orgBS.Remove(curOrg);
                _orgAdapter.Update(_orgTable);
                //_orgAdapter.Fill(_orgTable);

                _dialogResult = DialogResult.OK;
            }
        }
    }
}