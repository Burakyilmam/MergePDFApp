using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
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
    public partial class frmMerge : Form
    {
        public class PDFItem
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public bool IsMerged { get; set; }
            public List<string> MergedFiles { get; set; } = new List<string>();
            public string MergedFromDisplay => IsMerged && MergedFiles.Any() ? string.Join(", ", MergedFiles.Select(x => Path.GetFileName(x))) : string.Empty;
            public int PageSize { get; set; }
        }
        public PopupMenu popupMenu;
        public BarManager manager;
        public static BindingList<PDFItem> pdfList;
        public string outputPath;
        public PDFItem selectedPDF;
        public int[] selectedPDFs;

        public frmMerge()
        {
            InitializeComponent();
            Events();
            GridControlDataSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            GridAppearance();
            CreatePopupMenu();
            ButtonEnabled();
            ButtonCenter();
        }

        #region Eventler
        public void Events()
        {

            this.SizeChanged += Form1_SizeChanged;
            this.Load += Form1_Load;

            GridEvents();
            ButonEvents();
        }

        public void GridEvents()
        {
            grdvPDF.FocusedRowChanged += grdvPDF_FocusedRowChanged;
            grdvPDF.SelectionChanged += grdvPDF_SelectionChanged;
            grdvPDF.DoubleClick += grdvPDF_DoubleClick;
            grdvPDF.KeyDown += grdvPDF_KeyDown;
            grdvPDF.RowStyle += grdvPDF_RowStyle;
            grdvPDF.RowClick += grdvPDF_RowClick;
            grdvPDF.MouseUp += grdvPDF_MouseUp;
            grdPDF.DragEnter += grdPDF_DragEnter;
            grdPDF.DragDrop += grdPDF_DragDrop;
        }

        public void ButonEvents()
        {
            btnDownloadPDF.Click += btnDownloadPDF_Click;
            btnMergePDF.Click += btnMergePDF_Click;
            btnUploadPDF.Click += btnUploadPDF_Click;
            btnPartialPDF.Click += BtnPartialPDF_Click;
        }
        #endregion

        #region Grid Görünüm Ayarları
        public void ColumnAppearance(GridColumn column, string caption, bool visible = false)
        {
            Font font = new Font(grdvPDF.Appearance.HeaderPanel.Font, FontStyle.Bold);
            column.Caption = caption;
            column.AppearanceHeader.Font = font;
            column.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            column.Visible = visible;
        }

        public void GridAppearance()
        {
            ColumnAppearance(grdvPDF.Columns[0], "Dosya Adı", true);
            ColumnAppearance(grdvPDF.Columns[1], "Dosya Yolu", true);
            ColumnAppearance(grdvPDF.Columns[2], "IsMerged");
            ColumnAppearance(grdvPDF.Columns[3], "Birleştirildiği Dosyalar", true);
            ColumnAppearance(grdvPDF.Columns[4], "Sayfa", true);

            grdvPDF.BestFitColumns();
        }
        #endregion

        #region Popup Oluşturma
        public void CreatePopupMenu()
        {
            popupMenu = new PopupMenu();
            manager = new BarManager();
            popupMenu.Manager = manager;
            manager.Images = ımageCollection1;

            BarButtonItem pdfToWord = new BarButtonItem(manager, "PDF to Word");
            BarButtonItem pdfToExcel = new BarButtonItem(manager, "PDF to Excel");
            BarButtonItem pdfToHtml = new BarButtonItem(manager, "PDF to HTML");

            pdfToWord.ImageOptions.ImageIndex = 0;
            pdfToExcel.ImageOptions.ImageIndex = 1;
            pdfToHtml.ImageOptions.ImageIndex = 2;

            popupMenu.AddItem(pdfToWord);
            popupMenu.AddItem(pdfToExcel);
            popupMenu.AddItem(pdfToHtml);

            pdfToWord.ItemClick += PdfToWord_ItemClick;
            pdfToExcel.ItemClick += PdfToExcel_ItemClick;
            pdfToHtml.ItemClick += PdfToHtml_ItemClick;
        }
        #endregion

        #region Grid Veri Kaynağı
        public void GridControlDataSource()
        {
            pdfList = new BindingList<PDFItem>();
            grdPDF.DataSource = pdfList;
        }
        #endregion

        #region Buton Ayarları
        public void SetButtonIndex()
        {
            pnlButon.Controls.SetChildIndex(btnUploadPDF, 0);
            pnlButon.Controls.SetChildIndex(btnMergePDF, 1);
            pnlButon.Controls.SetChildIndex(btnPartialPDF, 2);
            pnlButon.Controls.SetChildIndex(btnDownloadPDF, 3);
        }

        public void ButtonEnabled()
        {
            btnDownloadPDF.Enabled = grdvPDF.RowCount > 0;
            btnPartialPDF.Enabled = grdvPDF.RowCount > 0;
            btnMergePDF.Enabled = grdvPDF.RowCount > 0;
        }

        public void ButtonCenter()
        {
            int spacing = 10;
            SetButtonIndex();

            var buttons = pnlButon.Controls.OfType<SimpleButton>().ToList();

            int totalButtonsWidth = buttons.Sum(b => b.Width) + spacing * (buttons.Count - 1);
            int startX = (pnlButon.Width - totalButtonsWidth) / 2;
            int currentX = startX;

            foreach (var btn in buttons)
            {
                btn.Location = new Point(currentX, (pnlButon.Height - btn.Height) / 2);
                currentX += btn.Width + spacing;
            }
        }
        #endregion

        #region Etkinlik Tetikleyicileri
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            ButtonCenter();
        }

        private void grdvPDF_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                selectedPDF = grdvPDF.GetFocusedRow() as PDFItem;
                selectedPDFs = grdvPDF.GetSelectedRows();
                ButtonEnabled();
            }
            else
            {
                pdfViewer.CloseDocument();
            }
        }

        private void grdvPDF_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            selectedPDFs = grdvPDF.GetSelectedRows();

            if (selectedPDFs.Length == 1) selectedPDF = grdvPDF.GetRow(selectedPDFs[0]) as PDFItem;
            else selectedPDF = null;

            btnPartialPDF.Enabled = selectedPDFs.Length == 1;
        }
        #endregion

        #region PDF Görüntüleme İşlemleri

        #region Grid Satır İşlemleri
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
        #endregion

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
                MessageBox.Show("PDF dosyası bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region PDF Silme İşlemleri
        public void DeletePDF()
        {
            selectedPDFs = grdvPDF.GetSelectedRows();

            if (selectedPDFs == null || selectedPDFs.Length == 0) return;

            foreach (int rowHandle in selectedPDFs.OrderByDescending(x => x))
            {
                PDFItem deleted = grdvPDF.GetRow(rowHandle) as PDFItem;
                if (deleted != null) pdfList.Remove(deleted);
            }

            MessageBox.Show("Seçili PDF'ler başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Grid Klavye İşlemleri
        private void grdvPDF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (selectedPDFs.Length > 10)
                {
                    DialogResult result = MessageBox.Show($"Seçili {selectedPDFs.Length} adet PDF dosyası açılacak. Bu işlem bilgisayarınızı yavaşlatabilir. Devam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result != DialogResult.Yes) return;
                }

                foreach (int rowHandle in selectedPDFs)
                {
                    PDFItem pdf = grdvPDF.GetRow(rowHandle) as PDFItem;
                    if (pdf != null)
                        OpenPDF(pdf.FilePath);
                }
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
        #endregion

        #region Fare İşlemleri
        private void grdvPDF_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitInfo = grdvPDF.CalcHitInfo(e.Location);
                if (hitInfo.InRow)
                {
                    grdvPDF.FocusedRowHandle = hitInfo.RowHandle;
                    popupMenu.ShowPopup(Control.MousePosition);
                }
            }
        }
        #endregion

        #region Grid Çift Tıklanma İşlemleri
        private void grdvPDF_DoubleClick(object sender, EventArgs e)
        {
            if (selectedPDF != null)
            {
                OpenPDF(selectedPDF.FilePath);
            }
        }
        #endregion

        #region Dosya Adı Benzersizleştirme İşlemleri
        private static string GetUniqueFileName(string filePath)
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
        #endregion

        #region PDF Yükleme İşlemleri
        public void UploadPDF()
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Multiselect = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    string fileName = Path.GetFileName(file);

                    if (pdfList.Any(p => p.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase))) continue;

                    PdfDocument doc = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    pdfList.Add(new PDFItem
                    {
                        FileName = fileName,
                        FilePath = file,
                        PageSize = doc.PageCount,
                        IsMerged = false
                    });
                }
            }
        }

        #region PDF Sürükle & Bırak İşlemleri
        private void grdPDF_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);

                    if (!Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase)) continue;

                    if (pdfList.Any(p => p.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase))) continue;

                    PdfDocument doc = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                    pdfList.Add(new PDFItem
                    {
                        FileName = fileName,
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
        #endregion

        private void btnUploadPDF_Click(object sender, EventArgs e)
        {
            UploadPDF();
        }
        #endregion

        #region PDF Birleştirme İşlemleri
        public void MergeAllPDF()
        {
            outputPath = Path.Combine(Path.GetTempPath(), "Merged.pdf");
            PdfDocument outputDoc = new PdfDocument();

            foreach (PDFItem pdf in pdfList)
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
                PdfDocument mergedDoc = PdfReader.Open(outputPath, PdfDocumentOpenMode.Import);
                pdfList.Add(new PDFItem
                {
                    FileName = Path.GetFileName(outputPath),
                    FilePath = outputPath,
                    PageSize = mergedDoc.PageCount,
                    IsMerged = true,
                    MergedFiles = pdfList.Select(p => p.FilePath).ToList()
                }); ;
            }

            MessageBox.Show("Tüm PDF'ler başarıyla birleştirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MergeSelectedPDF()
        {
            selectedPDFs = grdvPDF.GetSelectedRows();
            if (selectedPDFs == null || selectedPDFs.Length < 2)
            {
                MessageBox.Show("Birleştirmek için en az iki PDF seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            outputPath = Path.Combine(Path.GetTempPath(), DateTime.Now.ToString("yyMMddHHmmss") + "_Merged.pdf");
            PdfDocument outputDoc = new PdfDocument();

            var selectedItems = selectedPDFs.Select(x => grdvPDF.GetRow(x) as PDFItem).Where(x => x != null).ToList();

            foreach (PDFItem item in selectedItems)
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
                PdfDocument mergedDoc = PdfReader.Open(outputPath, PdfDocumentOpenMode.Import);
                pdfList.Add(new PDFItem
                {
                    FileName = Path.GetFileName(outputPath),
                    FilePath = outputPath,
                    PageSize = mergedDoc.PageCount,
                    IsMerged = true,
                    MergedFiles = selectedItems.Select(p => p.FilePath).ToList()
                });
            }

            MessageBox.Show("Seçili PDF'ler başarıyla birleştirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMergePDF_Click(object sender, EventArgs e)
        {
            MergeSelectedPDF();
        }
        #endregion

        #region PDF İndirme İşlemleri
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
                    foreach (PDFItem item in selectedItems)
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

        private void btnDownloadPDF_Click(object sender, EventArgs e)
        {
            DownloadPDF();
        }
        #endregion

        #region PDF Parçalama - Ayırma İşlemleri
        public static void CreatePartialPDF(PDFItem sourceItem, int startPage, int endPage)
        {
            if (sourceItem == null || !File.Exists(sourceItem.FilePath))
            {
                MessageBox.Show("Kaynak PDF bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (startPage < 1 || endPage > sourceItem.PageSize || startPage > endPage)
            {
                MessageBox.Show("Geçersiz sayfa aralığı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPath = Path.Combine(Path.GetTempPath(),
                             $"{Path.GetFileNameWithoutExtension(sourceItem.FileName)}_Part_{startPage}-{endPage}.pdf");

            newPath = GetUniqueFileName(newPath);

            int newDocPageCount = 0;

            using (PdfDocument outputDoc = new PdfDocument())
            {
                using (PdfDocument inputDoc = PdfReader.Open(sourceItem.FilePath, PdfDocumentOpenMode.Import))
                {
                    for (int i = startPage - 1; i < endPage; i++)
                    {
                        outputDoc.AddPage(inputDoc.Pages[i]);
                    }
                }

                newDocPageCount = outputDoc.PageCount;

                outputDoc.Save(newPath);
            }

            pdfList.Add(new PDFItem
            {
                FileName = Path.GetFileName(newPath),
                FilePath = newPath,
                PageSize = newDocPageCount,
                IsMerged = false
            });
        }

        private void BtnPartialPDF_Click(object sender, EventArgs e)
        {
            if (selectedPDF != null)
            {
                frmPartial frmPartial = new frmPartial(this);
                frmPartial.Show();
            }
        }
        #endregion

        #region Dönüştürme İşlemleri
        public void ConvertPDF(Spire.Pdf.FileFormat format, string extension, string dialogTitle)
        {
            if (selectedPDF == null || string.IsNullOrEmpty(selectedPDF.FilePath))
            {
                MessageBox.Show("Lütfen PDF seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = $"{extension.ToUpper()} Dosyası (*.{extension})|*.{extension}";
                sfd.Title = $"{dialogTitle} olarak kaydet";
                sfd.FileName = Path.GetFileNameWithoutExtension(selectedPDF.FilePath) + $".{extension}";

                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var pdf = new Spire.Pdf.PdfDocument())
                    {
                        pdf.LoadFromFile(selectedPDF.FilePath);
                        pdf.SaveToFile(sfd.FileName, format);
                    }

                    MessageBox.Show($"PDF başarıyla {dialogTitle} formatına dönüştürüldü:\n{sfd.FileName}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dönüştürme sırasında bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PdfToWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConvertPDF(Spire.Pdf.FileFormat.DOCX, "docx", "Word");
        }

        private void PdfToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConvertPDF(Spire.Pdf.FileFormat.XLSX, "xlsx", "Excel");
        }

        private void PdfToHtml_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConvertPDF(Spire.Pdf.FileFormat.HTML, "html", "HTML");
        }
        #endregion
    }
}
