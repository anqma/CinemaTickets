using CinemaTicketsAdminApp.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CinemaTicketsAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public OrderController() {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7119/api/Admin/GetAllActiveOrders";
            
            HttpResponseMessage response= client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<List<Order>>().Result;
            return View(data);
        }

        public IActionResult Details(int OrderId)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7119/api/Admin/GetDetailsForOrder";

            var model = new
            {
                Id = OrderId,
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL,content).Result;
            
            var data = response.Content.ReadAsAsync<Order>().Result;

            return View(data);
        }

        public FileResult SavePdf(int id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:7119/api/Admin/GetDetailsForOrder";

            var model = new
            {
                Id = id,
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = client.PostAsync(URL, content).Result;

            var result = responseMessage.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "InvoiceOrders.docx");

            var document = DocumentModel.Load(templatePath);


            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());

            document.Content.Replace("{{UserName}}", result.OrderedBy.UserName);

            StringBuilder sb = new StringBuilder();
            int totalPrice = 0;

            foreach (var item in result.CinemaTickets)
            {
                totalPrice += item.Quantity * item.CinemaTicket.TicketPrice;
                sb.AppendLine(item.CinemaTicket.MovieTitle + ", quantity: " + item.Quantity + " and price: " + item.CinemaTicket.TicketPrice + "$");
            }
            document.Content.Replace("{{CinemaTicketsList}}", sb.ToString());

            document.Content.Replace("{{TotalPrice}}", totalPrice.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
