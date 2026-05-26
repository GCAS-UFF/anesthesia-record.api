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

        public async Task<(byte[], string)> GeneratePdfAsync(int id)
        {
            var model = await _anesthesiaRecordRepository.GetByIdAsync(id);

            if (model == null)
                return (null, null);

            var layoutBase = _configuration["Pdf:ViewPath"];
            var html = await RenderViewToStringAsync(layoutBase, model);

            await EnsureBrowserDownloaded();

            var launchOptions = new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
            };

            await using var browser = await Puppeteer.LaunchAsync(launchOptions);
            await using var page = await browser.NewPageAsync();

            await page.SetContentAsync(html);

            var pdf = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "10mm",
                    Bottom = "10mm",
                    Left = "10mm",
                    Right = "10mm"
                }
            });

            return (pdf, model.ExternalPatientId);
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

        private static async Task EnsureBrowserDownloaded()
        {
            if (_browserDownloaded) 
                return;

            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();
            _browserDownloaded = true;
        }
    }
}