using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallShopApp.Entities;

namespace SmallShopApp.DataProviders
{
    public interface IProductsProvider
    {
        //get
        List<string> GetProductNames();
        float GetMinimumProductPrice();
        //orderby
        List<ProductWeighted> OrderByNameW();
        List<ProductPacked> OrderByNameP();
        //where
        List<ProductWeighted> WhereCostLowerPW(float price);
        List<ProductPacked> WhereCostLowerPP(float price);
        List<ProductWeighted> WhereNameSartsWIthWP(string name);
        List<ProductPacked> WhereNameSartsWIthPP(string name);
    }
}
