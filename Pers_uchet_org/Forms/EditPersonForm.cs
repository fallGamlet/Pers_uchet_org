﻿using System;
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
    public partial class EditPersonForm : Form
    {
        #region Поля
        string _connection;
        bool _el;
        long _personID;
        long _idocID;
        long _regadrID;
        long _factadrID;
        long _bornadrID;

        DataTable _doctypeTable;
        DataTable _citizenTable;

        BindingSource _doctypeBS;
        BindingSource _citizen1BS;
        BindingSource _citizen2BS;

        SQLiteCommand _select;
        SQLiteCommand _insert;
        SQLiteCommand _update;
        #endregion

        #region Конструктор и инициализатор
        public EditPersonForm(string connection)
        {
            InitializeComponent();

            this.adressrealCheckBox_CheckedChanged(null, null);

            _connection = connection;
            _personID = -1;
            _idocID = -1;
            _regadrID = -1;
            _factadrID = -1;
            _bornadrID = -1;
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

            SQLiteDataAdapter countryAdapter = new SQLiteDataAdapter(Country.GetSelectText(), _connection);
            SQLiteDataAdapter doctypeAdapter = new SQLiteDataAdapter(IDocType.GetSelectText(), _connection);

            if (_doctypeTable.Rows.Count <= 0)
                doctypeAdapter.Fill(_doctypeTable);
            if (_citizenTable.Rows.Count <= 0)
                countryAdapter.Fill(_citizenTable);

            this.doctypeBox.DataSource = _doctypeBS;
            this.doctypeBox.DisplayMember = IDocType.name;

            this.citizen1Box.DataSource = _citizen1BS;
            this.citizen1Box.DisplayMember = Country.name;
            this.citizen1Box.ValueMember = Country.id;
            this.citizen2Box.DataSource = _citizen2BS;
            this.citizen2Box.DisplayMember = Country.name;
            this.citizen2Box.ValueMember = Country.id;

            int pos = _citizen1BS.Find(Country.name, "ПМР");
            if (pos != -1)
                _citizen1BS.Position = pos;
            else
                _citizen1BS.MoveFirst();

            _citizen2BS.MoveFirst();
            _doctypeBS.MoveFirst();
        }
        #endregion

        #region Свойство
        public long PersonID
        {
            get { return _personID; }
            set
            {
                _personID = value;
                int ifind;
                string dateStr;
                SQLiteDataReader reader;
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = new SQLiteConnection(_connection);
                command.CommandText = PersonInfo.GetSelectText(_personID);

                command.Connection.Open();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // заполнение полей формы данными Персоны
                    this.socnumBox.Text = reader[PersonInfo.socNumber] as string;
                    this.fNameBox.Text = reader[PersonInfo.fname] as string;
                    this.mNameBox.Text = reader[PersonInfo.mname] as string;
                    this.lNameBox.Text = reader[PersonInfo.lname] as string;
                    object sexCode = reader[PersonInfo.sex];
                    this.sexBox.SelectedIndex = sexCode is DBNull ? -1 : (int)sexCode;
                    dateStr = reader[PersonInfo.birthday] as string;
                    this.birthdayBox.Value = DateTime.Parse(dateStr);
                    ifind = _citizen1BS.Find(Country.id, reader[PersonInfo.citizen1]);
                    _citizen1BS.Position = ifind;
                    ifind = _citizen2BS.Find(Country.id, reader[PersonInfo.citizen2]);
                    _citizen2BS.Position = ifind;
                    _idocID = (long)reader[PersonInfo.docID];
                    _regadrID = (long)reader[PersonInfo.regadrID];
                    _factadrID = (long)reader[PersonInfo.factadrID];
                    Object placeObj = reader[PersonInfo.birthplaceID];
                    _bornadrID = placeObj is DBNull? -1: (long)placeObj;

                    reader.Close();
                    //command.Connection.Close();

                    if (_regadrID == _factadrID)
                    {
                        this.adressrealCheckBox.Checked = false;
                        _factadrID = -1;
                    }
                    else
                    {
                        this.adressrealCheckBox.Checked = true;
                    }

                    command.CommandText = IDocInfo.GetSelectCommandText(_idocID);
                    //command.Connection.Open();
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        ifind = _doctypeBS.Find(IDocType.id, reader[IDocInfo.docTypeID]);
                        _doctypeBS.Position = ifind;
                        this.docseriesBox.Text = reader[IDocInfo.series] as string;
                        this.docnumBox.Text = reader[IDocInfo.number] as string;
                        dateStr = reader[IDocInfo.date] as string;
                        this.docdateBox.Value = DateTime.Parse(dateStr);
                        this.docorgBox.Text = reader[IDocInfo.org] as string;
                    }
                    reader.Close();

                    command.CommandText = Adress.GetSelectCommandText(_regadrID);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        this.regCountryBox.Text = reader[Adress.country] as string;
                        this.regAreaBox.Text = reader[Adress.area] as string;
                        this.regRegionBox.Text = reader[Adress.region] as string;
                        this.regCityBox.Text = reader[Adress.city] as string;
                        this.regIndexBox.Text = reader[Adress.zipCode] as string;
                        this.regStreetBox.Text = reader[Adress.street] as string;
                        this.regBuildingBox.Text = reader[Adress.building] as string;
                        this.regAppartmentBox.Text = reader[Adress.appartment] as string;
                    }
                    reader.Close();

                    if (_factadrID != -1 && _factadrID != _regadrID)
                    {
                        command.CommandText = Adress.GetSelectCommandText(_factadrID);
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            this.factCountryBox.Text = reader[Adress.country] as string;
                            this.factAreaBox.Text = reader[Adress.area] as string;
                            this.factRegionBox.Text = reader[Adress.region] as string;
                            this.factCityBox.Text = reader[Adress.city] as string;
                            this.factIndexBox.Text = reader[Adress.zipCode] as string;
                            this.factStreetBox.Text = reader[Adress.street] as string;
                            this.factBuildingBox.Text = reader[Adress.building] as string;
                            this.factAppartmentBox.Text = reader[Adress.appartment] as string;
                        }
                        reader.Close();
                    }
                    command.CommandText = Adress.GetSelectCommandText(_bornadrID);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        this.bornCountryBox.Text = reader[Adress.country] as string;
                        this.bornAreaBox.Text = reader[Adress.area] as string;
                        this.bornRegionBox.Text = reader[Adress.region] as string;
                        this.bornCityBox.Text = reader[Adress.city] as string;
                    }
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Персона с указанным идентификатором не найдена!", "Неверный идентификатор", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                command.Connection.Close();
            }
        }
        #endregion

        #region Методы - свои
        private bool ValidateData()
        {
            bool res = true;
            string err = "";

            if (this.lNameBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректная фамилии.";
                res &= false;
            }
            if(this.fNameBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректное имя.";
                res &= false;
            }
            if(this.sexBox.SelectedIndex == -1)
            {
                err += "\nНекорректный пол.";
                res &= false;
            }
            if (this.citizen1Box.SelectedValue == null || (long)this.citizen1Box.SelectedValue <= 0)
            {
                err += "\nНе указано основное гражданство.";
                res &= false;
            }
            if (regCityBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректное название города прописки";
                res &= false;
            }
            if (this.adressrealCheckBox.Checked && factCityBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректное название города проживания.";
                res &= false;
            }
            if (bornCityBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректное название города рождения.";
                res &= false;
            }
            if (this.doctypeBox.SelectedIndex < 0)
            {
                err += "\nНе указан тип документа.";
                res &= false;
            }
            if (this.docnumBox.Text.Trim().Length <= 0)
            {
                err += "\nНекорректная серия документа.";
                res &= false;
            }

            if (!res)
                MessageBox.Show("Были обнаружены следующие недопустимые некорректные данные:"+err, "Введены некорректные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return res;
        }

        private void SetDocValues(SQLiteCommand command)
        {
            DataRowView doctype = _doctypeBS.Current as DataRowView;
            
            command.Parameters[IDocInfo.pId].Value = _idocID;
            if (doctype != null)
                command.Parameters[IDocInfo.pDocTypeID].Value = doctype[IDocType.id];
            command.Parameters[IDocInfo.pSeries].Value = this.docseriesBox.Text.Trim();
            command.Parameters[IDocInfo.pNumber].Value = this.docnumBox.Text.Trim();
            string dateStr = this.docdateBox.Value.ToShortDateString();
            command.Parameters[IDocInfo.pDate].Value = dateStr;
            command.Parameters[IDocInfo.pOrg].Value = this.docorgBox.Text.Trim();
        }

        private void SetRegAdressValues(SQLiteCommand command)
        {
            command.Parameters[Adress.pId].Value = _regadrID;
            command.Parameters[Adress.pCountry].Value = this.regCountryBox.Text.Trim();
            command.Parameters[Adress.pArea].Value = this.regAreaBox.Text.Trim();
            command.Parameters[Adress.pRegion].Value = this.regRegionBox.Text.Trim();
            command.Parameters[Adress.pCity].Value = this.regCityBox.Text.Trim();
            command.Parameters[Adress.pZipCode].Value = this.regIndexBox.Text.Trim();
            command.Parameters[Adress.pStreet].Value = this.regStreetBox.Text.Trim();
            command.Parameters[Adress.pBuilding].Value = this.regBuildingBox.Text.Trim();
            command.Parameters[Adress.pAppartment].Value = this.regAppartmentBox.Text.Trim();
            //command.Parameters[Adress.pPhone].Value;
        }

        private void SetFactAdressValues(SQLiteCommand command)
        {
            command.Parameters[Adress.pId].Value = _factadrID;
            command.Parameters[Adress.pCountry].Value = this.factCountryBox.Text.Trim();
            command.Parameters[Adress.pArea].Value = this.factAreaBox.Text.Trim();
            command.Parameters[Adress.pRegion].Value = this.factRegionBox.Text.Trim();
            command.Parameters[Adress.pCity].Value = this.factCityBox.Text.Trim();
            command.Parameters[Adress.pZipCode].Value = this.factIndexBox.Text.Trim();
            command.Parameters[Adress.pStreet].Value = this.factStreetBox.Text.Trim();
            command.Parameters[Adress.pBuilding].Value = this.factBuildingBox.Text.Trim();
            command.Parameters[Adress.pAppartment].Value = this.factAppartmentBox.Text.Trim();
            //command.Parameters[Adress.pPhone].Value;
        }

        private void SetBornplaceValues(SQLiteCommand command)
        {
            command.Parameters[Adress.pId].Value = _bornadrID;
            command.Parameters[Adress.pCountry].Value = this.bornCountryBox.Text.Trim();
            command.Parameters[Adress.pArea].Value = this.bornAreaBox.Text.Trim();
            command.Parameters[Adress.pRegion].Value = this.bornRegionBox.Text.Trim();
            command.Parameters[Adress.pCity].Value = this.bornCityBox.Text.Trim();
            //command.Parameters[Adress.pZipCode].Value;
            //command.Parameters[Adress.pStreet].Value;
            //command.Parameters[Adress.pBuilding].Value;
            //command.Parameters[Adress.pAppartment].Value;
            //command.Parameters[Adress.pPhone].Value;
        }

        private void SetPersonValues(SQLiteCommand command)
        {
            long citizen1ID, citizen2ID;
            DataRowView citizen1 = _citizen1BS.Current as DataRowView;
            DataRowView citizen2 = _citizen2BS.Current as DataRowView;
            if (citizen1 == null)
                citizen1ID = -1;
            else
                citizen1ID = (long)citizen1[Adress.id];
            if (citizen2 == null) 
                citizen2ID = -1;
            else 
                citizen2ID = (long)citizen2[Adress.id];

            if(citizen1ID == -1 && citizen2ID != -1)
            {
                citizen1ID = citizen2ID;
                citizen2ID = -1;
            }

            command.Parameters[PersonInfo.pId].Value = _personID;
            command.Parameters[PersonInfo.pSocNumber].Value = this.socnumBox.Text.Trim();
            command.Parameters[PersonInfo.pFname].Value = this.fNameBox.Text.Trim();
            command.Parameters[PersonInfo.pMname].Value = this.mNameBox.Text.Trim();
            command.Parameters[PersonInfo.pLname].Value = this.lNameBox.Text.Trim();
            command.Parameters[PersonInfo.pBirthday].Value = this.birthdayBox.Value.ToShortDateString();
            command.Parameters[PersonInfo.pCitizen1].Value = citizen1ID;
            command.Parameters[PersonInfo.pCitizen2].Value = citizen2ID;
            command.Parameters[PersonInfo.pDocID].Value = _idocID;
            command.Parameters[PersonInfo.pRegadrID].Value = _regadrID;
            command.Parameters[PersonInfo.pFactadrID].Value = _factadrID;
            command.Parameters[PersonInfo.pBornplaceID].Value = _bornadrID;

            command.Parameters[PersonInfo.pSex].Value = this.sexBox.SelectedIndex;
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
            // присвоение подключения командам
            insPerson.Connection = connection;
            insRegadr.Connection = connection;
            insFactadr.Connection = connection;
            insBornadr.Connection = connection;
            insIDoc.Connection = connection;
            // заполнение команд параметрами
            this.SetRegAdressValues(insRegadr);
            this.SetFactAdressValues(insFactadr);
            this.SetBornplaceValues(insBornadr);
            this.SetDocValues(insIDoc);
            // открытие соединения
            connection.Open();
            // создание транзакции
            SQLiteTransaction transaction = connection.BeginTransaction();
            // присвоение транзакции командам
            insFactadr.Transaction = insRegadr.Transaction = insBornadr.Transaction = insIDoc.Transaction = insPerson.Transaction = transaction;

            // выполнение запросов для вставки данных в смежные таблицы
            // вставка прописки и получение ID записи
            _regadrID = (long)insRegadr.ExecuteScalar();
            // если фактический адресс проживания отличается от прописки
            if (this.adressrealCheckBox.Checked)
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
            this.SetPersonValues(insPerson);
            // исполнение команды вставки Персоны
            _personID = (long)insPerson.ExecuteScalar();
            // подтаверждение транзакции
            insRegadr.Transaction.Commit();
            // закрытие соединения
            connection.Close();
        }

        private void UpdateDataToDB()
        {
            // создание соединения с БД
            SQLiteConnection connection = new SQLiteConnection(_connection);
            
            // создание команд для обновления
            SQLiteCommand updatePerson = PersonInfo.CreateUpdateCommand();
            
            // инициализация комманды для Адресса прописки
            SQLiteCommand commandRegadr = null;
            if (_regadrID == -1)
                commandRegadr = Adress.CreateInsertCommand();
            else
                commandRegadr = Adress.CreateUpdateCommand();
            
            // инициализация комманды для Адресса проживания
            SQLiteCommand commandFactadr = null;
            if (this.adressrealCheckBox.Checked)
            {
                if (_factadrID == -1)
                    commandFactadr = Adress.CreateInsertCommand();
                else
                    commandFactadr = Adress.CreateUpdateCommand();
            }
            
            // инициализация комманды для Места рождения
            SQLiteCommand commandBornadr = null;
            if (_bornadrID == -1)
                commandBornadr = Adress.CreateInsertCommand();
            else
                commandBornadr = Adress.CreateUpdateCommand();
            
            // инициализация комманды для документа
            SQLiteCommand commandIDoc = null;
            if (_idocID == -1)
                commandIDoc = IDocInfo.CreateInsertCommand();
            else
                commandIDoc = IDocInfo.CreateUpdateCommand();

            // присвоение подключения командам
            updatePerson.Connection = connection;
            commandRegadr.Connection = connection;
            if (commandFactadr != null) commandFactadr.Connection = connection;
            commandBornadr.Connection = connection;
            commandIDoc.Connection = connection;
            // заполнение команд параметрами
            this.SetRegAdressValues(commandRegadr);
            if (commandFactadr != null) this.SetFactAdressValues(commandFactadr);
            this.SetBornplaceValues(commandBornadr);
            this.SetDocValues(commandIDoc);
            
            // открытие соединения
            connection.Open();
            // создание транзакции
            SQLiteTransaction transaction = connection.BeginTransaction();
            // присвоение транзакции командам
            commandRegadr.Transaction = commandBornadr.Transaction = commandIDoc.Transaction = updatePerson.Transaction = transaction;
            if (commandFactadr != null) commandFactadr.Transaction = transaction;
            
            // выполнение запросов для вставки данных в смежные таблицы
            if (_regadrID == -1)
                // вставка прописки и получение ID записи
                _regadrID = (long)commandRegadr.ExecuteScalar();
            else
                // обновление записи
                commandRegadr.ExecuteNonQuery();

            // если фактический адресс проживания отличается от прописки
            if (commandFactadr != null && this.adressrealCheckBox.Checked)
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
            this.SetPersonValues(updatePerson);
            // исполнение команды обновления Персоны
            updatePerson.ExecuteNonQuery();

            // подтаверждение транзакции
            transaction.Commit();
            // закрытие соединения
            connection.Close();
        }
        #endregion

        #region Методы - обработчики событий
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!this.ValidateData())
            {
                MainForm.ShowWarningMessage("Введенные данные не прошли проверку на корректность!","Внимание");
                return;
            }
            if (_personID == -1)
            {
                this.socnumBox.Text = PersonInfo.CorrectSocnumber(this.socnumBox.Text.Trim());
                if (PersonInfo.IsExist(this.socnumBox.Text, _connection))
                {
                    MainForm.ShowWarningMessage("Анкетные данные с таким номером уже присутствуют в базе данных!", "Внимание");
                    return;
                }
                else
                {
                    this.InsertDataToDB();
                }
            }
            else
            {
                this.UpdateDataToDB();
            }
            this.Close();
            
        }

        private void adressrealCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.adressrealGroupBox.Enabled = this.adressrealCheckBox.Checked;
        }
        #endregion
    }
}
