# Tiknas Framework

[![Build and Test](https://img.shields.io/github/actions/workflow/status/btiknas/tiknas/build-and-test.yml?branch=dev&style=flat-square)](https://github.com/btiknas/tiknas/actions) ⚪ [![NuGet](https://img.shields.io/nuget/v/Volo.Abp.Core.svg?style=flat-square)](https://www.nuget.org/packages/Volo.Abp.Core)

Tiknas Framework is a fork of the ABP Framework, focusing on providing an entirely **open-source**, **boilerplate-free** infrastructure for building **modern web applications** and **APIs**. Built on **ASP.NET Core**, Tiknas Framework helps developers adhere to **best practices** while embracing the **latest technologies**.

## Getting Started

- [Quick Start Tutorial](https://tiknas.io/docs/tutorials/todo) guides you through creating a simple application using Tiknas Framework.
- [Getting Started Guide](https://tiknas.io/docs/get-started) walks you through setting up Tiknas-based solutions.
- [Web Application Development Tutorial](https://tiknas.io/docs/tutorials/book-store) provides a comprehensive tutorial for building a full-stack web application.

### Quick Start

Install the Tiknas CLI:

```bash
> dotnet tool install -g Tiknas.Cli
```

Create a new solution:

```bash
> tiknas new BookStore -u mvc -d ef
```

> See the [CLI documentation](https://tiknas.io/docs/cli) for all available options.

### UI Framework Options

<img width="500" src="docs/images/ui-options.png">

### Database Provider Options

<img width="500" src="docs/images/db-options.png">

## What Tiknas Provides

Tiknas provides a **full-stack developer experience**, allowing you to build robust applications with minimal effort.

### Architecture

<img src="docs/images/ddd-microservice-simple.png">

Tiknas Framework offers a **modular**, **layered** software architecture based on **[Domain Driven Design](https://tiknas.io/docs/architecture/domain-driven-design)** principles. It also provides the necessary infrastructure to [implement this architecture](https://tiknas.io/books/implementing-domain-driven-design).

Tiknas is well-suited for both **microservice solutions** and monolithic applications.

### Infrastructure

Tiknas Framework includes features like [Event Bus](https://tiknas.io/docs/infrastructure/event-bus), [Background Job System](https://tiknas.io/docs/infrastructure/background-jobs), [Audit Logging](https://tiknas.io/docs/infrastructure/audit-logging), [BLOB Storing](https://tiknas.io/docs/infrastructure/blob-storing), and more, to ease real-world development challenges.

### Cross-Cutting Concerns

Tiknas simplifies cross-cutting concerns like [Exception Handling](https://tiknas.io/docs/fundamentals/exception-handling), [Validation](https://tiknas.io/docs/fundamentals/validation), [Authorization](https://tiknas.io/docs/fundamentals/authorization), [Localization](https://tiknas.io/docs/fundamentals/localization), and others.

### Application Modules

Tiknas provides **modular application functionalities** out of the box, including:

- [**Account**](https://tiknas.io/docs/modules/account): User login/registration.
- [**Identity**](https://tiknas.io/docs/modules/identity): Manage users, roles, and permissions.
- [**OpenIddict**](https://tiknas.io/docs/modules/openiddict): OpenIddict integration.
- [**Tenant Management**](https://tiknas.io/docs/modules/tenant-management): Multi-tenant management for SaaS solutions.

See the [Application Modules](https://tiknas.io/docs/modules) documentation for all pre-built modules.

### Startup Templates

[Startup templates](https://tiknas.io/docs/solution-templates) are pre-built Visual Studio solution templates that enable you to start development quickly.

## Mastering Tiknas Framework Book

Learn about Tiknas Framework and modern web development techniques with the **Mastering Tiknas Framework** book. Available on [Amazon](https://www.amazon.com/dp/B097Z2DM8Q) and [Packt Publishing](https://www.packtpub.com). More details at [tiknas.io/books/mastering-tiknas-framework](https://tiknas.io/books/mastering-tiknas-framework).

![Mastering Tiknas Framework](docs/images/book-mastering-tiknas-framework.png)

## Community

### Tiknas Community Web Site

The [Tiknas Community](https://tiknas.io/community) website allows developers to share articles and knowledge about Tiknas. Get involved by contributing content!

### Blog

Stay updated with the [Tiknas Blog](https://tiknas.io/blog).

### Samples

Check out the [sample projects](https://tiknas.io/docs/samples) built with Tiknas Framework.

### Want to Contribute?

Tiknas Framework is community-driven. See [the contribution guide](https://tiknas.io/docs/contribution) if you want to get involved.

## Official Links

* [Home Website](https://tiknas.io)
  * [Get Started](https://tiknas.io/get-started)
  * [Features](https://tiknas.io/framework)
* [Documentation](https://tiknas.io/docs)
* [Samples](https://tiknas.io/docs/samples)
* [Blog](https://tiknas.io/blog)
* [Community](https://tiknas.io/community)

## Support Tiknas

If you like Tiknas Framework, please support us by starring the GitHub repository :star:.

## Discord Server

Join our Discord server to discuss ideas, report issues, showcase your work, and stay updated: [Tiknas Discord](https://discord.gg/tiknas).

---

> Use `git rebase upstream/dev` to merge the latest changes from the Tiknas dev branch. Unfortunately, there is no `upstream/master` branch at the moment.

