using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace tutu
{
    public partial class Form1 : Form
    {
        // Статическое свойство для хранения строки подключения
        public static string ConnectionString { get; private set; }

        private string connectionString = "Host=195.46.187.72;Username=postgres;Password=1337;Database=sham";
        private Partner selectedPartner; // Текущий выбранный партнер

        public Form1()
        {
            InitializeComponent();
            ConnectionString = connectionString; // Инициализация статического свойства
            LoadPartners();
        }

        private void LoadPartners()
        {
            var partners = new List<Partner>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                // Загружаем данные о партнерах
                using (var cmd = new NpgsqlCommand("SELECT PartnerID, PartnerType, PartnerName, Director, Email, Phone, Address, Rating FROM Partners", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var partner = new Partner
                            {
                                PartnerID = reader.GetInt32(0),
                                PartnerType = reader.GetString(1),
                                PartnerName = reader.GetString(2),
                                Director = reader.GetString(3),
                                Email = reader.GetString(4),
                                Phone = reader.GetString(5),
                                Address = reader.IsDBNull(6) ? null : reader.GetString(6), // Обработка NULL
                                Rating = reader.GetInt32(7)
                            };

                            // Рассчитываем скидку на основе рейтинга
                            partner.Discount = partner.Rating * 0.01m; // Пример: скидка = рейтинг * 1%

                            partners.Add(partner);
                        }
                    }
                }
            }

            // Очищаем FlowLayoutPanel перед добавлением новых карточек
            flowLayoutPanel1.Controls.Clear();

            // Создаем карточки для каждого партнера
            foreach (var partner in partners)
            {
                var card = CreatePartnerCard(partner);
                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private Panel CreatePartnerCard(Partner partner)
        {
            // Создаем панель для карточки
            var panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Size = new Size(300, 150),
                Margin = new Padding(10),
                Tag = partner // Сохраняем партнера в Tag для дальнейшего использования
            };

            // Заголовок карточки (Тип | Наименование партнера)
            var lblHeader = new Label
            {
                Text = $"{partner.PartnerType} | {partner.PartnerName}    {partner.Discount:P0}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            // Директор
            var lblDirector = new Label
            {
                Text = $"Директор: {partner.Director}",
                Font = new Font("Arial", 10),
                Location = new Point(10, 40),
                AutoSize = true
            };

            // Телефон
            var lblPhone = new Label
            {
                Text = $"Телефон: {partner.Phone}",
                Font = new Font("Arial", 10),
                Location = new Point(10, 70),
                AutoSize = true
            };

            // Рейтинг
            var lblRating = new Label
            {
                Text = $"Рейтинг: {partner.Rating}",
                Font = new Font("Arial", 10),
                Location = new Point(10, 100),
                AutoSize = true
            };

            // Добавляем элементы на панель
            panel.Controls.Add(lblHeader);
            panel.Controls.Add(lblDirector);
            panel.Controls.Add(lblPhone);
            panel.Controls.Add(lblRating);

            // Добавляем обработчик события Click для карточки
            panel.Click += (sender, e) =>
            {
                // Выделяем карточку
                foreach (Panel p in flowLayoutPanel1.Controls)
                {
                    p.BackColor = Color.White; // Сбрасываем цвет всех карточек
                }
                panel.BackColor = Color.LightBlue; // Выделяем выбранную карточку

                // Сохраняем выбранного партнера
                selectedPartner = (Partner)panel.Tag;
            };

            return panel;
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            LoadPartners();
        }

        private void btnEditPartner_Click(object sender, EventArgs e)
        {
            if (selectedPartner != null)
            {
                Form2 form2 = new Form2(selectedPartner);
                form2.ShowDialog();
                LoadPartners(); // Перезагружаем данные после редактирования
            }
            else
            {
                MessageBox.Show("Выберите партнера для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            if (selectedPartner != null)
            {
                // Проверяем, есть ли история продаж для выбранного партнера
                if (HasSalesHistory(selectedPartner.PartnerID))
                {
                    Form3 form3 = new Form3(selectedPartner.PartnerName, connectionString);
                    form3.ShowDialog();
                }
                else
                {
                    MessageBox.Show("История продаж для выбранного партнера отсутствует.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите партнера для просмотра истории продаж.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool HasSalesHistory(int partnerId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM partnerproducts WHERE partnerin = @PartnerId", conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerId", partnerId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPartners(); // Перезагружаем данные
        }

        private void btnDeletePartner_Click(object sender, EventArgs e)
        {
            if (selectedPartner != null)
            {
                // Подтверждение удаления
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить партнера {selectedPartner.PartnerName}?",
                    "Подтверждение удаления",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DeletePartner(selectedPartner.PartnerID);
                    LoadPartners(); // Перезагружаем данные после удаления
                }
            }
            else
            {
                MessageBox.Show("Выберите партнера для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeletePartner(int partnerID)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("DELETE FROM Partners WHERE PartnerID = @PartnerID", conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerID", partnerID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class Partner
    {
        public int PartnerID { get; set; }
        public string PartnerType { get; set; }
        public string PartnerName { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; } // Может быть null
        public int Rating { get; set; }
        public decimal Discount { get; set; } // Новое поле для скидки
    }
}