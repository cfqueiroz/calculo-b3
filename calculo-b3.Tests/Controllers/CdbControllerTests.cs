using Xunit;
using System.Web.Http;
using System.Web.Http.Results;
using calculo_b3.Controllers;
using calculo_b3.Models;
using calculo_b3.Services;

namespace calculo_b3.Tests.Controllers
{
    public class CdbControllerTests
    {
        
        private class FakeCdbService : ICdbService
        {
            private readonly CdbResponse _response;

            public FakeCdbService(CdbResponse response)
            {
                _response = response;
            }

            public CdbResponse CalculaCDB(CdbRequest cdbRequest)
            {
                return _response;
            }
        }

        [Fact]
        public void Calcula_ShouldReturnOk_WhenRequestIsValid()
        {
            
            var validRequest = new CdbRequest { ValorInicial = 1000, Meses = 12 };
            var fakeResponse = new CdbResponse { Erro = false, ValorLiquido = 1100 }; 
            var fakeService = new FakeCdbService(fakeResponse);
            var controller = new CdbController(fakeService);

            
            IHttpActionResult actionResult = controller.Calcula(validRequest);
            var contentResult = actionResult as OkNegotiatedContentResult<CdbResponse>;

            
            Assert.NotNull(contentResult);
            Assert.NotNull(contentResult.Content);
            Assert.False(contentResult.Content.Erro); 
        }

        [Fact]
        public void Calcula_ShouldReturnBadRequest_WhenValorInicialIsInvalid()
        {
            
            var invalidRequest = new CdbRequest { ValorInicial = 0, Meses = 12 };
            var fakeResponse = new CdbResponse { Erro = true, MsgErro = "Valor inicial deve ser positivo." };
            var fakeService = new FakeCdbService(fakeResponse);
            var controller = new CdbController(fakeService);

            
            IHttpActionResult actionResult = controller.Calcula(invalidRequest);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            
            Assert.NotNull(contentResult);
            Assert.Equal("Valor inicial deve ser positivo.", contentResult.Message);
        }

        [Fact]
        public void Calcula_ShouldReturnBadRequest_WhenMesesIsInvalid()
        {
            
            var invalidRequest = new CdbRequest { ValorInicial = 1000, Meses = 1 };
            var fakeResponse = new CdbResponse { Erro = true, MsgErro = "Prazo deve ser maior que 1." };
            var fakeService = new FakeCdbService(fakeResponse);
            var controller = new CdbController(fakeService);

            
            IHttpActionResult actionResult = controller.Calcula(invalidRequest);
            var contentResult = actionResult as BadRequestErrorMessageResult;

            
            Assert.NotNull(contentResult);
            Assert.Equal("Prazo deve ser maior que 1.", contentResult.Message);
        }
    }
}
