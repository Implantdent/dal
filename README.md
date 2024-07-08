# DAL

Proyecto con el manejo de la persistencia de los datos que soporta la aplicación ImplantDent

| Sonarqube |
|---|
| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_dal&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Implantdent_dal) |
| [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_dal&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Implantdent_dal) |
| [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_dal&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Implantdent_dal) |
| [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Implantdent_dal&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Implantdent_dal) |

## CI/CD

Se ejecuta el pipeline https://github.com/Implantdent/dal/actions/workflows/build.yml

| Rama | Estado |
|:-:|:-:|
| dev | [![Compilar](https://github.com/Implantdent/dal/actions/workflows/build.yml/badge.svg?branch=dev)](https://github.com/Implantdent/dal/actions/workflows/build.yml) |
| qa | [![Compilar](https://github.com/Implantdent/dal/actions/workflows/build.yml/badge.svg?branch=qa)](https://github.com/Implantdent/dal/actions/workflows/build.yml) |
| main | [![Compilar](https://github.com/Implantdent/dal/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/Implantdent/dal/actions/workflows/build.yml) |

El despliegue se ejecuta en

| Rama | NuGet |
|:-:|:-:|
| dev | Dal 1.0.X-dev |
| qa | Dal 1.0.X-qa |
| main | Dal 1.0.X |

## Lenguaje

C# .Net 8

## Librerías y paquetes

| Paquete | Versión |
|:-:|:-:|
| xUnit | 2.5.3 |
| Dapper | 2.1.35 |
| Entities | 1.0.11 |

## Compilar y probar

Se ejecuta el proyecto de pruebas Dal.Test