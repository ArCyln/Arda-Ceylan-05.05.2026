using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1 
{ 

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lbldurum.Visible = false;
        }

        private void SaveToFile()
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "243154004 Arda Ceylan 05.05.2026",
                "kayitlar.txt"
            );

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        List<string> cells = new List<string>();

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cells.Add(cell.Value?.ToString() ?? "");
                        }

                        // separate columns with |
                        sw.WriteLine(string.Join("|", cells));
                    }
                }
            }
        }

        private void LoadFromFile()
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "243154004 Arda Ceylan 05.05.2026",
                "kayitlar.txt"
            );

            if (!File.Exists(filePath))
                return;

            dataGridView1.Rows.Clear();

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] cells = line.Split('|');
                dataGridView1.Rows.Add(cells);
            }
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            txtfiyat.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string phone = txtfiyat.Text;
            if (string.IsNullOrWhiteSpace(txturun.Text) ||
            string.IsNullOrWhiteSpace(txtkategori.Text) ||
            string.IsNullOrWhiteSpace(txtfiyat.Text))
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun!");
                return;
            }
            
            dataGridView1.Rows.Add(
            txturun.Text,
            txtkategori.Text,
            txtfiyat.Text
            );
            lbldurum.Visible = true;
            lbldurum.Text = "Kayıt Eklendi";
            SaveToFile();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            txtfiyat.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string phone = txtfiyat.Text;
            if (string.IsNullOrWhiteSpace(txturun.Text) ||
            string.IsNullOrWhiteSpace(txtkategori.Text) ||
            string.IsNullOrWhiteSpace(txtfiyat.Text))
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun!");
                return;
            }

            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Cells[0].Value = txturun.Text;
                dataGridView1.CurrentRow.Cells[1].Value = txtkategori.Text;
                dataGridView1.CurrentRow.Cells[2].Value = txtfiyat.Text;
                lbldurum.Visible = true;
                lbldurum.Text = "Kayıt Güncellendi";
                SaveToFile();
            }
        }



        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                lbldurum.Visible = true;
                lbldurum.Text = "Kayıt Silindi";
                SaveToFile();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txturun.Clear();
            txtkategori.Clear();
            txtfiyat.Clear();
            lbldurum.Visible = true;
            lbldurum.Text = "Alanlar Temizlendi";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txturun.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtkategori.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtfiyat.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 6;

            dataGridView1.Columns[0].Name = "Ürün";
            dataGridView1.Columns[1].Name = "Kategori";
            dataGridView1.Columns[2].Name = "Fiyat";
            LoadFromFile();

        }

        private void txturun_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtkategori_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txturun.Text = row.Cells[0].Value?.ToString();
                txtkategori.Text = row.Cells[1].Value?.ToString();
                txtfiyat.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtfiyat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void formToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (Control c in this.Controls)
            {
                if (c != txtabout && c != menuStrip1)
                    c.Visible = false;
            }

            txtabout.Visible = true;
            txtabout.BringToFront();
            txtabout.Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        }

        private void çıkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c != menuStrip1)
                    c.Visible = true;
            }

            txtabout.Visible = false;
        }
    }   
}