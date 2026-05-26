namespace UFF.FichaAnestesica.Application.Interfaces
{
    public interface IPdfService
    {
        Task<(byte[], string)> GeneratePdfAsync(int id);
    }
}