namespace WebApi.Models;

public class HoChua{
    public int objectid { get; set;}
    public string? idhochua { get; set; }
    public string? ten { get; set; }
    public string? loaiho { get; set; }
    public string? vitri { get; set; }
    public string? kinhdo { get; set; }
    public string? vido { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? ngay { get; set; }
    public string? h { get; set; }
    public string? w { get; set; }
    public string? qvh { get; set; }
    public string? qxa { get; set; }
    public string? qcsi { get; set; }
    public string? qcsii { get; set; }
    public string? qcsiii { get; set; }
    public string? qtb { get; set; }
    public string? bh { get; set; }
    public string? r { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? shape { get; set; }
}
public class ReservoirTotalStatistics {
    public string? tencachochua { get; set; }
    public int tongsophantu { get; set; }
    public string? thongso { get; set; }
    public double luuluongthapnhat { get; set; }
    public string? thoidiemluuluongthapnhat { get; set; }
    public double luuluongcaonhat { get; set; }
    public string? thoidiemluuluongcaonhat { get; set; }
    public double luuluongtrungbinh { get; set; }
}    
public class ReservoirDetailStatistics : ReservoirTotalStatistics{
    public int nam { get; set; }
}        
public class ThuyHeHoChua{
    public string? tenthuyhehochua { get; set; }
    public string? shape { get; set; }
}