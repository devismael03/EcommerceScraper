
using OpenQA.Selenium;
using System.Linq;
namespace ScrapeLogic;


public abstract class AbstractScraper
{
    protected IWebDriver Driver;
    protected List<ProductDetail> Products = new List<ProductDetail>();
    public List<ProductDetail> Search(string keyword){
        this.Products.Clear();
        CreateWebDriver();
        Navigate();
        GetProducts(keyword);
        GetDescriptions();
        return Products;
    }
    public abstract void CreateWebDriver();
    public abstract void Navigate();
    public abstract void GetProducts(string keyword);
    public abstract  void GetDescriptions();
    public List<ProductDetail> Filter<T>(Func<ProductDetail,T> FilterCondition){
        return this.Products.OrderBy(FilterCondition).ToList();
    }
}
