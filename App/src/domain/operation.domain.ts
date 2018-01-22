import {TypesOfTransaction} from "../enum/variables.enum";
import {MovimentDomain} from "./moviment.domain";
import {ExchangerSymbolDomain} from "./exchanger-symbol.domain";
import {CalculatorDomain} from "./calculator.domain";

export class OperationDomain{

  public currencyAmount: number;
  public moviments: Array<any> = [];
  public exchangerSymbol: ExchangerSymbolDomain;

  constructor(){}

  getCurrencyAmount(){
    let sum = 0;
    for(let item of this.moviments){
      if(item.check) {
        if (item.type === TypesOfTransaction.long) {
          sum += this.calculateAmount(item.entryValue, item.entryPrice, item.tax);
        } else {
          sum -= this.calculateAmount(item.entryValue, item.entryPrice, item.tax);
        }
      }
    }
    return sum.round(8);
  }

  calculateAmount(entryValue, entryPrice, tax){
    return (entryValue * (1 - tax.toPercent()) / (entryPrice)).round(8);
  }


  getAmountSpentOnTaxes(){
    let sum = 0;
    for(let item of this.moviments){
      if(item.check) {
        sum += (item.entryValue * item.tax.toPercent());
      }
    }
    return sum.round(4);
  }

  getPercentAmountSpentOnTaxes(){
    return (this.getAmountSpentOnTaxes()/this.calculateInvestment()).round(2);
  }


  calculateProfit(){
    let sum = 0;
    for(let item of this.moviments){
      if(item.check){
        if(item.type === TypesOfTransaction.short){
          sum +=  item.entryValue * (1 - item.tax.toPercent());
        } else {
          sum -= item.entryValue * (1 - item.tax.toPercent());
        }
      }
    }
    return sum.round(2);
  }

  calculateInvestment(){
    let sum = 0;
    for(let item of this.moviments){
      if(item.check) {
        if (item.type === TypesOfTransaction.long) {
          sum += item.entryValue;
        }
      }
    }
    return sum.round(2);
  }



}
