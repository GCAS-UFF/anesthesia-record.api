namespace UFF.FichaAnestesica.Domain.Commands.UserSettings
{
    public class ChangeAdminPasswordCommand
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
