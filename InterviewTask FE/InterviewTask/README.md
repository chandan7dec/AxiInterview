# Introduction

The project consists of a simple react app that displays a grid of trades. The usual commands
`npm i` and `npm run dev` should get you up and running.

Below are a series of tasks we'd like you work through. While working through the tasks you should not need to edit the provided mock implementations.

## First Task

Add formatting to the numerical values in the grid (quantity, booking price, current price, and
profit & loss) such that they are displayed to the correct number of decimal places. The decimal
places to format the numbers to are given on the `market` property of the trade
(`quantityDecimalPlaces`, which should be used for the quantity and profit & loss, and
`priceDecimalPlaces` which should be used for the other numerical values).

## Second Task

Rather than have a static grid of trades which is populated with a hardcoded set of data, we'd like
the grid of trades to update in real time.

An interface that provides a streaming connection for trades `StreamingConnection` is included,
along with an mock in-browser implementation called `MockStreamingConnection` that simulates a
streaming network connection.

The mock streaming connection returns mock stream objects (`MockStream<T>`). You can use the `setOnUpdate` method
to set a callback function that handles the streaming updates.

Take this mock streaming connection and update the app such that the grid displays trade updates from
the mock streaming connection in real time.

Note that you will **not** need to edit the mock classes.

## Third Task

We'd like the grid to include a profit & loss column that is in the user's account currency.
The exchange rate (FX rate) will change in real time, and is also available via the `StreamingConnection`.

Add this extra column to the trade grid, using the mock streaming connection to get the required FX rates for each trade.
Note the following:

- An update to the FX rate should update the trade row, and a update to the trade from the real time connection should reflect the current FX rate
- You should not subscribe to the same FX rate more than once; if the same rate is required more than once it should be shared between the trades
- You should unsubscribe from FX rates that are no longer needed (for example if the trade that required it is deleted from the grid)
- You can assume the account currency is currency id 1

## Fourth Task

The prices on trades are changing by large increments on each update, so it looks like there is a bug in the code
relating to price generation in the mock streaming code.

Try to find and fix the bug.
