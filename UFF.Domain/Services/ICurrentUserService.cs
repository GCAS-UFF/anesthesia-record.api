namespace UFF.FichaAnestesica.Domain.Services
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        bool IsAdmin { get; }
        bool IsAuthenticated { get; }
    }
}
