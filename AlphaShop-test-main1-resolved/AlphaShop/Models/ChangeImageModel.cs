namespace AlphaShop.Models;

public class ChangeImageModel
{
    public IFormFile Image { get; set; }
    public string ImageFolderPath { get; set; }
}