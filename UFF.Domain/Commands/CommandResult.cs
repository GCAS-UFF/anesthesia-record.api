namespace UFF.FichaAnestesica.Domain.Commands
{
    public class CommandResult
    {
        public bool Valid { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        public CommandResult(bool valid, object data = null, string message = "")
        {
            Valid = valid;
            Data = data;
            Message = message;
        }

        public static CommandResult Success(object data = null, string message = "Operação realizada com sucesso")
        {
            return new CommandResult(true, data, message);
        }

        public static CommandResult Fail(string message, object data = null)
        {
            return new CommandResult(false, data, message);
        }
    }
}