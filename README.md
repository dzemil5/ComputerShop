# ComputerShop - Projekat

## Opis projekta

ComputerShop je web aplikacija za prodaju racunarske opreme napravljena u ASP.NET Core MVC tehnologiji. Projekat implementira osnovne funkcionalnosti online prodavnice, ukljucujuci upravljanje proizvodima, korisnicku registraciju i prijavu, korpu i razlicite nivoe pristupa u zavisnosti od korisnicke uloge.

## Funkcionalnosti

- **Korisnicki nalog:** Registracija, login i logout putem ASP.NET Core Identity.
- **Role korisnika:** Gost, User i Admin sa razlicitim nivoima pristupa i prava.
- **Promena teme:** Mogucnost prebacivanja izmedju svetle i tamne teme sajta.
- **CRUD operacije:** Kreiranje, citanje, izmena i brisanje proizvoda.
- **Pretraga i paginacija:** Pretraga proizvoda i prikaz rezultata sa paginacijom (paged list).
- **Korpa:** Dodavanje proizvoda u korpu za ulogovane korisnike.
- **Dve staticke stranice:** Osnovne stranice sa statickim sadrzajem (bez baze).
- **Baza podataka:** Koristi SQL Server sa cetiri povezane tabele (proizvodi, korisnici, uloge i korpa).

## Tehnologije

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- ASP.NET Core Identity
- SQL Server
- Bootstrap 5
- X.PagedList za paginaciju
- JSON Session storage za korpu

## Pokretanje projekta

1. Klonirajte repozitorijum.
2. U `appsettings.json` podesite konekciju sa lokalnom SQL Server bazom podataka.
3. Pokrenite sledece komande u Package Manager Console za kreiranje baze i migracije:

    ```
    Add-Migration InitialCreate
    Update-Database
    ```

4. Pokrenite aplikaciju (npr. pritiskom na F5 u Visual Studio ili `dotnet run`).
5. Registrujte korisnika ili se prijavite kao admin (prethodno kreiran uloga Admin).
6. Testirajte funkcionalnosti dodavanja proizvoda, korpe, pretrage i paginacije.

## Napomene

- Samo korisnici sa ulogom **Admin** mogu dodavati, menjati i brisati proizvode.
- Gost i obicni korisnik mogu pregledati proizvode i dodavati ih u korpu (uz prethodnu prijavu).
- Tema sajta se pamti u `localStorage`.

---

Za bilo kakva pitanja, stojim na raspolaganju.
