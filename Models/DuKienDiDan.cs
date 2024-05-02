namespace WebApi.Models;

public class DuKienDiDan{
    public int objectid { get; set; }
    public string? idkhsotan { get; set; }
    public string? sovb { get; set; }
    public string? ngayvb { get; set; }
    public string? loaivb { get; set; }
    public string? quanhuyen { get; set; }
    public string? mahuyen { get; set; }
    public string? sophuongdidan { get; set; }
    public string? soho_bao8_9 { get; set; }
    public string? songuoi_bao8_9 { get; set; }
    public string? soho_bao10_13 { get; set; }
    public string? songuoi_bao10_13 { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? sohocandidoi { get; set; }
}
public class DuKienDiDanHistory : DuKienDiDan{
    public string? username { get; set; }
    public string? updatehour { get; set; }
    public string? updatedate { get; set; }
}
public class DuKienDiDanStatistics{
    public string? quan_huyen_tp { get; set; }
    public int? tongsohodidoibao8_9 { get; set; }
    public int? tongsonguoididoibao8_9 { get; set; }
    public int? tongsohodidoibao10_13 { get; set; }
    public int? tongsonguoididoibao10_13 { get; set; }
}