using Newtonsoft.Json;

namespace ETrade.Application.DTOs.Common;

public class DatatableResponseDto<T>
{
    [JsonProperty("draw")] 
    public int Draw { get; set; }
    
    [JsonProperty("recordsTotal")] 
    public int RecordsTotal { get; set; }
    
    [JsonProperty("recordsFiltered")] 
    public int RecordsFiltered { get; set; }

    [JsonProperty("data")] 
    public IEnumerable<T> Data { get; set; }

    [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
    public string Error { get; set; }
    
}