<!-- Allow this file to not have a first line heading -->
<!-- markdownlint-disable-file MD041 -->
<!-- Disable warning on emphasis after first heading -->
<!-- markdownlint-disable-file MD036 -->

<!-- inline html -->
<!-- markdownlint-disable-file MD033 -->

<div align="center">

# CryptoWatch REST API Client Library
![GitHub](https://img.shields.io/github/license/Azgrom/Cryptowatch.API)
![GitHub tag (with filter)](https://img.shields.io/github/v/tag/Azgrom/Cryptowatch.API)

This library provides a client to interact with the CryptoWatch REST API. You can access various information related to cryptocurrency markets including assets, exchanges, market data, and trading pairs.

</div>

## Table of Content
- [How it Works](#how-it-works)
- [Requirements](#requirements)
    - [Inputs](#inputs)
    - [Outputs](#outputs)
- [Example Usage](#example-usage)
- [Step-by-step Guide](#step-by-step-guide)
- [Contributing](#contributing)
    - [Bugs and Features](#bugs-and-features)
    - [Update the Source Code](#update-the-source-code)
    - [Versioning and Releases](#versioning-and-releases)
- [Notes](#notes)

## How it Works
The library sends HTTP requests to the CryptoWatch REST API and returns parsed responses. It abstracts the details of HTTP and API specifics, providing a clean and easy-to-use interface.

## Requirements

### Inputs
- API credentials (optional)
- `HttpClient` instance

### Outputs
- Parsed API responses, such as lists of assets, exchanges, markets, etc.

## Example Usage

```csharp
var api = new CryptoWatchRestApi(httpClientFactory);
var assets = await api.Assets.ListAsync();
```

## Step-by-step Guide
1. Install the CryptoWatch REST API Client Library package to your project. It is available at nuget;
2. Create an instance of the `CryptoWatchRestApi` class;
3. Use various methods like `ListAsync()` to interact with different parts of the CryptoWatch API;

## Contributing

### Bugs and Features
- Please open an issue to report a bug or request a feature.

### Update the Source Code
- Clone the repository.
- Create a new branch for your feature or bugfix.
- Commit your changes and open a Pull Request.

### Versioning and Releases
- We use [Semantic Versioning](http://semver.org/).
- For the versions available, see the [tags on this repository](https://github.com/yourusername/your-project-name/tags).

## Notes
- Ensure you have the necessary API credentials if your usage exceeds the public limits.
- The library is designed to be used with a properly configured HTTP client.


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
