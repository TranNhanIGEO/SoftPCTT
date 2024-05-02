namespace WebApi.Models;

public class RefreshToken{
    public string refreshtokenid { get; set; } = null!;
    // public string accesstoken { get; set; } = null!;
    // public string memberid { get; set; } = null!;
    public string memberloginid { get; set; } = null!;
    public string token { get; set; } = null!;
}