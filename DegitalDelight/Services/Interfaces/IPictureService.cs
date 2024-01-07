namespace DegitalDelight.Services.Interfaces
{
    public interface IPictureService
    {
        string CreateProductPicture(IFormFile picture);
        string CreateAccountPicture(IFormFile picture);
        string DeleteProductPicture(string url);
        string DeleteAccountPicture(string url);
    }
}
