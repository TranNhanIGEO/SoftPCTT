namespace WebApi.Models;

public class DeBaoBoBao{
    public int objectid { get; set; }
    public string? idkenhmuong { get; set; }
    public string? tenkenhmuong { get; set; }
    public string? vitri { get; set; }
    public string? chieudai { get; set; }
    public string? caotrinhdaykenh { get; set; }
    public string? berongkenh { get; set; }
    public string? hesomai { get; set; }
    public string? caotrinhbotrai { get; set; }
    public string? caotrinhbophai { get; set; }
    public string? berongbotrai { get; set; }
    public string? berongbophai { get; set; }
    public string? hanhlangbaove { get; set; }
    public string? capcongtrinh { get; set; }
    public string? ketcaucongtrinh { get; set; }
    public string? muctieunhiemvu { get; set; }
    public string? diadiem { get; set; }
    public string? namsudung { get; set; }
    public string? hethongcttl { get; set; }
    public string? donviquanly { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
    public string? shape_length { get; set; }
    public string? shape { get; set; }
    public string? toado { get; set; }
}
public class DebaoBoBaoStatistics{
    public string? donviquanly { get; set; }    
    public int tongsophantutheodvql { get; set; }
    public double? tongchieudaidebaobobao { get; set; }
}