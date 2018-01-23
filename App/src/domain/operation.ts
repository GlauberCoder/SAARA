import {TypesOfTransaction} from '../enum/variables.enum';
import {Moviment} from './moviment';
import {ExchangerSymbol} from './exchanger-symbol';
import {OperationCalculator} from './operation-calculator';

export class Operation {

  public currencyAmount: number;
  public moviments: Array<any> = [];
  public exchangerSymbol: ExchangerSymbol;

  constructor() {}

  getCurrencyAmount() {
    let sum = 0;
    for (const item of this.moviments) {
      sum = sum + (item.type * this.calculateAmount(item.entryValue, item.entryPrice, item.tax));
    }
    return sum.round(8);
  }

  calculateAmount(entryValue: number, entryPrice: number, tax: number) {
    return entryValue.reducePorcent(tax).proportionOn(entryPrice).round(8);
  }


  getAmountSpentOnTaxes() {
    let sum = 0;
    for (const item of this.moviments) {
        sum += (item.entryValue * item.tax.toPercent());
    }
    return sum.round(4);
  }

  getPercentAmountSpentOnTaxes() {
    return (this.getAmountSpentOnTaxes() / this.calculateInvestment()).round(2);
  }


  calculateProfit() {
    let sum = 0;
    for (const item of this.moviments) {
          sum = sum + (item.type * item.entryValue.reducePorcent(item.tax));
    }
    return sum.round(2);
  }

  calculateInvestment() {
    let sum = 0;
    for (const item of this.moviments){
        if (item.type === TypesOfTransaction.long) {
          sum += item.entryValue;
        }
    }
    return sum.round(2);
  }



}
