import React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { Trade } from "./streaming/entities/Trade";

export interface Props {
  trades: Trade[];
}

export function OpenTradesGrid(props: Props) {

  const formatNumber = (data: number, decmialplaces: number) => {
    return  data.toFixed(decmialplaces);

  }

  return (
    <TableContainer>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Booking Date</TableCell>
            <TableCell>Market</TableCell>
            <TableCell align="right">Quantity</TableCell>
            <TableCell align="right">Booking Price</TableCell>
            <TableCell align="right">Current Price</TableCell>
            <TableCell align="right">Profit &amp; Loss</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {props.trades.map((trade) => (
            <TableRow key={trade.id}>
              <TableCell>{trade.bookingDate.toLocaleString()}</TableCell>
              <TableCell>{trade.market.name}</TableCell>
              <TableCell align="right">{formatNumber(trade.quantity, trade?.market?.quantityDecimalPlaces)}</TableCell>
              <TableCell align="right">{formatNumber(trade.bookingPrice, trade?.market?.priceDecimalPlaces)}</TableCell>
              <TableCell align="right">{formatNumber(trade.currentPrice, trade?.market?.priceDecimalPlaces)}</TableCell>
              <TableCell align="right">{formatNumber(trade.profitAndLoss, trade?.market?.quantityDecimalPlaces)}</TableCell>
             
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
