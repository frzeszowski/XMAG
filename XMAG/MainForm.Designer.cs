namespace XMAG
{
    partial class MainFormDesigner : MainForm
    {
        private TextBox textBoxProductName;
        private TextBox textBoxQuantity;
        private ListView listViewProducts;
        private Label labelProductName;
        private Label labelQuantity;
        private Button buttonAddProduct;
        private Button buttonDeleteProduct;
        private Button buttonEditProduct;

        // Replace the ListView with a DataGridView
        private DataGridView dataGridViewProducts;
        private void InitializeComponent()
        {
            buttonAddProduct = new Button();
            buttonDeleteProduct = new Button();
            buttonEditProduct = new Button();
            dataGridViewProducts = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).BeginInit();

            // Adjust buttonAddProduct properties
            buttonAddProduct.AutoSize = true;
            buttonAddProduct.BackColor = Color.FromArgb(52, 152, 219);
            buttonAddProduct.Cursor = Cursors.Hand;
            buttonAddProduct.FlatAppearance.BorderSize = 0;
            buttonAddProduct.FlatStyle = FlatStyle.Flat;
            buttonAddProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAddProduct.ForeColor = Color.White;
            buttonAddProduct.Location = new Point(ClientSize.Width - 332, ClientSize.Height - 40); // Anchored to bottom-right corner
            buttonAddProduct.Name = "buttonAddProduct";
            buttonAddProduct.TabIndex = 0;
            buttonAddProduct.Text = "Dodaj produkt";
            buttonAddProduct.UseVisualStyleBackColor = false;

            // Adjust buttonDeleteProduct properties
            buttonDeleteProduct.AutoSize = true;
            buttonDeleteProduct.BackColor = Color.FromArgb(52, 152, 219);
            buttonDeleteProduct.Cursor = Cursors.Hand;
            buttonDeleteProduct.FlatAppearance.BorderSize = 0;
            buttonDeleteProduct.FlatStyle = FlatStyle.Flat;
            buttonDeleteProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDeleteProduct.ForeColor = Color.White;
            buttonDeleteProduct.Location = new Point(ClientSize.Width - 210, ClientSize.Height - 40); // Anchored to bottom-right corner
            buttonDeleteProduct.Name = "buttonDeleteProduct";
            buttonDeleteProduct.TabIndex = 1;
            buttonDeleteProduct.Text = "Usuń produkt";
            buttonDeleteProduct.UseVisualStyleBackColor = false;

            // Adjust buttonEditProduct properties
            buttonEditProduct.AutoSize = true;
            buttonEditProduct.BackColor = Color.FromArgb(52, 152, 219);
            buttonEditProduct.Cursor = Cursors.Hand;
            buttonEditProduct.FlatAppearance.BorderSize = 0;
            buttonEditProduct.FlatStyle = FlatStyle.Flat;
            buttonEditProduct.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditProduct.ForeColor = Color.White;
            buttonEditProduct.Location = new Point(ClientSize.Width - 95, ClientSize.Height - 40); // Anchored to bottom-right corner
            buttonEditProduct.Name = "buttonEditProduct";
            buttonEditProduct.TabIndex = 2;
            buttonEditProduct.Text = "Edytuj produkt";
            buttonEditProduct.UseVisualStyleBackColor = false;

            // 
            // dataGridViewProducts
            // 
            dataGridViewProducts.Location = new Point(12, 150);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.Size = new Size(400, 200);
            dataGridViewProducts.TabIndex = 3;
            // 
            // MainForm
            // 
            ClientSize = new Size(695, 446);
            Controls.Add(buttonAddProduct);
            Controls.Add(buttonDeleteProduct);
            Controls.Add(buttonEditProduct);
            Controls.Add(dataGridViewProducts);
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
