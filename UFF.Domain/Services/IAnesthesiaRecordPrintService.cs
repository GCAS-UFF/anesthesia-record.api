using UFF.FichaAnestesica.Domain.Response.Print;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IAnesthesiaRecordPrintService
    {
        Task<AnesthesiaRecordPrintViewModel?> BuildAsync(int id);
    }
}
