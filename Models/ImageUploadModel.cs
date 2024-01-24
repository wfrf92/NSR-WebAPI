public class ImageUploadModel
{
    public int ProductId { get; set; }
    public IFormFile PrimaryImage { get; set; }
    public List<IFormFile> OtherImages { get; set; }
}