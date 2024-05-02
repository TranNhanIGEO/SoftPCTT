namespace WebApi.Models;

public class Member{
    public string memberid{ get; set; } = null!;
    public string username { get; set; } = null!;
    public string fullname { get; set; } = null!;
    public string department { get; set; } = null!;
    public string unit { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string email { get; set; } = null!;
    public bool? isdeleted { get; set; }
    //public byte[] password { get; set; } = null!;
}

public class AddMember{
    public string memberid{ get; set; } = null!;
    public string username { get; set; } = null!;
    public string fullname { get; set; } = null!;
    public string department { get; set; } = null!;
    public string unit { get; set; } = null!;
    public string phone { get; set; } = null!;
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;
}