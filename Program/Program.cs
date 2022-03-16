using ScrapeLogic;


AbstractScraper scraper = new TapAzScraper();
Client user = new AuthenticatedUser(scraper);
List<ProductDetail> result = user.Search("divan");

foreach(ProductDetail detail in result){
    Console.WriteLine($"{detail.Title} - {detail.Price}");
}

List<ProductDetail> filtered = user.Filter<double>(p => p.Price);
Console.WriteLine("After filtering by price: ");

foreach(ProductDetail detail in filtered){
    Console.WriteLine($"{detail.Title} - {detail.Price}");
}