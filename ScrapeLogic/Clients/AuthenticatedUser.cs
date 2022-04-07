using ScrapeLogic.Scrapers;

namespace ScrapeLogic.Clients;

public class AuthenticatedUser : Client
{
    public AuthenticatedUser(List<AbstractScraper> scrapers): base(scrapers){
        Console.WriteLine("Authenticated User created!");
    }
}