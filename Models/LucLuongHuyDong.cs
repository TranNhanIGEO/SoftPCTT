namespace WebApi.Models;
public class LucLuongHuyDong{
    public int objectid { get; set; }
    public string? idkhlucluong { get; set; }
    public string? qhtp { get; set; }
    public string? tenlucluong { get; set; }
    public string? capql { get; set; }
    public string? slnguoihd { get; set; }
    public string? sovb { get; set; }
    public string? ngayvb { get; set; }
    public string? loaivb { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? namsudung { get; set; }
    public string? mahuyen { get; set; }
}
public class LucLuongHuyDongHistory : LucLuongHuyDong{
    public string? username { get; set; }
    public string? updatehour { get; set; }
    public string? updatedate { get; set; }
}

public class LucLuongHuyDongTotalStatistics{
    public string? tenlucluong { get; set; }
    public int? tongcong { get; set; }
    public string? phamvithongke { get; set; }
}
public class LucLuongHuyDongDetailStatistics {
    public string? qhtp { get; set; }
    public string? tenlucluong { get; set; }
    public int? thanhpho { get; set; }
    public int? quanhuyen { get; set; }
    public int? phuongxathitran { get; set; }
    public int? apkhupho { get; set; }
    public int? tongcong { get; set; }
}

public class LucLuongHuyDongAddEdit{
    public int objectid { get; set; }
    public string? idkhlucluong { get; set; }
    public string? qhtp { get; set; }
    public string? tenlucluong { get; set; }
    public string? capql { get; set; }
    public string? slnguoihd { get; set; }
    public string? sovb { get; set; }
    public string? ngayvb { get; set; }
    public string? loaivb { get; set; }
    public string? namcapnhat { get; set; }
    // public string? ghichu { get; set; }
    public string? namsudung { get; set; }
    public string? mahuyen { get; set; }
}