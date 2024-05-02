namespace WebApi.Models;

public class MucNuoc{
    public int objectid { get; set; }
    public string? idtrammucnuoc { get; set; }
    public string? tentram { get; set; }
    public string? gio { get; set; }
    public string? ngay { get; set; }
    public string? toadox { get; set; }
    public string? toadoy { get; set; }
    public string? mucnuoc { get; set; }
    public string? docaodinhtrieu { get; set; }
    public string? docaochantrieu { get; set; }
    public string? baodongi { get; set; }
    public string? baodongii { get; set; }
    public string? baodongiii { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? shape { get; set; }
}
public class TidalTotalStatistics {
    public string? Tentramdomucnuoc { get; set; }
    public int tongsophantu { get; set; }
    public double mucnuocthapnhat { get; set; }
    public string? thoidiemmucnuocthapnhat { get; set; }
    public double mucnuoccaonhat { get; set; }
    public string? thoidiemmucnuoccaonhat { get; set; }
    public double mucnuoctrungbinh { get; set; }
}
public class TidalDetailStatistics : TidalTotalStatistics{
    public int nam { get; set; }
}