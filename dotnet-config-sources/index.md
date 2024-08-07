---
status: Draft
---

# .NET Configuration Architecture: AppSettings, Environment Variables, and ...

.NET apps nowdays come out-the-box with verbose configuration sources support. A just scaffolded ASP .NET app reads configuration from JSON file (`appsettings.json`), environment variables, and console arguments. Despite and partially because of that maintaining clean configuration architecture becomes a challenge. Let's walk through some commonly arising architectural puzzles and try to find a solution for them.

> 🤷 Or jump straight to the [TLDR](#tldr) to find out the decisions made.

## TLDR;

Wrapping it up, let's summarize the decision made in this article. There are essentially two solutions, for two configuration value purposes: Connectivity and Behaviour.

### Connectivity

Connectivity configuration is your db connection strings, external and internal services urls, and everything else helping you connect to another applications. There could be a ton of various connectivity configurations for a single base service. Environment variables are a good place for those configuration values. For the local debugging put your `localhost:1111` in environment variables in `launchSettings`, and for local deployment using docker put the internal `service-a` in service environment variables using `docker-compose.yml`. In production there are numerous CI systems supplying environment variables to your app, specifying a particular one is out of scope of this article.

### Behaviour

Behaviour configuration refers to configuration of things like logging and exception handling. Most of the time, app behaviour could be split for just two scenarios: Development and Production. The `appsettings.Development.json` file is a perfect source for the first case, and `appsettings.json` is perfect for the second. 

In rare cases, you may want to introduce `appsettings.Staging.json` or override some particular from your CI system.

And that's about it!

