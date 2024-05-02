namespace WebApi.Models;

public class MemberRole{
    public int roleid { get; set; }
    public string rolename { get; set; } = null!;
    public string mahuyen { get; set; } = null!;
    public string tenhuyen { get; set; } = null!;
    public int malopdoituong { get; set; }
    public string nhomdulieu { get; set; } = null!;    
    public string tengocbang { get; set; } = null!;    
    public string lopdulieu { get; set; } = null!;
    public string malopdulieu { get; set; } = null!;
    public string maquanhuyen { get; set; } = null!;
}
public class MemberRoleAddEdit{
    public string memberid { get; set; } = null!;
    public int roleid { get; set; }
    public string? mahuyen { get; set; }
    public string[]? malopdulieu { get; set; }
}