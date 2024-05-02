namespace WebApi.Models;

public class SatLoLine{
    public int objectid { get; set; }
    public string? idsatlol { get; set; }
    public string? vitri { get; set; }
    public string? tuyensong { get; set; }
    public string? capsong { get; set; }
    public string? chieudai { get; set; }
    public string? chieurong { get; set; }
    public string? mucdo { get; set; }
    public string? tinhtrang { get; set; }
    public string? anhhuong { get; set; }
    public string? khoangcachah { get; set; }
    public string? ditichah { get; set; }
    public string? sohoah { get; set; }
    public string? songuoiah { get; set; }
    public string? hatangah { get; set; }
    public string? congtrinhchongsl { get; set; }
    public string? chudautu { get; set; }
    public string? tenduan { get; set; }
    public string? quymoduan { get; set; }
    public string? tongmucduan { get; set; }
    public string? tiendothuchien { get; set; }
    public string? nguongoc { get; set; }
    public string? dubao { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? ctxdke { get; set; }
    public string? shape_length { get; set; }
    public string? shape { get; set; }
    public string? toado { get; set; }
}
public class SatLoLineDetailStatistics{
    public string? quan_huyen_tp { get; set; }
    public string? mucdosatlo { get; set; }
    // public int soluongvitrisatlo { get; set; }
    public double tongchieudaisatlo { get; set; }
    public string? mamau { get; set; }
}
public class SatLoLineTotalStatistics{
    public string? mucdosatlo { get; set; }
    // public int soluongvitrisatlo { get; set; }
    public double tongchieudaisatlo { get; set; }
    public string? mamau { get; set; }
    public string? phamvithongke { get; set; }
}
