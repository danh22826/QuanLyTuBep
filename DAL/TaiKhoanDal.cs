using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyBanTuBep.Models;

namespace QuanLyBanTuBep.DAL
{
    public class TaiKhoanDal
    {
        private const string SqlChon =
            "SELECT t.MaTaiKhoan,t.MaNV,n.TenNV,t.TenDangNhap,t.MatKhauHash,t.VaiTro,t.TrangThai,t.NgayTao,t.LanDangNhapCuoi " +
            "FROM dbo.TaiKhoan t JOIN dbo.NhanVien n ON n.MaNV=t.MaNV ";

        public int Dem()
        {
            return Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM dbo.TaiKhoan"));
        }

        public TaiKhoan TimTheoTenDangNhap(string ten)
        {
            DataTable dt = Db.Query(SqlChon + "WHERE t.TenDangNhap=@ten", Db.P("@ten", ten));
            return dt.Rows.Count == 0 ? null : Doc(dt.Rows[0]);
        }

        public TaiKhoan LayTheoMa(string ma)
        {
            DataTable dt = Db.Query(SqlChon + "WHERE t.MaTaiKhoan=@ma", Db.P("@ma", ma));
            return dt.Rows.Count == 0 ? null : Doc(dt.Rows[0]);
        }

        public List<TaiKhoan> LayDanhSach(string tuKhoa)
        {
            return DocDuLieu.DanhSach<TaiKhoan>(Db.Query(
                SqlChon + "WHERE @q=N'%%' OR t.TenDangNhap LIKE @q OR t.MaNV LIKE @q OR n.TenNV LIKE @q " +
                "ORDER BY t.TenDangNhap", Db.P("@q", "%" + (tuKhoa ?? "").Trim() + "%")));
        }

        public List<LuaChon> NhanVienChuaCoTaiKhoan()
        {
            return DocDuLieu.DanhSach<LuaChon>(Db.Query(
                "SELECT n.MaNV AS Ma,n.TenNV AS Ten FROM dbo.NhanVien n " +
                "LEFT JOIN dbo.TaiKhoan t ON t.MaNV=n.MaNV WHERE t.MaTaiKhoan IS NULL ORDER BY n.TenNV"));
        }

        public void Them(string maNV, string ten, string hash, string vaiTro)
        {
            Db.NonQuery("INSERT dbo.TaiKhoan(MaNV,TenDangNhap,MatKhauHash,VaiTro) VALUES(@nv,@ten,@hash,@role)",
                Db.P("@nv", maNV), Db.P("@ten", ten), Db.P("@hash", hash), Db.P("@role", vaiTro));
        }

        public void TaoAdminDauTien(string maNV, string ten, string hash)
        {
            Db.GiaoDich(delegate(SqlConnection conn, SqlTransaction tran)
            {
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.TaiKhoan WITH (TABLOCKX,HOLDLOCK)", conn, tran))
                {
                    if (Convert.ToInt32(cmd.ExecuteScalar()) != 0)
                        throw new InvalidOperationException("Đã có tài khoản; không thể chạy quy trình khởi tạo ADMIN đầu tiên.");
                }
                Db.NonQuery(conn, tran,
                    "INSERT dbo.TaiKhoan(MaNV,TenDangNhap,MatKhauHash,VaiTro,TrangThai) VALUES(@nv,@ten,@hash,N'ADMIN',1)",
                    Db.P("@nv", maNV), Db.P("@ten", ten), Db.P("@hash", hash));
            });
        }

        public void CapNhatMatKhau(string ma, string hash)
        {
            if (Db.NonQuery("UPDATE dbo.TaiKhoan SET MatKhauHash=@hash WHERE MaTaiKhoan=@ma",
                Db.P("@hash", hash), Db.P("@ma", ma)) != 1)
                throw new InvalidOperationException("Tài khoản không còn tồn tại.");
        }

        public void CapNhatLanDangNhap(string ma)
        {
            Db.NonQuery("UPDATE dbo.TaiKhoan SET LanDangNhapCuoi=SYSUTCDATETIME() WHERE MaTaiKhoan=@ma AND TrangThai=1", Db.P("@ma", ma));
        }

        public TaiKhoan KiemTraPhien(string ma)
        {
            DataTable dt = Db.Query(SqlChon + "WHERE t.MaTaiKhoan=@ma AND t.TrangThai=1", Db.P("@ma", ma));
            return dt.Rows.Count == 0 ? null : Doc(dt.Rows[0]);
        }

        public bool CoTaiKhoanNhanVien(string maNV)
        {
            return Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM dbo.TaiKhoan WHERE MaNV=@nv", Db.P("@nv", maNV))) > 0;
        }

        public void CapNhatVaiTro(string ma, string vaiTro)
        {
            CapNhatAnToan(ma, vaiTro, null);
        }

        public void CapNhatTrangThai(string ma, bool hoatDong)
        {
            CapNhatAnToan(ma, null, hoatDong);
        }

        private static void CapNhatAnToan(string ma, string vaiTroMoi, bool? trangThaiMoi)
        {
            Db.GiaoDich(delegate(SqlConnection conn, SqlTransaction tran)
            {
                string vaiTro;
                bool trangThai;
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT VaiTro,TrangThai FROM dbo.TaiKhoan WITH (UPDLOCK,HOLDLOCK) WHERE MaTaiKhoan=@ma", conn, tran))
                {
                    cmd.Parameters.Add(Db.P("@ma", ma));
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (!r.Read()) throw new InvalidOperationException("Tài khoản không còn tồn tại.");
                        vaiTro = Convert.ToString(r.GetValue(0));
                        trangThai = Convert.ToBoolean(r.GetValue(1));
                    }
                }

                string vaiTroSau = vaiTroMoi ?? vaiTro;
                bool trangThaiSau = trangThaiMoi ?? trangThai;
                bool giamAdminCuoi = string.Equals(vaiTro, "ADMIN", StringComparison.OrdinalIgnoreCase) && trangThai &&
                                     (!string.Equals(vaiTroSau, "ADMIN", StringComparison.OrdinalIgnoreCase) || !trangThaiSau);
                if (giamAdminCuoi)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT COUNT(*) FROM dbo.TaiKhoan WITH (UPDLOCK,HOLDLOCK) WHERE VaiTro=N'ADMIN' AND TrangThai=1", conn, tran))
                    {
                        if (Convert.ToInt32(cmd.ExecuteScalar()) <= 1)
                            throw new InvalidOperationException("Không thể khóa hoặc hạ quyền ADMIN đang hoạt động cuối cùng.");
                    }
                }

                if (vaiTroMoi != null)
                    Db.NonQuery(conn, tran, "UPDATE dbo.TaiKhoan SET VaiTro=@v WHERE MaTaiKhoan=@ma", Db.P("@v", vaiTroMoi), Db.P("@ma", ma));
                if (trangThaiMoi.HasValue)
                    Db.NonQuery(conn, tran, "UPDATE dbo.TaiKhoan SET TrangThai=@v WHERE MaTaiKhoan=@ma", Db.P("@v", trangThaiMoi.Value), Db.P("@ma", ma));
            });
        }

        private static TaiKhoan Doc(DataRow row)
        {
            return new TaiKhoan
            {
                MaTaiKhoan = Convert.ToString(row["MaTaiKhoan"]),
                MaNV = Convert.ToString(row["MaNV"]),
                TenNV = Convert.ToString(row["TenNV"]),
                TenDangNhap = Convert.ToString(row["TenDangNhap"]),
                MatKhauHash = Convert.ToString(row["MatKhauHash"]),
                VaiTro = Convert.ToString(row["VaiTro"]),
                TrangThai = Convert.ToBoolean(row["TrangThai"]),
                NgayTao = Convert.ToDateTime(row["NgayTao"]),
                LanDangNhapCuoi = row.IsNull("LanDangNhapCuoi") ? (DateTime?)null : Convert.ToDateTime(row["LanDangNhapCuoi"])
            };
        }
    }
}
