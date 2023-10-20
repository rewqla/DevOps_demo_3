using Microsoft.EntityFrameworkCore;
using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using System.Collections.Generic;
using System.Linq;

namespace OilShop.Repo.Interfaces
{
    public class OilRepo : GenericRepository<Oil>, IOilRepo
    {
        private ApplicationDbContext _context;

        public OilRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void AddOil(Oil model, OilFullInfoViewModel oil)
        {
            _context.Add(model);
            _context.SaveChanges();
            foreach (var cat in oil.SelectedRecommndations)
            {
                _context.Add(new RecommendationOil { OilId = model.Id, RecommendationId = cat });
                _context.SaveChanges();
            }
            foreach (var cat in oil.SelectedSpecifications)
            {
                _context.Add(new SpecificationOil { OilId = model.Id, SpecificationId = cat });
                _context.SaveChanges();
            }
            foreach (var cat in oil.SelectedTolerances)
            {
                _context.Add(new ToleranceOil { OilId = model.Id, ToleranceId = cat });
                _context.SaveChanges();
            }
        }

        public List<OilRecommendation> GetRecommndations()
        {
            return _context.OilRecommendations.ToList();
        }

        public List<OilRecommendation> GetRecommndationsOfOil(long Id)
        {
            var oils = _context.Oils.Include(x => x.RecommendationOils).ThenInclude(x => x.Recommendation);
            return oils.FirstOrDefault(x => x.Id == Id).RecommendationOils.Select(x => x.Recommendation).ToList();
        }

        public List<OilSpecification> GetSpecifications()
        {
            return _context.OilSpecifications.ToList();
        }

        public List<OilSpecification> GetSpecificationsOfOil(long Id)
        {
            var oils = _context.Oils.Include(x => x.SpecificationOils).ThenInclude(x => x.Specification);
            return oils.FirstOrDefault(x => x.Id == Id).SpecificationOils.Select(x => x.Specification).ToList();
        }

        public List<OilTolerance> GetTolerance()
        {
            return _context.OilTolerances.ToList();
        }

        public List<OilTolerance> GetToleranceOfOil(long Id)
        {
            var oils = _context.Oils.Include(x => x.ToleranceOils).ThenInclude(x => x.Tolerance);
            return oils.FirstOrDefault(x => x.Id == Id).ToleranceOils.Select(x => x.Tolerance).ToList();
        }

        public void UpdateOil(OilFullInfoViewModel oil)
        {

            var currentoil = _context.Oils.Include(p => p.RecommendationOils).Include(p => p.ToleranceOils).Include(p => p.SpecificationOils).Where(p => p.Id == oil.Id).FirstOrDefault();
            currentoil.Name = oil.Name;
            currentoil.OilCapacityId = _context.OilCapacities.AsNoTracking().ToList().FirstOrDefault(x => x.Capacity.ToString() == oil.Capacity).Id;
            currentoil.OilTypeId = _context.OilTypes.AsNoTracking().ToList().FirstOrDefault(x => x.Name == oil.Type).Id;
            currentoil.Image = oil.PhotoBase64;
            currentoil.Price = oil.Price;
            currentoil.Description = oil.Description;
            currentoil.OilManafacturerId = _context.OilManafacturers.AsNoTracking().ToList().FirstOrDefault(x => x.Country == oil.Manafacturer).Id;
            currentoil.OilApplyingId = _context.OilApplyings.AsNoTracking().ToList().FirstOrDefault(x => x.Name == oil.Applying).Id;
            currentoil.Count = oil.Count;
            if (oil.SelectedSpecifications!=null && oil.SelectedTolerances != null && oil.SelectedRecommndations != null)
            {

                currentoil.RecommendationOils = new List<RecommendationOil>();
                currentoil.ToleranceOils = new List<ToleranceOil>();
                currentoil.SpecificationOils = new List<SpecificationOil>();
                foreach (var cat in oil.SelectedRecommndations)
                {
                    currentoil.RecommendationOils.Add(new RecommendationOil { RecommendationId = cat });
                }
                foreach (var cat in oil.SelectedSpecifications)
                {
                    currentoil.SpecificationOils.Add(new SpecificationOil { SpecificationId = cat });
                }
                foreach (var cat in oil.SelectedTolerances)
                {
                    currentoil.ToleranceOils.Add(new ToleranceOil { ToleranceId = cat });
                }
            }
            _context.SaveChanges();
        }
    }
}

