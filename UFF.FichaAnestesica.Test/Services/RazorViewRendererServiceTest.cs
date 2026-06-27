using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using UFF.FichaAnestesica.Infra.Services;

namespace UFF.FichaAnestesica.Test.Services
{
    public class RazorViewRendererServiceTest
    {
        private readonly Mock<IRazorViewEngine> _viewEngineMock;
        private readonly Mock<ITempDataProvider> _tempDataMock;
        private readonly Mock<IServiceProvider> _serviceProviderMock;
        private readonly RazorViewRendererService _service;

        public RazorViewRendererServiceTest()
        {
            _viewEngineMock = new Mock<IRazorViewEngine>();
            _tempDataMock = new Mock<ITempDataProvider>();
            _serviceProviderMock = new Mock<IServiceProvider>();
            _service = new RazorViewRendererService(
                _viewEngineMock.Object,
                _tempDataMock.Object,
                _serviceProviderMock.Object);
        }

        [Fact]
        public async Task RenderAsync_Should_Throw_When_View_Not_Found()
        {
            _viewEngineMock
                .Setup(v => v.GetView(null, "Views/Teste.cshtml", true))
                .Returns(ViewEngineResult.NotFound("Views/Teste.cshtml", new[] { "local1", "local2" }));

            await Assert.ThrowsAsync<Exception>(
                () => _service.RenderAsync("Views/Teste.cshtml", new { Nome = "João" }));
        }

        [Fact]
        public async Task RenderAsync_Should_Return_Html_When_View_Found()
        {
            var viewMock = new Mock<IView>();
            viewMock.Setup(v => v.RenderAsync(It.IsAny<ViewContext>()))
                    .Callback((ViewContext context) =>
                    {
                        context.Writer.Write("<p>Olá João</p>");
                    })
                    .Returns(Task.CompletedTask);

            _viewEngineMock
                .Setup(v => v.GetView(null, "Views/Sucesso.cshtml", true))
                .Returns(ViewEngineResult.Found("Views/Sucesso.cshtml", viewMock.Object));

            var html = await _service.RenderAsync("Views/Sucesso.cshtml", new { Nome = "João" });

            Assert.Equal("<p>Olá João</p>", html);
        }
    }
}