
namespace MergePDFApp
{
    partial class frmPartial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPartial));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnPartialPDF = new DevExpress.XtraEditors.SimpleButton();
            this.txtFirst = new DevExpress.XtraEditors.SpinEdit();
            this.txtLast = new DevExpress.XtraEditors.SpinEdit();
            this.lblFirst = new DevExpress.XtraEditors.LabelControl();
            this.lblLast = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirst.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLast.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblLast);
            this.panelControl1.Controls.Add(this.lblFirst);
            this.panelControl1.Controls.Add(this.btnPartialPDF);
            this.panelControl1.Controls.Add(this.txtFirst);
            this.panelControl1.Controls.Add(this.txtLast);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(212, 99);
            this.panelControl1.TabIndex = 6;
            // 
            // btnPartialPDF
            // 
            this.btnPartialPDF.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPartialPDF.ImageOptions.SvgImage")));
            this.btnPartialPDF.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnPartialPDF.Location = new System.Drawing.Point(63, 63);
            this.btnPartialPDF.Name = "btnPartialPDF";
            this.btnPartialPDF.Size = new System.Drawing.Size(83, 23);
            this.btnPartialPDF.TabIndex = 5;
            this.btnPartialPDF.Text = "Parçala";
            // 
            // txtFirst
            // 
            this.txtFirst.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtFirst.Location = new System.Drawing.Point(48, 31);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFirst.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtFirst.Properties.Mask.EditMask = "d";
            this.txtFirst.Size = new System.Drawing.Size(43, 20);
            this.txtFirst.TabIndex = 3;
            // 
            // txtLast
            // 
            this.txtLast.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtLast.Location = new System.Drawing.Point(120, 31);
            this.txtLast.Name = "txtLast";
            this.txtLast.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLast.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtLast.Properties.Mask.EditMask = "d";
            this.txtLast.Size = new System.Drawing.Size(43, 20);
            this.txtLast.TabIndex = 4;
            // 
            // lblFirst
            // 
            this.lblFirst.Location = new System.Drawing.Point(48, 12);
            this.lblFirst.Name = "lblFirst";
            this.lblFirst.Size = new System.Drawing.Size(47, 13);
            this.lblFirst.TabIndex = 6;
            this.lblFirst.Text = "Başlangıç ";
            // 
            // lblLast
            // 
            this.lblLast.Location = new System.Drawing.Point(120, 12);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(19, 13);
            this.lblLast.TabIndex = 6;
            this.lblLast.Text = "Bitiş";
            // 
            // frmPartial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 99);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmPartial";
            this.Text = "PDF Parçalama";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirst.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLast.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPartialPDF;
        private DevExpress.XtraEditors.SpinEdit txtFirst;
        private DevExpress.XtraEditors.SpinEdit txtLast;
        private DevExpress.XtraEditors.LabelControl lblLast;
        private DevExpress.XtraEditors.LabelControl lblFirst;
    }
}