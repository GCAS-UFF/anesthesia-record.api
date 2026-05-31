using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using UFF.FichaAnestesica.Application.Interfaces;

namespace UFF.FichaAnestesica.Infra.Services
{
    public class RazorViewRendererService : IRazorViewRenderer
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public RazorViewRendererService(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }
        public async Task<string> RenderAsync<T>(string viewPath, T model)
        {
            var actionContext = new ActionContext(
                new DefaultHttpContext
                {
                    RequestServices = _serviceProvider
                },
                new RouteData(),
                new ActionDescriptor());

            var viewEngineResult = _viewEngine.GetView(
                null,
                viewPath,
                true);

            if (!viewEngineResult.Success)
            {
                throw new Exception(
                    $"View '{viewPath}' não encontrada.");
            }

            var view = viewEngineResult.View;

            await using var output = new StringWriter();

            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<T>(
                    new EmptyModelMetadataProvider(),
                    new ModelStateDictionary())
                {
                    Model = model
                },
                new TempDataDictionary(
                    actionContext.HttpContext,
                    _tempDataProvider),
                output,
                new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            return output.ToString();
        }       
    }
}