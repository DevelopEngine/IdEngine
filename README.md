# ModulEngine

## Part of **DevelopEngine**

A simple and reusable primitive for reliably unique identifiers usable in any application. IDs are implicitly convertible to `string`, `Guid`, and can even be created from file paths.

### IdEngine.Json

This package provides a `Newtonsoft.Json.JsonConverter` implementation for the `IdEngine.Id` type, in case you have issues with (de)serializing, especially useful in ASP.NET Core apps.
