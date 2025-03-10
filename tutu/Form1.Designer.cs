namespace tutu
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAddPartner;
        private System.Windows.Forms.Button btnEditPartner;
        private System.Windows.Forms.Button btnSalesHistory;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDeletePartner;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddPartner = new System.Windows.Forms.Button();
            this.btnEditPartner = new System.Windows.Forms.Button();
            this.btnSalesHistory = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDeletePartner = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 400);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnAddPartner
            // 
            this.btnAddPartner.Location = new System.Drawing.Point(12, 441);
            this.btnAddPartner.Name = "btnAddPartner";
            this.btnAddPartner.Size = new System.Drawing.Size(150, 59);
            this.btnAddPartner.TabIndex = 1;
            this.btnAddPartner.Text = "Добавить партнера";
            this.btnAddPartner.UseVisualStyleBackColor = true;
            this.btnAddPartner.Click += new System.EventHandler(this.btnAddPartner_Click);
            // 
            // btnEditPartner
            // 
            this.btnEditPartner.Location = new System.Drawing.Point(186, 441);
            this.btnEditPartner.Name = "btnEditPartner";
            this.btnEditPartner.Size = new System.Drawing.Size(150, 59);
            this.btnEditPartner.TabIndex = 2;
            this.btnEditPartner.Text = "Редактировать партнера";
            this.btnEditPartner.UseVisualStyleBackColor = true;
            this.btnEditPartner.Click += new System.EventHandler(this.btnEditPartner_Click);
            // 
            // btnSalesHistory
            // 
            this.btnSalesHistory.Location = new System.Drawing.Point(357, 441);
            this.btnSalesHistory.Name = "btnSalesHistory";
            this.btnSalesHistory.Size = new System.Drawing.Size(150, 59);
            this.btnSalesHistory.TabIndex = 3;
            this.btnSalesHistory.Text = "История продаж";
            this.btnSalesHistory.UseVisualStyleBackColor = true;
            this.btnSalesHistory.Click += new System.EventHandler(this.btnSalesHistory_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(746, 441);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnRefresh.Size = new System.Drawing.Size(66, 59);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDeletePartner
            // 
            this.btnDeletePartner.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePartner.Image")));
            this.btnDeletePartner.Location = new System.Drawing.Point(674, 441);
            this.btnDeletePartner.Name = "btnDeletePartner";
            this.btnDeletePartner.Size = new System.Drawing.Size(66, 59);
            this.btnDeletePartner.TabIndex = 5;
            this.btnDeletePartner.UseVisualStyleBackColor = true;
            this.btnDeletePartner.Click += new System.EventHandler(this.btnDeletePartner_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 520);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDeletePartner);
            this.Controls.Add(this.btnSalesHistory);
            this.Controls.Add(this.btnEditPartner);
            this.Controls.Add(this.btnAddPartner);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Управление партнерами";
            this.ResumeLayout(false);

        }
    }
}