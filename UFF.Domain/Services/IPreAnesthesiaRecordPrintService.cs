using UFF.FichaAnestesica.Domain.Response.Print;

namespace UFF.FichaAnestesica.Domain.Services
{
    public interface IPreAnesthesiaRecordPrintService
    {
        Task<PreAnesthesiaRecordPrintViewModel?> BuildAsync(int anesthesiaRecordId);
    }
}
