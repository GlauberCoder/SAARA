import { TypesOfTransaction } from '../enum/variables.enum';
import { ExchangerSymbol } from './exchanger-symbol';

export class OperationCalculator {

  public amount: number;
  public entryPrice: number;
  public entryValue: number;
  public exitPrice: number;
  public exitValue: number;
  public exitPL: number;
  public exitPLpercent: number;
  public exchangerSymbol: ExchangerSymbol;
  public type: TypesOfTransaction;


  constructor() {}

  calculateEntryValue() {
    return (this.amount * this.entryPrice.increasePorcent(this.exchangerSymbol.exchanger.getBuyTax())).round(2);
  }

  calculateAmount() {
    return (this.entryValue.reducePorcent(this.exchangerSymbol.exchanger.getBuyTax()) / (this.entryPrice)).round(8);
    // return this.entryValue.reducePorcent(this.exchangerSymbol.exchanger.getBuyTax());
    // return this.entryValue * (1 - this.exchangerSymbol.exchanger.getBuyTax().toPercent());
  }

  public calculateExitPrice() {
    const exitProportion = this
                                .exitValue
                                .proportionOn(this.entryValue)
                                .reducePorcent(this.exchangerSymbol.exchanger.getBuyTax())
                                .increasePorcent(this.exchangerSymbol.exchanger.getSellTax());
    return (exitProportion * this.entryPrice).round(2);
  }

  public calculateExitValue(): number {
    return (this.exitPrice.proportionOn(this.entryPrice) * this.entryValue).round(2);
  }

  public calculateExitPL(): number {
    return this.type * this.calculatePL(this.exitValue);
  }

  calculatePL(value: number) {
    const buyValue = value.reducePorcent(this.exchangerSymbol.exchanger.getBuyTax());
    const entryValue = this.entryValue.reducePorcent(this.exchangerSymbol.exchanger.getSellTax());
    return (buyValue - entryValue).round(2);
  }

  public calculateExitPLPercentage() {
    return this.type * this.calculatePLPercentage();
  }

  public calculatePLPercentage() {
    return (this.exitPrice.proportionOn(this.entryPrice).toPorcent() - 100).round(2);
  }

  public calculateExitPriceWithPL(): number {
    return ((this.entryValue + this.exitPL).proportionOn(this.entryValue) * this.entryPrice).round(2);
  }

  public calculateExitPriceWithPLPercentage(): number {
    return (this.entryPrice.increasePorcent(this.exitPLpercent)).round(2);
  }

  public sendMoviment() {
    return {
      entryPrice: this.entryPrice,
      entryValue: this.entryValue,
      tax: this.exchangerSymbol.exchanger.getBuyTax(),
      type: this.type
    };
  }

}
