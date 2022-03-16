using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using System.Collections.ObjectModel;

namespace ScrapeLogic;

public class TapAzScraper : AbstractScraper
{
    public override void CreateWebDriver()
    {
        this.Driver = new ChromeDriver(@"C:\Users\mamed\Downloads\chromedriver_win32",new ChromeOptions());
    }

    public override void Navigate()
    {
        this.Driver.Navigate().GoToUrl("https://tap.az");
        this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
    }

    public override void GetProducts(string keyword)
    {
        IWebElement searchBar = this.Driver.FindElement(By.XPath("//*[@id='q_keywords']"));
        searchBar.SendKeys(keyword);
        searchBar.SendKeys(Keys.Enter);
        IWebElement title, price, url;

        int counter = 1;
        while(counter < 11){
            try{
                title = this.Driver.FindElement(By.XPath($"/html/body/div[5]/div/div/div[3]/div[2]/div[{counter}]/a/div[3]"));
                price = this.Driver.FindElement(By.XPath($"/html/body/div[5]/div/div/div[3]/div[2]/div[{counter}]/a/div[2]/div/span[1]"));
                url = this.Driver.FindElement(By.XPath($"/html/body/div[5]/div/div/div[3]/div[2]/div[{counter}]/a"));
                this.Products.Add(new ProductDetail(title.Text,url.GetAttribute("href"),Double.Parse(price.Text.Replace(" ",""))));
            }catch(NoSuchElementException){
                break;
            }

            counter++;
        }

        StringBuilder sb = new StringBuilder();
        foreach(ProductDetail product in this.Products){
            this.Driver.Navigate().GoToUrl(product.Url);

            ReadOnlyCollection<IWebElement> descriptionList = this.Driver.FindElement(By.ClassName("lot-text")).FindElements(By.TagName("p"));
            foreach(IWebElement descriptionElement in descriptionList){
                sb.Append(descriptionElement.Text);
            }
            product.Description = sb.ToString();
            sb.Clear();
            Thread.Sleep(2000);
        }
        Thread.Sleep(5000);
    }

    
   

    ~TapAzScraper(){
        this.Driver.Dispose();
    }

 
}