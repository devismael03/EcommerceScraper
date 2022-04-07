namespace ScrapeLogic.DTO;

public abstract class ProductDetail{
    public ProductDetail(string title, string url, double price, string source, string currency)
    {
        Title = title;
        Url = url;
        Price = price;
        Source = source;
        Currency = currency;
    }

    public string Title{get; set;}
    public string Url{ get; set;}
    public string Description{get; set;}
    public double Price {get;set;}
    public string Source { get; set; }
    public string Currency { get; set;} 
}