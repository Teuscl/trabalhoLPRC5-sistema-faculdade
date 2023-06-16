using System;
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
    public partial class frmMenuCurso : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        int id;
        public frmMenuCurso()
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
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM curso ORDER BY id", con);
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
            bool novo = true;
            frmCurso frm = new frmCurso(novo, 0);
            frm.ShowDialog();
        }


        private void btnAlterar_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            bool acao = false;
            frmCurso frm = new frmCurso(acao, id);
            frm.ShowDialog();
        }


        private void btnRemover_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            if (MessageBox.Show("Deseja excluir o registro?", "Confirma Exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE curso WHERE id=@id", con);
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

        private void frmMenuCurso_Activated(object sender, EventArgs e)
        {
            ExibirDados();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM curso WHERE nome LIKE '%" + txtPesquisar.Text + "%'" +
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
    }
}
