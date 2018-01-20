import {ExchangerDomain} from "./exchanger.domain";
import {TypesOfTransaction} from "../enum/variables.enum";

export class CalculatorDomain{

  public amount: any;
  public entryPrice: any;
  public entryValue: any;
  public exitPrice: any;
  public exitValue: any;
  public exitPL: any;
  public exitPLpercent: any;
  public exchanger: ExchangerDomain;
  public type: TypesOfTransaction;

  constructor(){}

  calculateEntryValue(){// Amount, EntryPrice, EntryValue
    return (this.amount * this.entryPrice * (1 + this.exchanger.getBuyTax().toPercent()) ).round(2);
  }

  calculateAmount(){
    return (this.entryValue * (1 - this.exchanger.getBuyTax().toPercent()) / (this.entryPrice)).round(8);
  }

  public calculateExitPrice(){
    let exitPrice = (( this.exitValue/this.entryValue ) / (1 - this.exchanger.getSellTax().toPercent()));
    // let value = (this.entryPrice * (1 - this.exchanger.getBuyTax().toPercent())) / ( )
    // console.log(value);
    return (exitPrice * this.entryPrice).round(2);
  }

  public calculateExitValue(): number{
    let value = ((this.exitPrice * (1 - this.exchanger.getBuyTax().toPercent())));
    return ((value/this.entryPrice) * this.entryValue).round(2);
  }

  public calculateExitPL(): number {
    let value = ((this.exitValue * (1 + this.exchanger.getBuyTax().toPercent())) / (1 - this.exchanger.getSellTax().toPercent()));
    return this.type === TypesOfTransaction.long ? this.calculatePL(value) : -1 * this.calculatePL(value);
  }

  calculatePL(value: number){
    return (value - this.entryValue).round(2);
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






}