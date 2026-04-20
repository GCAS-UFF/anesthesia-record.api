namespace UFF.FichaAnestesica.Domain.Dto
{
    public class SurgeryListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; } 
        public string MedicalRecord { get; set; } 
        public string Room { get; set; } 
        public string Procedure { get; set; } 
        public string Status { get; set; } 
    }
}