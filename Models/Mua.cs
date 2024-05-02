namespace WebApi.Models;

public class Mua{
    public int objectid { get; set; }
    public string? idtrammua { get; set; }
    public string? tentram { get; set; }
    public string? captram { get; set; }
    public string? vitritram { get; set; }
    public string? gio { get; set; }
    public string? ngay { get; set; }
    // public string? toadox { get; set; }
    // public string? toadoy { get; set; }
    public string? luongmua { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? shape { get; set; }
}
public class RainTotalStatistics{
    public string? Tentramdomua { get; set; }
    public int tongsophantu { get; set; }
    public double luongmuathapnhat { get; set; }
    public string? thoidiemluongmuathapnhat { get; set; }
    public double luongmuacaonhat { get; set; }
    public string? thoidiemluongmuacaonhat { get; set; }
    public double luongmuatrungbinh { get; set; }
}
public class RainDetailStatistics : RainTotalStatistics{
    public int nam { get; set; }
}