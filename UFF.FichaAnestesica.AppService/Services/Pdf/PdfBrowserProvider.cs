using PuppeteerSharp;

namespace UFF.FichaAnestesica.Service.Services.Pdf
{
    public interface IPdfBrowserProvider
    {
        Task<IBrowser> GetBrowserAsync();
    }

    // Instância única e reaproveitada do Chromium headless usado para gerar os PDFs
    // dos relatórios (evita baixar/abrir um navegador a cada requisição).
    public class PdfBrowserProvider : IPdfBrowserProvider, IAsyncDisposable
    {
        private static bool _fetched;
        private static readonly SemaphoreSlim Lock = new(1, 1);
        private IBrowser? _browser;

        public async Task<IBrowser> GetBrowserAsync()
        {
            if (_browser != null && _browser.IsConnected)
                return _browser;

            await Lock.WaitAsync();
            try
            {
                if (_browser != null && _browser.IsConnected)
                    return _browser;

                if (!_fetched)
                {
                    await new BrowserFetcher().DownloadAsync();
                    _fetched = true;
                }

                _browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    Args = new[] { "--no-sandbox", "--disable-gpu", "--disable-dev-shm-usage" }
                });

                return _browser;
            }
            finally
            {
                Lock.Release();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_browser != null)
                await _browser.CloseAsync();
        }
    }
}
