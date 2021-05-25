Migrations

~~~
dotnet ef database --project DAL.App.EF --startup-project WebApp drop
y
dotnet ef migrations --project DAL.App.EF --startup-project WebApp add Initial
dotnet ef database --project DAL.App.EF --startup-project WebApp update


docker push kaalte/webapp:latest
~~~


Remove cascade delete
~~~CSHARP
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
~~~

Install
Microsoft.VisualStudio.Web.CodeGeneration.Design
to WebApp

MVC Web controllers
~~~
dotnet aspnet-codegenerator controller -name RolesController          -actions -m Domain.App.Identity.AppRole       -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TeachersController       -actions -m Domain.App.Contract               -dc AppDbContext -outDir Areas/School/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name QuizzesController       -actions -m Domain.App.Quiz                  -dc AppDbContext -outDir Areas/QuizMaker/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TitlesController         -actions -m Domain.App.Title                  -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

Api controllers
~~~
dotnet aspnet-codegenerator controller -name PersonsController          -m Domain.App.Person           -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactsController         -m Domain.App.Contact          -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name ContactTypesController     -m Domain.App.ContactType      -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name SimplesController          -m Domain.App.Simple           -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
dotnet aspnet-codegenerator controller -name IntPkThingsController      -m Domain.App.IntPkThing       -actions -dc AppDbContext -outDir ApiControllers -api --useAsyncActions  -f
~~~

Solve the scaffolding problems on Ubuntu.
https://github.com/dotnet/Scaffolding/issues/1387#issuecomment-735289808

On project folder execute
~~~bash
mkdir Templates && mkdir Templates/ControllerGenerator && mkdir Templates/ViewGenerator
cp -r /home/$USER/.nuget/packages/microsoft.visualstudio.web.codegenerators.mvc/5.0.0/Templates/ControllerGenerator/* ./Templates/ControllerGenerator
cp -r /home/$USER/.nuget/packages/microsoft.visualstudio.web.codegenerators.mvc/5.0.0/Templates/ViewGenerator/* ./Templates/ViewGenerator/
~~~



Scaffold the existing database
~~~bash
dotnet ef dbcontext scaffold --project DAL.App.EF --startup-project WebApp "Server=barrel.itcollege.ee,1533;User Id=student;Password=Student.Bad.password.0;Database=akaver-distdemo01;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer --data-annotations --context AppDbContext --output-dir Models
~~~


Scaffold the identity pages
~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc DAL.App.EF.AppDbContext -f
~~~


Distributed Homeworks
~~~bash
https://gitlab.akaver.com/taltech-public/building-distributed-systems-2020-spring/course-materials/-/tree/master/homeworks
~~~

Documentation
~~~bash
ERD: In git named Autokool.qsee
Flow: https://miro.com/app/board/o9J_lW0hRQY=/
Description pdf: In Git named .pdf
~~~

Azure: http://autokool.azurewebsites.net/