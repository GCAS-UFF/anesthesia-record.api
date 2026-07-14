namespace UFF.FichaAnestesica.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }
    }
}