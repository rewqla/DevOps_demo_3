using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OilShop.Services.Interfaces
{

    public class OilService : IOilService
    {
        private readonly IOilRepo _oilRepo;
        private readonly IOilTypeRepo _oilTypeRepo;
        private readonly IOilManafacturerRepo _oilManafacturerRepo;
        private readonly IOilApplyingRepo _oilApplyingRepo;
        private readonly IOilCapacityRepo _oilCapacityRepo;
        private readonly ICartItemRepo _cartItemRepo;
        private readonly IOrderDetailRepo _orderDetailRepo;
        public OilService(IOilRepo oilRepo, IOilTypeRepo oilTypeRepo, IOilCapacityRepo oilCapacityRepo, IOilApplyingRepo oilApplyingRepo, IOrderDetailRepo orderDetailRepo, ICartItemRepo cartItemRepo, IOilManafacturerRepo oilManafacturerRepo)
        {
            _oilRepo = oilRepo;
            _oilTypeRepo = oilTypeRepo;
            _oilCapacityRepo = oilCapacityRepo;
            _oilApplyingRepo = oilApplyingRepo;
            _orderDetailRepo = orderDetailRepo;
            _cartItemRepo = cartItemRepo;
            _oilManafacturerRepo = oilManafacturerRepo;
        }
        public OilViewModel GetOilVMById(long Id)
        {
            var model = _oilRepo.FindById(Id);

            return new OilViewModel
            {
                Id = model.Id,
                Image = model.Image
            };
        }

        public List<OilViewModel> GetAllOils()
        {
            var allOils = _oilRepo.GetAll();
            List<OilViewModel> query = new List<OilViewModel>();
            foreach (var item in allOils)
            {
                query.Add(new OilViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Manafacturer = _oilManafacturerRepo.FindById(item.OilManafacturerId).Country,
                    Count = item.Count,
                    IsUsed = IsOilUsed(item.Id),
                    Capacity = _oilCapacityRepo.FindById(item.OilCapacityId).Capacity,
                });
            }
            return query;
        }

        public void AddOil(OilFullInfoViewModel entity)
        {
            _oilRepo.AddOil(ConvertModel(entity), entity);
        }
        public void DeleteOil(long Id)
        {
            _oilRepo.Delete(_oilRepo.FindById(Id));
        }
        public void UpdateOil(OilFullInfoViewModel entity)
        {
            _oilRepo.UpdateOil(entity);
        }
        public Oil ConvertModel(OilFullInfoViewModel entity)
        {
            try
            {
                var edit = _oilRepo.FindById(entity.Id);
                edit.Id = entity.Id;
                edit.Name = entity.Name;
                edit.OilCapacityId = _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Capacity.ToString() == entity.Capacity).Id;
                edit.OilTypeId = _oilTypeRepo.GetAll().FirstOrDefault(x => x.Name == entity.Type).Id;
                edit.Image = entity.PhotoBase64;
                edit.Price = entity.Price;
                edit.Description = entity.Description;
                edit.OilManafacturerId = _oilManafacturerRepo.GetAll().FirstOrDefault(x => x.Country == entity.Manafacturer).Id ;
                edit.OilApplyingId = _oilApplyingRepo.GetAll().FirstOrDefault(x => x.Name == entity.Applying).Id;
                edit.Count = entity.Count;
                return edit;
            }
            catch
            {
                return new Oil
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    OilCapacityId = _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Capacity.ToString() == entity.Capacity).Id,
                    OilTypeId = _oilTypeRepo.GetAll().FirstOrDefault(x => x.Name == entity.Type).Id,
                    Image = entity.PhotoBase64,
                    OilManafacturerId = _oilManafacturerRepo.GetAll().FirstOrDefault(x => x.Country == entity.Manafacturer).Id,
                    OilApplyingId = _oilApplyingRepo.GetAll().FirstOrDefault(x => x.Name == entity.Applying).Id,
                    Price = entity.Price,
                    Description = entity.Description,
                    Count = entity.Count,
                };
            }
        }

        public OilFullInfoViewModel GetOilFullInfoById(long Id)
        {
            var model = _oilRepo.FindById(Id);
            return new OilFullInfoViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Type = _oilTypeRepo.FindById(model.OilTypeId).Name,
                Capacity = _oilCapacityRepo.FindById(model.OilCapacityId).Capacity.ToString(),
                PhotoBase64 = model.Image,
                Price = model.Price,
                Manafacturer = _oilManafacturerRepo.FindById(model.OilManafacturerId).Country,
                Applying = _oilApplyingRepo.FindById(model.OilApplyingId).Name,
                Description = model.Description,
                Count = model.Count,
                Recommndations = _oilRepo.GetRecommndationsOfOil(Id),
                Tolerances = _oilRepo.GetToleranceOfOil(Id),
                Specifications = _oilRepo.GetSpecificationsOfOil(Id),
            };
        }

        public OilViewModelList GetOils(int page, string SearchData)
        {
            OilViewModelList model = new OilViewModelList();
            int pageSize = 8;
            var query = _oilRepo.GetAll();

            if (!String.IsNullOrEmpty(SearchData))
                query = query.Where(x => x.Name.ToLower().Contains(SearchData.ToLower()) || x.Id.ToString().Contains(SearchData)
                || _oilCapacityRepo.FindById(x.OilCapacityId).Capacity.ToString().Contains(SearchData)).ToList();

            int pageN = page - 1;
            model.List = query.OrderBy(x => x.Name)
                .Skip(pageN * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var item in model.List)
            {
                item.Name += " " + _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Id == item.OilCapacityId).Capacity;
            }

            int allCount = query.Count();
            model.Page = page;
            model.MaxPage = (int)Math.Ceiling((double)allCount / pageSize);

            return model;
        }

        public IEnumerable<int> GetCapacities()
        {
            return _oilCapacityRepo.GetAll().Select(x => x.Capacity);
        }

        public IEnumerable<string> GetManafacturers()
        {
            return _oilManafacturerRepo.GetAll().Select(x => x.Country);
        }

        public IEnumerable<string> GetTypes()
        {
            return _oilTypeRepo.GetAll().Select(x => x.Name);
        }

        public IEnumerable<string> GetApplyings()
        {
            return _oilApplyingRepo.GetAll().Select(x => x.Name);
        }

        public bool IsOilExist(string OilName, string Capacity,string Manafacturer)
        {
            var oils = _oilRepo.GetAll().Where(x => x.Name == OilName);
            if (oils != null)
            {
                if (oils.FirstOrDefault(x => x.OilCapacityId == _oilCapacityRepo.GetAll().FirstOrDefault(x => x.Capacity.ToString() == Capacity).Id) != null&& oils.FirstOrDefault(x => x.OilManafacturerId == _oilManafacturerRepo.GetAll().FirstOrDefault(x => x.Country.ToString() == Manafacturer).Id)!=null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOilUsed(long Id)
        {
            if (_cartItemRepo.GetAll().FirstOrDefault(x => x.OilId == Id) != null || _orderDetailRepo.GetAll().FirstOrDefault(x => x.OilId == Id) != null)
            {
                return true;
            }
            return false;
        }
    }
}
