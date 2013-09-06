namespace Pers_uchet_org
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalPage = new System.Windows.Forms.TabPage();
            this.otherPage = new System.Windows.Forms.TabPage();
            this.fioBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.showCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.leftshiftBox = new System.Windows.Forms.NumericUpDown();
            this.topshiftBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.columnsRadioButton = new System.Windows.Forms.RadioButton();
            this.rowsRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.browseprinterButton = new System.Windows.Forms.Button();
            this.otherprinterRadioButton = new System.Windows.Forms.RadioButton();
            this.defaultprinterRadioButton = new System.Windows.Forms.RadioButton();
            this.reservPage = new System.Windows.Forms.TabPage();
            this.browsedirButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.maxreservBox = new System.Windows.Forms.NumericUpDown();
            this.autoreservCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.orgTableAdapter1 = new Pers_uchet_org.orgDBDataSetTableAdapters.OrgTableAdapter();
            this.tabControl1.SuspendLayout();
            this.otherPage.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftshiftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topshiftBox)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.reservPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxreservBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.generalPage);
            this.tabControl1.Controls.Add(this.otherPage);
            this.tabControl1.Controls.Add(this.reservPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(681, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // generalPage
            // 
            this.generalPage.Location = new System.Drawing.Point(4, 22);
            this.generalPage.Name = "generalPage";
            this.generalPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalPage.Size = new System.Drawing.Size(673, 352);
            this.generalPage.TabIndex = 0;
            this.generalPage.Text = "Общие сведения";
            this.generalPage.UseVisualStyleBackColor = true;
            // 
            // otherPage
            // 
            this.otherPage.Controls.Add(this.fioBox);
            this.otherPage.Controls.Add(this.label10);
            this.otherPage.Controls.Add(this.showCheckBox);
            this.otherPage.Controls.Add(this.groupBox6);
            this.otherPage.Controls.Add(this.groupBox5);
            this.otherPage.Controls.Add(this.groupBox4);
            this.otherPage.Location = new System.Drawing.Point(4, 22);
            this.otherPage.Name = "otherPage";
            this.otherPage.Padding = new System.Windows.Forms.Padding(3);
            this.otherPage.Size = new System.Drawing.Size(673, 352);
            this.otherPage.TabIndex = 1;
            this.otherPage.Text = "Дополнительные параметры";
            this.otherPage.UseVisualStyleBackColor = true;
            // 
            // fioBox
            // 
            this.fioBox.Location = new System.Drawing.Point(381, 278);
            this.fioBox.Name = "fioBox";
            this.fioBox.Size = new System.Drawing.Size(276, 20);
            this.fioBox.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(378, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "ФИО исполнителя";
            // 
            // showCheckBox
            // 
            this.showCheckBox.Location = new System.Drawing.Point(381, 201);
            this.showCheckBox.Name = "showCheckBox";
            this.showCheckBox.Size = new System.Drawing.Size(276, 46);
            this.showCheckBox.TabIndex = 3;
            this.showCheckBox.Text = "Показывать экран выбора начальных параметров при запуске программы";
            this.showCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.leftshiftBox);
            this.groupBox6.Controls.Add(this.topshiftBox);
            this.groupBox6.Location = new System.Drawing.Point(372, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(291, 169);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Смещать выводимые на печать данные";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "слева на право на";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "сверху вниз на";
            // 
            // leftshiftBox
            // 
            this.leftshiftBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.leftshiftBox.Location = new System.Drawing.Point(161, 56);
            this.leftshiftBox.Name = "leftshiftBox";
            this.leftshiftBox.Size = new System.Drawing.Size(124, 20);
            this.leftshiftBox.TabIndex = 1;
            // 
            // topshiftBox
            // 
            this.topshiftBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topshiftBox.Location = new System.Drawing.Point(161, 30);
            this.topshiftBox.Name = "topshiftBox";
            this.topshiftBox.Size = new System.Drawing.Size(124, 20);
            this.topshiftBox.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.columnsRadioButton);
            this.groupBox5.Controls.Add(this.rowsRadioButton);
            this.groupBox5.Location = new System.Drawing.Point(8, 181);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(358, 165);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Сводная ведомость";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(251, 38);
            this.label7.TabIndex = 7;
            this.label7.Text = "Установить порядок обхода полей в экране ввода сводной ведомости (форма \"СЗВ-3\")";
            // 
            // columnsRadioButton
            // 
            this.columnsRadioButton.AutoSize = true;
            this.columnsRadioButton.Location = new System.Drawing.Point(263, 57);
            this.columnsRadioButton.Name = "columnsRadioButton";
            this.columnsRadioButton.Size = new System.Drawing.Size(89, 17);
            this.columnsRadioButton.TabIndex = 6;
            this.columnsRadioButton.TabStop = true;
            this.columnsRadioButton.Text = "по столбцам";
            this.columnsRadioButton.UseVisualStyleBackColor = true;
            // 
            // rowsRadioButton
            // 
            this.rowsRadioButton.AutoSize = true;
            this.rowsRadioButton.Location = new System.Drawing.Point(263, 34);
            this.rowsRadioButton.Name = "rowsRadioButton";
            this.rowsRadioButton.Size = new System.Drawing.Size(83, 17);
            this.rowsRadioButton.TabIndex = 5;
            this.rowsRadioButton.TabStop = true;
            this.rowsRadioButton.Text = "по строкам";
            this.rowsRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.browseprinterButton);
            this.groupBox4.Controls.Add(this.otherprinterRadioButton);
            this.groupBox4.Controls.Add(this.defaultprinterRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(8, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(358, 169);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Использовать следующее устройство для вывода на печать";
            // 
            // browseprinterButton
            // 
            this.browseprinterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseprinterButton.Location = new System.Drawing.Point(99, 50);
            this.browseprinterButton.Name = "browseprinterButton";
            this.browseprinterButton.Size = new System.Drawing.Size(47, 23);
            this.browseprinterButton.TabIndex = 8;
            this.browseprinterButton.Text = "...";
            this.browseprinterButton.UseVisualStyleBackColor = true;
            this.browseprinterButton.Click += new System.EventHandler(this.browseprinterButton_Click);
            // 
            // otherprinterRadioButton
            // 
            this.otherprinterRadioButton.AutoSize = true;
            this.otherprinterRadioButton.Location = new System.Drawing.Point(6, 53);
            this.otherprinterRadioButton.Name = "otherprinterRadioButton";
            this.otherprinterRadioButton.Size = new System.Drawing.Size(59, 17);
            this.otherprinterRadioButton.TabIndex = 7;
            this.otherprinterRadioButton.TabStop = true;
            this.otherprinterRadioButton.Text = "другое";
            this.otherprinterRadioButton.UseVisualStyleBackColor = true;
            // 
            // defaultprinterRadioButton
            // 
            this.defaultprinterRadioButton.AutoSize = true;
            this.defaultprinterRadioButton.Location = new System.Drawing.Point(6, 30);
            this.defaultprinterRadioButton.Name = "defaultprinterRadioButton";
            this.defaultprinterRadioButton.Size = new System.Drawing.Size(140, 17);
            this.defaultprinterRadioButton.TabIndex = 6;
            this.defaultprinterRadioButton.TabStop = true;
            this.defaultprinterRadioButton.Text = "принтер по умолчанию";
            this.defaultprinterRadioButton.UseVisualStyleBackColor = true;
            // 
            // reservPage
            // 
            this.reservPage.Controls.Add(this.browsedirButton);
            this.reservPage.Controls.Add(this.label11);
            this.reservPage.Controls.Add(this.textBox1);
            this.reservPage.Controls.Add(this.maxreservBox);
            this.reservPage.Controls.Add(this.autoreservCheckBox);
            this.reservPage.Controls.Add(this.label1);
            this.reservPage.Location = new System.Drawing.Point(4, 22);
            this.reservPage.Name = "reservPage";
            this.reservPage.Padding = new System.Windows.Forms.Padding(3);
            this.reservPage.Size = new System.Drawing.Size(673, 352);
            this.reservPage.TabIndex = 2;
            this.reservPage.Text = "Резервные копии БД";
            this.reservPage.UseVisualStyleBackColor = true;
            // 
            // browsedirButton
            // 
            this.browsedirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browsedirButton.Location = new System.Drawing.Point(507, 166);
            this.browsedirButton.Name = "browsedirButton";
            this.browsedirButton.Size = new System.Drawing.Size(47, 23);
            this.browsedirButton.TabIndex = 9;
            this.browsedirButton.Text = "...";
            this.browsedirButton.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 152);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Папка для хранения резервных копий";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 168);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(492, 20);
            this.textBox1.TabIndex = 3;
            // 
            // maxreservBox
            // 
            this.maxreservBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxreservBox.Location = new System.Drawing.Point(219, 92);
            this.maxreservBox.Name = "maxreservBox";
            this.maxreservBox.Size = new System.Drawing.Size(90, 20);
            this.maxreservBox.TabIndex = 5;
            // 
            // autoreservCheckBox
            // 
            this.autoreservCheckBox.Location = new System.Drawing.Point(6, 18);
            this.autoreservCheckBox.Name = "autoreservCheckBox";
            this.autoreservCheckBox.Size = new System.Drawing.Size(657, 62);
            this.autoreservCheckBox.TabIndex = 4;
            this.autoreservCheckBox.Text = "Включить автоматическое создание резервных копий Базы Данных при выходе из програ" +
    "ммы";
            this.autoreservCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Максимальное число пезервных копий";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(511, 405);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(592, 405);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // orgTableAdapter1
            // 
            this.orgTableAdapter1.ClearBeforeFill = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 440);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(695, 478);
            this.Name = "SettingsForm";
            this.Text = "Настройки программы";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.otherPage.ResumeLayout(false);
            this.otherPage.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftshiftBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topshiftBox)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.reservPage.ResumeLayout(false);
            this.reservPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxreservBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalPage;
        private System.Windows.Forms.TabPage otherPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TabPage reservPage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rowsRadioButton;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton columnsRadioButton;
        private System.Windows.Forms.Button browseprinterButton;
        private System.Windows.Forms.RadioButton otherprinterRadioButton;
        private System.Windows.Forms.RadioButton defaultprinterRadioButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown leftshiftBox;
        private System.Windows.Forms.NumericUpDown topshiftBox;
        private System.Windows.Forms.TextBox fioBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox showCheckBox;
        private System.Windows.Forms.Button browsedirButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown maxreservBox;
        private System.Windows.Forms.CheckBox autoreservCheckBox;
        private orgDBDataSetTableAdapters.OrgTableAdapter orgTableAdapter1;
    }
}