
using OpenQA.Selenium;
namespace ScrapeLogic;


public abstract class AbstractScraper
{
    protected IWebDriver Driver;
    protected List<ProductDetail> Products = new List<ProductDetail>();
    public List<ProductDetail> Search(string keyword){
        CreateWebDriver();
        Navigate();
        GetProducts(keyword);
        return Products;
    }
    public abstract void CreateWebDriver();
    public abstract void Navigate();
    public abstract void GetProducts(string keyword);
    public abstract void Filter();
}
