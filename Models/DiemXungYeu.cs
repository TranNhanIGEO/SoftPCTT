namespace WebApi.Models;

public class DiemXungYeu{
    public int objectid { get; set; }
    public string? idxungyeu { get; set; }
    public string? vitri { get; set; }
    public string? toadox { get; set; }
    public string? toadoy { get; set; }
    public string? sodan { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? phuongan { get; set; }
    public string? shape { get; set; }
}
public class DiemXungYeuStatistics{
    public string? quan_huyen_tp { get; set; }
    public int? Tongsovitrixungyeu { get; set; }
    public string? vitrixungyeutheophuongan { get; set; }
}