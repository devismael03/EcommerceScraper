using ScrapeLogic.Clients;
using ScrapeLogic.DTO;
using ScrapeLogic.Scrapers;


AbstractScraper scraper = new TapAzScraper();
Client user = new AuthenticatedUser(scraper);
string keyword = Console.ReadLine();
List<ProductDetail> result = user.Search(keyword);

foreach(ProductDetail detail in result){
    Console.WriteLine($"{detail.Title} - {detail.Price} AZN");
}

List<ProductDetail> filtered = user.Filter<double>(p => p.Price);
Console.WriteLine("After filtering by price: ");

foreach(ProductDetail detail in filtered){
    Console.WriteLine($"{detail.Title} - {detail.Description} {detail.Price} AZN");
}