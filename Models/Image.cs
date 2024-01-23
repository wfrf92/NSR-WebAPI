using Newtonsoft.Json;

public class OtherImage
{
    [JsonProperty("image")]
    public string? Image { get; set; }
    [JsonProperty("thumbImage")]
    public string? ThumbImage { get; set; }
    [JsonProperty("title")]
    public string? Title { get; set; }
}