using DegitalDelight.Services.Interfaces;

namespace DegitalDelight.Services
{
    public class PictureService : IPictureService
    {
        public PictureService()
        {
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "product")))
			{
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "product"));
			}
			if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "account")))
			{
				Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "account"));
			}
		}
        public string CreateProductPicture(IFormFile picture)
		{
			var fileextension = Path.GetExtension(picture.FileName);
			var filename = Guid.NewGuid().ToString() + fileextension;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "product", filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                picture.CopyTo(stream);
            }
            return filename;
        }

        public string CreateAccountPicture(IFormFile picture)
		{
			var fileextension = Path.GetExtension(picture.FileName);
			var filename = Guid.NewGuid().ToString() + fileextension;

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "account", filename);
			using (var stream = new FileStream(path, FileMode.Create))
			{
				picture.CopyTo(stream);
			}
			return filename;
		}

        public string DeleteProductPicture(string url)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "product", url);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return url;
        }

        public string DeleteAccountPicture(string url)
        {
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "account", url);
			if (File.Exists(path))
            {
				File.Delete(path);
			}
			return url;
		}
    }
}
