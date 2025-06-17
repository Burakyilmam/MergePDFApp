
namespace MergePDFApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.grdPDF = new DevExpress.XtraGrid.GridControl();
            this.grdvPDF = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlButon = new DevExpress.XtraEditors.PanelControl();
            this.btnUploadPDF = new DevExpress.XtraEditors.SimpleButton();
            this.btnMergePDF = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadPDF = new DevExpress.XtraEditors.SimpleButton();
            this.pdfViewer = new DevExpress.XtraPdfViewer.PdfViewer();
            ((System.ComponentModel.ISupportInitialize)(this.grdPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButon)).BeginInit();
            this.pnlButon.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdPDF
            // 
            this.grdPDF.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdPDF.Location = new System.Drawing.Point(0, 0);
            this.grdPDF.MainView = this.grdvPDF;
            this.grdPDF.Name = "grdPDF";
            this.grdPDF.Size = new System.Drawing.Size(657, 391);
            this.grdPDF.TabIndex = 7;
            this.grdPDF.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdvPDF});
            // 
            // grdvPDF
            // 
            this.grdvPDF.GridControl = this.grdPDF;
            this.grdvPDF.Name = "grdvPDF";
            this.grdvPDF.OptionsBehavior.Editable = false;
            this.grdvPDF.OptionsBehavior.ReadOnly = true;
            this.grdvPDF.OptionsDetail.EnableMasterViewMode = false;
            this.grdvPDF.OptionsSelection.MultiSelect = true;
            this.grdvPDF.OptionsView.ShowGroupPanel = false;
            // 
            // pnlButon
            // 
            this.pnlButon.Controls.Add(this.btnUploadPDF);
            this.pnlButon.Controls.Add(this.btnMergePDF);
            this.pnlButon.Controls.Add(this.btnDownloadPDF);
            this.pnlButon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButon.Location = new System.Drawing.Point(0, 391);
            this.pnlButon.Name = "pnlButon";
            this.pnlButon.Size = new System.Drawing.Size(1277, 35);
            this.pnlButon.TabIndex = 8;
            // 
            // btnUploadPDF
            // 
            this.btnUploadPDF.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnUploadPDF.ImageOptions.SvgImage")));
            this.btnUploadPDF.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnUploadPDF.Location = new System.Drawing.Point(488, 6);
            this.btnUploadPDF.Name = "btnUploadPDF";
            this.btnUploadPDF.Size = new System.Drawing.Size(119, 23);
            this.btnUploadPDF.TabIndex = 0;
            this.btnUploadPDF.Text = "Upload PDF";
            // 
            // btnMergePDF
            // 
            this.btnMergePDF.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnMergePDF.ImageOptions.SvgImage")));
            this.btnMergePDF.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnMergePDF.Location = new System.Drawing.Point(613, 6);
            this.btnMergePDF.Name = "btnMergePDF";
            this.btnMergePDF.Size = new System.Drawing.Size(112, 23);
            this.btnMergePDF.TabIndex = 0;
            this.btnMergePDF.Text = "Merge PDF";
            // 
            // btnDownloadPDF
            // 
            this.btnDownloadPDF.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDownloadPDF.ImageOptions.SvgImage")));
            this.btnDownloadPDF.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnDownloadPDF.Location = new System.Drawing.Point(731, 6);
            this.btnDownloadPDF.Name = "btnDownloadPDF";
            this.btnDownloadPDF.Size = new System.Drawing.Size(159, 23);
            this.btnDownloadPDF.TabIndex = 0;
            this.btnDownloadPDF.Text = "Download PDF";
            // 
            // pdfViewer
            // 
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.Location = new System.Drawing.Point(657, 0);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.Size = new System.Drawing.Size(620, 391);
            this.pdfViewer.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 426);
            this.Controls.Add(this.pdfViewer);
            this.Controls.Add(this.grdPDF);
            this.Controls.Add(this.pnlButon);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.grdPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdvPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButon)).EndInit();
            this.pnlButon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grdPDF;
        private DevExpress.XtraGrid.Views.Grid.GridView grdvPDF;
        private DevExpress.XtraEditors.PanelControl pnlButon;
        private DevExpress.XtraEditors.SimpleButton btnUploadPDF;
        private DevExpress.XtraEditors.SimpleButton btnMergePDF;
        private DevExpress.XtraEditors.SimpleButton btnDownloadPDF;
        private DevExpress.XtraPdfViewer.PdfViewer pdfViewer;
    }
}

