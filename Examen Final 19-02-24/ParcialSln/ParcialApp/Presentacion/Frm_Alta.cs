
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

namespace BibliotecaClases.Presentacion
{
    public partial class Frm_Alta : Form
    {

        public Frm_Alta()
        {
            InitializeComponent();

        }




        private void btnAceptar_Click(object sender, EventArgs e)
        {

          //completar...
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void Frm_Alta_Presupuesto_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void CargarCombo()
        {
            //completar...
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //completar...
        }

        private bool ExisteEspecialidadFecha(string medico, string fecha)
        {
            //completar...
            return false;
        }

       



        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 5)
            {
               //completar...
            }
        }
    }
}
