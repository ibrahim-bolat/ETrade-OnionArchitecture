using Newtonsoft.Json;

namespace ETrade.Application.DTOs.Common;

public class TreeViewDto
{
    public int id { get; set; }
    public string text { get; set; }
    public bool @checked { get; set; }
    public virtual List<TreeViewDto> children { get; set; }
}