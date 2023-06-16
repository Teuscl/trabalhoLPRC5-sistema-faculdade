using DotCEP;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Faculdade
{
    public partial class frmProfessor : Form
    {
        SqlConnection con = new SqlConnection(@"Server=DESKTOP-1A208KA;Database=faculdade;Trusted_Connection=True;");
        string sql, mensagem;
        bool novo;
        public frmProfessor(bool acao, int id)
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
                string consulta = "SELECT id, nome FROM departamento ORDER BY id";
                SqlConnection connection = con;
                con.Open();

                //Executa a consulta SQL para obter o dados
                SqlCommand cmd = new SqlCommand(consulta, connection);
                SqlDataReader leitor = cmd.ExecuteReader();

                //estrutura para armazenar os dados
                DataTable table = new DataTable();
                table.Load(leitor);

                cbDepartamento.DataSource = table;
                cbDepartamento.DisplayMember = "nome";
                cbDepartamento.ValueMember = "id";
                cbDepartamento.SelectedIndex = -1;
                con.Close();
            } catch 
            {
            }           
        }

        private void exibirDados(int id)
        {
            string sql = "SELECT * FROM professor WHERE registro = " + id.ToString();
            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = null;


            try
            {
                connection.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    txtRegistro.Text = dr["registro"].ToString();
                    txtNome.Text = dr["nome"].ToString();
                    txtEmail.Text = dr["email"].ToString();
                    cbDepartamento.SelectedValue = Convert.ToInt32(dr["id_departamento"].ToString());
                    txtCidade.Text = dr["cidade"].ToString();
                    txtEstado.Text = dr["estado"].ToString();
                    txtEndereco.Text = dr["endereco"].ToString();
                    txtCEP.Text = dr["cep"].ToString();
                    

                }
                dr.Close();
                con.Close();

            }
            catch
            {

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

                        MessageBox.Show("CEP não encontrado!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Por favor, insira um CEP válido!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor, conecte-se a internet para válidar o CEP!", "Problema de conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            if (novo)
            {
                sql = "INSERT INTO professor(nome, cidade, estado, email, endereco,"
                    + "data_inc, cep, id_departamento)" +
                    "VALUES(@nome,@cidade, @estado, @email, @endereco, @data,@cep,@id_departamento)";
                mensagem = "Registro incluido com sucesso";
            }
            else
            {
                sql = "UPDATE professor SET nome = @nome,cidade = @cidade, estado = @estado, email = @email," +
                    " endereco = @endereco, data_alt = @data, cep= @cep, id_departamento=@id_departamento WHERE registro=@id";
                mensagem = "Registro incluido com sucesso";
            }

            SqlConnection connection = con;
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", txtRegistro.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);            
            cmd.Parameters.AddWithValue("@cidade", txtCidade.Text);
            cmd.Parameters.AddWithValue("@estado", txtEstado.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@cep", txtCEP.Text);
            cmd.Parameters.AddWithValue("@data", date);
            if( cbDepartamento.SelectedIndex == -1 || cbDepartamento.SelectedValue == null)
            {
                cmd.Parameters.AddWithValue("@id_departamento", DBNull.Value);

            }
            else
            {
                cmd.Parameters.AddWithValue("@id_departamento", cbDepartamento.SelectedValue);
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

        private void frmProfessor_Load(object sender, EventArgs e)
        {
            
        }

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            buscaCEP();
        }

        

        
    }
}
