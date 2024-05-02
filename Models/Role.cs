namespace WebApi.Models;

public class RoleByMember{
    public string memberid{ get; set; } = null!;
    public int roleid { get; set; }
    public string mahuyen { get; set; } = null!;
    public string[] malopdulieu { get; set; } = null!;
}

// dung de luu cac quyen cua nguoi dung vao Claim
public class RoleOfMember{ 
    public int RoleId { get; set; }
}