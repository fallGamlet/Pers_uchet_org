namespace Pers_uchet_org
{
    partial class ReplaceDocTypeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.docTypesComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.allDocsRadioButton = new System.Windows.Forms.RadioButton();
            this.checkedDocsRadioButton = new System.Windows.Forms.RadioButton();
            this.curDocRadioButton = new System.Windows.Forms.RadioButton();
            this.replaceButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите новый тип формы";
            // 
            // docTypesComboBox
            // 
            this.docTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.docTypesComboBox.FormattingEnabled = true;
            this.docTypesComboBox.Location = new System.Drawing.Point(15, 25);
            this.docTypesComboBox.Name = "docTypesComboBox";
            this.docTypesComboBox.Size = new System.Drawing.Size(208, 21);
            this.docTypesComboBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Установить выбранный тип формы для:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.allDocsRadioButton);
            this.groupBox1.Controls.Add(this.checkedDocsRadioButton);
            this.groupBox1.Controls.Add(this.curDocRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(15, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // allDocsRadioButton
            // 
            this.allDocsRadioButton.AutoSize = true;
            this.allDocsRadioButton.Location = new System.Drawing.Point(6, 65);
            this.allDocsRadioButton.Name = "allDocsRadioButton";
            this.allDocsRadioButton.Size = new System.Drawing.Size(150, 17);
            this.allDocsRadioButton.TabIndex = 6;
            this.allDocsRadioButton.Text = "Всех документов пакета";
            this.allDocsRadioButton.UseVisualStyleBackColor = true;
            // 
            // checkedDocsRadioButton
            // 
            this.checkedDocsRadioButton.AutoSize = true;
            this.checkedDocsRadioButton.Location = new System.Drawing.Point(6, 42);
            this.checkedDocsRadioButton.Name = "checkedDocsRadioButton";
            this.checkedDocsRadioButton.Size = new System.Drawing.Size(151, 17);
            this.checkedDocsRadioButton.TabIndex = 5;
            this.checkedDocsRadioButton.Text = "Отмеченных документов";
            this.checkedDocsRadioButton.UseVisualStyleBackColor = true;
            // 
            // curDocRadioButton
            // 
            this.curDocRadioButton.AutoSize = true;
            this.curDocRadioButton.Checked = true;
            this.curDocRadioButton.Location = new System.Drawing.Point(6, 19);
            this.curDocRadioButton.Name = "curDocRadioButton";
            this.curDocRadioButton.Size = new System.Drawing.Size(132, 17);
            this.curDocRadioButton.TabIndex = 4;
            this.curDocRadioButton.TabStop = true;
            this.curDocRadioButton.Text = "Текущего документа";
            this.curDocRadioButton.UseVisualStyleBackColor = true;
            // 
            // replaceButton
            // 
            this.replaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.replaceButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.replaceButton.Location = new System.Drawing.Point(69, 172);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(75, 23);
            this.replaceButton.TabIndex = 4;
            this.replaceButton.Text = "Заменить";
            this.replaceButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(150, 172);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ReplaceDocTypeForm
            // 
            this.AcceptButton = this.replaceButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(237, 207);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.replaceButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.docTypesComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReplaceDocTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Замена типа формы документов \"СЗВ-1\"";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox docTypesComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton allDocsRadioButton;
        private System.Windows.Forms.RadioButton checkedDocsRadioButton;
        private System.Windows.Forms.RadioButton curDocRadioButton;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.Button cancelButton;
    }
}