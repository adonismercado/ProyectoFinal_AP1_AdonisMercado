using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Components.Forms;

namespace ProyectoFinal_AP1_AdonisMercado.Services;

public class CloudflareR2Service : ICloudflareR2Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _publicUrl;

    public CloudflareR2Service(IAmazonS3 s3client, IConfiguration configuration)
    {
        _s3Client = s3client;
        _bucketName = configuration["CloudflareR2:BucketName"]!;
        _publicUrl = configuration["CloudflareR2:PublicUrl"]!;
    }

    public CloudflareR2Service(IConfiguration configuration)
    {
        var accountId = configuration["CloudflareR2:AccountId"];
        var accessKey = configuration["CloudflareR2:AccessKey"];
        var secretKey = configuration["CloudflareR2:SecretKey"];
        _bucketName = configuration["CloudflareR2:BucketName"]!;
        _publicUrl = configuration["CloudflareR2:PublicUrl"]!;

        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        var config = new AmazonS3Config
        {
            ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(credentials, config);
    }

    public async Task<string?> SubirArchivo(IBrowserFile archivo, string carpeta, string? nombrePersonalizado = null)
    {
        const long maxSize = 10485760; // 10 MB
        if (archivo.Size > maxSize)
        {
            return null;
        }

        var extensionesPermitidas = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(archivo.Name).ToLowerInvariant();

        if (!extensionesPermitidas.Contains(extension))
        {
            return null;
        }

        var nombreArchivo = nombrePersonalizado ?? Guid.NewGuid().ToString();
        var key = $"{carpeta}/{nombreArchivo}{extension}";

        using var stream = archivo.OpenReadStream(maxSize);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = memoryStream,
            ContentType = archivo.ContentType,
            DisablePayloadSigning = true
        };

        await _s3Client.PutObjectAsync(request);

        return $"{_publicUrl}/{key}";
    }

    public async Task<bool> EliminarArchivo(string urlCompleta)
    {
        var key = urlCompleta.Replace(_publicUrl + "/", "");

        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        await _s3Client.DeleteObjectAsync(request);
        return true;
    }
}