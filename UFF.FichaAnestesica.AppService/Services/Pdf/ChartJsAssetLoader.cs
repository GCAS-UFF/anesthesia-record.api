namespace UFF.FichaAnestesica.Service.Services.Pdf
{
    public static class ChartJsAssetLoader
    {
        private static string? _cached;

        public static string GetSource()
        {
            if (_cached != null)
                return _cached;

            var assembly = typeof(ChartJsAssetLoader).Assembly;
            var resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith("chart.umd.min.js", StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new InvalidOperationException("Recurso embutido do Chart.js não encontrado.");

            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            _cached = reader.ReadToEnd();

            return _cached;
        }
    }
}
