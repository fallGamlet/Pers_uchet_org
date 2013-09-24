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
    public partial class MovePacketForm : Form
    {
        #region Поля
        private string _mainConnection;
        private Operator _operator;
        private DataTable _orgTable;
        private BindingSource _orgBS;
        private long listId;
        #endregion

        public MovePacketForm()
        {
            InitializeComponent();
        }

        public MovePacketForm(long listId)
        {
            InitializeComponent();
            this.listId = listId;
        }

        private void MovePacketForm_Load(object sender, EventArgs e)
        {

        }
    }
}
