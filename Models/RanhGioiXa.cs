namespace WebApi.Models;

public class RanhGioiXa{
    public int objectid { get; set; }
    public string maxa { get; set; } = null!;
    public string tenxa { get; set; } = null!;
    public string mahuyen { get; set; } = null!;
    public string tenhuyen { get; set; } = null!;
    public double? dientichtunhien { get; set; }
    public double? shape_length { get; set; }
    public double? shape_area { get; set; }
    public string centroid { get; set; } = null!;
    public string shape { get; set; } = null!;
}