using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenWordTest.Lib;

namespace OpenWordTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;

        public DocumentsController(ILogger<DocumentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocument()
        {
            byte[] binaryContent;

            using (var stream = new MemoryStream())
            {
                // write excel file to stream
                await Task.Run(() => new DocumentBuilder().CreateExcelFile(stream));

                // dump stream content to byte array
                binaryContent = stream.ToArray();
            }

            // return the bytes as openxml file
            return File(binaryContent, "application/vnd.openxmlformats", "text.xlsx");
        }
    }
}
