import {TypesOfTransaction} from "../enum/variables.enum";
import {EventEmitter} from "@angular/core";
import {ExchangerSymbolDomain} from './exchanger-symbol.domain';

export class CalculatorDomain{

  public amount: any;
  public entryPrice: any;
  public entryValue: any;
  public exitPrice: any;
  public exitValue: any;
  public exitPL: any;
  public exitPLpercent: any;
  public exchangers: Array<ExchangerSymbolDomain>;
  public exchangerSymbol: ExchangerSymbolDomain;
  public type: TypesOfTransaction;


  constructor(){}

  calculateEntryValue(){// Amount, EntryPrice, EntryValue
    return (this.amount * this.entryPrice * (1 + this.exchangerSymbol.exchanger.getBuyTax().toPercent()) ).round(2);
  }

  calculateAmount(){
    return (this.entryValue * (1 - this.exchangerSymbol.exchanger.getBuyTax().toPercent()) / (this.entryPrice)).round(8);
  }

  public calculateExitPrice(){
    let exitPrice = (( this.exitValue/this.entryValue ) * (1 - this.exchangerSymbol.exchanger.getBuyTax().toPercent()) / (1 - this.exchangerSymbol.exchanger.getSellTax().toPercent()));
    return (exitPrice * this.entryPrice).round(2);
  }

  public calculateExitValue(): number{
    let value = ((this.exitPrice));
    return ((value/this.entryPrice) * this.entryValue).round(2);
  }

  public calculateExitPL(): number {
    let value = ((this.exitValue ));
    return this.type === TypesOfTransaction.long ? this.calculatePL(value) : -1 * this.calculatePL(value);
  }

  calculatePL(value: number){
    return (value * ( 1 - this.exchangerSymbol.exchanger.getBuyTax().toPercent() )- this.entryValue * ( 1 - this.exchangerSymbol.exchanger.getSellTax().toPercent())).round(2);
  }

  public calculateExitPLPercentage(){
    return this.type === TypesOfTransaction.long ? this.calculatePLPercentage() : (-1 * this.calculatePLPercentage());
  }

  public calculatePLPercentage(){
    return (( (this.exitPrice/this.entryPrice) * 100) - 100).round(2);
  }

  public calculateExitPriceWithPL(): number{
    return (((parseFloat(this.entryValue) + parseFloat(this.exitPL)) / this.entryValue) * this.entryPrice).round(2);
  }

  public calculateExitPriceWithPLPercentage(): number{
    return (parseFloat(this.entryPrice) * ( 1 + parseFloat(this.exitPLpercent).toPercent())).round(2);
  }

  public sendMoviment(){
    return {
      entryPrice: parseFloat(this.entryPrice),
      entryValue: this.entryValue,
      tax: this.exchangerSymbol.exchanger.getBuyTax(),
      type: this.type
    };
  }

}
