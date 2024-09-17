
# Introduction

Contained in this folder is a .NET Core console application called `TradeEngine`. This application represents a service that would be part of a larger system. `TradeEngine` works with various state:

- Markets represent the real-world financial markets that can be traded
- Trades are created when a user decides to buy or sell on a market
- The price of a market changes frequently. Each update affects the valuation of all of the trades on the market
- These valuations are sent to downstream services (which are not included here)
- It offers an api to create customers and retrieve them

This project is a toy version of that kind of system. `Trade`s and `Market`s are created up-front. The valuation step is minimal: it should re-calculate an `OpenTradeEquity` amount every time the trade's market's price changes.

# The Open Trade Equity Calculation

## Background

A user will decide to trade based on whether the price of a market is going to go up or down. If the price is going to go up, the user can get a profit by buying now and selling later. After buying and before selling, the trade has an equity proportional to the change in the price of the market and the size of the trade that's been made.

The size of the trade has been abstracted as its `Quantity`. A positive quantity represents a buy trade and a negative quantity a sell trade.

A market has a property called `QuantityConversionFactor` which scales the quantity of the trades on the market. 

## Calculation

Open trade equity is calculated as follows:

`OTE = (market price - trade booking price) * trade quantity * market quantity conversion factor`

e.g. for the following situation:

```
trade booking price = 1.12
trade quantity = 0.5

market quantity conversion factor = 10

market price = 1.23
```

The OTE would be calculated as: 

```
price difference = 1.23 - 1.12 = 0.11
actual trade quantity = 0.5 * 10 = 5

OTE = 0.11 * 5 = 0.55
```

This would mean that if the user will close out the trade at that point, they will make a profit of 0.55.