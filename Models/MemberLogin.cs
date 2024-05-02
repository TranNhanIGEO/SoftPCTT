namespace WebApi.Models;

public class MemberLogin{
    public string memberloginid { get; set; } = null!;
    public string username { get; set; } = null!;
    public DateTime logindate { get; set; }
    public bool isdeleted { get; set; }
}
public class MemberLoginAdd {
    public string memLogId { get; set; } = null!;
    public string username { get; set; } = null!;
}