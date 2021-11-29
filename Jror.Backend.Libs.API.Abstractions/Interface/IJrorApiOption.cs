namespace Jror.Backend.Libs.API.Abstractions.Interface
{
    public interface IJrorApiOption

    {
        int MajorVersion { get; set; }
        int MinorVersion { get; set; }

        string HeaderApiVersionReader { get; set; }
        string GroupNameFormat { get; set; }
        string Title { get; set; }
        int DefaultVersion { get; set; }
        string Email { get; set; }
        string Uri { get; set; }
        string Description { get; set; }
    }
}