namespace Pers_uchet_org
{
    partial class RestoreDBForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.copyListBox = new System.Windows.Forms.ListBox();
            this.backupFileNameLabel = new System.Windows.Forms.Label();
            this.backupFolderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите резервную копию, из которой желаете восстановить данные";
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(197, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // restoreButton
            // 
            this.restoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restoreButton.Location = new System.Drawing.Point(46, 227);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(145, 23);
            this.restoreButton.TabIndex = 2;
            this.restoreButton.Text = "Восстановить данные";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // copyListBox
            // 
            this.copyListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyListBox.FormattingEnabled = true;
            this.copyListBox.Location = new System.Drawing.Point(12, 48);
            this.copyListBox.Name = "copyListBox";
            this.copyListBox.Size = new System.Drawing.Size(260, 147);
            this.copyListBox.TabIndex = 3;
            this.copyListBox.SelectedValueChanged += new System.EventHandler(this.copyListBox_SelectedValueChanged);
            // 
            // backupFileNameLabel
            // 
            this.backupFileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backupFileNameLabel.AutoSize = true;
            this.backupFileNameLabel.Location = new System.Drawing.Point(12, 211);
            this.backupFileNameLabel.Name = "backupFileNameLabel";
            this.backupFileNameLabel.Size = new System.Drawing.Size(79, 13);
            this.backupFileNameLabel.TabIndex = 4;
            this.backupFileNameLabel.Text = "Имя файла: ...";
            // 
            // backupFolderLabel
            // 
            this.backupFolderLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backupFolderLabel.AutoSize = true;
            this.backupFolderLabel.Location = new System.Drawing.Point(12, 198);
            this.backupFolderLabel.Name = "backupFolderLabel";
            this.backupFolderLabel.Size = new System.Drawing.Size(45, 13);
            this.backupFolderLabel.TabIndex = 5;
            this.backupFolderLabel.Text = "Папка: ";
            // 
            // RestoreDBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.backupFolderLabel);
            this.Controls.Add(this.backupFileNameLabel);
            this.Controls.Add(this.copyListBox);
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "RestoreDBForm";
            this.Text = "Восстановление данных из резервной копии";
            this.Load += new System.EventHandler(this.RestoreDBForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.ListBox copyListBox;
        private System.Windows.Forms.Label backupFileNameLabel;
        private System.Windows.Forms.Label backupFolderLabel;
    }
}