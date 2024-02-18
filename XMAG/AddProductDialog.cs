using System;
using System.Windows.Forms;

namespace XMAG
{
    public partial class AddProductDialog : Form
    {
        public TextBox textBoxProductName;
        public TextBox textBoxQuantity;
        public TextBox textBoxUwagi;
        public Button buttonAdd;
        public Button buttonCancel;

        public AddProductDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            textBoxProductName = new TextBox();
            textBoxQuantity = new TextBox();
            textBoxUwagi = new TextBox();
            buttonAdd = new Button();
            buttonCancel = new Button();
            Label labelName = new Label(); // Add label for product name
            Label labelQuantity = new Label(); // Add label for quantity
            Label labelUwagi = new Label(); // Add label for product name

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

            // Add buttonAdd
            buttonAdd.Location = new Point(50, 150);
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.Text = "Dodaj";
            buttonAdd.Click += new EventHandler(buttonAdd_Click);
            Controls.Add(buttonAdd);

            // Add buttonCancel
            buttonCancel.Location = new Point(150, 150);
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.Text = "Anuluj";
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
            Controls.Add(buttonCancel);


            ResumeLayout(false);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxProductName.Text.Trim();
            string quantityText = textBoxQuantity.Text.Trim();
            string uwagi = textBoxUwagi.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(quantityText))
            {
                MessageBox.Show("Wprowadż nazwę oraz ilość.", "Brakuje informacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(quantityText, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Wprowadź liczbę jako wartość", "Niepoprawna ilość", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Add the new product
                ProductManagement.DodajNowyProdukt(name, quantity, uwagi);
                MessageBox.Show("Produkt został dodany", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
