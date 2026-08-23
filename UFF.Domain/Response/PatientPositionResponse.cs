using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Enums;

namespace UFF.FichaAnestesica.Domain.Response
{
    public class PatientPositionResponse
    {
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public SurgicalPositionEnum Position { get; set; }

        public static PatientPositionResponse ToResponse(PatientPosition entity)
        {
            return new PatientPositionResponse
            {
                Time = entity.Time,
                Date = entity.Date,
                Position = entity.Position
            };
        }
    }
}
