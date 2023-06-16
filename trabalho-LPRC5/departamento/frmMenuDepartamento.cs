﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalho_LPRC5
{
    public partial class frmMenuDepartamento : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        int id;
        public frmMenuDepartamento()
        {
            InitializeComponent();
        }

        private void ExibirDados()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM departamento ORDER BY id", con);
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.RowCount == 0)
                {
                    btnAlterar.Enabled = false;
                    btnRemover.Enabled = false;

                }
                else
                {
                    btnAlterar.Enabled = true;
                    btnRemover.Enabled = true;
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            bool acao = true;
            frmDepartamento frm = new frmDepartamento(acao, 0);
            frm.ShowDialog();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            bool acao = false;
            frmDepartamento frm = new frmDepartamento(acao, id);
            frm.ShowDialog();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());

            if (MessageBox.Show("Deseja excluir o registro?", "Confirma Exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE departamento WHERE id=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro excluido com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM departamento WHERE nome LIKE '%" + txtPesquisar.Text + "%'" +
               "ORDER BY nome";


            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            connection.Close();
        }

        private void frmMenuDepartamento_Activated(object sender, EventArgs e)
        {
            ExibirDados();
        }
    }
}
