namespace  ETrade.Application.Model;

public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Action> Actions { get; set; } = new();
}