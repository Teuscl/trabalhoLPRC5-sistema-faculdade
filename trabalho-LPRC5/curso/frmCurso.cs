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
    public partial class frmCurso : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        bool novo;
        public frmCurso(bool acao, int id)
        {
            InitializeComponent();
            novo = acao;
            if (!novo)
            {
                ExibirDados(id);
            }
        }

        private void ExibirDados(int id)
        {
            string sql = "SELECT * FROM curso WHERE id= " + id.ToString();
            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = null;


            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtIdCurso.Text = dr["id"].ToString();
                    txtNomeCurso.Text = dr["nome"].ToString();
                    txtDuracao.Text = dr["duracao_sem"].ToString();
                    cbTipoCurso.Text = dr["tipo"].ToString();

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
                sql = "INSERT INTO curso(nome, duracao_sem, tipo, data_inc) VALUES(@nome,@duracao,@tipo, @data)";
                mensagem = "Inclusão realizada com sucesso!";
            }
            else
            {
                sql = "UPDATE curso SET nome=@nome, duracao_sem=@duracao, tipo=@tipo, data_alt=@data WHERE id=@id";
                mensagem = "Alteração realizada com sucesso!";
            }

            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", txtIdCurso.Text);
            cmd.Parameters.AddWithValue("@data", date);
            cmd.Parameters.AddWithValue("@nome", txtNomeCurso.Text);
            cmd.Parameters.AddWithValue("@duracao", Convert.ToInt32(txtDuracao.Text));
            cmd.Parameters.AddWithValue("@tipo", cbTipoCurso.Text);


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
