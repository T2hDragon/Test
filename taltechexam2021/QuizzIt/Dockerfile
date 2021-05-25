FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Directory.Build.props .

# copy everything else and build app
# copy all Base projects
COPY BLL.Base/*.csproj ./BLL.Base/
COPY Contracts.BLL.Base/*.csproj ./Contracts.BLL.Base/
COPY Contracts.DAL.Base/*.csproj ./Contracts.DAL.Base/
COPY Contracts.Domain.Base/*.csproj ./Contracts.Domain.Base/
COPY DAL.Base/*.csproj ./DAL.Base/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/
COPY Domain.Base/*.csproj ./Domain.Base/
COPY Extensions.Base/*.csproj ./Extensions.Base/
COPY Resources.Base/*.csproj ./Resources.Base/

# copy all App projects
COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain.App/*.csproj ./Domain.App/
COPY PublicAPI.DTO.v1/*.csproj ./PublicAPI.DTO.v1/
COPY Resources/*.csproj ./Resources/
COPY WebApp/*.csproj ./WebApp/
COPY WebApp/*.csproj ./WebApp/
COPY TestProject/*.csproj ./TestProject/

# Restore all the nuget packages
RUN dotnet restore


# Copy over the source code
# Copy all Base projects
COPY BLL.Base/. ./BLL.Base/
COPY Contracts.BLL.Base/. ./Contracts.BLL.Base/
COPY Contracts.DAL.Base/. ./Contracts.DAL.Base/
COPY Contracts.Domain.Base/. ./Contracts.Domain.Base/
COPY DAL.Base/. ./DAL.Base/
COPY DAL.Base.EF/. ./DAL.Base.EF/
COPY Domain.Base/. ./Domain.Base/
COPY Extensions.Base/. ./Extensions.Base/
COPY Resources.Base/. ./Resources.Base/

# copy all App projects
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain.App/. ./Domain.App/
COPY PublicAPI.DTO.v1/. ./PublicAPI.DTO.v1/
COPY Resources/. ./Resources/
COPY WebApp/. ./WebApp/
COPY TestProject/. ./TestProject/

WORKDIR /source/WebApp

RUN dotnet publish -c Release -o out

# Create new image from ASPnet runtime (no compliers)
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime

# RUN apt-get -y update && apt-get -y upgrade

WORKDIR /app
COPY --from=build /source/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]

RUN ls -al


