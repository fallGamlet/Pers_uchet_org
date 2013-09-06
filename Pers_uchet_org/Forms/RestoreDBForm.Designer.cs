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
            this.closeButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.copylistBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите резервную копию, данные из которой Вы желаете восстановить";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(294, 285);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Закрыть";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // restoreButton
            // 
            this.restoreButton.Location = new System.Drawing.Point(143, 285);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(145, 23);
            this.restoreButton.TabIndex = 2;
            this.restoreButton.Text = "Восстановить данные";
            this.restoreButton.UseVisualStyleBackColor = true;
            // 
            // copylistBox
            // 
            this.copylistBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.copylistBox.FormattingEnabled = true;
            this.copylistBox.Location = new System.Drawing.Point(15, 55);
            this.copylistBox.Name = "copylistBox";
            this.copylistBox.Size = new System.Drawing.Size(357, 223);
            this.copylistBox.TabIndex = 3;
            // 
            // RestoreDBForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(381, 320);
            this.Controls.Add(this.copylistBox);
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(397, 358);
            this.Name = "RestoreDBForm";
            this.Text = "Восстановление данных из резервной копии";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.ListBox copylistBox;
    }
}