using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OilShop.Entities
{
    public class SeederDB
    {
        public static void SeedData(IServiceProvider services,
            IWebHostEnvironment env, IConfiguration config)
        {
            try
            {
                using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                    var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var roleName = "Admin";
                    var result = managerRole.CreateAsync(new DbRole
                    {
                        Name = roleName
                    }).Result;

                    string email = "admin@gmail.com";
                    var user = new DbUser
                    {
                        Email = email,
                        UserName = email,
                        FullName = "Stepan New",
                        PhoneNumber = "+380974355566"
                    };
                    result = manager.CreateAsync(user, "12345678").Result;
                    result = manager.AddToRoleAsync(user, roleName).Result;
                    context.Carts.Add(new Cart { CustomerId = user.Id });

                    roleName = "User";
                    result = managerRole.CreateAsync(new DbRole
                    {
                        Name = roleName
                    }).Result;

                    email = "user@gmail.com";
                    user = new DbUser
                    {
                        Email = email,
                        UserName = email,
                        FullName = "Valera New",
                        PhoneNumber = "+380974355116"
                    };
                    result = manager.CreateAsync(user, "12345678").Result;
                    result = manager.AddToRoleAsync(user, roleName).Result;
                    context.Carts.Add(new Cart { CustomerId = user.Id });

                    var types = new List<OilType>
                    {
                        new OilType { Name = "Синтетика" },
                        new OilType { Name = "Мінеральні" },
                        new OilType { Name = "Напівсинтетика" }
                    };
                    context.OilTypes.AddRange(types);

                    var capacities = new List<OilCapacity>
                    {
                        new OilCapacity { Capacity = 1 },
                        new OilCapacity { Capacity = 4 },
                        new OilCapacity { Capacity = 5 },
                        new OilCapacity { Capacity = 20 },
                        new OilCapacity { Capacity = 60 },
                        new OilCapacity { Capacity = 205 }
                    };
                    context.OilCapacities.AddRange(capacities);

                    var applyings = new List<OilApplying>
                    {
                        new OilApplying { Name = "АКПП" },
                        new OilApplying { Name = "КПП" },
                        new OilApplying { Name = "Двигун" },
                        new OilApplying { Name = "Амортизатори" }
                    };
                    context.OilApplyings.AddRange(applyings);

                    var manafacturers = new List<OilManafacturer>
                    {
                        new OilManafacturer { Country = "Німеччина" },
                        new OilManafacturer { Country = "Польща" },
                        new OilManafacturer { Country = "Італія" }
                    };
                    context.OilManafacturers.AddRange(manafacturers);

                    var statuses = new List<OrderStatus>
                    {
                        new OrderStatus { Name ="В обробці" },
                        new OrderStatus { Name ="Відправлено" },
                        new OrderStatus { Name ="Завершено" },
                    };
                    context.OrderStatuses.AddRange(statuses);

                    var recomendations = new List<OilRecommendation>
                    {
                        new OilRecommendation { Name ="CUMMINS CES 20086" },
                        new OilRecommendation { Name ="DETROIT DIESEL 93K218" },
                        new OilRecommendation { Name ="DETROIT DIESEL 93K222" },
                        new OilRecommendation { Name ="JASO DH-2" },
                        new OilRecommendation { Name ="RENAULT RLD-4" },
                    };
                    context.OilRecommendations.AddRange(recomendations);

                    var tolerance = new List<OilTolerance>
                    {
                        new OilTolerance { Name ="DEUTZ DQC IV-18 LA" },
                        new OilTolerance { Name ="MACK EOS-4.5" },
                        new OilTolerance { Name ="MAN M 3477" },
                        new OilTolerance { Name ="MB-APPROVAL 228.51" },
                        new OilTolerance { Name ="MTU DDC TYPE 3.1" },
                        new OilTolerance { Name ="VOLVO VDS-4.5" },
                        new OilTolerance { Name ="SCANIA LA" },
                    };
                    context.OilTolerances.AddRange(tolerance);

                    var specifications = new List<OilSpecification>
                    {
                        new OilSpecification { Name ="ACEA E9/E7/E6" },
                        new OilSpecification { Name ="API CK-4/SN" },
                        new OilSpecification { Name ="CAT ECF-3" },
                    };
                    context.OilSpecifications.AddRange(specifications);


                    context.Oils.Add(new Oil
                    {
                        Name = "Fuchs Titan Supersyn 5W-40",
                        OilType = types.FirstOrDefault(x => x.Name == "Синтетика"),
                        Count = 30,
                        Price = 657,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "Двигун"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Німеччина"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 4),
                        Description = "Fuchs Titan Supersyn 5W-40 - синтетичне масло для бензинових / дизельних моторів. Розрахованe на ускладнені умови експлуатації. Унікальний склад допоможе захистити від відкладень на елементах двигуна. При дотриманні температурного режиму і не змішуванні з іншими маслами, може подовжити інтервал сервісного обслуговування.",
                        Image = "https://oilteam.com.ua/image/cache/data-fuchs-titan-supersyn-longlife-5w-40-500x500.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "Fuchs Titan Supersyn 5W-40",
                        OilType = types.FirstOrDefault(x => x.Name == "Синтетика"),
                        Count = 30,
                        Price = 9670,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "Двигун"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Німеччина"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 60),
                        Description = "Fuchs Titan Supersyn 5W-40 - синтетичне масло для бензинових / дизельних моторів. Розрахованe на ускладнені умови експлуатації. Унікальний склад допоможе захистити від відкладень на елементах двигуна. При дотриманні температурного режиму і не змішуванні з іншими маслами, може подовжити інтервал сервісного обслуговування.",
                        Image = "https://content1.rozetka.com.ua/goods/images/big/128144379.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "TITAN ATF 6000 SL",
                        OilType = types.FirstOrDefault(x => x.Name == "Синтетика"),
                        Count = 0,
                        Price = 266,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "АКПП"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Польща"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 1),
                        Description = "Fuchs TITAN ATF 6000 SL - високоякісна рідина для автоматичних трансмісій легкових автомобілів і легких вантажівок. Висока ефективність продукту гарантує стабільний і всебічний захист вузлів від зносу, мінімальне тертя, економію витрати палива і плавне перемикання передач. Стійке до старіння і окислення, завдяки чому підтримує свої первинні властивості на тривалий термін.",
                        Image = "https://a-static.mlcdn.com.br/618x463/oleo-de-transmissao-fuchs-titan-atf-6000-sl-1l/valerace/9405376525/f79ce59f0d7af3057fbf1b01a01a7e44.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "TITAN ATF 5500",
                        OilType = types.FirstOrDefault(x => x.Name == "Мінеральні"),
                        Count = 10,
                        Price = 5651,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "АКПП"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Польща"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 20),
                        Description = "TITAN ATF 5500 - робоча рідина преміум класу для автоматичних трансмісій транспортних засобів. Властивості використаного базового масла забезпечують прекрасні низькотемпературні властивості і високу стійкість до старіння. Рідина тривалий час зберігає задані фрикційні характеристики, за рахунок цього зберігаються параметри перемикання передач.",
                        Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSz0EzalHwNnHZFQlI8ltvNIgFCdmTlDgrc5m_uvu55IutKnUJt7KzLeGkMfMWpE7GYifc&usqp=CAU"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "Titan Cargo Maxx SAE 5W-30",
                        OilType = types.FirstOrDefault(x => x.Name == "Синтетика"),
                        Count = 15,
                        Price = 47664,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "Двигун"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Польща"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 205),
                        Description = "Fuchs TITAN CARGO MAXX 5W-30 - це високоякісне синтетичне дизельне моторне масло класу MAXX для комерційного транспорту. Забезпечує максимальну економію палива, завдяки його неперевершеній стабільності проти окислення і старіння. Гарантує оптимальний захист для сучасних сажових фільтрів і каталізаторів та підвищує довговічність їх роботи.",
                        Image = "https://oelbaron24.de/media/image/product/9260/md/fuchs-titan-cytrac-sl-75w-90-gl5~3.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "Fuchs Titan Cytrac SL 75W-90 GL5",
                        OilType = types.FirstOrDefault(x => x.Name == "Синтетика"),
                        Count = 5,
                        Price = 55089,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "КПП"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Італія"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 205),
                        Description = "TITAN CYTRAC SL 75W-90 - це високоякісне синтетичне універсальне трансмісійне масло для привідних мостів, механічних коробок передач, коробок відбору потужності та допоміжних редукторів.",
                        Image = "https://oelbaron24.de/media/image/product/9260/md/fuchs-titan-cytrac-sl-75w-90-gl5~3.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "AGRIFARM STOU MC SAE 10W-30",
                        OilType = types.FirstOrDefault(x => x.Name == "Напівсинтетика"),
                        Count = 2,
                        Price = 43371,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "КПП"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Німеччина"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 205),
                        Description = "AGRIFARM STOU MC SAE 10W-30 - yніверсальне масло для аграрної та будівельної техніки. Продукт застосовується як робоче масло для турбодизельних моторів, трансмісій, зокрема механічних коробок передач і гідросистем, а також в гальмах «мокрого» типу, гідромуфтах.",
                        Image = "https://oelbaron24.de/media/image/product/9260/md/fuchs-titan-cytrac-sl-75w-90-gl5~3.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "Silkolene FOAM FILTER OIL",
                        OilType = types.FirstOrDefault(x => x.Name == "Мінеральні"),
                        Count = 7,
                        Price = 650,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "КПП"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Польща"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 1),
                        Description = "FUCHS Silkolene FOAM FILTER OIL - це високотехнологічне покриття, розроблене для підвищення ефективності роботи поролонових повітряних фільтрів. Містить полімерні добавки, які роблять поверхню дуже липкою і дозволяють фільтру затримувати навіть легко проникні частинки піску, пилу і води, не впливаючи на надходження повітря в двигун.",
                        Image = "https://images.ua.prom.st/3065083069_w700_h500_ochistitel-vozdushnogo-filtra.jpg"
                    });

                    context.Oils.Add(new Oil
                    {
                        Name = "SILKOLENE RSF 7.5",
                        OilType = types.FirstOrDefault(x => x.Name == "Синтетика"),
                        Count = 40,
                        Price = 320,
                        OilApplying = applyings.FirstOrDefault(x => x.Name == "Амортизатори"),
                        OilManafacturer = manafacturers.FirstOrDefault(x => x.Country == "Німеччина"),
                        OilCapacity = capacities.FirstOrDefault(x => x.Capacity == 1),
                        Description = "SILKOLENE RSF 7.5 - cинтетична ефірна мастильна рідина для підвісок мотоциклів, серія Range. У поєднанні з антифрикційними синтетичними присадками забезпечує плавну амортизацію на всіх дорожніх нерівностях.",
                        Image = "https://veloservice.if.ua/wp-content/uploads/2020/08/210874033_images_18122784901.jpg"
                    });
                    context.SaveChanges();

                    context.Orders.Add(new Order()
                    {
                        Address = "Nova 12 ",
                        City = "Rivne",
                        PhoneNumber = "+380973455521",
                        OrderDate = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                        Reciever = "Stepan tut",
                        OrderStatus = statuses.FirstOrDefault(x => x.Name == "Завершено"),
                        DeclarationNumber = "124",
                        Customer = manager.FindByIdAsync("2").Result,
                        OrderLines = new List<OrderDetail>()
                        {
                            new OrderDetail {Amount=20,OilId=1,Order=context.Orders.FirstOrDefault(x=>x.Id==3),Price=741},
                        }
                    });

                    context.Orders.Add(new Order()
                    {
                        Address = "Mova 212 ",
                        City = "Rivne",
                        PhoneNumber = "+380973455521",
                        OrderDate = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                        Reciever = "Stepan tut",
                        OrderStatus = statuses.FirstOrDefault(x => x.Name == "Відправлено"),
                        DeclarationNumber = "123",
                        Customer = manager.FindByIdAsync("1").Result,
                        OrderLines = new List<OrderDetail>()
                        {
                            new OrderDetail {Amount=1,OilId=5,Order=context.Orders.FirstOrDefault(x=>x.Id==2),Price=46241},
                            new OrderDetail {Amount=20,OilId=1,Order=context.Orders.FirstOrDefault(x=>x.Id==2),Price=631},
                            new OrderDetail {Amount=20,OilId=9,Order=context.Orders.FirstOrDefault(x=>x.Id==2),Price=287},
                        }
                    });


                    context.Orders.Add(new Order()
                    {
                        Address = "Mova 12 ",
                        City = "Rivne",
                        PhoneNumber = "+380973455521",
                        OrderDate = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                        Reciever = "Valera tut",
                        OrderStatus = statuses.FirstOrDefault(x => x.Name == "В обробці"),
                        Customer = manager.FindByIdAsync("1").Result,
                        OrderLines = new List<OrderDetail>()
                            {
                                new OrderDetail {Amount=3,OilId=2,Order=context.Orders.FirstOrDefault(x=>x.Id==1),Price=9601},
                                new OrderDetail {Amount=13,OilId=3,Order=context.Orders.FirstOrDefault(x=>x.Id==1),Price=291},
                                new OrderDetail {Amount=2,OilId=6,Order=context.Orders.FirstOrDefault(x=>x.Id==1),Price=55387},
                            }
                    });

                    var specificationoils = new List<SpecificationOil>
                {
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==1)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==1)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==1)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==1)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==3)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==2)},
                    new SpecificationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Specification=context.OilSpecifications.FirstOrDefault(x=>x.Id==1)},
                };
                    context.SpecificationOils.AddRange(specificationoils);
                    context.SaveChanges();

                    var toleranceoils = new List<ToleranceOil>
                {
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==1)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==2)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==3)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==4)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==1)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==6)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==7)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==2)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==3)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==1)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==2)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==4)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==3)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==2)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==3)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==1)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==2)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==4)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==1)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==6)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==1)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==6)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==6)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==5)},
                    new ToleranceOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Tolerance=context.OilTolerances.FirstOrDefault(x=>x.Id==4)},
                };
                    context.ToleranceOils.AddRange(toleranceoils);
                    context.SaveChanges();

                    var recomendationoils = new List<RecommendationOil>
                {
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==1)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==2)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==3)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==5)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==1),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==4)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==1)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==2)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==3)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==2),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==4)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==5)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==3)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==3),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==1)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==1)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==2)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==4),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==3)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==4)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==5)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==5),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==1)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==2)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==3)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==6),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==4)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==5)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==1)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==7),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==2)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==3)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==4)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==8),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==5)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==2)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==4)},
                    new RecommendationOil { Oil=context.Oils.FirstOrDefault(x=>x.Id==9),Recommendation=context.OilRecommendations.FirstOrDefault(x=>x.Id==5)},
                };
                    context.RecommendationOils.AddRange(recomendationoils);

                    context.SaveChanges();
                }

            }
            catch { }
        }
    }
}
