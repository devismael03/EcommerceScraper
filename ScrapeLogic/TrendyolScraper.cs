using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Collections.ObjectModel;

namespace ScrapeLogic;

public class TrendyolScraper : AbstractScraper
{
    public override void CreateWebDriver()
    {
        var options = new ChromeOptions();
        options.AddExcludedArgument("enable-logging"); 
        options.AddArguments("--disable-notifications");

        this.Driver = new ChromeDriver(@"C:\Users\mamed\Downloads\chromedriver_win32",options=options);
    }

    public override void Navigate()
    {
        this.Driver.Navigate().GoToUrl("https://www.trendyol.com");
        this.Driver.Manage().Cookies.AddCookie(new Cookie("countryCode","TR"));  //cookies needed to remove extra work
        this.Driver.Manage().Cookies.AddCookie(new Cookie("language","tr"));
        this.Driver.Manage().Cookies.AddCookie(new Cookie("storefrontId","1"));
        this.Driver.Manage().Cookies.AddCookie(new Cookie("hvtb","1"));

        this.Driver.Navigate().GoToUrl("https://www.trendyol.com");
        Thread.Sleep(3000);
    }

    public override void GetProducts(string keyword)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)this.Driver;

        IWebElement searchBar = this.Driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/div[2]/div/div/div[2]/div/div/div/div/input"));
        searchBar.SendKeys(keyword);
        searchBar.SendKeys(Keys.Enter);
        Thread.Sleep(2000);

        js.ExecuteScript("window.scrollBy(0,250)");
        Thread.Sleep(1000);
        IWebElement title, price, url;

        int counter = 1;
        while(counter < 11){
            try{
                title = this.Driver.FindElement(By.XPath($"/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[3]/div/div[{counter}]/div[1]/a/div[2]/div[1]/div/span[2]"));
                price = this.Driver.FindElements(By.ClassName("prc-box-dscntd"))[counter-1];
                url = this.Driver.FindElement(By.XPath($"/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[3]/div/div[{counter}]/div[1]/a"));
                this.Products.Add(new ProductDetail(title.Text,url.GetAttribute("href"),Double.Parse(price.Text.Substring(0, price.Text.IndexOf(" ")))));
            }catch(NoSuchElementException){
                title = this.Driver.FindElement(By.XPath($"/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[4]/div/div[{counter}]/div[1]/a/div[2]/div[1]/div/span[2]"));
                price = this.Driver.FindElements(By.ClassName("prc-box-dscntd"))[counter-1];
                url = this.Driver.FindElement(By.XPath($"/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[3]/div/div[{counter}]/div[1]/a"));
                this.Products.Add(new ProductDetail(title.Text,url.GetAttribute("href"),Double.Parse(price.Text)));
            }

            counter++;
        }

        StringBuilder sb = new StringBuilder();
        foreach(ProductDetail product in this.Products){
            this.Driver.Navigate().GoToUrl(product.Url);

            ReadOnlyCollection<IWebElement> descriptionList = this.Driver.FindElement(By.ClassName("detail-desc-list")).FindElements(By.TagName("li"));
            int limit = 0;
            foreach(IWebElement descriptionElement in descriptionList){
                if(limit == 5){
                    break;
                }
                sb.Append(descriptionElement.Text);
                limit++;
            }
            product.Description = sb.ToString();
            sb.Clear();
            Thread.Sleep(2000);
        }
        Thread.Sleep(5000);
    }

    

    ~TrendyolScraper(){
        this.Driver.Dispose();
    }

 
}