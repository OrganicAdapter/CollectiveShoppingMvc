using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CollectiveShoppingMvc.Models
{
    public class DataProviderService
    {
        private static ShoppingModelEntities Context = new ShoppingModelEntities();


        public async static void SyncShops(List<Shop> userShops, string culture)
        {
            foreach (var userShop in userShops)
            {
                if (!userShop.IsEnabled)
                {
                    if (Context.ShopSet.Where((x) => x.Culture.Equals(culture) && x.Name.Equals(userShop.Name)).FirstOrDefault() == null)
                        Context.ShopSet.Add(userShop);
                }
                else if (Context.ShopSet.Where((x) => x.Culture.Equals(culture)).Where((x) => x.ShopId == userShop.ShopId).FirstOrDefault() == null)
                {
                    userShop.IsEnabled = false;
                    Context.ShopSet.Add(userShop);
                }
            }

            var itemsCount = await Context.SaveChangesAsync();
        }

        public async static void SyncProducts(List<Product> userProducts, int shopId)
        {
            foreach (var userProduct in userProducts)
            {
                if (!userProduct.IsEnabled)
                {
                    if (Context.ProductSet.Where((x) => x.ShopId.Equals(shopId) && x.Name.Equals(userProduct.Name)).FirstOrDefault() == null)
                        Context.ProductSet.Add(userProduct);
                }
                else if (Context.ProductSet.Where((x) => x.ShopId.Equals(shopId)).Where((x) => x.ProductId == userProduct.ProductId).FirstOrDefault() == null)
                {
                    userProduct.IsEnabled = false;
                    Context.ProductSet.Add(userProduct);
                }
                else if (Context.ProductSet.Where((x) => x.ProductId == userProduct.ProductId).FirstOrDefault() != null)
                {
                    var akt = Context.ProductSet.Where((x) => x.ProductId == userProduct.ProductId).FirstOrDefault();

                    if (DateTime.Parse(akt.Date) < DateTime.Parse(userProduct.Date) && !userProduct.Price.Equals("0"))
                    {
                        akt.Price = userProduct.Price;
                        akt.UnitQuantity = userProduct.UnitQuantity;
                        akt.Unit = userProduct.Unit;
                        akt.Date = DateTime.Now.ToString();
                    }
                }
            }

            var itemsCount = await Context.SaveChangesAsync();
        }

        //public async static void SyncCategories(List<Category> userCategories, int shopId)
        //{
        //    foreach (var userCategory in userCategories)
        //    {
        //        if (Context.CategorySet.Where((x) => x.ShopId == shopId).Where((x) => x.Name.Equals(userCategory.Name)).FirstOrDefault() == null)
        //        {
        //            userCategory.IsEnabled = false;
        //            Context.CategorySet.Add(userCategory);
        //        }
        //    }

        //    var itemsCount = await Context.SaveChangesAsync();
        //}

        public async static void SyncShop(Shop userShop)
        {
            if (Context.ShopSet.Where((x) => x.Culture == userShop.Culture).Where((x) => x.Name.Equals(userShop.Name)).FirstOrDefault() == null)
                Context.ShopSet.Add(userShop);

            var itemsCount = await Context.SaveChangesAsync();
        }
    }
}