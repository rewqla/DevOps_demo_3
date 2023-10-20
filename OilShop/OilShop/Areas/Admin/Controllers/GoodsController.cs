using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OilShop.Controllers;
using OilShop.Helpers;
using OilShop.Models;
using OilShop.Repo.Implement;
using OilShop.Services.Implement;
using System.Linq;

namespace OilShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GoodsController : Controller
    {
        private readonly IOilService _oilService;
        private readonly IOilRepo _oilRepo;
        private readonly IWebHostEnvironment _env;

        public GoodsController(IOilService oilService, IWebHostEnvironment env, IOilRepo oilRepo)
        {
            _oilService = oilService;
            _env = env;
            _oilRepo = oilRepo;
        }

        public IActionResult Index(string SearchData)
        {
            var model = _oilService.GetAllOils();
            if (SearchData != null)
                model = model.Where(x => x.Name.ToLower().Contains(SearchData.ToLower()) || x.Id.ToString().Contains(SearchData) || x.Capacity.ToString().Contains(SearchData)).ToList();
            model = model.OrderBy(x => x.Id).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Recommndations = new SelectList(_oilRepo.GetRecommndations(), "Id", "Name");
            ViewBag.Tolerances = new SelectList(_oilRepo.GetTolerance(), "Id", "Name");
            ViewBag.Specifications = new SelectList(_oilRepo.GetSpecifications(), "Id", "Name");
            FillModels();

            var oil = new OilFullInfoViewModel();
            return View(oil);
        }

        [HttpPost]
        public IActionResult Create(OilFullInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_oilService.IsOilExist(model.Name, model.Capacity, model.Manafacturer))
                {
                    string base64 = model.PhotoBase64;
                    model.PhotoBase64 = base64.urlCreator(_env, "");
                    _oilService.AddOil(model);

                    TempData["message"] = "Мастило " + model.Name + " " + model.Capacity + "л успішно створене";
                    return Redirect("/admin/goods");
                }
                else
                {
                    ModelState.AddModelError("Name", "Мастило " + model.Name + " " + model.Capacity + "л " + model.Manafacturer + " уже існує");
                    FillModels();
                    return View(model);
                }
            }
            FillModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            if (!_oilService.IsOilUsed(Id))
            {
                var imgPath = _env.ContentRootPath + "\\wwwroot\\" + _oilService.GetOilVMById(Id).Image;
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
                _oilService.DeleteOil(Id);

                TempData["message"] = "Мастило №" + Id + " успішно видалене";
                return Redirect("/admin/goods");
            }
            return new ErrorController().Iternal();
        }

        [HttpGet]
        public IActionResult Update(int Id)
        {
            ViewBag.Recommndations = new SelectList(_oilRepo.GetRecommndations(), "Id", "Name");
            ViewBag.Tolerances = new SelectList(_oilRepo.GetTolerance(), "Id", "Name");
            ViewBag.Specifications = new SelectList(_oilRepo.GetSpecifications(), "Id", "Name");
            FillModels();

            var model = _oilService.GetOilFullInfoById(Id);
            model.SelectedRecommndations = _oilRepo.GetRecommndationsOfOil(Id).Select(x => x.Id).ToList();
            model.SelectedTolerances = _oilRepo.GetToleranceOfOil(Id).Select(x => x.Id).ToList();
            model.SelectedSpecifications = _oilRepo.GetSpecificationsOfOil(Id).Select(x => x.Id).ToList();
            ViewBag.IsUsed = _oilService.IsOilUsed(model.Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(OilFullInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_oilService.IsOilUsed(model.Id))
                {
                    if (!model.PhotoBase64.Contains("Uploads") && !model.PhotoBase64.Contains("https"))
                    {
                        string base64 = model.PhotoBase64;
                        model.PhotoBase64 = base64.urlCreator(_env, _oilService.GetOilVMById(model.Id).Image);
                    }
                    _oilService.UpdateOil(model);
                    return Redirect("/admin/goods");
                }
                else
                {
                    var newModel = _oilService.GetOilFullInfoById(model.Id);
                    newModel.Count = model.Count;
                    newModel.Price = model.Price;
                    _oilService.UpdateOil(newModel);

                    TempData["message"] = "Мастило №" + model.Id + " успішно оновлене";
                    return Redirect("/admin/goods");
                }
            }
            FillModels();
            return View(model);
        }

        void FillModels()
        {
            ViewBag.Types = _oilService.GetTypes();
            ViewBag.Capacities = _oilService.GetCapacities().OrderBy(x => x);
            ViewBag.Applyings = _oilService.GetApplyings();
            ViewBag.Manafacturers = _oilService.GetManafacturers();
        }
    }
}
