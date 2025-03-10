using System;
using System.Windows.Forms;
using Npgsql;

namespace tutu
{
    public partial class Form2 : Form
    {
        private Partner partner; // Используем класс Partner
        private bool isEditMode; // Режим редактирования

        public Form2()
        {
            InitializeComponent();
            isEditMode = false;
        }

        public Form2(Partner partner) : this()
        {
            this.partner = partner;
            isEditMode = true;
            LoadPartnerData();
        }

        private void LoadPartnerData()
        {
            // Заполняем поля данными партнера
            txtName.Text = partner.PartnerName;
            cmbType.SelectedItem = partner.PartnerType;
            numRating.Value = partner.Rating;
            txtDirector.Text = partner.Director;
            txtPhone.Text = partner.Phone;
            txtEmail.Text = partner.Email;
            txtAddress.Text = partner.Address ?? string.Empty; // Обработка NULL
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверяем, что все обязательные поля заполнены
            if (string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(cmbType.Text) ||
                string.IsNullOrEmpty(txtDirector.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) ||
                string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сохраняем данные партнера
            if (isEditMode)
            {
                // Редактирование существующего партнера
                UpdatePartner();
            }
            else
            {
                // Добавление нового партнера
                AddPartner();
            }

            this.Close();
        }

        private void AddPartner()
        {
            using (var conn = new NpgsqlConnection(Form1.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO Partners (PartnerType, PartnerName, Director, Email, Phone, Address, Rating) " +
                    "VALUES (@PartnerType, @PartnerName, @Director, @Email, @Phone, @Address, @Rating)", conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerType", cmbType.Text);
                    cmd.Parameters.AddWithValue("@PartnerName", txtName.Text);
                    cmd.Parameters.AddWithValue("@Director", txtDirector.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text); // Обработка NULL
                    cmd.Parameters.AddWithValue("@Rating", (int)numRating.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdatePartner()
        {
            using (var conn = new NpgsqlConnection(Form1.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "UPDATE Partners SET PartnerType = @PartnerType, PartnerName = @PartnerName, Director = @Director, " +
                    "Email = @Email, Phone = @Phone, Address = @Address, Rating = @Rating WHERE PartnerID = @PartnerID", conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerType", cmbType.Text);
                    cmd.Parameters.AddWithValue("@PartnerName", txtName.Text);
                    cmd.Parameters.AddWithValue("@Director", txtDirector.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text); // Обработка NULL
                    cmd.Parameters.AddWithValue("@Rating", (int)numRating.Value);
                    cmd.Parameters.AddWithValue("@PartnerID", partner.PartnerID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}