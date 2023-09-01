using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        // Constructor for initializing the 'PhotoService' with Cloudinary configuration.
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            // Create a Cloudinary account using configuration values.
            var account = new Account(
                config.Value.CloudName,   // Cloudinary cloud name
                config.Value.ApiKey,      // Cloudinary API key
                config.Value.ApiSecret    // Cloudinary API secret
            );

            // Create a Cloudinary instance with the account settings.
            _cloudinary = new Cloudinary(account);
        }

        // Asynchronously upload an image to Cloudinary and return the upload result.
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            // Create an instance to store the upload result.
            var uploadResult = new ImageUploadResult();

            // Check if the uploaded file has content.
            if (file.Length > 0)
            {
                // Open a stream to read the file content.
                using var stream = file.OpenReadStream();

                // Define parameters for the image upload.
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream), // Specify the file to upload.
                    Transformation = new Transformation()
                        .Height(500)
                        .Width(500)
                        .Crop("fill")
                        .Gravity("face"), // Define image transformation options.
                    Folder = "da-net7u" // Specify the destination folder in Cloudinary.
                };

                // Perform the asynchronous image upload to Cloudinary.
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            // Return the upload result, which contains information about the uploaded image.
            return uploadResult;
        }

        // Asynchronously delete an image from Cloudinary using its publicId.
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            // Create deletion parameters specifying the publicId of the image to delete.
            var deleteParams = new DeletionParams(publicId);

            // Perform the asynchronous image deletion from Cloudinary.
            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}