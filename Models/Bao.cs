namespace WebApi.Models;

public class Bao{
    public int objectid { get; set; }
    public string? idbao { get; set; }
    public string? tenbao { get; set; }
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
    public string? capbao { get; set; }
    public string? ngaybatdau { get; set; }
    public string? ngayketthuc { get; set; }
    public string? centerid { get; set; }
    public string? tenvn { get; set; }
    public string? kvahhcm { get; set; }
}

public class SearchBao : Bao{
    public string shape { get; set; } = null!;
    public string line { get; set; } = null!;
}

public class StormStatistics{
    public string? capdobao { get; set; }
    public int tansuatxuathien { get; set; }
    public double phantramcapdobao { get; set; }
    public string? tencacconbao { get; set; }
    public string? mamau { get; set; }
}

public class BaoDetail{
    public int objectid { get; set; }
    public string? idbao { get; set; }
    public string? tenbao { get; set; }
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
    public string? capbao { get; set; }
    public string? ngaybatdau { get; set; }
    public string? ngayketthuc { get; set; }
    public string? centerid { get; set; }
    public string? tenvn { get; set; }
    public string? kvahhcm { get; set; }
    public string? shape { get; set; }
}