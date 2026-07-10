using DashkevichStudio.Application.Abstractions;
using Microsoft.Extensions.Options;

namespace DashkevichStudio.Infrastructure.Files;

public sealed class LocalFileStorage(IOptions<FileStorageOptions> options) : IFileStorage
{
    private readonly string _rootPath = Path.GetFullPath(options.Value.RootPath);

    public async Task<string> SaveAsync(Stream content, string extension, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(_rootPath);
        var storedName = $"{Guid.NewGuid():N}{extension}";
        var path = GetPath(storedName);

        await using var target = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await content.CopyToAsync(target, cancellationToken);
        return storedName;
    }

    public Task DeleteAsync(string storedName, CancellationToken cancellationToken)
    {
        var path = GetPath(storedName);
        if (File.Exists(path))
            File.Delete(path);

        return Task.CompletedTask;
    }

    public string GetPath(string storedName)
    {
        var safeName = Path.GetFileName(storedName);
        return Path.Combine(_rootPath, safeName);
    }
}
