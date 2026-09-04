using Microsoft.Extensions.Configuration;
using Moq;
using UFF.FichaAnestesica.Domain.Response;
using UFF.FichaAnestesica.Domain.Response.Print;
using UFF.FichaAnestesica.Domain.Services;
using UFF.FichaAnestesica.Infra.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class PdfServiceTest
    {
        private readonly Mock<IAnesthesiaRecordPrintService> _printServiceMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly PdfService _service;

        public PdfServiceTest()
        {
            _printServiceMock = new Mock<IAnesthesiaRecordPrintService>();
            _configMock = new Mock<IConfiguration>();

            _service = new PdfService(
                null!,
                null!,
                null!,
                null!,
                _configMock.Object,
                _printServiceMock.Object);
        }

        [Fact]
        public async Task GeneratePdfAsync_Should_Return_Null_When_Record_Not_Found()
        {
            _printServiceMock.Setup(r => r.BuildAsync(99))
                     .ReturnsAsync((AnesthesiaRecordPrintViewModel?)null);

            var (html, patientId) = await _service.GeneratePdfAsync(99);

            Assert.Null(html);
            Assert.Null(patientId);
        }

        [Fact]
        public async Task GeneratePdfAsync_Should_Return_Html_And_PatientId_When_Record_Found()
        {
            var viewModel = new AnesthesiaRecordPrintViewModel
            {
                Record = new AnesthesiaRecordResponse { ExternalPatientId = "123" }
            };

            _printServiceMock.Setup(r => r.BuildAsync(1))
                     .ReturnsAsync(viewModel);
            _configMock.Setup(c => c["Pdf:ViewPath"])
                       .Returns("Views/Pdf/AnesthesiaRecord.cshtml");

            var exception = await Record.ExceptionAsync(() => _service.GeneratePdfAsync(1));

            Assert.NotNull(exception);
            _printServiceMock.Verify(r => r.BuildAsync(1), Times.Once);
            _configMock.Verify(c => c["Pdf:ViewPath"], Times.Once);
        }
    }
}
