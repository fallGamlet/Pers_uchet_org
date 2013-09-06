namespace Pers_uchet_org
{
    partial class MovePacketOtherYearForm
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
            this.yearNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.movePacketButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.yearNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Укажите отчетный год, в который\r\nбудет перемещен пакет № ";
            // 
            // yearNumericUpDown
            // 
            this.yearNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yearNumericUpDown.Location = new System.Drawing.Point(83, 46);
            this.yearNumericUpDown.Maximum = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.yearNumericUpDown.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.yearNumericUpDown.Name = "yearNumericUpDown";
            this.yearNumericUpDown.Size = new System.Drawing.Size(82, 23);
            this.yearNumericUpDown.TabIndex = 1;
            this.yearNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.yearNumericUpDown.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // movePacketButton
            // 
            this.movePacketButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.movePacketButton.Location = new System.Drawing.Point(15, 77);
            this.movePacketButton.Name = "movePacketButton";
            this.movePacketButton.Size = new System.Drawing.Size(128, 23);
            this.movePacketButton.TabIndex = 2;
            this.movePacketButton.Text = "Переместить пакет";
            this.movePacketButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(149, 77);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отменить";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // MovePacketOtherYearForm
            // 
            this.AcceptButton = this.movePacketButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(236, 112);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.movePacketButton);
            this.Controls.Add(this.yearNumericUpDown);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MovePacketOtherYearForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Перемещение пакета в другой год";
            ((System.ComponentModel.ISupportInitialize)(this.yearNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown yearNumericUpDown;
        private System.Windows.Forms.Button movePacketButton;
        private System.Windows.Forms.Button cancelButton;
    }
}