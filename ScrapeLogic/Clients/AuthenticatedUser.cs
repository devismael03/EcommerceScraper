using ScrapeLogic.Scrapers;

namespace ScrapeLogic.Clients;

public class AuthenticatedUser : Client
{
    public AuthenticatedUser(AbstractScraper scraper): base(scraper){
        Console.WriteLine("Authenticated User created!");
    }
}