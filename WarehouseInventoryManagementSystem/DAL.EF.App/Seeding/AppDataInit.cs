using System.Security.Claims;
using DAL.EF.Base;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Seeding;

public static class AppDataInit
{
    private static readonly Guid AdminId = Guid.Parse("a0ced5f1-3eb8-4b54-8943-7e0ec692e6f0");
    private static readonly Guid DefaultUserId = Guid.Parse("8d85b5a0-2a30-4554-ac31-ad250f5ecd37");
    private static readonly Guid UnitId = Guid.Parse("cbe4b39c-bcc4-4e58-8bab-d7063e53a1fb");
    private static readonly Guid PizzaId = Guid.Parse("3b7dac36-82e2-443f-8b5c-73d7d9645611");
    private static readonly Guid DoughId = Guid.Parse("87bf56ff-2e31-471c-8ca5-e348f53a2fae");
    private static readonly Guid KebabId = Guid.Parse("b8a9d605-95f4-4467-8693-69b1b995253c");
    private static readonly Guid WarehouseId = Guid.Parse("21467176-122d-4821-8cc9-6aad6f1ce705");
    private static readonly Guid StoreId = Guid.Parse("22c2b386-0e90-42b4-a421-e746ca24aee4");

    public static void MigrateDatabase(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        var roleDefault = new AppRole()
        {
            Name = "Default"
        };
        var roleAdmin = new AppRole()
        {
            Name = "Admin"
        };
        var adminRole = roleManager.FindByNameAsync("Admin").Result;
        if (adminRole == null)
        {
            var result = roleManager.CreateAsync(roleAdmin).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot create role, {result}");
            }
        }

        var defaultRole = roleManager.FindByNameAsync("Default").Result;
        if (defaultRole == null)
        {
            var result = roleManager.CreateAsync(roleDefault).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot create role, {result}");
            }
        }

        (Guid id, string email, string userName, string pwd) adminData =
            (AdminId, "rmazantsev@gmail.com", "romaza", "Qqwerty-1");
        (Guid id, string email, string userName, string pwd) defaultUserData =
            (DefaultUserId, "test@test.com", "default user", "Qqwerty-1");

        var userAdmin = userManager.FindByEmailAsync(adminData.email).Result;
        if (userAdmin == null)
        {
            userAdmin = new AppUser()
            {
                Id = adminData.id,
                Email = adminData.email,
                UserName = adminData.email,
                EmailConfirmed = true,
                FirstName = "Roman",
                LastName = "Mazantsev",
            };
            var result = userManager.CreateAsync(userAdmin, adminData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result}");
            }

            if (result.Succeeded)
            {
                var admin = roleManager.FindByNameAsync("Admin").Result;
                if (admin != null)
                {
                    var addRoleResult = userManager.AddToRoleAsync(userAdmin, admin.Name!).Result;
                    if (!addRoleResult.Succeeded)
                    {
                        throw new ApplicationException($"Cannot assign admin role to master user, {result}");
                    }
                }
            }

            var var1 = userManager.AddClaimsAsync(userAdmin, new List<Claim>()
            {
                new(ClaimTypes.GivenName, userAdmin.FirstName),
                new(ClaimTypes.Surname, userAdmin.LastName)
            }).Result;

            if (!var1.Succeeded)
            {
                throw new ApplicationException($"Cannot add clims to admin, {var1}");
            }
        }

        var defaultUser = userManager.FindByEmailAsync(defaultUserData.email).Result;
        if (defaultUser == null)
        {
            defaultUser = new AppUser()
            {
                Id = defaultUserData.id,
                Email = defaultUserData.email,
                UserName = defaultUserData.email,
                EmailConfirmed = true,
                FirstName = "Default",
                LastName = "User",
            };
            var result = userManager.CreateAsync(defaultUser, defaultUserData.pwd).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result}");
            }

            if (result.Succeeded)
            {
                var role = roleManager.FindByNameAsync("Default").Result;
                if (role != null)
                {
                    var userRes = userManager.AddToRoleAsync(defaultUser, role.Name!).Result;
                    if (userRes == null)
                    {
                        throw new ApplicationException($"Cannot assign default role to user, {userRes}");
                    }
                }
            }

            var var2 = userManager.AddClaimsAsync(defaultUser, new List<Claim>()
            {
                new(ClaimTypes.GivenName, defaultUser.FirstName),
                new(ClaimTypes.Surname, defaultUser.LastName)
            }).Result;

            if (!var2.Succeeded)
            {
                throw new ApplicationException($"Cannot add claims to default user, {var2}");
            }
        }
    }

    public static void SeedAppData(ApplicationDbContext context)
    {
        SeedAppDataCategories(context);
        SeedAppDataUnits(context);
        SeedAppDataItems(context);
        SeedAppDataWarehouses(context);
        SeedAppDataStores(context);
        SeedAppDataItemComponents(context);
        SeedAppDataInventories(context);
        SeedAppDataInventoryTransactions(context);
        context.SaveChanges();
    }

    private static void SeedAppDataItems(ApplicationDbContext context)
    {
        if (context.Items.Any()) return;

        context.Set<Item>().AddRange(
            new Item
            {
                Id = PizzaId,
                Name = "Pizza",
                Description = "Delicious Kebab Pizza.",
                UnitId = UnitId,
            },
            new Item
            {
                Id = DoughId,
                Name = "Dough",
                UnitId = UnitId,
            },
            new Item
            {
                Id = KebabId,
                Name = "Kebab meat",
                Description = "kebaaaaaaaaaaaaaab",
                UnitId = UnitId,
            },
            new Item
            {
                Name = "Juicy Grilled Steak with Chimichurri",
                Description = "Succulent grilled steak topped with vibrant herb-based Argentine chimichurri sauce.",
                UnitId = UnitId,
            },
            new Item
            {
                Name = "Crispy Honey Glazed Salmon",
                Description =
                    "Delicate salmon fillet with a golden crispy crust glazed in a sweet and tangy honey sauce.",
                UnitId = UnitId,
            },
            new Item
            {
                Name = "Decadent Chocolate Lava Cake",
                Description = "Indulgent, moist chocolate cake with a molten, gooey center that melts in your mouth.",
                UnitId = UnitId,
            }
        );
    }

    private static void SeedAppDataUnits(ApplicationDbContext context)
    {
        if (context.Units.Any()) return;

        context.Set<Unit>().AddRange(
            new Unit
            {
                Name = "kg",
                Description = "Kilograms",
            },
            new Unit
            {
                Id = UnitId,
                Name = "g",
                Description = "Grams",
            },
            new Unit
            {
                Name = "mg",
                Description = "Milligrams",
            },
            new Unit
            {
                Name = "l",
                Description = "Liters",
            },
            new Unit
            {
                Name = "ml",
                Description = "Milliliters",
            },
            new Unit
            {
                Name = "m",
                Description = "Meters",
            },
            new Unit
            {
                Name = "cm",
                Description = "Centimeters",
            },
            new Unit
            {
                Name = "pc",
                Description = "Piece",
            }
        );
    }

    private static void SeedAppDataWarehouses(ApplicationDbContext context)
    {
        if (context.Warehouses.Any()) return;

        context.Set<Warehouse>().AddRange(
            new Warehouse
            {
                Id = WarehouseId,
                Name = "Das Companies, Inc",
                Address = "Tulsa, Oklahoma",
            },
            new Warehouse
            {
                Name = "Morgan Self Storage",
                Address = "Manchester, New Hampshire",
            },
            new Warehouse
            {
                Name = "Storage Solutions",
                Address = "Westfield, Indiana, United States",
            },
            new Warehouse
            {
                Name = "Sysco East Texas, Llc",
                Address = "Longview, Tx",
            },
            new Warehouse
            {
                Name = "Minturn Nut Co",
                Address = "Chowchilla, California",
            });
    }

    private static void SeedAppDataStores(ApplicationDbContext context)
    {
        if (context.Stores.Any()) return;

        context.Set<Store>().AddRange(
            new Store
            {
                Id = StoreId,
                Name = "Walmart",
                Address = "Bentonville, Ark.",
            },
            new Store
            {
                Name = "The Kroger Co.",
                Address = "Cincinnati",
            },
            new Store
            {
                Name = "Amazon",
                Address = "Seattle",
            },
            new Store
            {
                Name = "Costco",
                Address = "Issaquah, Wash.",
            },
            new Store
            {
                Name = "The Home Depot",
                Address = "Atlanta",
            });
    }

    private static void SeedAppDataInventories(ApplicationDbContext context)
    {
        if (context.StoreInventories.Any()) return;

        context.Set<StoreInventory>().AddRange(
            new StoreInventory
            {
                ItemId = KebabId,
                StoreId = StoreId,
                Quantity = 234234,
            },
            new StoreInventory
            {
                ItemId = PizzaId,
                StoreId = StoreId,
                Quantity = 234,
            },
            new StoreInventory
            {
                ItemId = DoughId,
                StoreId = StoreId,
                Quantity = 4324,
            },
            new StoreInventory
            {
                ItemId = KebabId,
                StoreId = StoreId,
                Quantity = 23424,
            },
            new StoreInventory
            {
                ItemId = PizzaId,
                StoreId = StoreId,
                Quantity = 43244,
            }
        );

        if (context.WarehouseInventories.Any()) return;

        context.Set<WarehouseInventory>().AddRange(
            new WarehouseInventory
            {
                ItemId = KebabId,
                WarehouseId = WarehouseId,
                Quantity = 234234,
            },
            new WarehouseInventory
            {
                ItemId = PizzaId,
                WarehouseId = WarehouseId,
                Quantity = 43244,
            },
            new WarehouseInventory
            {
                ItemId = DoughId,
                WarehouseId = WarehouseId,
                Quantity = 4234,
            },
            new WarehouseInventory
            {
                ItemId = KebabId,
                WarehouseId = WarehouseId,
                Quantity = 4324234,
            },
            new WarehouseInventory
            {
                ItemId = PizzaId,
                WarehouseId = WarehouseId,
                Quantity = 3454,
            }
        );
    }

    private static void SeedAppDataItemComponents(ApplicationDbContext context)
    {
        if (context.ItemComponents.Any()) return;

        context.Set<ItemComponent>().AddRange(
            new ItemComponent
            {
                ItemId = PizzaId,
                ComponentItemId = DoughId,
                ComponentQuantity = 200,
            },
            new ItemComponent
            {
                ItemId = PizzaId,
                ComponentItemId = KebabId,
                ComponentQuantity = 115,
            }
        );
    }

    private static void SeedAppDataCategories(ApplicationDbContext context)
    {
        if (context.Categories.Any()) return;

        context.Set<Category>().AddRange(
            new Category
            {
                Name = "Food",
            },
            new Category
            {
                Name = "Construction Materials",
            },
            new Category
            {
                Name = "Appliances",
            },
            new Category
            {
                Name = "Appliances",
            },
            new Category
            {
                Name = "Hardware",
            }
        );
    }

    private static void SeedAppDataInventoryTransactions(ApplicationDbContext context)
    {
        if (context.InventoryTransactions.Any()) return;

        context.Set<InventoryTransaction>().AddRange(
            new InventoryTransaction()
            {
                ItemId =
                    PizzaId,
                AppUserId = AdminId,
                Quantity = 22442,
                TransactionType = (char)TransactionType.Addition,
                FromWarehouse = WarehouseId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    DoughId,
                AppUserId = AdminId,
                Quantity = 22442,
                TransactionType = (char)TransactionType.Withdrawing,
                FromWarehouse = WarehouseId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    PizzaId,
                AppUserId = AdminId,
                Quantity = 22442,
                TransactionType = (char)TransactionType.Addition,
                FromWarehouse = WarehouseId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    KebabId,
                AppUserId = AdminId,
                Quantity = 22442,
                TransactionType = (char)TransactionType.Addition,
                FromWarehouse = WarehouseId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    PizzaId,
                AppUserId = AdminId,
                Quantity = 22442,
                TransactionType = (char)TransactionType.Withdrawing,
                FromWarehouse = WarehouseId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    KebabId,
                AppUserId = AdminId,
                Quantity = 23,
                TransactionType = (char)TransactionType.Withdrawing,
                FromStore = StoreId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    DoughId,
                AppUserId = AdminId,
                Quantity = 73,
                TransactionType = (char)TransactionType.Addition,
                FromStore = StoreId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    PizzaId,
                AppUserId = AdminId,
                Quantity = 34353492345,
                TransactionType = (char)TransactionType.Withdrawing,
                FromStore = StoreId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    DoughId,
                AppUserId = AdminId,
                Quantity = 23,
                TransactionType = (char)TransactionType.Addition,
                FromStore = StoreId,
            },
            new InventoryTransaction()
            {
                ItemId =
                    KebabId,
                AppUserId = AdminId,
                Quantity = 23233,
                TransactionType = (char)TransactionType.Withdrawing,
                FromStore = StoreId,
            }
        );
    }
}