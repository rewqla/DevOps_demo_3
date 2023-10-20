using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Interfaces;
using System.Collections.Generic;

namespace OilShop.Repo.Implement
{
    public interface IOilRepo: IGenericRepository<Oil>
    {
        public List<OilRecommendation> GetRecommndationsOfOil(long Id);
        public void AddOil(Oil model,OilFullInfoViewModel oil);
        public void UpdateOil(OilFullInfoViewModel oil);
        public List<OilRecommendation> GetRecommndations();
        public List<OilSpecification> GetSpecificationsOfOil(long Id);
        public List<OilSpecification> GetSpecifications();
        public List<OilTolerance> GetToleranceOfOil(long Id);
        public List<OilTolerance> GetTolerance();
    }
}
