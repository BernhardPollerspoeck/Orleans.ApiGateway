# Orleans ApiGateway

This repository contains extensions for integrating Orleans with ASP.NET Core endpoints. These extensions allow you to easily map Orleans grains to HTTP routes, making it seamless to expose Orleans functionality over HTTP.

## Installation
![Nuget](https://img.shields.io/nuget/v/Orleans.ApiGateway?logo=NuGet&color=00aa00)

To use these extensions in your ASP.NET Core application, simply install the `Orleans.ApiGateway` package from NuGet:
```bash
dotnet add package Orleans.ApiGateway
```


## Usage

### Mapping Orleans Grains to HTTP Routes

Example
Here's an example of how you can use these extension methods to map an Orleans grain to an HTTP endpoint:

```csharp Program.cs
app.MapOrleans<IIotGrain, DeviceCommandDto, CommandExecutionResponseDto>(
     EHttpMethod.POST,
     "api/device/command",
     EArgumentType.JsonBody,
     async (grain, body, httpContext) => await grain.SendCommand(body));
```
In this example, IIotGrain represents your Orleans grain interface. You can specify the HTTP method (EHttpMethod), the route path, and the grain method to invoke for handling the HTTP request.

A client now can use the '***grainKey***' header to specify a grain key in string format. Optionally the '***grainType***' header can be used to create a grainId with a specified type.

## Contributing

Contributions to this repository are welcome. Feel free to open issues or pull requests for any improvements or fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

This project was inspired by the need to integrate Orleans with ASP.NET Core endpoints efficiently. Special thanks to the Orleans Discord community for their contributions and support.
