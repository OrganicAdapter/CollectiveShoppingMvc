using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CollectiveShoppingMvc.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShoppingListService" in both code and config file together.
    [ServiceContract]
    public interface IShoppingListService
    {
        [OperationContract]
        [WebGet(UriTemplate = "SyncShops?userShops={jsonUserShops}&culture={culture}")]
        string SyncShops(string jsonUserShops, string culture);

        [OperationContract]
        [WebGet(UriTemplate = "SyncProducts?userProducts={jsonUserProducts}&shopId={shopId}")]
        string SyncProducts(string jsonUserProducts, int shopId);

        [OperationContract]
        [WebGet(UriTemplate = "SyncCategories?userCategories={jsonUserCategories}&shopId={shopId}")]
        string SyncCategories(string jsonUserCategories, int shopId);

        [OperationContract]
        [WebGet(UriTemplate = "SyncShop?userShop={jsonUserShop}")]
        int SyncShop(string jsonUserShop);
    }
}
