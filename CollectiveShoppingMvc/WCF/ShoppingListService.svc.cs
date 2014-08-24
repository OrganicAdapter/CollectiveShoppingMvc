using CollectiveShoppingMvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CollectiveShoppingMvc.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShoppingListService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ShoppingListService.svc or ShoppingListService.svc.cs at the Solution Explorer and start debugging.
    public class ShoppingListService : IShoppingListService
    {
        public ShoppingModelEntities Context { get; set; }


        public ShoppingListService()
        {
            Context = new ShoppingModelEntities();
        }


        public string SyncShops(string jsonUserShops, string culture)
        {
            var userShops = JsonConvert.DeserializeObject<List<Shop>>(jsonUserShops);

            DataProviderService.SyncShops(userShops, culture);
            return JsonConvert.SerializeObject(Context.ShopSet.Where((x) => x.Culture.Equals(culture) && x.IsEnabled));
        }

        public string SyncProducts(string jsonUserProducts, int shopId)
        {
            var userProducts = JsonConvert.DeserializeObject<List<Product>>(jsonUserProducts);

            DataProviderService.SyncProducts(userProducts, shopId);
            return JsonConvert.SerializeObject(Context.ProductSet.Where((x) => x.ShopId.Equals(shopId) && x.IsEnabled));
        }

        public string SyncCategories(string jsonUserCategories, int shopId)
        {
            var userCategories = JsonConvert.DeserializeObject<List<Category>>(jsonUserCategories);

            //DataProviderService.SyncCategories(userCategories, shopId);
            return JsonConvert.SerializeObject(Context.CategorySet.Where((x) => x.ShopId.Equals(shopId)));
        }

        public int SyncShop(string jsonUserShop)
        {
            var userShop = JsonConvert.DeserializeObject<Shop>(jsonUserShop);
            DataProviderService.SyncShop(userShop);

            return Context.ShopSet.Where((x) => x.Culture == userShop.Culture).Where((x) => x.Name.Equals(userShop.Name)).FirstOrDefault().ShopId;
        }
    }
}
