namespace WebApi.Models;

public class CongDap{
    public int objectid { get; set; }
    public string? idcongdap { get; set; }
    public string? tencongdap { get; set; }
    public string? lytrinh { get; set; }
    public string? toadox { get; set; }
    public string? toadoy { get; set; }
    public string? cumcongtrinh { get; set; }
    public string? goithau { get; set; }
    public string? loaicongtrinh { get; set; }
    public string? hinhthuc { get; set; }
    public string? chieudai { get; set; }
    public string? duongkinh { get; set; }
    public string? berong { get; set; }
    public string? chieucao { get; set; }
    public string? socua { get; set; }
    public string? caotrinhdaycong { get; set; }
    public string? caotrinhdinhcong { get; set; }
    public string? hinhthucvanhanh { get; set; }
    public string? muctieunhiemvu { get; set; }
    public string? diadiem { get; set; }
    public string? namsudung { get; set; }
    public string? capcongtrinh { get; set; }
    public string? hethongcttl { get; set; }
    public string? donviquanly { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? shape { get; set; }
}

public class SewerDetailStatistics{
    public string? donviquanly { get; set; }
    public string? capcongtrinh { get; set; }
    public int tongsocapcongtrinh { get; set; }
    public double tongchieudaitheocapcongtrinh { get; set; } 
    public string? mamau { get; set; }
}
public class SewerTotalStatistics{
    public string? capcongtrinh { get; set; }
    public int tongsocapcongtrinh { get; set; }
    public double tongchieudaitheocapcongtrinh { get; set; } 
    public string? mamau { get; set; }
    public string? phamvithongke { get; set; }
}