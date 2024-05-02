namespace WebApi.Models;

public class ChangePassword{
    public string MemberId { get; set; } = null!;
    public string OldPwd { get; set; } = null!;
    public string NewPwd { get; set; } = null!;
}
public class ResetPassword{
    public string MemberId { get; set; } = null!;
    public string Pwd { get; set; } = null!;
}