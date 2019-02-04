using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Realdigital.Interview.Domain.Contracts;

namespace Realmdigital_Interview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields

        //Create a readonly connection to the Domain service.  The dependency is pass in
        //through dependency injection in the controller below
        private readonly IGetWebApi _webApiService;

        #endregion

        #region Constructor

        public ProductController(IGetWebApi webApiService)
        {
            _webApiService = webApiService;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"welcome"};
        }

        //Change productId to productId to make parameter more descriptive
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            //Extract iteration procedure into separate method for higher cohesion
            var result = _webApiService.GetProductById(productId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if (result.Count > 0)
                return Ok(result[0]);
            else
                return BadRequest("No Product by that productId found");
        }

        //Reuse the same methods as for GetProductById
        [HttpGet("search/{productName}")]
        public async Task<IActionResult> GetProductsByName(string productName)
        {
            //Use existing method to build the result 
            var result = _webApiService.GetProductByName(productName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (result.Count > 0)
                return Ok(result[0]);
            else
                return BadRequest("No Product by that productId found");
        }

        #endregion
    }
}