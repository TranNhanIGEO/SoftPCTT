namespace WebApi.Models;

public class TuLieuHinhAnh{
    public int objectid { get; set; }
    public string? idhinhanh { get; set; }
    public string? tenhinhanh { get; set; }
    public string? ngayhinhanh { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
}
public class TuLieuHinhAnhAddEdit{
    public int? objectid { get; set; }
    public string? idhinhanh { get; set; }
    public string? tenhinhanh { get; set; }
    public string? ngayhinhanh { get; set; }
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
public class TuLieuHinhAnhDetail{
    public int? objectid { get; set; }
    public string? idhinhanh { get; set; }
    public string? tenhinhanh { get; set; }
    public string? ngayhinhanh { get; set; }
    public string? noidung { get; set; }
    public string? diadiem { get; set; }
    public string? dvql { get; set; }
    public string? nguongoc { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
}