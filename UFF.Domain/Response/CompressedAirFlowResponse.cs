namespace UFF.FichaAnestesica.Domain.Response
{
    public class CompressedAirFlowResponse
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public decimal? FlowRateLPerMin { get; set; }
        public bool IsActive { get; set; }

        public static CompressedAirFlowResponse ToResponse(CompressedAirFlow entity)
        {
            return new CompressedAirFlowResponse
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
