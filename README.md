# Tiknas Framework

[![Build and Test](https://img.shields.io/github/actions/workflow/status/btiknas/tiknas/build-and-test.yml?branch=dev&style=flat-square)](https://github.com/btiknas/tiknas/actions) ⚪ [![NuGet](https://img.shields.io/nuget/v/Volo.Abp.Core.svg?style=flat-square)](https://www.nuget.org/packages/Volo.Abp.Core)

Tiknas Framework is a fork of the ABP Framework, focusing on providing an entirely **open-source**, **boilerplate-free** infrastructure for building **modern web applications** and **APIs**. Built on **ASP.NET Core**, Tiknas Framework helps developers adhere to **best practices** while embracing the **latest technologies**.

## Getting Started

- [Quick Start Tutorial](https://tiknas.de/docs/tutorials/todo) guides you through creating a simple application using Tiknas Framework.
- [Getting Started Guide](https://tiknas.de/docs/get-started) walks you through setting up Tiknas-based solutions.
- [Web Application Development Tutorial](https://tiknas.de/docs/tutorials/book-store) provides a comprehensive tutorial for building a full-stack web application.

### Quick Start

Install the Tiknas CLI:

```bash
> dotnet tool install -g Tiknas.Cli
```

Create a new solution:

```bash
> tiknas new BookStore -u mvc -d ef
```

> See the [CLI documentation](https://tiknas.de/docs/cli) for all available options.

### UI Framework Options

<img width="500" src="docs/en/images/ui-options.png">

### Database Provider Options

<img width="500" src="docs/en/images/db-options.png">

## What Tiknas Provides

Tiknas provides a **full-stack developer experience**, allowing you to build robust applications with minimal effort.

### Architecture

<img src="docs/en/images/ddd-microservice-simple.png">

Tiknas Framework offers a **modular**, **layered** software architecture based on **[Domain Driven Design](https://tiknas.de/docs/architecture/domain-driven-design)** principles. It also provides the necessary infrastructure to [implement this architecture](https://tiknas.de/books/implementing-domain-driven-design).

Tiknas is well-suited for both **microservice solutions** and monolithic applications.

### Infrastructure

Tiknas Framework includes features like [Event Bus](https://tiknas.de/docs/infrastructure/event-bus), [Background Job System](https://tiknas.de/docs/infrastructure/background-jobs), [Audit Logging](https://tiknas.de/docs/infrastructure/audit-logging), [BLOB Storing](https://tiknas.de/docs/infrastructure/blob-storing), and more, to ease real-world development challenges.

### Cross-Cutting Concerns

Tiknas simplifies cross-cutting concerns like [Exception Handling](https://tiknas.de/docs/fundamentals/exception-handling), [Validation](https://tiknas.de/docs/fundamentals/validation), [Authorization](https://tiknas.de/docs/fundamentals/authorization), [Localization](https://tiknas.de/docs/fundamentals/localization), and others.

### Application Modules

Tiknas provides **modular application functionalities** out of the box, including:

- [**Account**](https://tiknas.de/docs/modules/account): User login/registration.
- [**Identity**](https://tiknas.de/docs/modules/identity): Manage users, roles, and permissions.
- [**OpenIddict**](https://tiknas.de/docs/modules/openiddict): OpenIddict integration.
- [**Tenant Management**](https://tiknas.de/docs/modules/tenant-management): Multi-tenant management for SaaS solutions.

See the [Application Modules](https://tiknas.de/docs/modules) documentation for all pre-built modules.

### Startup Templates

[Startup templates](https://tiknas.de/docs/solution-templates) are pre-built Visual Studio solution templates that enable you to start development quickly.
<!---
## Mastering Tiknas Framework Book

Learn about Tiknas Framework and modern web development techniques with the **Mastering Tiknas Framework** book. Available on [Amazon](https://www.amazon.com/dp/B097Z2DM8Q) and [Packt Publishing](https://www.packtpub.com). More details at [tiknas.de/books/mastering-tiknas-framework](https://tiknas.de/books/mastering-tiknas-framework).

![Mastering Tiknas Framework](docs/images/book-mastering-tiknas-framework.png)

## Community

### Tiknas Community Web Site

The [Tiknas Community](https://tiknas.de/community) website allows developers to share articles and knowledge about Tiknas. Get involved by contributing content!

### Blog

Stay updated with the [Tiknas Blog](https://tiknas.de/blog).

### Samples

Check out the [sample projects](https://tiknas.de/docs/samples) built with Tiknas Framework.

### Want to Contribute?

Tiknas Framework is community-driven. See [the contribution guide](https://tiknas.de/docs/contribution) if you want to get involved.


## Official Links

* [Home Website](https://tiknas.de)
  * [Get Started](https://tiknas.de/get-started)
  * [Features](https://tiknas.de/framework)
* [Documentation](https://tiknas.de/docs)
* [Samples](https://tiknas.de/docs/samples)
* [Blog](https://tiknas.de/blog)
* [Community](https://tiknas.de/community)
-->

## Support Tiknas

If you like Tiknas Framework, please support us by starring the GitHub repository :star:.

<!---
## Discord Server

Join our Discord server to discuss ideas, report issues, showcase your work, and stay updated: [Tiknas Discord](https://discord.gg/tiknas).
--->
---

> We use `git rebase upstream/dev` to merge the latest changes from the ABP Framework to our Tiknas Framework. Unfortunately, there is no `upstream/master` branch at the moment.
