# Personal-Movie-Database (PMDb)

## **Main task**

Create personal library of films (such as "IMDb" or "Кинопоиск")

## **Features**

- Possibility to add own marks for any film
- Registration/Authentication though Google/Facebook (SSO)
- Possibility to create different lists (watch later or favorite films)
- Possibility to add tag to film
- Possibility to add short review
- Possibility to sort movies by genre or mark
- Possibility to watch information from 
  - Marks
  - Synopsis
  - Genre
  - Authors (Director, Producers, Actors, Composers, Scriptwriters)
  - Technical information (year of release, studio, budget, etc.)
  - Poster
- Possibility to export database to file
- Possibility to share your mark/review in popular social networks (facebook)

## **Frameworks are going to be used**

- .NET Core
-	ASP.NET Core
-	Entity Framework Core
-	DotRas
-	Angular 2

## **Several issues in depth**

As you might know "Kinopoisk.ru" is blocked within territory of Ukraine thus sort of necessity appears to create a virtual private network (VPN) through DotRas library. Besides "Kinopoisk.ru" doesn't give an access to it's API so there is another necessity to use 3rd party services such as (getmovie.cc/api-kinopoisk.html).
