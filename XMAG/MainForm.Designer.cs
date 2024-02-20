using System;
using System.Drawing;
using System.Windows.Forms;

namespace XMAG
{
    partial class MainFormDesigner : MainForm
    {
        private TextBox textBoxProductName;
        private TextBox textBoxQuantity;
        private DataGridView dataGridViewProducts;
        private Label labelProductName;
        private Label labelQuantity;
        private Button buttonAddProduct;
        private Button buttonDeleteProduct;
        private Button buttonEditProduct;

        private void InitializeComponent()
        {
            buttonAddProduct = new Button();
            buttonDeleteProduct = new Button();
            buttonEditProduct = new Button();
            dataGridViewProducts = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewProducts)).BeginInit();

            // Configure DataGridView
            dataGridViewProducts.Location = new Point(12, 150);
            dataGridViewProducts.Name = "dataGridViewProducts";
            dataGridViewProducts.Size = new Size(300, 150);
            dataGridViewProducts.TabIndex = 3;

            // Adjust the size to be 50% smaller
            dataGridViewProducts.Size = new Size(dataGridViewProducts.Width / 2, dataGridViewProducts.Height / 2);

            // MainFormDesigner
            ClientSize = new Size(1024, 640);
            Controls.Add(buttonAddProduct);
            Controls.Add(buttonDeleteProduct);
            Controls.Add(buttonEditProduct);
            Controls.Add(dataGridViewProducts);
            Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(dataGridViewProducts)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
