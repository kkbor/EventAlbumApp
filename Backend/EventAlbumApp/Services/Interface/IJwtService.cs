namespace EventAlbumApp.Services.Interface
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string email);
    }
}
