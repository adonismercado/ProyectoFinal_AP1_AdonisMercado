using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Moq;
using ProyectoFinal_AP1_AdonisMercado.Services;
using Xunit;

namespace ProyectoFinal_AP1_AdonisMercado.Tests;

public class CloudflareR2ServiceTests
{
    private static CloudflareR2Service CreateService(Mock<IAmazonS3> s3Mock)
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "CloudflareR2:BucketName", "test-bucket" },
                { "CloudflareR2:PublicUrl", "https://cdn.test.com" }
            })
            .Build();

        return new CloudflareR2Service(s3Mock.Object, config);
    }

    private static Mock<IBrowserFile> CreateFile(string nombre, long tamano, string tipoContenido)
    {
        var fileMock = new Mock<IBrowserFile>();
        fileMock.Setup(f => f.Name).Returns(nombre);
        fileMock.Setup(f => f.Size).Returns(tamano);
        fileMock.Setup(f => f.ContentType).Returns(tipoContenido);
        fileMock
            .Setup(f => f.OpenReadStream(
                It.IsAny<long>(), It.IsAny<CancellationToken>())
            )
            .Returns(new MemoryStream(new byte[] { 1 }));

        return fileMock;
    }

    [Fact]
    public async Task SubirArchivo_Valido_LlamaS3()
    {
        var s3Mock = new Mock<IAmazonS3>();
        s3Mock.Setup(s => s.PutObjectAsync(
            It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>())
        )
        .ReturnsAsync(new PutObjectResponse());

        var servicio = CreateService(s3Mock);
        var archivo = CreateFile("test.pdf", 100, "application/pdf");

        var resultado = await servicio.SubirArchivo(archivo.Object, "docs");

        Assert.NotNull(resultado);
        s3Mock.Verify(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task SubirArchivo_MuyGrande_RetornaNull()
    {
        var s3Mock = new Mock<IAmazonS3>();
        var servicio = CreateService(s3Mock);
        var archivo = CreateFile("big.pdf", 20 * 1024 * 1024, "application/pdf");

        var resultado = await servicio.SubirArchivo(archivo.Object, "docs");

        Assert.Null(resultado);
        s3Mock.Verify(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SubirArchivo_ExtensionInvalida_RetornaNull()
    {
        var s3Mock = new Mock<IAmazonS3>();
        var servicio = CreateService(s3Mock);
        var archivo = CreateFile("virus.exe", 100, "application/octet-stream");

        var resultado = await servicio.SubirArchivo(archivo.Object, "docs");

        Assert.Null(resultado);
        s3Mock.Verify(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task SubirArchivo_NombrePersonalizado_UsaKeyCorrecta()
    {
        var s3Mock = new Mock<IAmazonS3>();
        PutObjectRequest? request = null;

        s3Mock.Setup(s => s.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
              .Callback<PutObjectRequest, CancellationToken>((r, _) => request = r)
              .ReturnsAsync(new PutObjectResponse());

        var servicio = CreateService(s3Mock);
        var archivo = CreateFile("foto.jpg", 100, "image/jpeg");

        await servicio.SubirArchivo(archivo.Object, "imgs", "perfil");

        Assert.Equal("imgs/perfil.jpg", request!.Key);
    }

    [Fact]
    public async Task EliminarArchivo_LlamaS3()
    {
        var s3Mock = new Mock<IAmazonS3>();
        s3Mock.Setup(s => s.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
              .ReturnsAsync(new DeleteObjectResponse());

        var servicio = CreateService(s3Mock);

        var resultado = await servicio.EliminarArchivo("https://cdn.test.com/docs/test.pdf");

        Assert.True(resultado);
        s3Mock.Verify(
            s => s.DeleteObjectAsync(
                It.Is<DeleteObjectRequest>(r => r.Key == "docs/test.pdf"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}