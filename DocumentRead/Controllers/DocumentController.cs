using DocumentService;
using Microsoft.AspNetCore.Mvc;

namespace DocumentRead.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        IReadService _service;
        public DocumentController(IReadService service)
        {
            _service = service;
        }

        [HttpGet(Name = "DocumentRead/{clientID}/{documentID}/{actionTypeID}")]
        public async Task<IActionResult> Get(int clientID, int documentID, int actionTypeID)
        {
            using (var client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("" +
                "https://www.borakasmer.com/wp-content/uploads/2022/09/istockphoto-898902746-612x612-1.jpeg"))
            {
                try
                {
                    var count = await _service.IncrementDocumentOpenedCountAsync(clientID, documentID, actionTypeID);
                }
                catch(Exception ex)
                {                  
                    Console.WriteLine(ex.Message);
                }
                byte[] fileContents = await response.Content.ReadAsByteArrayAsync();
                return File(fileContents, "image/jpeg");
            }
        }
    }
}