using UFF.FichaAnestesica.Domain.Entities;

namespace UFF.FichaAnestesica.Domain.Response
{
   
    public class PreAnesthesiaChecklistGroupResponse
    {
        public int Id { get; set; }
        public string GroupKey { get; set; } = string.Empty;
        public List<string> Findings { get; set; } = new();
        public string? OtherDescription { get; set; }
        public string? Observations { get; set; }

        public static PreAnesthesiaChecklistGroupResponse ToResponse(PreAnesthesiaComorbidity entity)
        {
            return new PreAnesthesiaChecklistGroupResponse
            {
                Id = entity.Id,
                GroupKey = entity.GroupKey,
                Findings = entity.Findings,
                OtherDescription = entity.OtherDescription,
                Observations = entity.Observations
            };
        }

        public static PreAnesthesiaChecklistGroupResponse ToResponse(PreAnesthesiaPhysicalExamArea entity)
        {
            return new PreAnesthesiaChecklistGroupResponse
            {
                Id = entity.Id,
                GroupKey = entity.AreaKey,
                Findings = entity.Findings,
                OtherDescription = entity.OtherDescription,
                Observations = entity.Observations
            };
        }
    }
}
