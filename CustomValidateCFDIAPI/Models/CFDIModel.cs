using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace CustomValidateCFDIAPI.Models
{
    public class CFDIModel
    {
        public string RFC { get; set; }
        public IFormFile XML { get; set; }
    }
}
