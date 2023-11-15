
# Levi9 Hackathon




## Technologies
* C#
* ASP.NET Core Entity Framework
* PostgreSQL
* SWAGGER


## Documentation

IDE za pokretanje programa je Visual Studio, pokrećemo klikom na run dugme IIS Express webservera. Tako nam se otvara Open Api(Swagger) gde možemo testirati ovaj api. Korišćen je entity framework core, u projektu nije utiliziran ORM, nego su manuelno kreirane tabele u public šemu, baze fiba, sledećom skriptom:

CREATE TABLE IF NOT EXISTS "Players" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(255),
    "Position" INTEGER,
    "FTM" INTEGER,
    "FTA" INTEGER,
    "TwoPM" INTEGER,
    "TwoPA" INTEGER,
    "ThreePM" INTEGER,
    "ThreePA" INTEGER,
    "REB" INTEGER,
    "BLK" INTEGER,
    "AST" INTEGER,
    "STL" INTEGER,
    "TOV" INTEGER
);

Druge korisne skripte za proveru:
SELECT * FROM public."Players";
DELETE FROM public."Players";

Svako pokretanje programa briše sve podatke iz baze i unosi sve podatke iz csv fajla unutar projekta. U appsettings.json nalaze se svi važni paramtri za konekciju sa lokalnom bazom.

"ConnectionStrings": {
    "FibaConnection": "Host='localhost';Port=5432;Database='fiba';Username='postgres';Password='super';Pooling=true;"
  },





## 



