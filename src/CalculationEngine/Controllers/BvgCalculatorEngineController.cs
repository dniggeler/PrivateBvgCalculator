using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Implementation;
using Microsoft.AspNet.Mvc;

namespace CalculationEngine.Controllers
{
    [Route("api/bvg")]
    public class BvgCalculatorEngineController : Controller
    {
        // POST api/bvg
        [HttpPost, Route("details")]
        public BvgCalculationInput GetDetails([FromBody]BvgCalculationInput input)
        {
            var engine = new BvgCalculator(new BvgConstants());

            var result = engine.CalculateAsync(new BvgPlan(), input);

            return result;
        }
    }
}
