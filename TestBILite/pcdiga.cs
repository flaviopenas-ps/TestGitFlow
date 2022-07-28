using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBILite
{
    public class PCDiga
    {
        Dictionary<string, double> productsData = new Dictionary<string, double>();

        DriverExtras webDriver;

        public PCDiga(DriverExtras webDriver)
        {
            this.webDriver = webDriver; 
                //var task = SendDiscordMsg("@everyone " + name + " com desconto na Worten: https://www.worten.pt" + product.GetAttribute("data-category"));
                //task.Wait();

            verPromo("https://www.worten.pt/gaming/xbox/jogos");
        }

        async Task SendDiscordMsg(string msg)
        {
            /*var responseString = await ""
              .PostUrlEncodedAsync(new { content = msg }).ReceiveString();*/
        }

        void verPromo(string link)
        {
            webDriver.NavigateTo(link + "?seller_name=Worten");
            Thread.Sleep(1000);
            try
            {
                var popups = webDriver.FindElements(By.ClassName("w-cookies-popup__footer"));

                if (popups.Count > 0)
                    popups[0].FindElement(By.ClassName("w-button-primary")).Click();
            }
            catch { }

            int nextAvaiable = 1;

            do
            {
                Thread.Sleep(3000);
                var elementProducts = webDriver.FindElement(By.Id("products-list-block"));
                var products = elementProducts.FindElements(By.ClassName("w-product"));

                foreach (var product in products)
                {
                    double price = Convert.ToDouble(product.FindElement(By.ClassName("w-product-price__main")).Text);
                    string name = product.FindElement(By.ClassName("w-product__title")).Text + " " + product.GetAttribute("data-id");

                    try
                    {
                        productsData.TryGetValue(name, out double value1);
                        if ((price < (value1 / 5) * 4) && (price > 0))
                        {
                            var task = SendDiscordMsg("@everyone " + name + " desceu " + (100 - ((price * 100) / value1)).ToString("#.##") + "% relativamente ao preço anterior, preço actual na Worten " + price + "€\n https://www.worten.pt" + product.GetAttribute("data-category"));
                            task.Wait();
                        }
                    }
                    catch { }
                    productsData[name] = price;
                }
                var next = webDriver.FindElements(By.ClassName("pagination-next"));

                if (!next[0].GetAttribute("class").Contains("disabled"))
                    next[0].Click();
                else
                    nextAvaiable = 0;
            } while (nextAvaiable == 1);
        }

    }
}