using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsFormsApp1.Model;
using WindowsFormsApp1.Repositories;

namespace WindowsFormsApp1
{
    public partial class Clientes : Form
    {
        public Clientes()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja incluir novo registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            Cliente novoCliente = new Cliente
            {
                Nome = textNome.Text,
                Endereço = textEndereco.Text,
                Email = textEmail.Text,
                Telefone = textTelefone.Text
            };

            ClienteRepository repo = new ClienteRepository();
            repo.AdicionarCliente(novoCliente);

            AtualizarGrid();

            MessageBox.Show("Registro incluído com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Deseja excluir o registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                int clienteId = Convert.ToInt32(dataGridViewClientes.SelectedRows[0].Cells["Id"].Value);
                ClienteRepository repo = new ClienteRepository();
                repo.RemoverCliente(clienteId);

                AtualizarGrid();

                MessageBox.Show("Registro excluído com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AtualizarGrid()
        {
            ClienteRepository repo = new ClienteRepository();
            dataGridViewClientes.DataSource = null;
            dataGridViewClientes.DataSource = repo.CarregarClientes();
            LimparCampos();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Deseja salvar as alterações?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                Cliente cliente = new Cliente
                {
                    Id = Convert.ToInt32(dataGridViewClientes.SelectedRows[0].Cells["Id"].Value),
                    Nome = textNome.Text,
                    Endereço = textEndereco.Text,
                    Email = textEmail.Text,
                    Telefone = textTelefone.Text
                };

                ClienteRepository repo = new ClienteRepository();
                repo.EditarCliente(cliente);

                AtualizarGrid();

                MessageBox.Show("Alterações salvas com sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewClientes.Rows[e.RowIndex];

                textNome.Text = row.Cells["Nome"].Value.ToString();
                textEndereco.Text = row.Cells["Endereço"].Value.ToString();
                textTelefone.Text = row.Cells["Telefone"].Value.ToString();
                textEmail.Text = row.Cells["Email"].Value.ToString();

                dataGridViewClientes.ClearSelection();
                dataGridViewClientes.Rows[e.RowIndex].Selected = true;
                // Faz o DataGridView focar na linha selecionada
                dataGridViewClientes.FirstDisplayedScrollingRowIndex = e.RowIndex;
            }
        }

        private void LimparCampos()
        {
            textNome.Text = "";
            textEndereco.Text = "";
            textTelefone.Text = "";
            textEmail.Text = "";
            dataGridViewClientes.ClearSelection();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }
}
