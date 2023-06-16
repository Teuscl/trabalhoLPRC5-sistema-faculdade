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
    public partial class frmTurma : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        bool novo;
        string consulta_professor = "SELECT registro, nome FROM professor ORDER BY nome";
        string consulta_curso = "SELECT id, nome FROM curso ORDER BY nome";
        string consulta_disciplina = "SELECT id, nome FROM disciplina ORDER BY nome";

        private void frmTurma_Load(object sender, EventArgs e)
        {


        }



        public frmTurma(bool acao, int id)
        {
            InitializeComponent();
            carregaCombo(consulta_professor, ref cbProfessor, "registro");
            carregaCombo(consulta_curso, ref cbCurso, "id");
            carregaCombo(consulta_disciplina, ref cbDisciplina, "id");
            novo = acao;
            if (!acao)
            {
                ExibirDados(id);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExibirDados(int id)
        {
            string sql = "SELECT * FROM turma WHERE id = " + id.ToString();
            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = null;

            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtNomeTurma.Text = dr["nome"].ToString();
                    txtId.Text = dr["id"].ToString();
                    cbCurso.SelectedValue = Convert.ToInt32(dr["curso"].ToString());
                    cbDisciplina.SelectedValue = Convert.ToInt32(dr["disciplina"].ToString());
                    cbProfessor.SelectedValue = Convert.ToInt32(dr["professor"].ToString());
                    



                }
            } catch 
            { 
            }
            finally
            {
                connection.Close();
                dr.Close();
            }

        }   

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
                var date = DateTime.Now;
                if (novo)
                {
                    sql = "INSERT INTO turma(curso,nome, professor, disciplina, data_inc)" +
                        "VALUES(@curso,@nome, @professor, @disciplina, @data)";
                    mensagem = "Registro incluido com sucesso";
                }
                else
                {
                    sql = "UPDATE turma SET curso= @curso, nome=@nome, professor=@professor,disciplina=@disciplina, data_alt= @data WHERE id=@id";
                    mensagem = "Registro alterado com sucesso";

                }

                SqlConnection connection = con;
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@data", date);
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@nome", txtNomeTurma.Text);



            //curso
            if (cbCurso.SelectedIndex == -1 || cbCurso.SelectedValue == null)
                {
                    cmd.Parameters.AddWithValue("@curso", DBNull.Value);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@curso", cbCurso.SelectedValue);
                }

                //disciplina
                if (cbDisciplina.SelectedIndex == -1 || cbDisciplina.SelectedValue == null)
                {
                    cmd.Parameters.AddWithValue("@disciplina", DBNull.Value);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@disciplina", cbDisciplina.SelectedValue);
                }

                //professor
                if (cbProfessor.SelectedIndex == -1 || cbProfessor.SelectedValue == null)
                {
                    cmd.Parameters.AddWithValue("@professor", DBNull.Value);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@professor", cbProfessor.SelectedValue);
                }

                connection.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show(mensagem,
                            "Informação",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    connection.Close();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void carregaCombo(string consulta, ref ComboBox nome_combo, string col)
            {
                try
                {

                    //abre a conexão
                    SqlConnection connection = con;
                    con.Open();

                    //Executa a consulta SQL para obter o dados                         
                    SqlCommand cmd = new SqlCommand(consulta, connection);
                    SqlDataReader leitor = cmd.ExecuteReader();
                    if (leitor != null)
                    {

                    }

                    //estrutura para armazenar os dados

                    DataTable table = new DataTable();
                    table.Load(leitor);


                    nome_combo.DataSource = table;
                    nome_combo.DisplayMember = "nome";
                    nome_combo.ValueMember = col;
                    nome_combo.SelectedIndex = -1;

                    leitor.Close();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao preencher o ComboBox: " + ex.Message);
                }
            }
    }
}


