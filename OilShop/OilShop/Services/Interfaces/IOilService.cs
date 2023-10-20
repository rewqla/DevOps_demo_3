using OilShop.Entities;
using OilShop.Models;
using System.Collections.Generic;

namespace OilShop.Services.Implement
{
    public interface IOilService
    {
        void AddOil(OilFullInfoViewModel entity);
        void DeleteOil(long Id);
        void UpdateOil(OilFullInfoViewModel entity);
        bool IsOilExist(string OilName, string Capacity, string Manafacturer);
        bool IsOilUsed(long Id);
        OilViewModel GetOilVMById(long Id);
        OilViewModelList GetOils(int page,string SearchData);
        IEnumerable<int> GetCapacities();
        IEnumerable<string> GetTypes();
        IEnumerable<string> GetManafacturers();
        IEnumerable<string> GetApplyings();
        List<OilViewModel> GetAllOils();
        OilFullInfoViewModel GetOilFullInfoById(long Id);
        Oil ConvertModel(OilFullInfoViewModel entity);
    }

}
