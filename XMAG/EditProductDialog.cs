using System;
using System.Windows.Forms;

namespace XMAG
{
    public partial class EditProductDialog : Form
    {
        private TextBox textBoxQuantity;
        private TextBox textBoxUwagi;
        private TextBox textBoxProductName;
        private Button buttonAdd;
        private Button buttonSave;
        private Button buttonCancel;
        private Product product;

        public EditProductDialog(Product product)
        {
            InitializeComponent();
            this.product = product; // Przypisanie informacji o produkcie
            PopulateFields(); // Wypełnienie pól formularza danymi o produkcie
        }

        private void PopulateFields()
        {
            textBoxProductName.Text = product.Name; // Ustawienie nazwy produktu w polu tekstowym
            textBoxQuantity.Text = product.Quantity.ToString(); // Ustawienie ilości produktu w polu tekstowym
        }
        private void InitializeComponent()
        {
            textBoxProductName = new TextBox();
            textBoxQuantity = new TextBox();
            textBoxUwagi = new TextBox();
            buttonSave = new Button();
            buttonCancel = new Button();
            Label labelName = new Label(); // Add label for product name
            Label labelQuantity = new Label(); // Add label for quantity
            Label labelUwagi = new Label();

            SuspendLayout();

            // Add textBoxProductName
            textBoxProductName.Location = new Point(120, 20);
            textBoxProductName.Size = new Size(150, 20);
            Controls.Add(textBoxProductName);
            // Add label for product name
            labelName.Text = "Nazwa:";
            labelName.Location = new Point(50, 20);
            labelName.Size = new Size(50, 20);
            Controls.Add(labelName);

            // Add textBoxQuantity
            textBoxQuantity.Location = new Point(120, 60);
            textBoxQuantity.Size = new Size(150, 20);
            Controls.Add(textBoxQuantity);
            // Add label for quantity
            labelQuantity.Text = "Ilość:";
            labelQuantity.Location = new Point(50, 60);
            labelQuantity.Size = new Size(50, 20);
            Controls.Add(labelQuantity);
            // Add textBoxUwagi
            textBoxUwagi.Location = new Point(120, 100);
            textBoxUwagi.Size = new Size(150, 20);
            Controls.Add(textBoxUwagi);
            // Add label for uwagi
            labelUwagi.Text = "Uwagi:";
            labelUwagi.Location = new Point(50, 100);
            labelUwagi.Size = new Size(50, 20);
            Controls.Add(labelUwagi);

            // Add buttonSave
            buttonSave.Location = new Point(50, 150);
            buttonSave.Size = new Size(75, 23);
            buttonSave.Text = "Zapisz";
            buttonSave.Click += new EventHandler(buttonSave_Click);
            Controls.Add(buttonSave);

            // Add buttonCancel
            buttonCancel.Location = new Point(150, 150);
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.Text = "Anuluj";
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
            Controls.Add(buttonCancel);

            ResumeLayout(false);
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string newName = textBoxProductName.Text.Trim();
            string quantityText = textBoxQuantity.Text.Trim();
            string NewUwagi = textBoxUwagi.Text.Trim();

            if (string.IsNullOrEmpty(newName) || string.IsNullOrEmpty(quantityText))
            {
                MessageBox.Show("Wprowadż nazwę oraz ilość.", "Brakuje informacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(quantityText, out int newQuantity) || newQuantity <= 0)
            {
                MessageBox.Show("Wprowadź liczbę jako wartość", "Niepoprawna ilość", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Update the product in the database
                ProductManagement.EdytujProdukt(product.Id, newName, newQuantity, NewUwagi);
                MessageBox.Show("Produkt został zedytowany", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Zamknięcie okna dialogowego bez dokonywania zmian
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
