namespace Pers_uchet_org.Forms
{
    partial class OperatorEnterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OperatorEnterForm));
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loginComboBox = new System.Windows.Forms.ComboBox();
            this.showPassButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.Location = new System.Drawing.Point(115, 68);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(80, 23);
            this.acceptButton.TabIndex = 5;
            this.acceptButton.Text = "Вход";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(201, 68);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Выход";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин";
            // 
            // passwordBox
            // 
            this.passwordBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordBox.Location = new System.Drawing.Point(78, 38);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(174, 20);
            this.passwordBox.TabIndex = 3;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passwordBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Пароль";
            // 
            // loginComboBox
            // 
            this.loginComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.loginComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.loginComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loginComboBox.FormattingEnabled = true;
            this.loginComboBox.Location = new System.Drawing.Point(78, 12);
            this.loginComboBox.Name = "loginComboBox";
            this.loginComboBox.Size = new System.Drawing.Size(203, 21);
            this.loginComboBox.TabIndex = 2;
            this.loginComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.loginComboBox_KeyDown);
            // 
            // showPassButton
            // 
            this.showPassButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showPassButton.BackColor = System.Drawing.Color.Transparent;
            this.showPassButton.FlatAppearance.BorderSize = 0;
            this.showPassButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showPassButton.Image = global::Pers_uchet_org.Properties.Resources.visible_16;
            this.showPassButton.Location = new System.Drawing.Point(258, 36);
            this.showPassButton.Name = "showPassButton";
            this.showPassButton.Size = new System.Drawing.Size(23, 23);
            this.showPassButton.TabIndex = 4;
            this.showPassButton.UseVisualStyleBackColor = false;
            this.showPassButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPassButton_MouseDown);
            this.showPassButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPassButton_MouseUp);
            // 
            // OperatorEnterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 102);
            this.Controls.Add(this.showPassButton);
            this.Controls.Add(this.loginComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 141);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(309, 141);
            this.Name = "OperatorEnterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Введите логин и пароль";
            this.Load += new System.EventHandler(this.OperatorEnterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox loginComboBox;
        private System.Windows.Forms.Button showPassButton;
    }
}