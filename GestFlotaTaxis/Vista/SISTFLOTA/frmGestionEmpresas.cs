﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Controladora;
using Modelo.SEGURIDAD;

namespace Vista.SISTFLOTA
{
    public partial class frmGestionEmpresas : Form
    {
        Controladora.ControladoraEmpresas ctrlEmpresas = Controladora.ControladoraEmpresas.getINSTANCIA;
        Usuario oUsuarioActual;
        private Modelo.Empresa oEmpresa;

        public frmGestionEmpresas(Usuario oUsuario)
        {
            oUsuarioActual = oUsuario;
            InitializeComponent();

            btnNuevo.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
        }

        private void ArmarGrilla()
        {
            lblSinResultados.Visible = false;
            bdsEmpresa.Sort = null;
            bdsEmpresa.DataSource = null;
            bdsEmpresa.Clear();
            bdsEmpresa.DataSource = ctrlEmpresas.ListarEmpresas();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEmpresa ofrmEmpresa = new frmEmpresa();

            DialogResult drABIERTO = ofrmEmpresa.ShowDialog();
            if (drABIERTO == DialogResult.OK)
            {
                ArmarGrilla();
                MessageBox.Show("La Empresa se agregó correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       

        private void grillaEmpresas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (bdsEmpresa.Current!= null)
            {
                oEmpresa = (Modelo.Empresa)bdsEmpresa.Current;

                frmEmpresa ofrmEmpresa = new frmEmpresa("CONSULTA", oEmpresa);
                DialogResult drABIERTO = ofrmEmpresa.ShowDialog();
                if (drABIERTO == DialogResult.OK)
                {
                    ArmarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una Empresa", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (bdsEmpresa.Current != null)
            {
                oEmpresa = (Modelo.Empresa)bdsEmpresa.Current;

                frmEmpresa ofrmEmpresa = new frmEmpresa("MODIFICACION", oEmpresa);
                DialogResult drABIERTO = ofrmEmpresa.ShowDialog();
                if (drABIERTO == DialogResult.OK)
                {
                    MessageBox.Show("La Empresa ha sido modificada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ArmarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Vehículo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (bdsEmpresa.Current != null)
            {
                oEmpresa = (Modelo.Empresa)bdsEmpresa.Current;

                DialogResult result = MessageBox.Show("¿Esta seguro que desea eliminar la Empresa " + oEmpresa.RazonSocial+ "?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ctrlEmpresas.EliminarEmpresa(oEmpresa);

                 ArmarGrilla();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Vehículo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
