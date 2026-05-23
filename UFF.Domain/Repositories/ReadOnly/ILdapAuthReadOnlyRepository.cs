namespace UFF.FichaAnestesica.Domain.Repositories.ReadOnly
{
    public interface ILdapAuthReadOnlyRepository
    {
        bool ValidateCredentials(string username, string password);
    }
}
