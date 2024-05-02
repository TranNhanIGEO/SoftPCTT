namespace WebApi.Models;

public class TuLieuVideo{
    public int objectid { get; set; }
    public string? idvideo { get; set; }
    public string? tenvideo { get; set; }
    public string? ngayvideo { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public short? namcapnhat { get; set; }
    public string? ghichu { get; set; }
}
public class TuLieuVideoAddEdit{
    public int objectid { get; set; }
    public string? tenvideo { get; set; }
    public string? ngayvideo { get; set; }
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
public class TuLieuVideoDetail{
    public int objectid { get; set; }
    public string? idvideo { get; set; }
    public string? tenvideo { get; set; } // tra ve chuoi string rong
    public string? ngayvideo { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public short? namcapnhat { get; set; }
}