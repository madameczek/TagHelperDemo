namespace TagHelperDemo.WebApp.Domain;

public class FamilyName
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public char Gender { get; set; }
    public int OccurenceCount { get; set; }
}