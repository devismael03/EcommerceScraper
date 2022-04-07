using ScrapeLogic.Scrapers;
using ScrapeLogic.DTO;

namespace ScrapeLogic.Clients;


public abstract class Client{
    protected List<AbstractScraper> Scrapers;

    public Client(List<AbstractScraper> scrapers){
        Scrapers = scrapers;
    }

    public List<ProductDetail> Search(string keyword){
        List<ProductDetail> result = new List<ProductDetail> ();
        foreach(AbstractScraper scraper in Scrapers)
        {
            result.AddRange(scraper.Search(keyword));
        }
        return result;
    }

    /*public List<ProductDetail> Filter<T>(Func<ProductDetail,T> FilterCondition){
        return Scraper.Filter<T>(FilterCondition);
    }*/
}