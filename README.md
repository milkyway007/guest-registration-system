# guest-registration-system

Lubatud aeg: 48 tundi
Kulutatud aeg: 120 tundi

# Põhjus

Süvenesin liiga palju uute tehnoloogiate kasutamise õpimisesse (ASP.NET Core, AutoMapper, MediatR, Entity Framework, Fluent Validation, Axios. Eeldasin, et on hea lihtne projekt, mille peal õppida tänapäeval kasutatavaid tehnoloogiaid. Samuti rakendasin üleliigset (tõenäoliselt mittevajalikku) loomingulisust ja implementeerisin sündmuste/osalejate lisamise/muutmise võimalust modaalsete akendena, mis tegi javascripti koodi keerulisemaks.

# Arendamisvahendid

Visual Studio Community 2022, Visual Studio Code, SQLite Studio.

# Paigaldamine

Ülesande kirjeldus väidab: rakenduse kood peab olema vigadeta kompileeruv ning eelneva seadistuseta Visual Studiost avatav ning käivitatav.

Seega, avame koodi Visual Studios, valime käsu Restore Nuget Packages ja kompileerime. Võib-olla, tuleb enne kompileerimist Visual Studio kinni panna ja veel kord avada. Käivitame teenust Debug tingimustes.

Klienti saab näha aadressil http://localhost:5000/staticfiles.

# Rakenduse arhitektuur

## Andmebaas

Andmebaasina on projektis kasutatud SQLite.
Andmebaasi diagrammi saab näha siin: 
http://schemacrawler-webapp.herokuapp.com/schemacrawler/results/bs8attxlwtpo

Andmebaasi struktuuril ja realisatsioonil on teatud puudujäägid. 

Oleks võimalik lisada kaks klassifikaatortabelit: payment_methods ja countries. Hetkel eksisteerivad nende väärtused kliendi koodis massiividena.

Teistmoodi pidi organiseerima osalejate kohta käivaid tabeleid, nt:

#### persons

FirstName

LastName

#### companies

Name

#### participants

Code

#### event_participants

EventId

ParticipantCode

PaymentMethod

ParticipantCount

Description

## Persistence

ORM raamistikuna kasutatakse Entity Framework.

Configuration kaustas on klassid, mis määravad piiranguid tabelitele, primary keys ja foreign keys.
Klass Seed on vajalik andmebaasi täitmiseks esialgsete andmetega.

Eraldi on projekt Persistence.Interfaces ja üks liides IDataContext (vajalik mock-objektide loomiseks testimisel).

Liideste olemasolu eraldi projektides on hea praktika, kuna lubab vältida mittevajalikke sõltuvusi projektide vahel.

## Domain

Domeeni objektid

## Application

Rakenduse loogika, valmistab ette vastuseid kliendi päringutele ja annab edasi API-le. Sisaldab samuti validaatoreid, mis teevad kindlaks, kas päringutes sisalduvad andmed on vastuseks piisavad.

Kasutusel on AutoMapper (Application.Core.MappingProfiles). Kasutusel nt klassis Application.Events.Commands.Edit: mugav muutuste ülekandmine andmebaasis olevale objektile. Samuti  Application.Participants.Commands.Edit.

Projektis kasutatakse Fluent Validation validaatorite loomiseks.

Projektis kasutatakse samuti MediatR CQRS mustri realiseerimiseks päringute töötlejate loomisel.

Erakdi on projekt Application.Interfaces. Endiselt vajalik testimise otstarbel.

## API

Kaustas Controllers asuvad kontrollerid kliendi päringute vastuvõtmiseks, mis annavad neid edasi Application projektile. Projektis on ka kontrollerid ja päringud, mida klient tegelikult ei kasuta.
Kaustas StaticFiles asub kliendi kood.

## Tests

Tehnoloogiad: NUnit, Moq, MockQueryable.
Sisaldab 56 ühiktesti. Kõik kasutusjuhtumid ei ole kaetud ühiktestidega. Need muudatused, mis on tekkinud pärast kliendi lisamist ja manuaalset testimist, ei ole testitud.

## Klient

Tehnoloogiad Javascript, CSS, HTML.

Kaua aega kulutasin javacsript failide organiseerimise sobiva viisi leidmisele. Lõpetasin moodulite lahendusega. Kood on jagatud failideks vastavalt teostatavale operatsioonile. Ei tea, kuidas seda tehakse professionaalsetes tingimustes.

Päringute saatmiseks on kasutusel Axios (kaust Requests). Failis agent.js proovisin tegeleda serverist saadetavate vigade kasutajale näitamisega. Rahuldavalt tuli välja valideerimisvigade näitamine.

CSS raamistikuna kasutasin Bulma-t. Valisin selle, kuna omasin sellega eelnevat kogemust. Mõned ikoonid on võetud Font Awesome raamatukogust.

