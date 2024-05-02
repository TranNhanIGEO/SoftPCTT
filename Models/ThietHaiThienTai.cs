namespace WebApi.Models;

public class ThietHaiThienTai{
    public int objectid { get; set; }
    public string? idthiethai { get; set; }
    public string? loaithientai { get; set; }
    public string? doituongthiethai { get; set; }
    public string? motathiethai { get; set; }
    public string? dvtthiethai { get; set; }
    public string? soluong { get; set; }
    public string? giatrithiethai { get; set; }
    public string? diadiem { get; set; }
    public string? gio { get; set; }
    public string? ngay { get; set; }
    public string? maxa { get; set; }
    public string? mahuyen { get; set; }
    public string? namcapnhat { get; set; }
    public string? ghichu { get; set; }
}
public class ThietHaiThienTaiDetailStatistics{
    public string? tenhuyen { get; set; } 
    public string? loaithientai { get; set; }
    public string? doituongthiethai { get; set; }
    public string? motathiethai { get; set; }
    public double soluong { get; set; }
    public string? dvtthiethai { get; set; }
    public string mamau { get; set; } = null!;
}
public class ThietHaiThienTaiTotalStatistics{
    public string? doituongthiethai { get; set; }
    public double soluong { get; set; }
    public string? mamau { get; set; }
    public string? phamvithongke { get; set; }
}