using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScrapeLogic.DTO;
using ScrapeLogic.Clients;
using ScrapeLogic.Scrapers;

namespace ScraperWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<ProductDetail> Products;
        public Client Client { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Products = new List<ProductDetail>();
            Client = new AuthenticatedUser(new List<AbstractScraper> { new TapAzScraper(), new TrendyolScraper() });
        }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                this.Products = Client.Search(SearchString);
            }
        }
    }
}