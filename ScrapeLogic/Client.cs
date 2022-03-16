namespace ScrapeLogic;

public abstract class Client{
    protected AbstractScraper Scraper;

    public Client(AbstractScraper scraper){
        Scraper = scraper;
    }

    public List<ProductDetail> Search(string keyword){
        List<ProductDetail> result = Scraper.Search(keyword);
        return result;
    }

}