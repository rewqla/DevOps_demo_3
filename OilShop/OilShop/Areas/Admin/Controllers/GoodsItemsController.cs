using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OilShop.Entities;
using OilShop.Models;
using OilShop.Repo.Implement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OilShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GoodsItemsController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IOilApplyingRepo _oilApplyingRepo;
        private readonly IOilManafacturerRepo _oilManafacturerRepo;
        private readonly IOilCapacityRepo _oilCapacityRepo;
        private readonly IOilTypeRepo _oilTypeRepo;
        private readonly IOilRepo _oilRepo;
        private readonly IOilToleranceRepo _oilToleranceRepo;
        private readonly IOilRecommendationRepo _oilRecommendationRepo;
        private readonly IOilSpecificationRepo _oilSpecificationRepo;

        public GoodsItemsController(IOilApplyingRepo oilApplyingRepo, IOilManafacturerRepo oilManafacturerRepo, IOilCapacityRepo oilCapacityRepo
            , IOilTypeRepo oilTypeRepo, IOilToleranceRepo oilToleranceRepo, IOilRecommendationRepo oilRecommendationRepo, IOilSpecificationRepo oilSpecificationRepo, IOilRepo oilRepo, ApplicationDbContext context)
        {
            _oilApplyingRepo = oilApplyingRepo;
            _oilManafacturerRepo = oilManafacturerRepo;
            _oilCapacityRepo = oilCapacityRepo;
            _oilTypeRepo = oilTypeRepo;
            _oilToleranceRepo = oilToleranceRepo;
            _oilRecommendationRepo = oilRecommendationRepo;
            _oilSpecificationRepo = oilSpecificationRepo;
            _oilRepo = oilRepo;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            FillItems();
            FillItemsValues();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            FillItems();
            return View();
        }

        [HttpPost]
        public IActionResult createManafacturer(OilItemCreate model)
        {
            if (model.Data!=null)
            {

                if (_oilManafacturerRepo.GetAll().FirstOrDefault(x => x.Country == model.Data) == null)
                {
                    _oilManafacturerRepo.Add(new OilManafacturer() { Country = model.Data });

                    TempData["message"] = "Виробник " + model.Data + " успішно створений";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Виробник " + model.Data + " уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public IActionResult createRecomndation(OilItemCreate model)
        {
            if (model.Data != null)
            {
                if (_oilRecommendationRepo.GetAll().FirstOrDefault(x => x.Name == model.Data) == null)
                {
                    _oilRecommendationRepo.Add(new OilRecommendation() { Name = model.Data });

                    TempData["message"] = "Рекомендація " + model.Data + " успішно створена";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Рекомендація " + model.Data + " уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public IActionResult createSpecification(OilItemCreate model)
        {
            if (model.Data != null)
            {
                if (_oilSpecificationRepo.GetAll().FirstOrDefault(x => x.Name == model.Data) == null)
                {
                    _oilSpecificationRepo.Add(new OilSpecification() { Name = model.Data });

                    TempData["message"] = "Специфікація " + model.Data + " успішно створена";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Специфікація " + model.Data + " уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public IActionResult createType(OilItemCreate model)
        {
            if (model.Data != null)
            {
                if (_oilTypeRepo.GetAll().FirstOrDefault(x => x.Name == model.Data) == null)
                {
                    _oilTypeRepo.Add(new OilType() { Name = model.Data });

                    TempData["message"] = "Тип " + model.Data + " успішно створений";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Тип " + model.Data + " уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public IActionResult createApplying(OilItemCreate model)
        {
            if (model.Data != null)
            {
                if (_oilApplyingRepo.GetAll().FirstOrDefault(x => x.Name == model.Data) == null)
                {
                    _oilApplyingRepo.Add(new OilApplying() { Name = model.Data });

                    TempData["message"] = "Застосвання " + model.Data + " успішно створене";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Застосвання " + model.Data + " уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public IActionResult createCapacity(OilItemCreate model)
        {
            if (model.Data != null)
            {
                if (_oilCapacityRepo.GetAll().FirstOrDefault(x => x.Capacity.ToString() == model.Data) == null)
                {
                    _oilCapacityRepo.Add(new OilCapacity() { Capacity = Convert.ToInt32(model.Data) });

                    TempData["message"] = "Об'єм " + model.Data + "л успішно створений";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Об'єм " + model.Data + "л уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public IActionResult createTolerance(OilItemCreate model)
        {
            if (model.Data != null)
            {
                if (_oilToleranceRepo.GetAll().FirstOrDefault(x => x.Name == model.Data) == null)
                {
                    _oilToleranceRepo.Add(new OilTolerance() { Name = model.Data });

                    TempData["message"] = "Допуск " + model.Data + " успішно створений";
                    return Redirect("/admin/goodsitems/");
                }
                else
                {
                    FillItems();

                    TempData["message"] = "Допуск  " + model.Data + " уже існує";
                    return View("Create");
                }
            }
            else
            {
                FillItems();

                TempData["message"] = "Поле має бутим не пустим!";
                return View("Create");
            }
        }

        [HttpPost]
        public ActionResult DeleteType(long Id)
        {
            if (_context.Oils.FirstOrDefault(x => x.OilTypeId == Id) == null)
            {
                _oilTypeRepo.Delete(_oilTypeRepo.FindById(Id));

                TempData["message"] = "Тип №" + Id + " успішно видалений";
                return Redirect("/admin/goodsitems/");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Тип №" + Id + " неможливо видалити, оскільки він вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }

        [HttpPost]
        public ActionResult DeleteTolerance(long Id)
        {
            if (_context.ToleranceOils.FirstOrDefault(x => x.ToleranceId == Id) == null)
            {
                _oilToleranceRepo.Delete(_oilToleranceRepo.FindById(Id));

                TempData["message"] = "Допуск №" + Id + " успішно видалений";
                return Redirect("/admin/goodsitems");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Допуск №" + Id + " неможливо видалити, оскільки він вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }

        [HttpPost]
        public ActionResult DeleteCapacity(long Id)
        {
            if (_context.Oils.FirstOrDefault(x => x.OilCapacityId == Id) == null)
            {
                _oilCapacityRepo.Delete(_oilCapacityRepo.FindById(Id));

                TempData["message"] = "Об'єм №" + Id + " успішно видалений";
                return Redirect("/admin/goodsitems");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Об'єм №" + Id + " неможливо видалити, оскільки він вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }

        [HttpPost]
        public ActionResult DeleteApplying(long Id)
        {
            if (_context.Oils.FirstOrDefault(x => x.OilApplyingId == Id) == null)
            {
                _oilApplyingRepo.Delete(_oilApplyingRepo.FindById(Id));
                TempData["message"] = "Застосування №" + Id + " успішно видалену";
                return Redirect("/admin/goodsitems");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Застосування №" + Id + " неможливо видалити, оскільки воно вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }

        [HttpPost]
        public ActionResult DeleteManafacturer(long Id)
        {
            if (_context.Oils.FirstOrDefault(x => x.OilManafacturerId == Id) == null)
            {
                _oilManafacturerRepo.Delete(_oilManafacturerRepo.FindById(Id));

                TempData["message"] = "Виробник №" + Id + " успішно видалений";
                return Redirect("/admin/goodsitems");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Виробник №" + Id + " неможливо видалити, оскільки він вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }

        [HttpPost]
        public ActionResult DeleteRecommendation(long Id)
        {
            if (_context.RecommendationOils.FirstOrDefault(x => x.RecommendationId == Id) == null)
            {
                _oilRecommendationRepo.Delete(_oilRecommendationRepo.FindById(Id));

                TempData["message"] = "Рекомендація №" + Id + " успішно видалена";
                return Redirect("/admin/goodsitems");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Рекомендація №" + Id + " неможливо видалити, оскільки вона вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }

        [HttpPost]
        public ActionResult DeleteSpecification(long Id)
        {
            if (_context.SpecificationOils.FirstOrDefault(x => x.SpecificationId == Id) == null)
            {
                _oilSpecificationRepo.Delete(_oilSpecificationRepo.FindById(Id));

                TempData["message"] = "Специфікація №" + Id + " успішно видалений";
                return Redirect("/admin/goodsitems");
            }
            else
            {
                FillItems();
                FillItemsValues();

                TempData["message"] = "Специфікація №" + Id + " неможливо видалити, оскільки вона вжe застосовується";
                return Redirect("/admin/goodsitems/");
            }
        }
        public void FillItems()
        {
            ViewBag.Items = new List<string>()
            {
                "Виробник","Допуск","Застосування","Об'єм",  "Рекомендація", "Специфікація", "Тип",
            };
        }

        public void FillItemsValues()
        {
            ViewBag.Manafacturers = _oilManafacturerRepo.GetAll();
            ViewBag.Specifications = _oilSpecificationRepo.GetAll();
            ViewBag.Tolerances = _oilToleranceRepo.GetAll();
            ViewBag.Recommendations = _oilRecommendationRepo.GetAll();
            ViewBag.Capacities = _oilCapacityRepo.GetAll();
            ViewBag.Applyings = _oilApplyingRepo.GetAll();
            ViewBag.Types = _oilTypeRepo.GetAll();
        }
    }
}
