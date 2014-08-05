using System;
using System.Collections.Generic;
using System.Xml;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Pers_uchet_org.Forms
{
    public partial class EditPersonForm : Form
    {
        #region Поля

        private string _connection; // строка соединения с БД
        private string _operName; // имя оператора, работающего с формой
        private long _orgID; // ID организации, для для которой открыта форма
        private long _personID; // ID персоны, котору редактируют, или создают (при создании сначала = -1)
        private long _idocID; // ID записи документа
        private long _regadrID; // ID записи рег. адреса
        private long _factadrID; // ID записи фактического адреса
        private long _bornadrID; // ID записи адреса рождения рождения

        private string _oldSocnum;
        private string _oldFName;
        private string _oldMName;
        private string _oldLName;

        private DataTable _doctypeTable; // таблица с типами документов
        private DataTable _citizenTable; // таблица с странами (для гражданства)

        private BindingSource _doctypeBS;
        private BindingSource _citizen1BS;
        private BindingSource _citizen2BS;

        // Браузер для формирования оточета на изменение анкетных данных
        WebBrowser _wbAnketaChenged;
        #endregion

        #region Конструктор и инициализатор

        public EditPersonForm(string connection, string operatorName, long orgId)
        {
            InitializeComponent();

            adressrealCheckBox_CheckedChanged(null, null);

            _connection = connection;
            _personID = -1;
            _idocID = -1;
            _regadrID = -1;
            _factadrID = -1;
            _bornadrID = -1;
            _operName = operatorName;
            if (_operName == null || _operName.Length <= 0)
            {
                //MainForm.ShowWarningMessage("Не указан оператор.\nОбратитесь к разработчикам программы для решения данной проблемы", "Внимание");
                //this.DialogResult = DialogResult.Abort;
                _operName = "Mr.Unknown";
            }
            _orgID = orgId;
            // Отключение кнопки для печати, так как нет исходных данных для редактирования
            this.printButton.Enabled = false;
        }

        private void EditPersonForm_Load(object sender, EventArgs e)
        {
            if (MainForm.IDocTypeTable == null)
                MainForm.IDocTypeTable = IDocType.CreateTable();
            if (MainForm.CountryTable == null)
                MainForm.CountryTable = Country.CreateTable();

            _doctypeTable = MainForm.IDocTypeTable;
            _citizenTable = MainForm.CountryTable;

            _doctypeBS = new BindingSource();
            _doctypeBS.DataSource = _doctypeTable;
            _citizen1BS = new BindingSource();
            _citizen1BS.DataSource = _citizenTable;
            _citizen2BS = new BindingSource();
            _citizen2BS.DataSource = _citizenTable;

            if (_doctypeTable.Rows.Count <= 0)
            {
                SQLiteDataAdapter doctypeAdapter = new SQLiteDataAdapter(IDocType.GetSelectText(), _connection);
                doctypeAdapter.Fill(_doctypeTable);
            }
            if (_citizenTable.Rows.Count <= 0)
            {
                SQLiteDataAdapter countryAdapter = new SQLiteDataAdapter(Country.GetSelectText(), _connection);
                countryAdapter.Fill(_citizenTable);
            }
            doctypeBox.DataSource = _doctypeBS;
            doctypeBox.DisplayMember = IDocType.name;

            citizen1Box.DataSource = _citizen1BS;
            citizen1Box.DisplayMember = Country.name;
            citizen1Box.ValueMember = Country.id;
            citizen2Box.DataSource = _citizen2BS;
            citizen2Box.DisplayMember = Country.name;
            citizen2Box.ValueMember = Country.id;

            int pos = _citizen1BS.Find(Country.name, "ПМР");
            if (pos != -1)
                _citizen1BS.Position = pos;
            else
                _citizen1BS.MoveFirst();

            _citizen2BS.MoveFirst();
            _doctypeBS.MoveFirst();
            sexBox.SelectedIndex = sexBox.Items.Count > 0 ? 0 : -1;
        }

        #endregion

        #region Свойство

        public long PersonId
        {
            get { return _personID; }
            set
            {
                _personID = value;
                SQLiteCommand command = new SQLiteCommand();
                try
                {
                    command.Connection = new SQLiteConnection(_connection);
                    command.CommandText = PersonInfo.GetSelectText(_personID);

                    if (command.Connection.State != ConnectionState.Open)
                        command.Connection.Open();
                    SQLiteDataReader reader;
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        // заполнение полей формы данными Персоны
                        socNumBox.Text = reader[PersonInfo.socNumber] as string;
                        fNameBox.Text = reader[PersonInfo.fname] as string;
                        mNameBox.Text = reader[PersonInfo.mname] as string;
                        lNameBox.Text = reader[PersonInfo.lname] as string;

                        _oldSocnum = socNumBox.Text.Trim('-', ' ');
                        _oldFName = fNameBox.Text;
                        _oldMName = mNameBox.Text;
                        _oldLName = lNameBox.Text;

                        object sexCode = reader[PersonInfo.sex];
                        sexBox.SelectedIndex = sexCode is DBNull ? -1 : (int)sexCode;
                        string dateStr = reader[PersonInfo.birthday] as string;
                        birthdayBox.Value = DateTime.Parse(dateStr);
                        int ifind = _citizen1BS.Find(Country.id, reader[PersonInfo.citizen1]);
                        _citizen1BS.Position = ifind;
                        ifind = _citizen2BS.Find(Country.id, reader[PersonInfo.citizen2]);
                        _citizen2BS.Position = ifind;
                        _idocID = (long)reader[PersonInfo.docID];
                        _regadrID = (long)reader[PersonInfo.regadrID];
                        _factadrID = (long)reader[PersonInfo.factadrID];
                        Object placeObj = reader[PersonInfo.birthplaceID];
                        _bornadrID = placeObj is DBNull ? -1 : (long)placeObj;

                        reader.Close();
                        //command.Connection.Close();

                        if (_regadrID == _factadrID)
                        {
                            adressrealCheckBox.Checked = false;
                            _factadrID = -1;
                        }
                        else
                        {
                            adressrealCheckBox.Checked = true;
                        }

                        command.CommandText = IDocInfo.GetSelectCommandText(_idocID);
                        //command.Connection.Open();
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            ifind = _doctypeBS.Find(IDocType.id, reader[IDocInfo.docTypeID]);
                            _doctypeBS.Position = ifind;
                            docseriesBox.Text = reader[IDocInfo.series] as string;
                            docnumBox.Text = reader[IDocInfo.number] as string;
                            dateStr = reader[IDocInfo.date] as string;
                            docdateBox.Value = DateTime.Parse(dateStr);
                            docorgBox.Text = reader[IDocInfo.org] as string;
                        }
                        reader.Close();
                        // заполнение полей формы данными Адреса прописки
                        command.CommandText = Adress.GetSelectCommandText(_regadrID);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            regCountryBox.Text = reader[Adress.country] as string;
                            regAreaBox.Text = reader[Adress.area] as string;
                            regRegionBox.Text = reader[Adress.region] as string;
                            regCityBox.Text = reader[Adress.city] as string;
                            regIndexBox.Text = reader[Adress.zipCode] as string;
                            regStreetBox.Text = reader[Adress.street] as string;
                            regBuildingBox.Text = reader[Adress.building] as string;
                            regAppartmentBox.Text = reader[Adress.appartment] as string;
                        }
                        reader.Close();
                        // заполнение полей формы данными Адреса проживания
                        if (_factadrID != -1 && _factadrID != _regadrID)
                        {
                            command.CommandText = Adress.GetSelectCommandText(_factadrID);
                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                factCountryBox.Text = reader[Adress.country] as string;
                                factAreaBox.Text = reader[Adress.area] as string;
                                factRegionBox.Text = reader[Adress.region] as string;
                                factCityBox.Text = reader[Adress.city] as string;
                                factIndexBox.Text = reader[Adress.zipCode] as string;
                                factStreetBox.Text = reader[Adress.street] as string;
                                factBuildingBox.Text = reader[Adress.building] as string;
                                factAppartmentBox.Text = reader[Adress.appartment] as string;
                            }
                            reader.Close();
                        }
                        // заполнение полей формы данными Места рождения
                        command.CommandText = Adress.GetSelectCommandText(_bornadrID);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            bornCountryBox.Text = reader[Adress.country] as string;
                            bornAreaBox.Text = reader[Adress.area] as string;
                            bornRegionBox.Text = reader[Adress.region] as string;
                            bornCityBox.Text = reader[Adress.city] as string;
                        }
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("Персона с указанным идентификатором не найдена!", "Неверный идентификатор",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    // Включение кнопки для печати, так как есть исходные данные для редактирования
                    this.printButton.Enabled = true;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        #endregion

        #region Методы - свои

        private bool ValidateData()
        {
            bool res = true;
            string err = "";

            if (lNameBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректная фамилия.";
                res = false;
            }
            if (fNameBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректное имя.";
                res = false;
            }
            if (sexBox.SelectedIndex == -1)
            {
                err += "\nНекорректный пол.";
                res = false;
            }
            if (!PersonInfo.IsCorrectSocNumber(socNumBox.Text.Trim('-', ' ')))
            {
                err += "\nНекорректный страховой номер.";
                res = false;
            }
            if (citizen1Box.SelectedValue == null || (long)citizen1Box.SelectedValue <= 0)
            {
                err += "\nНе указано основное гражданство.";
                res = false;
            }
            //if (regCityBox.Text.Trim().Length <= 0)
            //{
            //    err += "\nНекорректное название города прописки";
            //    res &= false;
            //}
            //if (this.adressrealCheckBox.Checked && factCityBox.Text.Trim().Length <= 0)
            //{
            //    err += "\nНекорректное название города проживания.";
            //    res &= false;
            //}
            //if (bornCityBox.Text.Trim().Length <= 0)
            //{
            //    err += "\nНекорректное название города рождения.";
            //    res &= false;
            //}
            if (doctypeBox.SelectedIndex < 0)
            {
                err += "\nНе указан тип документа.";
                res = false;
            }
            //if (this.docnumBox.Text.Trim().Length <= 0)
            //{
            //    err += "\nНекорректная серия документа.";
            //    res &= false;
            //}

            if (!res)
                MainForm.ShowWarningMessage("Были обнаружены следующие некорректные данные:" + err,
                    "Введены некорректные данные");
            return res;
        }

        private void SetDocValues(SQLiteCommand command)
        {
            DataRowView doctype = _doctypeBS.Current as DataRowView;

            command.Parameters[IDocInfo.pId].Value = _idocID;
            if (doctype != null)
                command.Parameters[IDocInfo.pDocTypeID].Value = doctype[IDocType.id];
            command.Parameters[IDocInfo.pSeries].Value = docseriesBox.Text.Trim();
            command.Parameters[IDocInfo.pNumber].Value = docnumBox.Text.Trim();
            command.Parameters[IDocInfo.pDate].Value = docdateBox.Value.ToString("yyyy-MM-dd");
            command.Parameters[IDocInfo.pOrg].Value = docorgBox.Text.Trim();
        }

        private void SetRegAdressValues(SQLiteCommand command)
        {
            command.Parameters[Adress.pId].Value = _regadrID;
            command.Parameters[Adress.pCountry].Value = regCountryBox.Text.Trim();
            command.Parameters[Adress.pArea].Value = regAreaBox.Text.Trim();
            command.Parameters[Adress.pRegion].Value = regRegionBox.Text.Trim();
            command.Parameters[Adress.pCity].Value = regCityBox.Text.Trim();
            command.Parameters[Adress.pZipCode].Value = regIndexBox.Text.Trim();
            command.Parameters[Adress.pStreet].Value = regStreetBox.Text.Trim();
            command.Parameters[Adress.pBuilding].Value = regBuildingBox.Text.Trim();
            command.Parameters[Adress.pAppartment].Value = regAppartmentBox.Text.Trim();
            //command.Parameters[Adress.pPhone].Value;
        }

        private void SetFactAdressValues(SQLiteCommand command)
        {
            command.Parameters[Adress.pId].Value = _factadrID;
            command.Parameters[Adress.pCountry].Value = factCountryBox.Text.Trim();
            command.Parameters[Adress.pArea].Value = factAreaBox.Text.Trim();
            command.Parameters[Adress.pRegion].Value = factRegionBox.Text.Trim();
            command.Parameters[Adress.pCity].Value = factCityBox.Text.Trim();
            command.Parameters[Adress.pZipCode].Value = factIndexBox.Text.Trim();
            command.Parameters[Adress.pStreet].Value = factStreetBox.Text.Trim();
            command.Parameters[Adress.pBuilding].Value = factBuildingBox.Text.Trim();
            command.Parameters[Adress.pAppartment].Value = factAppartmentBox.Text.Trim();
            //command.Parameters[Adress.pPhone].Value;
        }

        private void SetBornplaceValues(SQLiteCommand command)
        {
            command.Parameters[Adress.pId].Value = _bornadrID;
            command.Parameters[Adress.pCountry].Value = bornCountryBox.Text.Trim();
            command.Parameters[Adress.pArea].Value = bornAreaBox.Text.Trim();
            command.Parameters[Adress.pRegion].Value = bornRegionBox.Text.Trim();
            command.Parameters[Adress.pCity].Value = bornCityBox.Text.Trim();
            //command.Parameters[Adress.pZipCode].Value;
            //command.Parameters[Adress.pStreet].Value;
            //command.Parameters[Adress.pBuilding].Value;
            //command.Parameters[Adress.pAppartment].Value;
            //command.Parameters[Adress.pPhone].Value;
        }

        private void SetPersonValues(SQLiteCommand command)
        {
            long citizen1Id;
            long citizen2Id;
            DataRowView citizen1 = _citizen1BS.Current as DataRowView;
            DataRowView citizen2 = _citizen2BS.Current as DataRowView;
            if (citizen1 == null)
                citizen1Id = -1;
            else
                citizen1Id = (long)citizen1[Adress.id];
            if (citizen2 == null)
                citizen2Id = -1;
            else
                citizen2Id = (long)citizen2[Adress.id];

            if (citizen1Id == -1 && citizen2Id != -1)
            {
                citizen1Id = citizen2Id;
                citizen2Id = -1;
            }

            command.Parameters[PersonInfo.pId].Value = _personID;
            command.Parameters[PersonInfo.pSocNumber].Value = socNumBox.Text.Trim('-', ' ');
            command.Parameters[PersonInfo.pFname].Value = fNameBox.Text.Trim();
            command.Parameters[PersonInfo.pMname].Value = mNameBox.Text.Trim();
            command.Parameters[PersonInfo.pLname].Value = lNameBox.Text.Trim();
            command.Parameters[PersonInfo.pBirthday].Value = birthdayBox.Value.ToString("yyyy-MM-dd");
            command.Parameters[PersonInfo.pCitizen1].Value = citizen1Id;
            command.Parameters[PersonInfo.pCitizen2].Value = citizen2Id;
            command.Parameters[PersonInfo.pDocID].Value = _idocID;
            command.Parameters[PersonInfo.pRegadrID].Value = _regadrID;
            command.Parameters[PersonInfo.pFactadrID].Value = _factadrID;
            command.Parameters[PersonInfo.pBornplaceID].Value = _bornadrID;
            command.Parameters[PersonInfo.pSex].Value = sexBox.SelectedIndex;
        }

        private void InsertDataToDB()
        {
            // создание соединения с БД
            SQLiteConnection connection = new SQLiteConnection(_connection);
            // создание команд для вставки
            SQLiteCommand insPerson = PersonInfo.CreateInsertCommand();
            SQLiteCommand insRegadr = Adress.CreateInsertCommand();
            SQLiteCommand insFactadr = Adress.CreateInsertCommand();
            SQLiteCommand insBornadr = Adress.CreateInsertCommand();
            SQLiteCommand insIDoc = IDocInfo.CreateInsertCommand();
            SQLiteCommand insFixdata = new SQLiteCommand();
            SQLiteCommand insPersonOrg = new SQLiteCommand();
            // присвоение подключения командам
            insPerson.Connection = connection;
            insRegadr.Connection = connection;
            insFactadr.Connection = connection;
            insBornadr.Connection = connection;
            insIDoc.Connection = connection;
            insFixdata.Connection = connection;
            insPersonOrg.Connection = connection;
            // заполнение команд параметрами
            SetRegAdressValues(insRegadr);
            SetFactAdressValues(insFactadr);
            SetBornplaceValues(insBornadr);
            SetDocValues(insIDoc);
            // открытие соединения
            connection.Open();
            // создание транзакции
            SQLiteTransaction transaction = connection.BeginTransaction();
            // присвоение транзакции командам
            insPersonOrg.Transaction =
                insFixdata.Transaction =
                    insFactadr.Transaction =
                        insRegadr.Transaction =
                            insBornadr.Transaction = insIDoc.Transaction = insPerson.Transaction = transaction;

            // выполнение запросов для вставки данных в смежные таблицы
            // вставка прописки и получение ID записи
            _regadrID = (long)insRegadr.ExecuteScalar();
            // если фактический адресс проживания отличается от прописки
            if (adressrealCheckBox.Checked)
            {
                // вставка адресса проживания и получение ID записи
                _factadrID = (long)insFactadr.ExecuteScalar();
            }
            else // если адресс прописки совпадает с адрессом фактического проживания
            {
                _factadrID = _regadrID;
            }
            // вставка места рождения и получение ID записи
            _bornadrID = (long)insBornadr.ExecuteScalar();
            // вставка документа и получение ID записи
            _idocID = (long)insIDoc.ExecuteScalar();
            // заполнение команды вставки Персоны параметрами, 
            // в том числе определенными ID ранее вставленных записей адрессов и документа
            SetPersonValues(insPerson);
            // исполнение команды вставки Персоны
            _personID = (long)insPerson.ExecuteScalar();
            // выполнение запроса на фиксацию факта создания записи
            insFixdata.CommandText = FixData.GetReplaceText(PersonInfo.tablename, FixData.FixType.New, _personID,
                _operName, DateTime.Now);
            insFixdata.ExecuteNonQuery();

            insPersonOrg.CommandText = PersonOrg.GetInsertPersonOrgText(_personID, _orgID);
            insPersonOrg.ExecuteScalar();
            // подтверждение транзакции
            insRegadr.Transaction.Commit();
            // закрытие соединения
            connection.Close();
        }

        private void UpdateDataToDB()
        {
            // создание соединения с БД
            SQLiteConnection connection = new SQLiteConnection(_connection);
            // команда для фиксации факта изменения записи
            SQLiteCommand fixdata =
                new SQLiteCommand(FixData.GetReplaceText(PersonInfo.tablename, FixData.FixType.Edit, _personID,
                    _operName, DateTime.Now));
            // создание команд для обновления
            SQLiteCommand updatePerson = PersonInfo.CreateUpdateCommand();
            // инициализация комманды для Адресса прописки
            SQLiteCommand commandRegadr = _regadrID == -1 ? Adress.CreateInsertCommand() : Adress.CreateUpdateCommand();

            // инициализация комманды для Адресса проживания
            SQLiteCommand commandFactadr = null;
            if (adressrealCheckBox.Checked)
            {
                commandFactadr = _factadrID == -1 ? Adress.CreateInsertCommand() : Adress.CreateUpdateCommand();
            }

            // инициализация комманды для Места рождения
            SQLiteCommand commandBornadr = _bornadrID == -1
                ? Adress.CreateInsertCommand()
                : Adress.CreateUpdateCommand();

            // инициализация комманды для документа
            SQLiteCommand commandIDoc = _idocID == -1 ? IDocInfo.CreateInsertCommand() : IDocInfo.CreateUpdateCommand();

            // присвоение подключения командам
            updatePerson.Connection = connection;
            commandRegadr.Connection = connection;
            if (commandFactadr != null) commandFactadr.Connection = connection;
            commandBornadr.Connection = connection;
            commandIDoc.Connection = connection;
            fixdata.Connection = connection;
            // заполнение команд параметрами
            SetRegAdressValues(commandRegadr);
            if (commandFactadr != null) SetFactAdressValues(commandFactadr);
            SetBornplaceValues(commandBornadr);
            SetDocValues(commandIDoc);

            // открытие соединения
            connection.Open();
            // создание транзакции
            SQLiteTransaction transaction = connection.BeginTransaction();
            // присвоение транзакции командам
            fixdata.Transaction =
                commandRegadr.Transaction =
                    commandBornadr.Transaction =
                        commandIDoc.Transaction =
                            updatePerson.Transaction = transaction;
            if (commandFactadr != null) commandFactadr.Transaction = transaction;

            // выполнение запросов для вставки данных в смежные таблицы
            if (_regadrID == -1)
                // вставка прописки и получение ID записи
                _regadrID = (long)commandRegadr.ExecuteScalar();
            else
                // обновление записи
                commandRegadr.ExecuteNonQuery();

            // если фактический адресс проживания отличается от прописки
            if (commandFactadr != null && adressrealCheckBox.Checked)
            {
                if (_factadrID == -1)
                    // вставка адресса проживания и получение ID записи
                    _factadrID = (long)commandFactadr.ExecuteScalar();
                else
                    // обновление записи
                    commandFactadr.ExecuteNonQuery();
            }
            else // если адресс прописки совпадает с адрессом фактического проживания
            {
                _factadrID = _regadrID;
            }

            if (_bornadrID == -1)
                // вставка места рождения и получение ID записи
                _bornadrID = (long)commandBornadr.ExecuteScalar();
            else
                // обновление записи
                commandBornadr.ExecuteNonQuery();

            if (_idocID == -1)
                // вставка документа и получение ID записи
                _idocID = (long)commandIDoc.ExecuteScalar();
            else
                commandIDoc.ExecuteNonQuery();

            // заполнение команды вставки Персоны параметрами, 
            // в том числе определенными ID ранее вставленных записей адрессов и документа
            SetPersonValues(updatePerson);
            // исполнение команды обновления Персоны
            updatePerson.ExecuteNonQuery();
            fixdata.ExecuteNonQuery();
            // подтаверждение транзакции
            transaction.Commit();
            // закрытие соединения
            connection.Close();
        }

        #endregion

        #region Методы - обработчики событий

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                //MainForm.ShowWarningMessage("Введенные данные не прошли проверку на корректность!", "Внимание");
                return;
            }

            socNumBox.Text = PersonInfo.CorrectSocnumberRusToEn(socNumBox.Text.Trim());
            string socNumber = socNumBox.Text.Trim('-', ' ');
            if (PersonInfo.IsExist(_personID, socNumber, _connection))
            {
                MainForm.ShowWarningMessage("Анкетные данные с таким номером уже присутствуют в базе данных!",
                    "Внимание");
                return;
            }

            if (_personID == -1)
            {
                InsertDataToDB();
            }
            else
            {
                UpdateDataToDB();
            }
            MainForm.ShowInfoMessage("Данные успешно сохранены!", "Сохранение");
            Close();
        }

        private void adressrealCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            adressrealGroupBox.Enabled = adressrealCheckBox.Checked;
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if ((_oldSocnum == null && _oldFName == null && _oldMName == null && _oldLName == null) ||
                (_oldSocnum.Length == 0 && _oldFName.Length == 0 && _oldMName.Length == 0 && _oldLName.Length == 0))
            {
                MainForm.ShowInfoMessage("Нет исходных данных для оформления заявления об изменении анкетных данных", "Внимание");
                return;
            }

            string regAddress = "";
            if (this.regCountryBox.Text.Length > 0)
                regAddress = this.regCountryBox.Text + " ";
            if (this.regAreaBox.Text.Length > 0)
                regAddress += this.regAreaBox.Text + " ";
            if (this.regRegionBox.Text.Length > 0)
                regAddress += this.regRegionBox.Text + " ";
            if (this.regCityBox.Text.Length > 0)
                regAddress += this.regCityBox.Text + " ";
            if (this.regStreetBox.Text.Length > 0)
                regAddress += "ул." + this.regStreetBox.Text + " ";
            if (this.regBuildingBox.Text.Length > 0)
                regAddress += "д." + this.regBuildingBox.Text + " ";
            if (this.regAppartmentBox.Text.Length > 0)
                regAddress += "кв." + this.regAppartmentBox.Text;

            string factAddress = "";
            if (this.adressrealCheckBox.Checked)
            {
                if (this.factCountryBox.Text.Length > 0)
                    factAddress = this.factCountryBox.Text + " ";
                if (this.factAreaBox.Text.Length > 0)
                    factAddress += this.factAreaBox.Text + " ";
                if (this.factRegionBox.Text.Length > 0)
                    factAddress += this.factRegionBox.Text + " ";
                if (this.factCityBox.Text.Length > 0)
                    factAddress += this.factCityBox.Text + " ";
                if (this.factStreetBox.Text.Length > 0)
                    factAddress += "ул." + this.factStreetBox.Text + " ";
                if (this.factBuildingBox.Text.Length > 0)
                    factAddress += "д." + this.factBuildingBox.Text + " ";
                if (this.factAppartmentBox.Text.Length > 0)
                    factAddress += "кв." + this.factAppartmentBox.Text;
            }
            DataRowView citizen1Row = _citizen1BS.Current as DataRowView;
            DataRowView citizen2Row = _citizen1BS.Current as DataRowView;
            DataRowView doctypeRow = _doctypeBS.Current as DataRowView;


            // Формирования XML для отчета для заявления об изменении анкетных данных
            Dictionary<string, string> personDict = new Dictionary<string,string>(30);
            personDict["old_socnum"] = _oldSocnum;
            personDict["old_fname"] = _oldFName;
            personDict["old_mname"] = _oldMName;
            personDict["old_lname"] = _oldLName;

            personDict[PersonView.lName] = this.lNameBox.Text;
            personDict[PersonView.fName] = this.fNameBox.Text;
            personDict[PersonView.mName] = this.mNameBox.Text;
            personDict[PersonView.socNumber] = this.socNumBox.Text;
            personDict[PersonView.birthday] = this.birthdayBox.Value.ToString("dd.MM.yyyy");
            personDict[PersonView.sex] = this.sexBox.Text;
            if (doctypeRow != null) personDict[PersonView.docType] = doctypeRow[DocTypes.name] as string;
            personDict[PersonView.docSeries] = this.docseriesBox.Text;
            personDict[PersonView.docNumber] = this.docnumBox.Text;
            personDict[PersonView.docDate] = this.docdateBox.Value.ToString("dd.MM.yyyy");
            personDict[PersonView.docOrg] = this.docorgBox.Text;
            personDict[PersonView.regAdressZipcode] = this.regIndexBox.Text;
            personDict[PersonView.regAdress] = regAddress;
            personDict[PersonView.factAdressZipcode] = this.factIndexBox.Text;
            personDict[PersonView.factAdress] = factAddress;
            personDict[PersonView.bornAdressCountry] = this.bornCountryBox.Text;
            personDict[PersonView.bornAdressArea] = this.bornAreaBox.Text;
            personDict[PersonView.bornAdressRegion] = this.bornAreaBox.Text;
            personDict[PersonView.bornAdressCity] = this.bornCityBox.Text;
            if (citizen1Row != null)
            {
                personDict["citizen1_id"] = citizen1Row[Country.id].ToString();
                personDict["citizen1"] = citizen1Row[Country.name] as string;
            }
            if (citizen2Row != null)
            {
                personDict["citizen2_id"] = citizen2Row[Country.id].ToString();
                personDict["citizen2"] = citizen2Row[Country.name] as string;
            }

            XmlDocument xml = XmlData.Adv2Xml(personDict);
            ////////
            if (_wbAnketaChenged == null)
            {
                _wbAnketaChenged = new WebBrowser();
                _wbAnketaChenged.Visible = false;
                _wbAnketaChenged.Parent = this;
                _wbAnketaChenged.ScriptErrorsSuppressed = true;
                _wbAnketaChenged.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(_wbAnketaChenged_DocumentCompleted);
            }
            _wbAnketaChenged.Tag = xml;
            string file = System.IO.Path.GetFullPath(Properties.Settings.Default.report_adv2);
            _wbAnketaChenged.Navigate(file);
        }

        void _wbAnketaChenged_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = sender as WebBrowser;
            if (wb == null)
            {
                return;
            }
            XmlDocument xml = wb.Tag as XmlDocument;
            if (xml == null)
            {
                MainForm.ShowInfoMessage("Отчет не может быть отображен, та как нет входных данных!", "Внимание");
                return;
            }
            HtmlDocument htmlDoc = wb.Document;
            htmlDoc.InvokeScript("setAllData", new object[] { xml.InnerXml });
            //MyPrinter.ShowWebPage(wb);
            MyPrinter.ShowPrintPreviewWebPage(wb);
        }
        #endregion
    }
}