namespace WebApiWithFastEndpoints;

public class TheResponse
{
    public int RequestedId { get; set; }
    public string FullName { get; set; } = null!;
    public bool IsOver18 { get; set; }
}