using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MergePDFApp
{
    public partial class frmPDFBirlestir : Form
    {
        public class PDFItem
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public int PageSize { get; set; }
            public bool IsMerged { get; set; }
            public List<string> MergedFiles { get; set; } = new List<string>();
            public string MergedFromDisplay => IsMerged && MergedFiles.Any() ? string.Join(", ", MergedFiles.Select(x => Path.GetFileName(x))) : "";
        }
        public string outputPath;
        public BindingList<PDFItem> pdfList;
        public PDFItem selectedPDF;
        public int[] selectedPDFs;

        public frmPDFBirlestir()
        {
            InitializeComponent();
            this.SizeChanged += Form1_SizeChanged;
            this.Load += Form1_Load;
            grdvPDF.FocusedRowChanged += grdvPDF_FocusedRowChanged;
            grdvPDF.DoubleClick += grdvPDF_DoubleClick;
            grdvPDF.KeyDown += grdvPDF_KeyDown;
            grdvPDF.PopupMenuShowing += GrdvPDF_PopupMenuShowing;
            grdvPDF.RowStyle += grdvPDF_RowStyle;
            grdvPDF.RowClick += grdvPDF_RowClick;
            grdPDF.DragEnter += grdPDF_DragEnter;
            grdPDF.DragDrop += grdPDF_DragDrop;

            btnDownloadPDF.Click += btnDownloadMergedPDF_Click;
            btnMergePDF.Click += btnMergePDF_Click;
            btnUploadPDF.Click += btnUploadPDF_Click;

            GridControlDataSource();
        }

        private void grdPDF_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (var file in files)
                {
                    if (!Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (pdfList.Any(p => p.FileName.Equals(Path.GetFileName(file), StringComparison.OrdinalIgnoreCase)))
                        continue;

                    PdfDocument doc = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    pdfList.Add(new PDFItem
                    {
                        FileName = Path.GetFileName(file),
                        FilePath = file,
                        PageSize = doc.PageCount,
                        IsMerged = false
                    });
                }
                grdPDF.RefreshDataSource();
            }
        }

        private void grdPDF_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.All(f => Path.GetExtension(f).Equals(".pdf", StringComparison.OrdinalIgnoreCase)))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ButtonCenter();
        }

        private void grdvPDF_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                PDFItem selectedItem = grdvPDF.GetRow(e.RowHandle) as PDFItem;
                if (selectedItem != null && File.Exists(selectedItem.FilePath))
                {
                    pdfViewer.LoadDocument(selectedItem.FilePath);
                }
            }
        }

        private void grdvPDF_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                PDFItem item = grdvPDF.GetRow(e.RowHandle) as PDFItem;
                if (item != null)
                {
                    if (item.IsMerged)
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ButtonCenter();
        }

        public void GridAppearance()
        {
            Font font = new Font(grdvPDF.Appearance.HeaderPanel.Font, FontStyle.Bold);

            grdvPDF.Columns[0].Caption = "Dosya Adı";
            grdvPDF.Columns[0].AppearanceHeader.Font = font;
            grdvPDF.Columns[0].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            grdvPDF.Columns[1].Caption = "Dosya Yolu";
            grdvPDF.Columns[1].AppearanceHeader.Font = font;
            grdvPDF.Columns[1].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            grdvPDF.Columns[2].Caption = "Sayfa Sayısı";
            grdvPDF.Columns[2].AppearanceHeader.Font = font;
            grdvPDF.Columns[2].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            grdvPDF.Columns[3].Visible = false;

            grdvPDF.Columns[4].Caption = "Birleştirildiği Dosyalar";
            grdvPDF.Columns[4].AppearanceHeader.Font = font;
            grdvPDF.Columns[4].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        public void GridControlDataSource()
        {
            pdfList = new BindingList<PDFItem>();
            grdPDF.DataSource = pdfList;
            GridAppearance();
        }

        public void ButtonCenter()
        {
            int totalButtonsWidth = 0;
            int spacing = 10;

            foreach (Control control in pnlButon.Controls)
            {
                if (control is SimpleButton) totalButtonsWidth += control.Width + spacing;
            }

            totalButtonsWidth -= spacing;

            int startX = (pnlButon.Width - totalButtonsWidth) / 2;
            int currentX = startX;

            foreach (Control control in pnlButon.Controls)
            {
                if (control is SimpleButton)
                {
                    control.Location = new Point(currentX, (pnlButon.Height - control.Height) / 2);
                    currentX += control.Width + spacing;
                }
            }
        }

        private void grdvPDF_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                selectedPDF = grdvPDF.GetFocusedRow() as PDFItem;
            }
        }

        public void OpenPDF(string filePath)
        {
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("PDF dosyası bulunamadı.");
            }
        }

        public void DeletePDF()
        {
            selectedPDFs = grdvPDF.GetSelectedRows();

            if (selectedPDFs == null || selectedPDFs.Length == 0) return;

            foreach (var rowHandle in selectedPDFs.OrderByDescending(x => x))
            {
                PDFItem deleted = grdvPDF.GetRow(rowHandle) as PDFItem;
                if (deleted != null) pdfList.Remove(deleted);
            }

            MessageBox.Show("Seçili PDF'ler başarıyla silindi.");
        }

        private void grdvPDF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenPDF(selectedPDF.FilePath);
            }

            if (e.KeyCode == Keys.Delete)
            {
                DialogResult result = MessageBox.Show("Silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeletePDF();
                }
            }
        }

        private void grdvPDF_DoubleClick(object sender, EventArgs e)
        {
            if (selectedPDF != null)
            {
                OpenPDF(selectedPDF.FilePath);
            }
        }

        public void UploadPDF()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    if (pdfList.Any(p => p.FileName.Equals(Path.GetFileName(file), StringComparison.OrdinalIgnoreCase))) continue;

                    PdfDocument doc = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    pdfList.Add(new PDFItem
                    {
                        FileName = Path.GetFileName(file),
                        FilePath = file,
                        PageSize = doc.PageCount,
                        IsMerged = false
                    });
                }
            }
        }

        private void btnUploadPDF_Click(object sender, EventArgs e)
        {
            UploadPDF();
        }

        public void MergeSelectedPDF()
        {
            selectedPDFs = grdvPDF.GetSelectedRows();
            if (selectedPDFs == null || selectedPDFs.Length < 2)
            {
                MessageBox.Show("Birleştirmek için en az iki PDF seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            outputPath = Path.Combine(Path.GetTempPath(), DateTime.Now.ToString("yyMMddHHmmss") + "_Merged.pdf");
            PdfDocument outputDoc = new PdfDocument();

            var selectedItems = selectedPDFs.Select(x => grdvPDF.GetRow(x) as PDFItem).Where(x => x != null).ToList();

            foreach (var item in selectedItems)
            {
                PdfDocument inputDoc = PdfReader.Open(item.FilePath, PdfDocumentOpenMode.Import);
                foreach (PdfPage page in inputDoc.Pages)
                {
                    outputDoc.AddPage(page);
                }
            }

            outputDoc.Save(outputPath);

            if (!pdfList.Any(p => string.Equals(p.FilePath, outputPath, StringComparison.OrdinalIgnoreCase)))
            {
                var mergedDoc = PdfReader.Open(outputPath, PdfDocumentOpenMode.Import);
                pdfList.Add(new PDFItem
                {
                    FileName = Path.GetFileName(outputPath),
                    FilePath = outputPath,
                    PageSize = mergedDoc.PageCount,
                    IsMerged = true,
                    MergedFiles = selectedItems.Select(p => p.FilePath).ToList()
                });
            }

            MessageBox.Show("Seçili PDF'ler başarıyla birleştirildi.");
        }

        public void MergeAllPDF()
        {
            outputPath = Path.Combine(Path.GetTempPath(), "Merged.pdf");
            PdfDocument outputDoc = new PdfDocument();

            foreach (var pdf in pdfList)
            {
                PdfDocument inputDoc = PdfReader.Open(pdf.FilePath, PdfDocumentOpenMode.Import);
                foreach (PdfPage page in inputDoc.Pages)
                {
                    outputDoc.AddPage(page);
                }
            }

            outputDoc.Save(outputPath);

            if (!pdfList.Any(p => string.Equals(p.FilePath, outputPath, StringComparison.OrdinalIgnoreCase)))
            {
                var mergedDoc = PdfReader.Open(outputPath, PdfDocumentOpenMode.Import);
                pdfList.Add(new PDFItem
                {
                    FileName = Path.GetFileName(outputPath),
                    FilePath = outputPath,
                    PageSize = mergedDoc.PageCount,
                    IsMerged = true,
                    MergedFiles = pdfList.Select(p => p.FilePath).ToList()
                }); ;
            }

            MessageBox.Show("Tüm PDF'ler başarıyla birleştirildi.");
        }

        private void btnMergePDF_Click(object sender, EventArgs e)
        {
            MergeSelectedPDF();
        }

        private string GetUniqueFileName(string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            string uniquePath = filePath;
            int counter = 1;

            while (File.Exists(uniquePath))
            {
                uniquePath = Path.Combine(directory, $"{filenameWithoutExt}_{counter}{extension}");
                counter++;
            }

            return uniquePath;
        }

        public void DownloadPDF()
        {
            selectedPDFs = grdvPDF.GetSelectedRows();

            if (selectedPDFs == null || selectedPDFs.Length == 0)
            {
                MessageBox.Show("Lütfen en az bir PDF seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItems = selectedPDFs
                .Select(x => grdvPDF.GetRow(x) as PDFItem)
                .Where(x => x != null)
                .Reverse()
                .ToList();

            if (selectedItems.Count == 1)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF files (*.pdf)|*.pdf";
                sfd.FileName = selectedItems[0].FileName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.Copy(selectedItems[0].FilePath, sfd.FileName, true);
                        MessageBox.Show("PDF başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "PDF'lerin kaydedileceği klasörü seçiniz.";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in selectedItems)
                    {
                        try
                        {
                            string destinationPath = Path.Combine(fbd.SelectedPath, item.FileName);

                            destinationPath = GetUniqueFileName(destinationPath);

                            File.Copy(item.FilePath, destinationPath, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"'{item.FileName}' dosyası kopyalanırken hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    MessageBox.Show("Seçili PDF'ler başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDownloadMergedPDF_Click(object sender, EventArgs e)
        {
            DownloadPDF();
        }

        private void GrdvPDF_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
