# استخدام صورة ASP.NET Core الرسمية
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# صورة SDK لإنشاء التطبيق
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# نسخ ملفات المشروع واستعادة الحزم
COPY ["program.csproj", "./"]
RUN dotnet restore "./program.csproj"

# نسخ بقية الملفات وبناء المشروع
COPY . .
RUN dotnet publish -c Release -o /app/publish

# تشغيل التطبيق
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "StoryApp.dll"]