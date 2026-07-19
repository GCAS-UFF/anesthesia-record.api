using Microsoft.Extensions.Configuration;
using Moq;
using UFF.FichaAnestesica.Domain.Entities;
using UFF.FichaAnestesica.Domain.Repositories;
using UFF.FichaAnestesica.Infra.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class PdfServiceTest
    {
        private readonly Mock<IAnesthesiaRecordRepository> _repoMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly PdfService _service;

        public PdfServiceTest()
        {
            _repoMock = new Mock<IAnesthesiaRecordRepository>();
            _configMock = new Mock<IConfiguration>();
           
            _service = new PdfService(
                null!, 
                null!,
                null!, 
                null!, 
                _configMock.Object,
                _repoMock.Object);
        }

        [Fact]
        public async Task GeneratePdfAsync_Should_Return_Null_When_Record_Not_Found()
        {
            _repoMock.Setup(r => r.GetByIdAsync(99))
                     .ReturnsAsync((AnesthesiaRecord?)null);

            var (html, patientId) = await _service.GeneratePdfAsync(99);

            Assert.Null(html);
            Assert.Null(patientId);
        }

        [Fact]
        public async Task GeneratePdfAsync_Should_Return_Html_And_PatientId_When_Record_Found()
        {
            var record = AnesthesiaRecord.Create(new Domain.Commands.AnesthesiaRecord.AnesthesiaRecordCommand
            {
                PatientId = "123"
            });

            _repoMock.Setup(r => r.GetByIdAsync(1))
                     .ReturnsAsync(record);
            _configMock.Setup(c => c["Pdf:ViewPath"])
                       .Returns("Views/Pdf/AnesthesiaRecord.cshtml");

            var exception = await Record.ExceptionAsync(() => _service.GeneratePdfAsync(1));

            Assert.NotNull(exception);
            _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
            _configMock.Verify(c => c["Pdf:ViewPath"], Times.Once);
        }
    }
}