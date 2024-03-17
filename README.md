![MasterHead](https://upload.wikimedia.org/wikipedia/commons/2/20/Matrix_Digital_rain_banner.gif)

# 🏹 MaToSD (Lab 1)

Цей застосунок конвертує .md файли до .html. В ньому присутні тестові .md файли, з допомогою яких можна перевірити коректність роботи програми.

## 👷 Як зібрати і запустити проєкт

Коли знаходитесь у корені репозиторію, то пропишіть
```
cd MaToSD_2
```
що б перейти до репозиторію самої програми

Що б зібрати проєкт:

```
dotnet build MaToSD_2.csproj
```

І запустити:

```
dotnet run MaToSD_2.csproj <--out> <шлях до файлу>
```
## 👷 Як запустити тести

Коли знаходитесь у корені репозиторію, то пропишіть
```
cd MaToSD_2UnitTest
```
що б перейти до репозиторію тестів

Що б запустити:

```
dotnet test
```

## [Комміт, на якому впали тести CI](https://github.com/DreammyOleksandr/MaToSD_2/commit/36ad228e937988fa5943e1d35009e1e089bffd6a)

## 📝 Висновки

Спочатку, написання юніт-тестів здавалося марною тратою часу, але в поєднанні з CI/CD GitHub`a потенціал тестів для мене розкрився на повну. За допомогою GitHub Actions CI/CD та юніт-тестів можна одразу відстежувати правильність роботи програми і від цього вже здійснювати наступні дії.