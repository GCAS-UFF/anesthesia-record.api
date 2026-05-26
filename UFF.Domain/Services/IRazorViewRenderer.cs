namespace UFF.FichaAnestesica.Application.Interfaces
{
    public interface IRazorViewRenderer
    {
        Task<string> RenderAsync<T>(string viewName, T model);
    }
}