using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace tutu
{
    public partial class Form3 : Form
    {
        private string partnerName;
        private string connectionString;

        public Form3(string partnerName, string connectionString)
        {
            InitializeComponent();
            this.partnerName = partnerName;
            this.connectionString = connectionString;
            this.Text = $"История продаж: {partnerName}";
            lblPartnerName.Text = $"Партнер: {partnerName}";
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            var sales = new List<SalesHistory>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                // Загружаем историю продаж для партнера
                using (var cmd = new NpgsqlCommand("SELECT \"Продукция\", \"Количество продукции\", \"Дата продажи\" FROM partnerproducts WHERE partnerin = @PartnerName", conn))
                {
                    cmd.Parameters.AddWithValue("@PartnerName", partnerName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sale = new SalesHistory
                            {
                                ProductName = reader.GetString(0),
                                Quantity = reader.GetInt32(1),
                                SaleDate = reader.GetDateTime(2)
                            };

                            sales.Add(sale);
                        }
                    }
                }
            }

            // Привязываем данные к DataGridView
            dataGridView1.DataSource = sales;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class SalesHistory
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}