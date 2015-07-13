namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using System.Linq;

    /// <summary>
    /// Summary description for InformeAltaBajaLegajo.
    /// </summary>
    public partial class CredencialVehiculosEventuales : Telerik.Reporting.Report
    {
        public CredencialVehiculosEventuales()
        {
            /// <summary>
            /// Required for telerik Reporting designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public void InitReport(string Patente, string Vigencia,string PuestoIngreso, string Empresa)
        {
            txtPatente.Value = Patente;
            txtVigencia.Value = Vigencia;
            txtPuestoIngreso.Value = PuestoIngreso;
            txtEmpresa.Value = Empresa;

        }
    }
}