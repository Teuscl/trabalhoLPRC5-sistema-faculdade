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
using trabalho_LPRC5.turma;

namespace trabalho_LPRC5
{
    public partial class frmMenuTurma : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        int id;
        bool novo;
        public frmMenuTurma()
        {
            InitializeComponent();
            btnRemoverAluno.Enabled = false;
        }
        private void ExibirDados()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM turma ORDER BY id", con);
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
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
               
            }
            finally
            {
                con.Close();
               
            }
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            novo = true;
            frmTurma frm = new frmTurma(novo, 0);
            frm.ShowDialog();

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            novo = false;
            frmTurma frm = new frmTurma(novo, id);
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
                    SqlCommand cmd = new SqlCommand("DELETE turma WHERE id=@id", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro excluido com sucesso!");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    exibirDados2();
                    
                }
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM turma WHERE nome LIKE '%" + txtPesquisar.Text + "%'" +
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

        private void btnCadastraAluno_Click(object sender, EventArgs e)
        {
            
            id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());            
            frmAlunoTurma frm = new frmAlunoTurma(id);
            frm.ShowDialog();
            
        }

        private void frmMenuTurma_Activated(object sender, EventArgs e)
        {
            ExibirDados();
        }

        private void exibirDados2()
        {
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                try
                {
                    id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    string a = $"SELECT id_aluno as Matricula,a.nome as 'Nome do aluno' ,id_turma, t.nome as 'Nome da turma' FROM aluno_turma INNER JOIN turma t ON id_turma = t.id " +
                        $"INNER JOIN aluno a ON id_aluno = a.matricula WHERE id_turma = {id}  ORDER BY id_aluno";
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapt = new SqlDataAdapter(a, con);
                    adapt.Fill(dt);
                    dataGridView2.DataSource = dt;
                    if (dataGridView2.RowCount == 0)
                    {
                        btnRemoverAluno.Enabled = false;


                    }
                    else
                    {
                        
                        btnRemoverAluno.Enabled = true;
                    }

                    con.Close();

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
            else
            {

            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            exibirDados2();

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            exibirDados2();
        }

        private void btnRemoverAluno_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            int id_turma = Convert.ToInt32(dataGridView2.CurrentRow.Cells[2].Value.ToString());
            if (MessageBox.Show("Deseja excluir o registro?", "Confirma Exclusão",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE aluno_turma WHERE id_aluno=@id AND " +
                        "id_turma =@id_turma  ", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@id_turma", id_turma);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro excluido com sucesso!");
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    con.Close();
                    ExibirDados();
                    exibirDados2();

                }
            }
        }
    }
}
