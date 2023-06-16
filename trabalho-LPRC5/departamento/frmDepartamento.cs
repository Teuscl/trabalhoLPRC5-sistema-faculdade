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
    public partial class frmDepartamento : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        bool novo;

        public frmDepartamento(bool acao, int id)
        {
            InitializeComponent();
            carregaCombo();
            novo = acao;
            if (!acao)
            {
                ExibirDados(id);
            }
        }

        private void carregaCombo()
        {
            try
            {
                string consulta = "SELECT registro, nome FROM professor ORDER BY nome";
                //abre a conexão
                SqlConnection connection = con;
                con.Open();

                //Executa a consulta SQL para obter o dados                         
                SqlCommand cmd = new SqlCommand(consulta, connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                //estrutura para armazenar os dados
                
                DataTable table = new DataTable();
                table.Load(leitor);
                
                cbProf_resp.DataSource = table;                
                cbProf_resp.DisplayMember = "nome";
                cbProf_resp.ValueMember = "registro";
                cbProf_resp.SelectedIndex = -1;          
                


                leitor.Close();
                con.Close();
                




            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro ao preencher o ComboBox: " + ex.Message);
            }
        }

        private void ExibirDados(int id) 
        {
            string sql = "SELECT * FROM departamento WHERE id= " + id.ToString();
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
                    txtLocalizacao.Text = dr["localizacao"].ToString();
                    cbProf_resp.SelectedValue = Convert.ToInt32(dr["prof_responsavel"].ToString());

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

        private void frmDepartamento_Load(object sender, EventArgs e)
        {
           
            

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            if (novo)
            {
                sql = "INSERT INTO departamento(nome, prof_responsavel, localizacao, data_inc) VALUES(@nome,@prof,@localizacao, @data)";
                mensagem = "Registro incluido com sucesso";
            }
            else
            {
                sql = "UPDATE departamento SET nome=@nome, prof_responsavel=@prof, localizacao=@localizacao, data_alt=@data WHERE id=@id";
                mensagem = "Registro alterado com sucesso";
            }

            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@data", date);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@localizacao", txtLocalizacao.Text);
            

            if (cbProf_resp.SelectedIndex == -1 || cbProf_resp.SelectedValue == null)
            {
                cmd.Parameters.AddWithValue("@prof", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@prof", cbProf_resp.SelectedValue);
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
