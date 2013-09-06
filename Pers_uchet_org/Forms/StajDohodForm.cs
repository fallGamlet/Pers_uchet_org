using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pers_uchet_org
{
    public partial class StajDohodForm : Form
    {
        public StajDohodForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            PacketDopInfoForm packetDopInfoForm = new PacketDopInfoForm();
            if (packetDopInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void movePacketOrgButton_Click(object sender, EventArgs e)
        {
            MovePacketForm movePacketForm = new MovePacketForm();
            if (movePacketForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void movePacketYearButton_Click(object sender, EventArgs e)
        {
            MovePacketOtherYearForm movePacketOtherYear = new MovePacketOtherYearForm();
            if (movePacketOtherYear.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void addDocButton_Click(object sender, EventArgs e)
        {
            ChoicePersonForm choicePersonForm = new ChoicePersonForm();
            if (choicePersonForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void editDocButton_Click(object sender, EventArgs e)
        {
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void moveDocButton_Click(object sender, EventArgs e)
        {
            MoveDocumentForm moveDocForm = new MoveDocumentForm();
            if (moveDocForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void packetView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            PacketDopInfoForm packetDopInfoForm = new PacketDopInfoForm();
            if (packetDopInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void documentView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddEditDocumentSzv1Form szv1Form = new AddEditDocumentSzv1Form();
            if (szv1Form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void changeTypedDocButton_Click(object sender, EventArgs e)
        {
            ReplaceDocTypeForm replaceDocTypeForm = new ReplaceDocTypeForm();
            if (replaceDocTypeForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void printFormButton_Click(object sender, EventArgs e)
        {
            PrintStajForm printStajForm = new PrintStajForm();
            if (printStajForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            PacketDopInfoForm packetDopInfoForm = new PacketDopInfoForm();
            if (packetDopInfoForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            { }
        }


    }
}
