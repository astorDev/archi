---
status: Draft
---

# .NET Configuration Architecture

> Juggling AppSettings, Environment Variables, and Co.

.NET apps nowadays come out of the box with a robust set of configuration sources. A newly scaffolded ASP .NET app reads configuration from JSON files (`appsettings.json`), environment variables, and command-line arguments. Despite this and partially because of it, maintaining a clean configuration architecture becomes a challenge. Let's walk through some commonly arising architectural puzzles and try to find a solution for them.

> 🤷 Or jump straight to the [TLDR](#tldr) to find out the decisions made.

![](thumb.png)

## Database and AppSettings

Let's say we are developing an ASP .NET application. We've just scaffolded it via `dotnet new web`. Now we want to connect it to a database, so we need to put our connection string somewhere. Obviously, we can't put it straight in code, as we'll have different databases in development and production. At the same time, a brief look at the folder structure hints that `appsettings.json` and `appsettings.Development.json` can easily handle the use case. Indeed, if we'll add in `appsettings.json`

```jsonc
{
    "ConnectionStrings": {
        "Db" : "ProductionDbConnectionString"
    },
    // 
}
```

and in `appsettings.Development.json`:

```jsonc
{
    "ConnectionStrings": {
        "Db" : "DevelopmentDbConnectionString"
    },
    // 
}
```

and then use that connection string:

```csharp
var dbConnectionString = app.Configuration.GetConnectionString("Db");
app.Logger.LogInformation("Db Connection string: {dbConnectionString}", dbConnectionString);
```

We'll be able to get the different connection strings depending on the environment we run in:

```sh
dotnet run --environment=Development # logs `Db Connection string: DevelopmentDbConnectionString`
```

```sh
dotnet run --environment=Production # logs `Db Connection string: ProductionDbConnectionString`
```

However, there are a few problems with this approach:

1. It's not secure to store connection strings in the code, since any developer will have easy access to the production database password.
2. It's entirely possible that there will be a ton of environments. In fact, in my practice, I've seen repositories having `appsettings.Local.json`, `appsettings.Development.json`, `appsettings.Local.json`, `appsettings.QA1.json`, `appsettings.QA2.json`, `appsettings.Staging.json`, `appsettings.json` files in every single project. Of course, that resulted in a lot of duplications and general maintenance mess.
3. Since `appsetting.json` serves as both production and default configuration source, forgetting to override something may result in connecting and potentially modifying something on production during debugging.
4. Blurs the list of configuration values that need to be specified when configuring a new environment. Sometimes resulting in false-positive configuration as in point #3.

## Environment Variables for Connectivity

## Onboard Developers with LaunchSettings

## Do we need AppSettings then?

## TLDR;

Wrapping it up, let's summarize the decision made in this article. There are essentially two solutions for two configuration value purposes: Connectivity and Behaviour.

### Connectivity

Connectivity configuration is your db connection strings, external and internal services urls, and everything else helping you connect to other applications. There could be a ton of various connectivity configurations for a single base service. Environment variables are a good place for those configuration values. For the local debugging put your `localhost:1111` in environment variables in `launchSettings`, and for local deployment using docker put the internal `service-a` in service environment variables using `docker-compose.yml`. In production, there are numerous CI systems supplying environment variables to your app, specifying a particular one is out of the scope of this article.

### Behaviour

Behaviour configuration refers to a configuration of things like logging and exception handling. Most of the time, app behaviour could be split into just two scenarios: Development and Production. The `appsettings.Development.json` file is a perfect source for the first case, and `appsettings.json` is perfect for the second. 

In rare cases, you may want to introduce `appsettings.Staging.json` or override some particular config from your CI system.

And that's about it!

