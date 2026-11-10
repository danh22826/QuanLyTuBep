using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;

namespace QuanLyBanTuBep.UI
{
    internal sealed class FrmXemBaoCao : Form
    {
        private readonly CrystalReportViewer viewer = new CrystalReportViewer();
        private readonly ReportDocument report = new ReportDocument();

        private FrmXemBaoCao()
        {
            Text = "Xem / In báo cáo";
            StartPosition = FormStartPosition.CenterParent;
            WindowState = FormWindowState.Maximized;
            viewer.Dock = DockStyle.Fill;
            viewer.ShowRefreshButton = false;
            viewer.ShowPrintButton = true;
            viewer.ShowExportButton = true;
            Controls.Add(viewer);
        }

        internal static void Mo(Form owner, DataTable data, string kyBaoCao)
        {
            if (data.Rows.Count == 0)
            {
                MessageBox.Show(owner, "Không có dữ liệu theo bộ lọc đã chọn.", "Báo cáo");
                return;
            }

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "rpt" + data.TableName + ".rpt");
            if (!File.Exists(path))
                throw new FileNotFoundException("Thiếu mẫu Crystal Reports: " + Path.GetFileName(path), path);

            using (var form = new FrmXemBaoCao())
            {
                form.report.Load(path);
                form.report.SetDataSource(data);
                ((TextObject)form.report.ReportDefinition.ReportObjects["KyBaoCao"]).Text = kyBaoCao;
                ((TextObject)form.report.ReportDefinition.ReportObjects["NgayIn"]).Text =
                    "Ngày lập: " + DateTime.Today.ToString("dd/MM/yyyy");
                form.viewer.ReportSource = form.report;
                form.ShowDialog(owner);
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    try { viewer.ReportSource = null; }
                    finally
                    {
                        try { report.Close(); }
                        finally { report.Dispose(); }
                    }
                }
            }
            finally { base.Dispose(disposing); }
        }
    }
}
