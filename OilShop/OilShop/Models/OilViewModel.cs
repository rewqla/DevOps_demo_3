using OilShop.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OilShop.Models
{
    public class OilViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Manafacturer { get; set; }
        public float Price { get; set; }
        public bool IsUsed { get; set; }
        public int Count { get; set; }
        public int Capacity { get; set; }
        public string Image { get; set; }
    }

    public class OilViewModelList
    {
        public int Page { get; set; }
        public int MaxPage { get; set; }
        public List<Oil> List { get; set; }
    }

    public class OilFullInfoViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public string Name { get; set; }

        [Display(Name = "Фото")]
        [Required(ErrorMessage = "Оберіть фото")]
        public string PhotoBase64 { get; set; }

        [Display(Name = "Опис")]
        [Required(ErrorMessage = "Вкажіть опис")]
        public string Description { get; set; }

        [Display(Name = "Ціна")]
        [Required(ErrorMessage = "Вкажіть ціну"), Range(0.0, float.PositiveInfinity, ErrorMessage = "Ціна не може бути меншою за 0")]
        public float Price { get; set; }

        [Display(Name = "Тип масла")]
        [Required(ErrorMessage = "Вкажіть тип")]
        public string Type { get; set; }

        [Display(Name = "Виробник масла")]
        [Required(ErrorMessage = "Вкажіть виробника")]
        public string Manafacturer { get; set; }

        [Display(Name = "Застосування масла")]
        [Required(ErrorMessage = "Вкажіть застосування")]
        public string Applying { get; set; }

        [Display(Name = "Кількість")]
        [Required(ErrorMessage = "Вкажіть кількість"), Range(0, 1000, ErrorMessage = "Кількість не може бути меншою за 0 і більшою за 1000")]
        public int Count { get; set; }

        [Display(Name = "Об'єм")]
        [Required(ErrorMessage = "Вкажіть Об'єм")]
        public string Capacity { get; set; }
        public List<OilRecommendation> Recommndations { get; set; }
        [Display(Name = "Рекомендації")]
        [Required(ErrorMessage = "Виберіть рекомендації")]
        public List<long> SelectedRecommndations { get; set; }
        public List<OilTolerance> Tolerances { get; set; }
        [Display(Name = "Допуски")]
        [Required(ErrorMessage = "Виберіть допуски")]
        public List<long> SelectedTolerances { get; set; }
        public List<OilSpecification> Specifications { get; set; }
        [Display(Name = "Специфікації")]
        [Required(ErrorMessage = "Виберіть специфікаї")]
        public List<long> SelectedSpecifications { get; set; }
    }

    public class OilItemCreate
    {
        public string Data { get; set; }
    }
}
