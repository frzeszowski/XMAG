namespace XMAG
{
    public partial class MainForm : Form
    {
        private TabPage tabPageProducts;
        private TabControl tabControl;
        private DataGridView dataGridViewProducts;
        private Button buttonAddProduct;
        private Button buttonDeleteProduct;
        private Button buttonEditProduct;

        public MainForm()
        {
            CheckForUpdates();
            ProductManagement.InitializeDatabase();
            InitializeComponent();
            InitializeTabs();
            this.Resize += MainForm_Resize;
        }

        // Method to handle the Resize event
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Adjust the position of the buttons based on the form size
            AdjustControlSizes();
        }

        private void AdjustControlSizes()
        {
            if (tabControl.SelectedTab == tabPageProducts)
            {
                // Adjust DataGridView size and position
                dataGridViewProducts.Location = new Point(this.Width / 5, this.Height / 3);
                dataGridViewProducts.Size = new Size(this.Width - this.Width / 5, this.Height - this.Height / 3 - this.Height / 10);

                // Adjust button positions
                int buttonWidth = 100;
                int buttonHeight = 30;
                int buttonSpacing = 10;

                // Anchor buttons to the bottom-right corner of the DataGridView
                buttonAddProduct.Location = new Point(dataGridViewProducts.Right - buttonWidth - buttonSpacing, dataGridViewProducts.Bottom + buttonSpacing);
                buttonAddProduct.Size = new Size(buttonWidth, buttonHeight);

                buttonEditProduct.Location = new Point(buttonAddProduct.Left - buttonWidth - buttonSpacing, dataGridViewProducts.Bottom + buttonSpacing);
                buttonEditProduct.Size = new Size(buttonWidth, buttonHeight);

                buttonDeleteProduct.Location = new Point(buttonEditProduct.Left - buttonWidth - buttonSpacing, dataGridViewProducts.Bottom + buttonSpacing);
                buttonDeleteProduct.Size = new Size(buttonWidth, buttonHeight);
            }
        }

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabPageProducts = new TabPage();

            // DataGridView configuration
            this.dataGridViewProducts = new DataGridView();
            this.dataGridViewProducts.Dock = DockStyle.Fill;
            this.tabPageProducts.Controls.Add(dataGridViewProducts);

            // Add tabPageProducts to tabControl
            this.tabControl.TabPages.Add(tabPageProducts);

            // Add tabControl to MainForm
            this.Controls.Add(tabControl);

            // Initialize buttons
            buttonAddProduct = new Button();
            buttonAddProduct.Text = "Dodaj produkt";

            buttonEditProduct = new Button();
            buttonEditProduct.Text = "Edytuj produkt";

            buttonDeleteProduct = new Button();
            buttonDeleteProduct.Text = "Usuñ produkt";

            // Refresh the product list
            RefreshProductList();
        }

        private void InitializeTabs()
        {
            // Attach button click events
            buttonAddProduct.Click += buttonAddProduct_Click;
            buttonEditProduct.Click += buttonEditProduct_Click;
            buttonDeleteProduct.Click += buttonDeleteProduct_Click;

            // Add buttons to MainForm
            this.Controls.Add(buttonAddProduct);
            this.Controls.Add(buttonEditProduct);
            this.Controls.Add(buttonDeleteProduct);
        }

        private void RefreshProductList()
        {
            try
            {
                // Retrieve product data from the warehouse
                var products = ProductManagement.PokazProdukty();

                // Bind the product data to the DataGridView
                dataGridViewProducts.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddProduct_Click(object sender, EventArgs e)
        {
            using (AddProductDialog addProductForm = new AddProductDialog())
            {
                DialogResult result = addProductForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string productName = addProductForm.textBoxProductName.Text.Trim();
                    string quantityText = addProductForm.textBoxQuantity.Text.Trim();
                    string uwagi = addProductForm.textBoxUwagi.Text.Trim();

                    if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(quantityText))
                    {
                        MessageBox.Show("Please enter both name and quantity.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
                    {
                        MessageBox.Show("Please enter a valid quantity as a number.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        ProductManagement.DodajNowyProdukt(productName, quantity, uwagi);
                        MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshProductList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonEditProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                if (dataGridViewProducts.SelectedRows[0].DataBoundItem is Product selectedProduct)
                {
                    using (var editProductForm = new EditProductDialog(selectedProduct))
                    {
                        if (editProductForm.ShowDialog() == DialogResult.OK)
                        {
                            RefreshProductList();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Wybierz produkt z listy.", "Nie wybrano", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                if (dataGridViewProducts.SelectedRows[0].DataBoundItem is Product selectedProduct)
                {
                    try
                    {
                        ProductManagement.UsunWybranyProdukt(selectedProduct.Id);
                        RefreshProductList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Wybierz produkt do usuniêcia", "Nie wybrano", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Check for updates method
        public void CheckForUpdates()
        {
            UpdateManager updateManager = new UpdateManager();

            // Check for updates
            string currentVersion = "1.1"; // Replace this with the actual current version of your application
            if (updateManager.IsUpdateAvailable(currentVersion))
            {
                Console.WriteLine("An update is available.");

                // Assuming updateInfo includes the download URL of the update
                string downloadUrl = "https://your-update-service.com/download/update.zip";
                string destinationFilePath = "update.zip"; // Specify the destination file path

                // Download the update
                updateManager.DownloadUpdate(downloadUrl, destinationFilePath);

                // Install the update
                updateManager.InstallUpdate(destinationFilePath);
            }
            else
            {
                Console.WriteLine("No updates available.");
            }
        }
    }
}
