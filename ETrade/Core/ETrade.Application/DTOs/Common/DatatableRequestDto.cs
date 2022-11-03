namespace ETrade.Application.DTOs.Common;

public class DatatableRequestDto
{
    public int Draw { get; set; }
    public int Start { get; set; }
    public int Length { get; set; }
    public Search Search { get; set; }
    public List<Column> Columns { get; set; }
    public List<Order> Order { get; set; }
}

public class Column
{
    public string Data { get; set; }
    public string Name { get; set; }
    public bool Searchable { get; set; }
    public bool Orderable { get; set; }
    public Search Search { get; set; }
}

public class Order
{
    public int Column { get; set; }
    public OrderDirType Dir { get; set; }
}

public enum OrderDirType
{
    Asc,
    Desc
}

public class Search
{
    public string Value { get; set; }
    public bool Regex { get; set; }
}