import React from 'react';
import './App.css';
import 'fontsource-roboto';
import { OpenTradesGrid } from './OpenTradesGrid';
import { Trade } from './streaming/entities/Trade';
import { MockStreamingConnection } from './streaming/mock/MockStreamingConnection';

const trades: Trade[] = [
  { 
    id: 1, 
    bookingDate: new Date(2020, 8, 10, 10, 43, 22), 
    market: { 
      id: 1, 
      name: "GBP/USD", 
      quantityDecimalPlaces: 2, 
      priceDecimalPlaces: 5 
    },
    quantity: 10,
    bookingPrice: 1.23531,
    currentPrice: 1.23452,
    profitAndLoss: 4.23,
    currency: {
      id: 1,
      code: "GBP"
    }
   },
   { 
    id: 43, 
    bookingDate: new Date(2020, 8, 9, 16, 10, 49), 
    market: { 
      id: 243, 
      name: "EUR/USD", 
      quantityDecimalPlaces: 2, 
      priceDecimalPlaces: 5 
    },
    quantity: 17.5400,
    bookingPrice: 1.17487,
    currentPrice: 1.17417,
    profitAndLoss: -7.451,
    currency: {
      id: 1,
      code: "GBP"
    }
   },
   { 
    id: 64574, 
    bookingDate: new Date(2020, 8, 11, 13, 1, 5), 
    market: { 
      id: 1, 
      name: "GBP/USD", 
      quantityDecimalPlaces: 2, 
      priceDecimalPlaces: 5 
    },
    quantity: 4.5,
    bookingPrice: 1.23741,
    currentPrice: 1.23452,
    profitAndLoss: -2.1474101,
    currency: {
      id: 1,
      code: "GBP"
    }
   }
];

function App() {
 const obj = new MockStreamingConnection();
 obj.connect
  
  return (
    <div className="App">
      <OpenTradesGrid trades={trades} />
    </div>
  );
}

export default App;
