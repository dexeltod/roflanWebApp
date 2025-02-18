ARG IMAGE_NAME=roflanwebapp

# Используем официальный образ .NET SDK для сборки приложения.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Устанавливаем рабочую директорию в контейнере.
WORKDIR /app

# Копируем файлы проекта (за исключением .gitignore файлов)
COPY . .

# Восстанавливаем зависимости NuGet
RUN dotnet restore

# Публикуем приложение для дальнейшего копирования в финальный образ
RUN dotnet publish -c Release -o /app/out

# Создаем новый образ, основанный на runtime.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Устанавливаем рабочую директорию в контейнере.
WORKDIR /app

# Копируем опубликованное приложение из образа сборки.
COPY --from=build /app/out ./

# Выставляем порт приложения
EXPOSE 80

# Команда для запуска приложения.
ENTRYPOINT ["dotnet", "Boot.dll"]
