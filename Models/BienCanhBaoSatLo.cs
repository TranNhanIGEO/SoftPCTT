namespace WebApi.Models;

public class BienCanhBaoSatLo{
    public int objectid { get; set; }
    public string? idbcbsl { get; set; }
    public string? sohieubien { get; set; }
    public string? toadox { get; set; }
    public string? toadoy { get; set; }
    public string? vitrisatlo { get; set; }
    public string? phamvi { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namxaydung { get; set; }
    public string? hinhanh { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? tuyensr { get; set; }
    public string? shape { get; set; }
}
public class BienCanhBaoSatLoAddEdit{
    public int objectid { get; set; }
    public string? idbcbsl { get; set; }
    public string? sohieubien { get; set; }
    public string? toadox { get; set; }
    public string? toadoy { get; set; }
    public string? vitrisatlo { get; set; }
    public string? phamvi { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namxaydung { get; set; }
    public string? hinhanh { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? tuyensr { get; set; }
    public string? shape { get; set; }
    public IFormFile? file {get; set;}
}
public class BienCanhBaoStatistics{
    public string? quan_huyen_tp { get; set; }
    public int tongsobiencanhbaosatlo { get; set; }
    public string? tentuyensr { get; set; }
}