namespace WebApi.Models;

public class DoMan{
    public int objectid { get; set; }
    public string? idtramman { get; set; }
    public string? tentram { get; set; }
    public string? gio { get; set; }
    public string? ngay { get; set;}
    // public string? toadox { get; set; }
    // public string? toadoy { get; set; }
    public string? tensong { get; set; }
    public string? doman { get; set; }
    public string? vitri { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? shape { get; set; }
}
public class SalinityTotalStatistics {
    public string? tencactramdoman { get; set; }
    public int tongsophantu { get; set; }
    public double domanthapnhat { get; set; }
    public string? thoidiemdomanthapnhat { get; set; }
    public double domancaonhat { get; set; }
    public string? thoidiemdomancaonhat { get; set; }
    public double domantrungbinh { get; set; }
} 

public class SalinityDetailStatistics : SalinityTotalStatistics {
    public int nam { get; set; }
} 