namespace Pers_uchet_org
{
    partial class AnketaPrintForm
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
            this.printButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bydateCheckBox = new System.Windows.Forms.RadioButton();
            this.selectedCheckBox = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.currentCheckBox = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.printunregisteredCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.printButton.Location = new System.Drawing.Point(236, 143);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(75, 23);
            this.printButton.TabIndex = 0;
            this.printButton.Text = "Печатать";
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(404, 119);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bydateCheckBox);
            this.tabPage1.Controls.Add(this.selectedCheckBox);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.currentCheckBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(396, 93);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Анкеты формы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bydateCheckBox
            // 
            this.bydateCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bydateCheckBox.AutoSize = true;
            this.bydateCheckBox.Location = new System.Drawing.Point(8, 61);
            this.bydateCheckBox.Name = "bydateCheckBox";
            this.bydateCheckBox.Size = new System.Drawing.Size(264, 17);
            this.bydateCheckBox.TabIndex = 3;
            this.bydateCheckBox.TabStop = true;
            this.bydateCheckBox.Text = "Печать всех анкет с датой внесения начиная с";
            this.bydateCheckBox.UseVisualStyleBackColor = true;
            // 
            // selectedCheckBox
            // 
            this.selectedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedCheckBox.AutoSize = true;
            this.selectedCheckBox.Location = new System.Drawing.Point(8, 38);
            this.selectedCheckBox.Name = "selectedCheckBox";
            this.selectedCheckBox.Size = new System.Drawing.Size(159, 17);
            this.selectedCheckBox.TabIndex = 2;
            this.selectedCheckBox.TabStop = true;
            this.selectedCheckBox.Text = "Печать выделенных анкет";
            this.selectedCheckBox.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(278, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // currentCheckBox
            // 
            this.currentCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentCheckBox.AutoSize = true;
            this.currentCheckBox.Location = new System.Drawing.Point(8, 15);
            this.currentCheckBox.Name = "currentCheckBox";
            this.currentCheckBox.Size = new System.Drawing.Size(147, 17);
            this.currentCheckBox.TabIndex = 0;
            this.currentCheckBox.TabStop = true;
            this.currentCheckBox.Text = "Печать текущей анкеты";
            this.currentCheckBox.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.printunregisteredCheckBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(396, 93);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Дополнительно";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // printunregisteredCheckBox
            // 
            this.printunregisteredCheckBox.AutoSize = true;
            this.printunregisteredCheckBox.Location = new System.Drawing.Point(8, 15);
            this.printunregisteredCheckBox.Name = "printunregisteredCheckBox";
            this.printunregisteredCheckBox.Size = new System.Drawing.Size(220, 17);
            this.printunregisteredCheckBox.TabIndex = 0;
            this.printunregisteredCheckBox.Text = "Печать списка незастрахованных лиц";
            this.printunregisteredCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(317, 143);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // AnketaPrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(404, 178);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.printButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AnketaPrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Печать анкет";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RadioButton bydateCheckBox;
        private System.Windows.Forms.RadioButton selectedCheckBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton currentCheckBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox printunregisteredCheckBox;
        private System.Windows.Forms.Button cancelButton;
    }
}