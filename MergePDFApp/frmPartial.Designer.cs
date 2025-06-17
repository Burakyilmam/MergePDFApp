
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
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFirst.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLast.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnPartialPDF);
            this.panelControl1.Controls.Add(this.txtFirst);
            this.panelControl1.Controls.Add(this.txtLast);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(127, 103);
            this.panelControl1.TabIndex = 6;
            // 
            // btnPartialPDF
            // 
            this.btnPartialPDF.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPartialPDF.ImageOptions.SvgImage")));
            this.btnPartialPDF.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnPartialPDF.Location = new System.Drawing.Point(5, 58);
            this.btnPartialPDF.Name = "btnPartialPDF";
            this.btnPartialPDF.Size = new System.Drawing.Size(114, 23);
            this.btnPartialPDF.TabIndex = 5;
            this.btnPartialPDF.Text = "Partial PDF";
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(12, 24);
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
            this.txtLast.Location = new System.Drawing.Point(70, 24);
            this.txtLast.Name = "txtLast";
            this.txtLast.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtLast.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtLast.Properties.Mask.EditMask = "d";
            this.txtLast.Size = new System.Drawing.Size(43, 20);
            this.txtLast.TabIndex = 4;
            // 
            // frmPartial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(127, 103);
            this.Controls.Add(this.panelControl1);
            this.Name = "frmPartial";
            this.Text = "frmPartial";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFirst.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLast.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPartialPDF;
        private DevExpress.XtraEditors.SpinEdit txtFirst;
        private DevExpress.XtraEditors.SpinEdit txtLast;
    }
}