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

Proyecto Dal

| Paquete | Versión |
|:--|:-:|
| Dapper | 2.1.35 |
| Entities | 1.0.23 |
| Microsoft.Data.SqlClient | 5.2.1 |

Proyecto Dal.Test

| Paquete | Versión |
|:--|:-:|
| coverlet.collector | 6.0.2 |
| Microsoft.Extensions.Configuration | 8.0.0 |
| Microsoft.Extensions.Configuration.EnvironmentVariables | 8.0.0 |
| Microsoft.Extensions.Configuration.Json | 8.0.0 |
| Microsoft.NET.Test.Sdk | 17.10.0 |
| xUnit | 2.9.0 |
| xUnit.runner.visualstudio | 2.8.2 |

## Compilar y probar

Se ejecuta el proyecto de pruebas Dal.Test