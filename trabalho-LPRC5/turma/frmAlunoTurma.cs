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

namespace trabalho_LPRC5.turma
{
    public partial class frmAlunoTurma : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        public frmAlunoTurma(int id)
        {
            InitializeComponent();
            
            carregaCombo(id);

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            sql = "INSERT INTO aluno_turma(id_aluno,id_turma) VALUES (@id_aluno, @id_turma)";                  
            mensagem = "Registro incluido com sucesso";

            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id_aluno", cbAluno.SelectedValue);
            cmd.Parameters.AddWithValue("@id_turma", Convert.ToInt32(txtNumTurma.Text));


            connection.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show(mensagem,
                        "Informação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString(),
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            finally
            {
                connection.Close();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void carregaCombo(int id)
        {
            try
            {

                string consulta = "SELECT matricula, nome FROM aluno WHERE matricula NOT IN " +
                    "(SELECT id_aluno from aluno_turma where id_turma = @id_turma)";
                SqlConnection connection = con;
                con.Open();

                //Executa a consulta SQL para obter o dados
                
                SqlCommand cmd = new SqlCommand(consulta, connection);
                cmd.Parameters.AddWithValue("@id_turma", id);
                SqlDataReader leitor = cmd.ExecuteReader();

                //estrutura para armazenar os dados
                DataTable table = new DataTable();
                table.Load(leitor);
                txtNumTurma.Text = Convert.ToString(id);
                cbAluno.DataSource = table;
                cbAluno.DisplayMember = "nome";
                cbAluno.ValueMember = "matricula";
                cbAluno.SelectedIndex = -1;
                
                

            }
            catch
            {

            }
            finally
            {
                con.Close();
            }

        }

    }
}
