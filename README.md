# CryptoWatch REST API Client Library

This library provides a client to interact with the CryptoWatch REST API. You can access various information related to cryptocurrency markets including assets, exchanges, market data, and trading pairs.

## Installation

Include the required package in your project.

## Usage

### Creating an Instance

```csharp
var api = new CryptoWatchRestApi(httpClientFactory);
```

### Assets API

#### List Assets

```csharp
var assets = await api.Assets.ListAsync();
```

#### List Assets with Limit

```csharp
var assets = await api.Assets.ListAsync(limit: 50);
```

#### Get Asset Details

```csharp
var assetDetails = await api.Assets.DetailsAsync("bitcoin");
```

### Exchanges API

#### List Exchanges

```csharp
var exchanges = await api.Exchanges.ListAsync();
```

#### Get Exchange Details

```csharp
var exchange = await api.Exchanges.DetailsAsync("kraken");
```

### Markets API

#### Get Markets for an Exchange

```csharp
var response = await api.Markets.ExchangeAsync("kraken");
```

#### List Markets with Limit

```csharp
var markets = await api.Markets.ListAsync(limit: 50);
```

#### Get Order Book

```csharp
var orderBook = await api.Markets.OrderBookAsync("kraken", "btceur");
```

... and many more methods available.

### Pairs API

#### List Pairs

```csharp
var pairs = await api.Pairs.ListAsync();
```

#### Get Pair Details

```csharp
var pairDetails = await api.Pairs.DetailsAsync("btceur");
```

## Contributing

Please refer to the project's contribution guidelines.

## License

This project is licensed under the MIT License.
