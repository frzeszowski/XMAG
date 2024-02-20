using System;
using System.Drawing;
using System.Windows.Forms;

namespace XMAG
{
    public partial class MainForm : Form
    {
        private TabPage tabPageProducts;
        private TabPage tabPageParts;
        private TabControl tabControl;
        private DataGridView dataGridViewProducts;
        private DataGridView dataGridViewParts;
        private Button buttonAddProduct = new Button();
        private Button buttonEditProduct = new Button();
        private Button buttonDeleteProduct = new Button();

        public MainForm()
        {
            CheckForUpdates();
            ProductManagement.InitializeDatabase();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //button properties
            int buttonWidth = 100;
            int buttonHeight = 30;
            int buttonSpacing = 10;
            int buttonBottomSpacing = 20;
            int buttonYCoordinate = this.Height - buttonBottomSpacing - buttonHeight;
            // Initialize TabControl
            this.tabControl = new TabControl();
            this.tabControl.Dock = DockStyle.Top;
            this.tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            this.tabControl.Width = this.Width - 30; // Adjust width to fill the form
            this.tabControl.Height = 350;
            this.tabControl.Location = new Point(10, 10); // Adjust location
            this.Controls.Add(this.tabControl);

            // Initialize tab pages
            tabPageProducts = new TabPage("Products");
            tabPageParts = new TabPage("Parts");

            // Add tab pages to tab control
            this.tabControl.TabPages.AddRange(new TabPage[] { tabPageProducts, tabPageParts });

            // Initialize DataGridView for Products
            dataGridViewProducts = new DataGridView();
            dataGridViewProducts.Dock = DockStyle.Fill;
            tabPageProducts.Controls.Add(dataGridViewProducts); // Add to the "Products" tab page

            // Create a panel for the buttons
            Panel buttonPanelProducts = new Panel();
            buttonPanelProducts.Dock = DockStyle.Bottom;
            buttonPanelProducts.Height = 100; // Adjust height as needed
            tabPageProducts.Controls.Add(buttonPanelProducts);

            // Initialize buttons for Products tab
            buttonAddProduct = new Button();
            buttonAddProduct.Text = "Add Product";
            buttonAddProduct.Size = new Size(buttonWidth, buttonHeight);
            buttonAddProduct.Location = new Point(20, buttonPanelProducts.Height - buttonHeight - 10); // Adjust as needed
            buttonAddProduct.Click += buttonAddProduct_Click;
            buttonPanelProducts.Controls.Add(buttonAddProduct);

            buttonEditProduct = new Button();
            buttonEditProduct.Text = "Edit Product";
            buttonEditProduct.Size = new Size(buttonWidth, buttonHeight);
            buttonEditProduct.Location = new Point(buttonAddProduct.Right + buttonSpacing, buttonPanelProducts.Height - buttonHeight - 10);
            buttonEditProduct.Click += buttonEditProduct_Click;
            buttonPanelProducts.Controls.Add(buttonEditProduct);

            buttonDeleteProduct = new Button();
            buttonDeleteProduct.Text = "Delete Product";
            buttonDeleteProduct.Size = new Size(buttonWidth, buttonHeight);
            buttonDeleteProduct.Location = new Point(buttonEditProduct.Right + buttonSpacing, buttonPanelProducts.Height - buttonHeight - 10);
            buttonDeleteProduct.Click += buttonDeleteProduct_Click;
            buttonPanelProducts.Controls.Add(buttonDeleteProduct);

            // Initialize DataGridView for Parts
            dataGridViewParts = new DataGridView();
            dataGridViewParts.Dock = DockStyle.Fill;
            tabPageParts.Controls.Add(dataGridViewParts); // Add to the "Parts" tab page

            // Create a panel for the buttons
            Panel buttonPanelParts = new Panel();
            buttonPanelParts.Dock = DockStyle.Bottom;
            buttonPanelParts.Height = 100; // Adjust height as needed
            tabPageParts.Controls.Add(buttonPanelParts);

            // Initialize buttons for Parts tab
            Button buttonAddPart = new Button();
            buttonAddPart.Text = "Add Part";
            buttonAddPart.Location = new Point(20, buttonPanelParts.Height - buttonHeight - 10);
            buttonAddPart.Size = new Size(buttonWidth, buttonHeight);
            buttonPanelParts.Controls.Add(buttonAddPart);

            Button buttonEditPart = new Button();
            buttonEditPart.Text = "Edit Part";
            buttonEditPart.Location = new Point(buttonAddPart.Right + 10, buttonPanelParts.Height - buttonHeight - 10);
            buttonEditPart.Size = new Size(buttonWidth, buttonHeight);
            buttonPanelParts.Controls.Add(buttonEditPart);

            Button buttonDeletePart = new Button();
            buttonDeletePart.Text = "Delete Part";
            buttonDeletePart.Location = new Point(buttonEditPart.Right + 10, buttonPanelParts.Height - buttonHeight - 10);
            buttonDeletePart.Size = new Size(buttonWidth, buttonHeight);
            buttonPanelParts.Controls.Add(buttonDeletePart);

            // Hide the button panel initially
            buttonPanelParts.Visible = false;

            // Handle the SelectedIndexChanged event of the tab control
            this.tabControl.SelectedIndexChanged += (sender, e) =>
            {
                if (tabControl.SelectedTab == tabPageProducts)
                {
                    buttonPanelProducts.Visible = true;
                    buttonPanelParts.Visible = false;
                }
                else if (tabControl.SelectedTab == tabPageParts)
                {
                    buttonPanelProducts.Visible = false;
                    buttonPanelParts.Visible = true;
                }
                else 
                {
                    buttonPanelProducts.Visible = false;
                    buttonPanelParts.Visible = false;
                }
            };

            // Refresh the product list
            RefreshProductList();
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTabAppearance();
        }

        private void UpdateTabAppearance()
        {
            // Loop through all tab pages and set their appearance
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                // Set the font style of the tab based on whether it's selected or not
                tabPage.Font = (tabPage == tabControl.SelectedTab) ? new Font(tabPage.Font, FontStyle.Bold) : new Font(tabPage.Font, FontStyle.Regular);
            }
        }

        private void RefreshProductList()
        {
            try
            {
                // Retrieve product data from the warehouse
                var products = ProductManagement.PokazProdukty();

                if (products != null)
                {
                    if (dataGridViewProducts != null)
                    {
                        dataGridViewProducts.DataSource = products;
                    }
                    else
                    {
                        dataGridViewProducts = new DataGridView();
                        dataGridViewProducts.Dock = DockStyle.Fill;
                        tabPageProducts.Controls.Add(dataGridViewProducts);
                        dataGridViewProducts.DataSource = products;
                    }
                }
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
                RefreshProductList();
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
