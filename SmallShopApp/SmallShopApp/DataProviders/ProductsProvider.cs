using SmallShopApp.Entities;
using SmallShopApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallShopApp.DataProviders
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly IRepository<ProductWeighted> _productsWeightedRepository;
        private readonly IRepository<ProductPacked> _productsPackedRepository;
        public ProductsProvider(IRepository<ProductWeighted> productsWeightedRepository, IRepository<ProductPacked> productsPackedRepository) 
        {
            _productsWeightedRepository = productsWeightedRepository;
            _productsPackedRepository = productsPackedRepository;
        }

        public float GetMinimumProductPrice()
        {
            var productsW = _productsWeightedRepository.GetAll();
            var productsP = _productsPackedRepository.GetAll();
            var priceMInW = productsW.Select(x => x.Price).Min();
            var priceMInP = productsP.Select(x => x.Price).Min();
            return Math.Min(priceMInW, priceMInP);
        }

        public List<string> GetProductNames()
        {
            List<string> names = new List<string>();
            var productsW = _productsWeightedRepository.GetAll();
            var productsP = _productsPackedRepository.GetAll();
            var colorsW = productsW.Select(x => x.Name).Distinct().ToList();
            var colorsP = productsP.Select(x => x.Name).Distinct().ToList();
            string namesAll = string.Join(";", colorsW) + ";" + string.Join(";", colorsP);
            names = namesAll.Split(';').ToList();
            return names;
        }

        public List<ProductWeighted> OrderByNameW()
        {
            var productsW = _productsWeightedRepository.GetAll();
            return productsW.OrderBy(x => x.Name).ThenBy(x => x.Price).ToList();

        }

        public List<ProductPacked> OrderByNameP()
        {
            var productsP = _productsPackedRepository.GetAll();
            return productsP.OrderBy(x => x.Name).ThenBy(x => x.Price).ToList();
        }

        public List<ProductWeighted> WhereCostLowerPW(float price)
        {
            var productsW = _productsWeightedRepository.GetAll();
            return productsW.Where(x => x.Price < price).ToList();
        }

        public List<ProductPacked> WhereCostLowerPP(float price)
        {
            var productsP = _productsPackedRepository.GetAll();
            return productsP.Where(x => x.Price < price).ToList();
        }

        public List<ProductWeighted> WhereNameSartsWIthWP(string prefix)
        {
            var productsW = _productsWeightedRepository.GetAll();
            return productsW.Where(x=> x.Name.StartsWith(prefix)).ToList();
        }

        public List<ProductPacked> WhereNameSartsWIthPP(string prefix)
        {
            var productsP = _productsPackedRepository.GetAll();
            return productsP.Where(x => x.Name.StartsWith(prefix)).ToList();
        }
    }
}
