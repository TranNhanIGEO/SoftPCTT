namespace WebApi.Models;

public class RanhGioiHuyen{
    public int objectid { get; set; }
    public string mahuyen { get; set; } = null!;
    public string tenhuyen { get; set; } = null!;
    public double? dientichtunhien { get; set; }
    public double? shape_length { get; set; }
    public double? shape_area { get; set; }
    public string centroid { get; set; } = null!;
    public string shape { get; set; } = null!;
}