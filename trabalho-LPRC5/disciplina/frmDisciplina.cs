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
    public partial class frmDisciplina : Form
    {

        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        bool novo;
        public frmDisciplina(bool acao, int id)
        {
            InitializeComponent();
            novo = acao;
            if(!novo)
            {
                ExibirDados(id);
            }
        }

        private void ExibirDados(int id)
        {
            string sql = "SELECT * FROM disciplina WHERE id= " + id.ToString();
            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = null;


            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtId.Text = dr["id"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtCarga_horaria.Text = dr["carga_horario"].ToString();
                    txtSemestre.Text = dr["semestre"].ToString();                  

                }

            }
            catch
            {

            }
            finally
            {
                dr.Close();
                con.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            if (novo)
            {
                sql = "INSERT INTO disciplina(nome, carga_horario, semestre, data_inc) VALUES(@nome,@carga_horaria,@semestre, @data)";
                mensagem = "Registro incluido com sucesso";
            }
            else
            {
                sql = "UPDATE disciplina SET nome=@nome, carga_horario=@carga_horaria, semestre=@semestre, data_alt=@data WHERE id=@id";
                mensagem = "Registro incluido com sucesso";
            }

            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@data", date);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@carga_horaria", Convert.ToDouble(txtCarga_horaria.Text));
            cmd.Parameters.AddWithValue("@semestre", Convert.ToInt32(txtSemestre.Text));

           
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
    }
}
