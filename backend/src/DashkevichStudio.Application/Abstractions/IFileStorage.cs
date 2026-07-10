namespace DashkevichStudio.Application.Abstractions;

public interface IFileStorage
{
    Task<string> SaveAsync(Stream content, string extension, CancellationToken cancellationToken);
    Task DeleteAsync(string storedName, CancellationToken cancellationToken);
    string GetPath(string storedName);
}
