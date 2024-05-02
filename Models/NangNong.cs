namespace WebApi.Models;

public class NangNong{
    public int objectid { get; set; }
    public string? idtramkt { get; set; }
    public string? tentram { get; set; }
    public string? captram { get; set; }
    public string? vitritram { get; set; }
    // public string? toadox { get; set; }
    // public string? toadoy { get; set; }
    public string? thang { get; set; }
    public string? sogionang { get; set; }
    public string? nhietdomin { get; set; }
    public string? nhietdomax { get; set; }
    public string? nhietdotb { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? ngay { get; set; }
    public string? shape { get; set; }
}

public class TemperatureTotalStatistics{
    public string? Tentramdonhietdo { get; set; }
    public int tongsophantu { get; set; }
    public double nhietdothapnhat { get; set; }
    public string? thoidiemnhietdothapnhat { get; set; }
    public double nhietdocaonhat { get; set; }
    public string? thoidiemnhietdocaonhat { get; set; }
    public double nhietdotrungbinh { get; set; }
}

public class TemperatureDetailStatistics : TemperatureTotalStatistics{
    public int nam { get; set; }
}