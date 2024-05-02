namespace WebApi.Models;

public class TuLieuKhac{
    public int objectid { get; set; }
    public string? idtulieu { get; set; }
    public string? tentulieu { get; set; }
    public string? ngaytulieu { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public short? namcapnhat { get; set; }
    public string? ghichu { get; set; }
}
public class TuLieuKhacAddEdit{
    public int objectid { get; set; }
    public string? tentulieu { get; set; }
    public string? ngaytulieu { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    // public string? ghichu { get; set; }
    public IFormFile? file {get; set;}
}
public class TuLieuKhacDetail{
    public int objectid { get; set; }
    public string? idtulieu { get; set; }
    public string? tentulieu { get; set; }
    public string? ngaytulieu { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public short? namcapnhat { get; set; }
}