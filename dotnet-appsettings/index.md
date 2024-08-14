# .NET Configuration Architecture: AppSettings

> Do we even need them?

In the [previous article](https://medium.com/p/87526b9fbc68) we've started discussing different configuration sources present out of the box in .NET applications. We've found a few pitfalls of storing configuration values like database connection strings in the `appsettings.*.json` files and figured out a better alternative: environment variables. As we discovered the nice world of using environment variables for configuration a question arose: Where is the place for `apppsettings` then? Let's try to answer this now!

> We'll also summarize both article's discoveries in the [TLDR;](#tldr) in the end 😉

## Story of `IsDevelopmentLike`

## Do we need AppSettings then?

## TLDR;

During our journey in different configuration sources, we've discovered two primary configuration purposes: Connectivity and Behaviour.

### Connectivity

Connectivity configuration is your db connection strings, external and internal services urls, and everything else helping you connect to other applications. There could be a ton of various connectivity configurations for a single service. Environment variables are a good place for those configuration values. For the local debugging put your `localhost:1111` in environment variables in `launchSettings`, and for local deployment using docker put the internal `service-a` in service environment variables using `docker-compose.yml`. In production, there are numerous CI systems supplying environment variables to your app, so I'll leave the choice up to you 😉

### Behaviour

Behaviour configuration refers to a configuration of things like logging and exception handling. Most of the time, app behaviour could be split into just two scenarios: Development and Production. The `appsettings.Development.json` file is a perfect source for the first case, and `appsettings.json` is perfect for the second. 

--- 

That's almost everything you need to know about architecting configuration sources in .NET. Don't forget to check the [first article](https://medium.com/p/87526b9fbc68) and also the in-depth discussion of [Environment Variables](https://medium.com/me/stats/post/d6b4ea6cff9f). 

And, as always ... Claps are appreciated! 👏
