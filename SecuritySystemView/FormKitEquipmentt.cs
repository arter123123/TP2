using SecuritySystemContracts.BusinessLogicsContracts;
using SecuritySystemContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecuritySystemView
{
    public partial class FormKitEquipmentt : Form
    {
        public int Id
        {
            get { return Convert.ToInt32(comboBoxEguipment.SelectedValue); }
            set { comboBoxEguipment.SelectedValue = value; }
        }
        public string EquipmenttName { get { return comboBoxEguipment.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public FormKitEquipmentt(IEquipmenttLogic logic)
        {
            InitializeComponent();

            List<EquipmenttViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxEguipment.DisplayMember = "EquipmenttName";
                comboBoxEguipment.ValueMember = "Id";
                comboBoxEguipment.DataSource = list;
                comboBoxEguipment.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxEguipment.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
