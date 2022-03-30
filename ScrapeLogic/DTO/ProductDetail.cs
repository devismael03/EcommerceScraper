namespace ScrapeLogic.DTO;

public abstract class ProductDetail{
    public ProductDetail(string title, string url, double price)
    {
        Title = title;
        Url = url;
        Price = price;
    }

    public string Title{get; set;}
    public string Url{ get; set;}
    public string Description{get; set;}
    public double Price {get;set;}
}