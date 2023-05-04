using DotCEP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Faculdade
{
    public partial class frmProfessor : Form
    {
        public frmProfessor()
        {
            InitializeComponent();
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

        private void txtCEP_Leave(object sender, EventArgs e)
        {
            buscaCEP();
        }
    }
}
