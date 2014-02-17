namespace Pers_uchet_org
{
    partial class PrintStajForm
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
            this.listCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.executorTextBox = new System.Windows.Forms.TextBox();
            this.docCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.allDocsRadioButton = new System.Windows.Forms.RadioButton();
            this.checkedDocsRadioButton = new System.Windows.Forms.RadioButton();
            this.curDocRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.cancelButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listCheckBox
            // 
            this.listCheckBox.AutoSize = true;
            this.listCheckBox.Location = new System.Drawing.Point(12, 12);
            this.listCheckBox.Name = "listCheckBox";
            this.listCheckBox.Size = new System.Drawing.Size(243, 17);
            this.listCheckBox.TabIndex = 0;
            this.listCheckBox.Text = "Печатать опись форм документов \"СЗВ-2\"";
            this.listCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Исполнитель";
            // 
            // executorTextBox
            // 
            this.executorTextBox.Location = new System.Drawing.Point(109, 35);
            this.executorTextBox.Name = "executorTextBox";
            this.executorTextBox.Size = new System.Drawing.Size(146, 20);
            this.executorTextBox.TabIndex = 2;
            // 
            // docCheckBox
            // 
            this.docCheckBox.AutoSize = true;
            this.docCheckBox.Location = new System.Drawing.Point(12, 61);
            this.docCheckBox.Name = "docCheckBox";
            this.docCheckBox.Size = new System.Drawing.Size(175, 17);
            this.docCheckBox.TabIndex = 3;
            this.docCheckBox.Text = "Печатать документы \"СЗВ-1\"";
            this.docCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.allDocsRadioButton);
            this.groupBox1.Controls.Add(this.checkedDocsRadioButton);
            this.groupBox1.Controls.Add(this.curDocRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(32, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 94);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // allDocsRadioButton
            // 
            this.allDocsRadioButton.AutoSize = true;
            this.allDocsRadioButton.Location = new System.Drawing.Point(6, 65);
            this.allDocsRadioButton.Name = "allDocsRadioButton";
            this.allDocsRadioButton.Size = new System.Drawing.Size(205, 17);
            this.allDocsRadioButton.TabIndex = 7;
            this.allDocsRadioButton.TabStop = true;
            this.allDocsRadioButton.Text = "Печатать все документы из пакета";
            this.allDocsRadioButton.UseVisualStyleBackColor = true;
            // 
            // checkedDocsRadioButton
            // 
            this.checkedDocsRadioButton.AutoSize = true;
            this.checkedDocsRadioButton.Location = new System.Drawing.Point(6, 42);
            this.checkedDocsRadioButton.Name = "checkedDocsRadioButton";
            this.checkedDocsRadioButton.Size = new System.Drawing.Size(196, 17);
            this.checkedDocsRadioButton.TabIndex = 6;
            this.checkedDocsRadioButton.TabStop = true;
            this.checkedDocsRadioButton.Text = "Печатать отмеченные документы";
            this.checkedDocsRadioButton.UseVisualStyleBackColor = true;
            // 
            // curDocRadioButton
            // 
            this.curDocRadioButton.AutoSize = true;
            this.curDocRadioButton.Location = new System.Drawing.Point(6, 19);
            this.curDocRadioButton.Name = "curDocRadioButton";
            this.curDocRadioButton.Size = new System.Drawing.Size(169, 17);
            this.curDocRadioButton.TabIndex = 5;
            this.curDocRadioButton.TabStop = true;
            this.curDocRadioButton.Text = "Печатать текущий документ";
            this.curDocRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Дата печати";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(130, 184);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(125, 20);
            this.dateTimePicker.TabIndex = 6;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(180, 210);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.printButton.Location = new System.Drawing.Point(99, 210);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(75, 23);
            this.printButton.TabIndex = 8;
            this.printButton.Text = "Печатать";
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // PrintStajForm
            // 
            this.AcceptButton = this.printButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(269, 241);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.docCheckBox);
            this.Controls.Add(this.executorTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintStajForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать сведений о стаже и заработке";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox listCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox executorTextBox;
        private System.Windows.Forms.CheckBox docCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton allDocsRadioButton;
        private System.Windows.Forms.RadioButton checkedDocsRadioButton;
        private System.Windows.Forms.RadioButton curDocRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button printButton;
    }
}