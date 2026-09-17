namespace UFF.FichaAnestesica.Domain.Response
{
    public class OxygenFlowResponse
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public decimal? FlowRateLPerMin { get; set; }
        public bool IsActive { get; set; }

        public static OxygenFlowResponse ToResponse(OxygenFlow entity)
        {
            return new OxygenFlowResponse
            {
                Id = entity.Id,
                Time = entity.Time,
                Date = entity.Date,
                FlowRateLPerMin = entity.FlowRateLPerMin,
                IsActive = entity.IsActive
            };
        }
    }
}
