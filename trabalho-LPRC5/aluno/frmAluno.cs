using DotCEP;
using DotCEP.Localidades;
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
    public partial class frmAluno : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        bool novo;
        public frmAluno(bool acao, int id)
        {
            InitializeComponent();
            carregaCombo();
            novo = acao;
            if (!novo)
            {
                exibirDados(id);
            } 
        }
        private void carregaCombo()
        {
            try
            {

                string consulta = "SELECT id, nome FROM curso ORDER BY id";
                SqlConnection connection = con;
                con.Open();
                
                //Executa a consulta SQL para obter o dados
                SqlCommand cmd = new SqlCommand(consulta, connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                //estrutura para armazenar os dados
                DataTable table = new DataTable();
                table.Load(leitor);

                cbCurso.DataSource = table;
                cbCurso.DisplayMember = "nome";
                cbCurso.ValueMember = "id";
                cbCurso.SelectedIndex = -1;


            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            
        }

        private void exibirDados(int id)
        {
            string sql = "SELECT * FROM aluno WHERE matricula = " + id.ToString();
            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = null;


            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();               
                
                while(dr.Read())
                {
                    txtMatricula.Text = dr["matricula"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    cbCurso.SelectedValue = Convert.ToInt32(dr["curso"].ToString());
                    txtCidade.Text = dr["cidade"].ToString();
                    txtEstado.Text = dr["estado"].ToString();
                    txtEndereco.Text = dr["endereco"].ToString();
                    txtCEP.Text = dr["cep"].ToString();
                    dtpDataNasc.Text = dr["data_nasc"].ToString();
                    dtpDataIngresso.Text = dr["data_ing"].ToString();
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

        private void buscaCEP()
        {
            Endereco endereco = new Endereco();
            try
            {
                if (!string.IsNullOrEmpty(txtCEP.Text))
                {
                    endereco = Consultas.ObterEnderecoCompleto(txtCEP.Text);
                    if (!DotCEP.Validacoes.VerificarExistenciaDoCEP(txtCEP.Text))
                    {
                        
                        MessageBox.Show("CEP não encontrado!","AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // txtCEP.Text = endereco.cep.Replace("-","");
                        txtCidade.Text = endereco.localidade;
                        txtEndereco.Text = endereco.logradouro;
                        txtEstado.Text = endereco.uf;

                    }
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Por favor, conecte-se a internet para válidar o CEP!","Problema de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            if (novo)
            {
                sql = "INSERT INTO aluno(nome, data_nasc, data_ing, curso, cidade, estado, email, endereco,"
                    + "data_inc, cep) " +
                    "VALUES(@nome,@data_nasc, @data_ing, @curso, @cidade, @estado, @email, @endereco, @data,@cep)";
                mensagem = "Registro incluido com sucesso";
            }
            else
            {
                sql = "UPDATE aluno SET nome = @nome, data_nasc = @data_nasc, data_ing = @data_ing, curso = @curso, cidade = @cidade, " +
                    "estado = @estado, email = @email, endereco = @endereco, data_alt = @data, cep= @cep WHERE matricula=@id";
                mensagem = "Registro alterado com sucesso";

            }

            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id",txtMatricula.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@data_nasc", dtpDataNasc.Text);
            cmd.Parameters.AddWithValue("@data_ing", dtpDataIngresso.Text);            
            cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
            cmd.Parameters.AddWithValue("@estado", txtEstado.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cep", txtCEP.Text);
            cmd.Parameters.AddWithValue("@data", date);

            if (cbCurso.SelectedIndex == -1 || cbCurso.SelectedValue == null)
            {
                cmd.Parameters.AddWithValue("@curso", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@curso", cbCurso.SelectedValue);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAluno_Load(object sender, EventArgs e)
        {
           
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            buscaCEP();
        }
    }
}
