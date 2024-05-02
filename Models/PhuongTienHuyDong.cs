namespace WebApi.Models;

public class PhuongTienHuyDong{
    public int objectid { get; set; }
    public string? idkhphuogtien { get; set; }
    public string? tenphuongtienttb { get; set; }
    public string? dvql { get; set; }
    public string? dvt { get; set; }
    public string? soluong { get; set; }
    public string? sovb { get; set; }
    public string? ngayvb { get; set; }
    public string? loaivb { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? phannhom1 { get; set; }
    public string? phannhom2 { get; set; }
    public string? phannhom3 { get; set; }
    public string? mahuyen { get; set; }
}
public class PhuongTienHuyDongDetailStatistics{
    public string? donviquanly { get; set; }
    public string? tenphuongtienttb { get; set; }
    public double? soluongphuongtienttb { get; set; }
    public string? donvitinh { get; set; }
}
public class PhuongTienHuyDongTotalStatistics{
    public string? tenphuongtienttb { get; set; }
    public string? donvitinh { get; set; }
    public double? soluongphuongtienttb { get; set; }
    public string? donviquanly { get; set; }
}
public class PhuongTienHuyDongAddEdit{
    public int objectid { get; set; }
    public string? idkhphuogtien { get; set; }
    public string? tenphuongtienttb { get; set; }
    public string? dvql { get; set; }
    public string? dvt { get; set; }
    public double? soluong { get; set; }
    public string? sovb { get; set; }
    public string? ngayvb { get; set; }
    public string? loaivb { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? phannhom1 { get; set; }
    public string? phannhom2 { get; set; }
    public string? phannhom3 { get; set; }
    public string? mahuyen { get; set; }
}