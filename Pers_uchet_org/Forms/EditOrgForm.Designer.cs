namespace Pers_uchet_org
{
    partial class EditOrgForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.buhfioorgBox = new System.Windows.Forms.TextBox();
            this.bosspostorgBox = new System.Windows.Forms.TextBox();
            this.bossfioorgBox = new System.Windows.Forms.TextBox();
            this.nameorgBox = new System.Windows.Forms.TextBox();
            this.regnumorgBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.acceptButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(334, 147);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // buhfioorgBox
            // 
            this.buhfioorgBox.Location = new System.Drawing.Point(156, 116);
            this.buhfioorgBox.Name = "buhfioorgBox";
            this.buhfioorgBox.Size = new System.Drawing.Size(253, 20);
            this.buhfioorgBox.TabIndex = 5;
            // 
            // bosspostorgBox
            // 
            this.bosspostorgBox.Location = new System.Drawing.Point(156, 64);
            this.bosspostorgBox.Name = "bosspostorgBox";
            this.bosspostorgBox.Size = new System.Drawing.Size(253, 20);
            this.bosspostorgBox.TabIndex = 3;
            // 
            // bossfioorgBox
            // 
            this.bossfioorgBox.Location = new System.Drawing.Point(156, 90);
            this.bossfioorgBox.Name = "bossfioorgBox";
            this.bossfioorgBox.Size = new System.Drawing.Size(253, 20);
            this.bossfioorgBox.TabIndex = 4;
            // 
            // nameorgBox
            // 
            this.nameorgBox.Location = new System.Drawing.Point(156, 38);
            this.nameorgBox.Name = "nameorgBox";
            this.nameorgBox.Size = new System.Drawing.Size(253, 20);
            this.nameorgBox.TabIndex = 2;
            // 
            // regnumorgBox
            // 
            this.regnumorgBox.Location = new System.Drawing.Point(156, 12);
            this.regnumorgBox.Name = "regnumorgBox";
            this.regnumorgBox.Size = new System.Drawing.Size(253, 20);
            this.regnumorgBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "ФИО бухгалтера";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "ФИО руководителя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Должность руководителя";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Наименование";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Регистрационный №";
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.acceptButton.Location = new System.Drawing.Point(223, 147);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(105, 23);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "Сохранить";
            this.acceptButton.UseVisualStyleBackColor = true;
            // 
            // EditOrgForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(421, 182);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.buhfioorgBox);
            this.Controls.Add(this.bosspostorgBox);
            this.Controls.Add(this.bossfioorgBox);
            this.Controls.Add(this.nameorgBox);
            this.Controls.Add(this.regnumorgBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditOrgForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Изменение данных организации";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox buhfioorgBox;
        private System.Windows.Forms.TextBox bosspostorgBox;
        private System.Windows.Forms.TextBox bossfioorgBox;
        private System.Windows.Forms.TextBox nameorgBox;
        private System.Windows.Forms.TextBox regnumorgBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button acceptButton;
    }
}