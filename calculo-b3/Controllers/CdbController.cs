using System.Web.Http;
using calculo_b3.Models;
using calculo_b3.Services;
using Microsoft.AspNetCore.Mvc;


namespace calculo_b3.Controllers
{
    
   

    public class CdbCalculatorController : ApiController
    {
       

        [HttpPost]
        [Route("api/calculo/cdb")]
        public IHttpActionResult Calcula([FromBody] CdbRequest cdbRequest)
        {
            var cdbService = new CdbService();
            CdbResponse cdbResponse = cdbService.CalculaCDB(cdbRequest);

            if (cdbResponse.Erro)
            {
                return BadRequest(cdbResponse.MsgErro);
            }
            return Ok(cdbResponse);
        }

      
    }
}
