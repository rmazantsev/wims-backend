# icd0021-22-23-s

name: Roman Mazantsev

uni-id: romaza

student code: 230617TAF


# Generate db migration

~~~bash
# install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# create migration
dotnet ef migrations add Initial --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
dotnet ef migrations add Token --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 

# apply migration
dotnet ef database update --project DAL.EF.App --startup-project WebApp --context ApplicationDbContext 
~~~

# generate rest controllers

Add nuget packages
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer
-
~~~bash
# install tooling
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

cd WebApp
# MVC
dotnet aspnet-codegenerator controller -m Item -name ItemsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Unit -name UnitsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Category -name CategoriesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m ItemComponent -name ItemComponentsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Warehouse -name WarehousesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m WarehouseInventory -name WarehouseInventoriesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Store -name StoresController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m StoreInventory -name StoreInventoriesController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m InventoryTransaction -name InventoryTransactionsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Location -name LocationsController -outDir Controllers -dc ApplicationDbContext  -udl --referenceScriptLibraries -f

# Rest API
dotnet aspnet-codegenerator controller -m Item -name ItemsController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Unit -name UnitsController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Category -name CategoriesController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m ItemComponent -name ItemComponentsController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Warehouse -name WarehousesController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Domain.App.Warehouse -name WarehousesController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m WarehouseInventory -name WarehouseInventoriesController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Store -name StoresController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m StoreInventory -name StoreInventoriesController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m InventoryTransaction -name InventoryTransactionsController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f
dotnet aspnet-codegenerator controller -m Location -name LocationsController -outDir ApiControllers -api -dc ApplicationDbContext  -udl -f

~~~

Generate Identity UI

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.EF.App.ApplicationDbContext --userClass AppUser -f 
~~~
