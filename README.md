# Basket Api

[![Build status](https://ci.appveyor.com/api/projects/status/ulm6oty4g46o3crh/branch/master?svg=true)](https://ci.appveyor.com/project/JamesLappin/basketapi/branch/master)
<a href="https://github.com/actions/create-release"><img alt="GitHub Actions status" src="https://github.com/actions/create-release/workflows/Tests/badge.svg"></a>

## Overview
There are four projects.
1) Basket.Api is the web api project containing the basket controller, swagger spec and the Dockerfile used to deploy the app
2) Basket.Domain contains all the domain logic required to complete the task. The database is just the InMemoryDatabase.
3) Basket.Client is all the logic to complete task 2.
4) Basket.Tests contains all the tests for the domain and the client.

## Remarks 
I have assumed that the basket will be tied to a cusomter. I have also assumed that the customer will be contained in another service.
A basket contains items. Basket items are an instance of a product. The product is what is being sold, basket item is that product in the basket. Again, I have assumed that the products come from an external service.

## Building
There are two ways to build basket api
1) Open Basket.sln in visual studio and then press ctrl-B
2) Open console are the root of the project and run `dotnet build`

## Running the web app
If you are using visual studio, press f5 and it will start up.
From the console run `dotnet run .\src\Basket.Api\Basket.Api.csproj`
Both should start the web app on the url http://localhost:55603 (however this could change). To check that it is running correctly navigate to the healthcheck url http://localhost:55603/healthcheck

## Running the tests
Within visual studio, right-click the test project and select run unit tests or `dotnet test`
In the console run `dotnet test .\src\Basket.Tests\Basket.Tests.csproj`

## Swagger
I find swagger a good way to document and test the api. I have setup swagger and the swagger UI can be found at http://localhost:55603/swagger

## CI/CD
I have setup appveyor to run on every commit. This makes sure the solution builds, the tests pass and produces the app in a container. The code for appveyor is in the ci folder. The current status can be found at https://ci.appveyor.com/project/JamesLappin/basketapi 

## Docker
Once the tests have passed, a docker image is created and uploaded to dockerhub. This can be found [here](https://hub.docker.com/r/jameslappin/basketapi/).
To run the app from docker run `docker run -it -p 55603:80 jameslappin/basketapi`
To run the app from docker run `docker run -it -p 55603:80 jameslappin/basketapi`
