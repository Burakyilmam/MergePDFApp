using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MergePDFApp
{
    public partial class frmPartial : Form
    {
        public frmMerge _frmPDFBirlestir;
        public frmPartial(frmMerge frmPDFBirlestir)
        {
            InitializeComponent();
           
            _frmPDFBirlestir = frmPDFBirlestir;

            btnPartialPDF.Click += BtnPartialPDF_Click;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            txtFirst.Properties.MinValue = 1;
            txtFirst.Properties.MaxValue = _frmPDFBirlestir.selectedPDF.PageSize;

            txtLast.Properties.MinValue = 1;
            txtLast.Properties.MaxValue = _frmPDFBirlestir.selectedPDF.PageSize;
            txtLast.EditValue = _frmPDFBirlestir.selectedPDF.PageSize;
        }

        private void BtnPartialPDF_Click(object sender, EventArgs e)
        {
            int first = Convert.ToInt32(txtFirst.EditValue);
            int last = Convert.ToInt32(txtLast.EditValue);
            frmMerge.CreatePartialPDF(_frmPDFBirlestir.selectedPDF, first, last);
            this.Close();
        }
    }
}
