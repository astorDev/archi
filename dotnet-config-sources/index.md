---
status: Draft
---

# .NET Configuration Architecture

> Juggling AppSettings, Environment Variables, and Co.

.NET apps nowadays come out of the box with a robust set of configuration sources. A newly scaffolded ASP .NET app reads configuration from JSON files (`appsettings.json`), environment variables, and command-line arguments. Despite this and partially because of it, maintaining a clean configuration architecture becomes a challenge. Let's walk through some commonly arising architectural puzzles and try to find a solution for them.

> 🤷 Or jump straight to the [TLDR](#tldr) to find out the decisions made.

![](thumb.png)

## TLDR;

Wrapping it up, let's summarize the decision made in this article. There are essentially two solutions, for two configuration value purposes: Connectivity and Behaviour.

### Connectivity

Connectivity configuration is your db connection strings, external and internal services urls, and everything else helping you connect to other applications. There could be a ton of various connectivity configurations for a single base service. Environment variables are a good place for those configuration values. For the local debugging put your `localhost:1111` in environment variables in `launchSettings`, and for local deployment using docker put the internal `service-a` in service environment variables using `docker-compose.yml`. In production, there are numerous CI systems supplying environment variables to your app, specifying a particular one is out of the scope of this article.

### Behaviour

Behaviour configuration refers to a configuration of things like logging and exception handling. Most of the time, app behaviour could be split into just two scenarios: Development and Production. The `appsettings.Development.json` file is a perfect source for the first case, and `appsettings.json` is perfect for the second. 

In rare cases, you may want to introduce `appsettings.Staging.json` or override some particular config from your CI system.

And that's about it!

