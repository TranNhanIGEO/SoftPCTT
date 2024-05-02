namespace WebApi.Models; 

public class DiemAnToan{
    public int objectid { get; set; }
    public string? idantoan { get; set; }
    public string? vitri { get; set; }
    public string? toadox { get; set; }
    public string? toadoy { get; set; }
    public int? succhua { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public short? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? phuongan { get; set; }
    public string? shape { get; set; }
}
public class DiemAnToanStatistics{
    public string? quan_huyen_tp { get; set; }
    public int? Tongsovitriantoan { get; set; }
    public string? vitriantoantheophuongan { get; set; }
}

