using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalho_LPRC5
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void btnAluno_Click(object sender, EventArgs e)
        {
            frmMenuAluno frm = new frmMenuAluno();
            frm.ShowDialog();
        }

        private void btnCurso_Click(object sender, EventArgs e)
        {
            frmMenuCurso frm = new frmMenuCurso();
            frm.ShowDialog();
        }

        private void btnDepartamento_Click(object sender, EventArgs e)
        {
            frmMenuDepartamento frm = new frmMenuDepartamento();
            frm.ShowDialog();
        }

        private void btnTurma_Click(object sender, EventArgs e)
        {
            frmMenuTurma frm = new frmMenuTurma();
            frm.ShowDialog();
        }

        private void btnProfessor_Click(object sender, EventArgs e)
        {
            frmMenuProfessor frm = new frmMenuProfessor();
            frm.ShowDialog();
        }

        private void btnDisciplina_Click(object sender, EventArgs e)
        {
            frmMenuDisciplina frm = new frmMenuDisciplina();
            frm.ShowDialog();
        }
    }
}
