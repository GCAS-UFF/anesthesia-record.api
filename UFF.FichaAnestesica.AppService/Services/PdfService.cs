using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using UFF.FichaAnestesica.Application.Interfaces;
using UFF.FichaAnestesica.Domain.Repositories;

namespace UFF.FichaAnestesica.Infra.Services
{
    public class PdfService : IPdfService
    {
        private static bool _browserDownloaded = false;
        private readonly IAnesthesiaRecordRepository _anesthesiaRecordRepository;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public PdfService(ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider,
            IConfiguration configuration, IAnesthesiaRecordRepository anesthesiaRecordRepository)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _anesthesiaRecordRepository = anesthesiaRecordRepository;
        }

        public async Task<(string, string)> GeneratePdfAsync(int id)
        {
            var model = await _anesthesiaRecordRepository.GetByIdAsync(id);

            if (model == null)
                return (null, null);

            var layoutBase = _configuration["Pdf:ViewPath"];
            var html = await RenderViewToStringAsync(layoutBase, model);

            return (html, model.PatientId);
        }

        private async Task<string> RenderViewToStringAsync(string viewPath, object model)
        {
            var httpContext = _httpContextAccessor.HttpContext ?? CreateEmptyHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            var viewResult = _viewEngine.GetView(viewPath, viewPath, false);

            await using var writer = new StringWriter();

            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            var tempData = new TempDataDictionary(httpContext, _tempDataProvider);
            var viewContext = new ViewContext(actionContext, viewResult.View, viewData, tempData, writer, new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }

        private HttpContext CreateEmptyHttpContext()
        {
            var context = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };
            return context;
        }      
    }
}