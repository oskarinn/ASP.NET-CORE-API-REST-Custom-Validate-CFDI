using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomValidateCFDIAPI.Data;
using CustomValidateCFDIAPI.Models;
using Microsoft.AspNetCore.Http;

namespace CustomValidateCFDIAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidateController : Controller
    {
        // POST
        [HttpPost]
        public ActionResult Post([FromForm]string rfc, [FromForm]IFormFile xml)
        {
            string message = "";

            XMLManager xmlManager = new XMLManager();
            CFDIModel cfdi = new CFDIModel();

            if (rfc != null && !String.IsNullOrEmpty(rfc))
            {
                if (xml.FileName != "")
                {
                    cfdi.RFC = rfc;
                    cfdi.XML = xml;

                    message = xmlManager.Validate(cfdi);                    
                }
                else
                {
                    message = "El XML es obligatorio";
                }
            }
            else
            {
                message = "El RFC es obligatorio";
            }

            return Content(message);
        }
    }
}
