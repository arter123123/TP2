using SecuritySystemContracts.BindingModels;
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
using Unity;

namespace SecuritySystemView
{
    public partial class FormKit : Form
    {
        public int Id { set { id = value; } }
        private readonly IKitLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> kitEquipmentts;
        public FormKit(IKitLogic logic)
        {
            InitializeComponent();
            _logic = logic;

        }

        private void FormKit_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    KitViewModel view = _logic.Read(new KitBindingModel { Id = id.Value })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.KitName;
                        textBoxPrice.Text = view.Price.ToString();
                        kitEquipmentts = view.KitEquipmentts;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                kitEquipmentts = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (kitEquipmentts != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in kitEquipmentts)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormKitEquipmentt>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (kitEquipmentts.ContainsKey(form.Id))
                {
                    kitEquipmentts[form.Id] = (form.EquipmenttName, form.Count);
                }
                else
                {
                    kitEquipmentts.Add(form.Id, (form.EquipmenttName, form.Count));
                }
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormKitEquipmentt>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = kitEquipmentts[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    kitEquipmentts[form.Id] = (form.EquipmenttName, form.Count);
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        kitEquipmentts.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (kitEquipmentts == null || kitEquipmentts.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new KitBindingModel
                {
                    Id = id,
                    KitName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    KitEquipmentts = kitEquipmentts
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
