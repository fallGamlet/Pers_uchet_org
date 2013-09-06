namespace Pers_uchet_org
{
    partial class MoveDocumentForm
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fioLabel = new System.Windows.Forms.Label();
            this.regNumLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.moveDocButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите № пакета";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(122, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(136, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(246, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "в который будет перемещён документ \"СЗВ-1\"\r\nзастрахованного лица:";
            // 
            // fioLabel
            // 
            this.fioLabel.AutoSize = true;
            this.fioLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fioLabel.ForeColor = System.Drawing.Color.Blue;
            this.fioLabel.Location = new System.Drawing.Point(12, 70);
            this.fioLabel.Name = "fioLabel";
            this.fioLabel.Size = new System.Drawing.Size(36, 15);
            this.fioLabel.TabIndex = 3;
            this.fioLabel.Text = "ФИО";
            // 
            // regNumLabel
            // 
            this.regNumLabel.AutoSize = true;
            this.regNumLabel.Location = new System.Drawing.Point(12, 97);
            this.regNumLabel.Name = "regNumLabel";
            this.regNumLabel.Size = new System.Drawing.Size(76, 13);
            this.regNumLabel.TabIndex = 4;
            this.regNumLabel.Text = "страховой №:";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(183, 123);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // moveDocButton
            // 
            this.moveDocButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.moveDocButton.Location = new System.Drawing.Point(15, 123);
            this.moveDocButton.Name = "moveDocButton";
            this.moveDocButton.Size = new System.Drawing.Size(162, 23);
            this.moveDocButton.TabIndex = 6;
            this.moveDocButton.Text = "Переместить документ!";
            this.moveDocButton.UseVisualStyleBackColor = true;
            // 
            // MoveDocumentForm
            // 
            this.AcceptButton = this.moveDocButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(272, 158);
            this.Controls.Add(this.moveDocButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.regNumLabel);
            this.Controls.Add(this.fioLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveDocumentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перемещение документа \"СЗВ-1\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label fioLabel;
        private System.Windows.Forms.Label regNumLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button moveDocButton;
    }
}