namespace  ETrade.Application.Model;

public class Menu
{
    public string Name { get; set; }
    public virtual List<Action> Actions { get; set; } = new();
    
}