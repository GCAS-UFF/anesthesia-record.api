namespace UFF.FichaAnestesica.Application.Interfaces
{
    public interface IPdfService
    {
        Task<(string, string)> GeneratePdfAsync(int id);
    }
}