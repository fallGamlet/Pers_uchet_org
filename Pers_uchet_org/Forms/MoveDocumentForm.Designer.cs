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
            this.listsComboBox = new System.Windows.Forms.ComboBox();
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
            // listsComboBox
            // 
            this.listsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listsComboBox.FormattingEnabled = true;
            this.listsComboBox.Location = new System.Drawing.Point(12, 25);
            this.listsComboBox.Name = "listsComboBox";
            this.listsComboBox.Size = new System.Drawing.Size(232, 21);
            this.listsComboBox.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(169, 55);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // moveDocButton
            // 
            this.moveDocButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDocButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.moveDocButton.Location = new System.Drawing.Point(75, 55);
            this.moveDocButton.Name = "moveDocButton";
            this.moveDocButton.Size = new System.Drawing.Size(88, 23);
            this.moveDocButton.TabIndex = 2;
            this.moveDocButton.Text = "Переместить";
            this.moveDocButton.UseVisualStyleBackColor = true;
            // 
            // MoveDocumentForm
            // 
            this.AcceptButton = this.moveDocButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(256, 90);
            this.Controls.Add(this.moveDocButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.listsComboBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MoveDocumentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перемещение документа(ов) СЗВ-1";
            this.Load += new System.EventHandler(this.MoveDocumentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox listsComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button moveDocButton;
    }
}