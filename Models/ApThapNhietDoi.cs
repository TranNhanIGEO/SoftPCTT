namespace WebApi.Models;

public class ApThapNhietDoi{
    public int objectid { get; set; }
    public string? idapthap { get; set; }
    public string? tenapthap { get; set; }
    public string? gio { get; set; }
    public string? ngay { get; set; }
    // public string? toadox { get; set; }
    // public string? toadoy { get; set; }
    public string? apsuat { get; set; }
    public string? tocdogio { get; set; }
    public string? vitri { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? ngaybatdau { get; set; }
    public string? ngayketthuc { get; set; }
    public string? centerid { get; set; }
    public string? tenvn { get; set; }
    public string? kvahhcm { get; set; }
}
public class SearchApThapNhietDoi : ApThapNhietDoi{
    public string? shape { get; set; }
    public string? line { get; set; }
}
public class ApThapNhietDoiDetail{
    public int objectid { get; set; }
    public string? idapthap { get; set; }
    public string? tenapthap { get; set; }
    public string? gio { get; set; }
    public string? ngay { get; set; }
    // public string? toadox { get; set; }
    // public string? toadoy { get; set; }
    public string? apsuat { get; set; }
    public string? tocdogio { get; set; }
    public string? vitri { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? ngaybatdau { get; set; }
    public string? ngayketthuc { get; set; }
    public string? centerid { get; set; }
    public string? tenvn { get; set; }
    public string? kvahhcm { get; set; }
    public string? shape { get; set; }
}

public class TropicalDepressionStatistics{
    public string? nam { get; set; }
    public int tongsoATND { get; set; }
    public string? tenATND { get; set; }
}