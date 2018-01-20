import {ExchangerSymbolDomain} from "./exchanger-symbol.domain";
import {TypesOfTransaction} from "../enum/variables.enum";

export class MovimentDomain{

  constructor(
    public entryPrice?: number,
    public investiment?: number,
    public tax?: number,
    public type?: TypesOfTransaction,
  ){}

  // public sellPrice(): number{
  //   let value = ((this.entryPrice * (1 + this.expectedProfit.toPercent() + this.exchangerSymbol.exchanger.getBuyTax().toPercent())) / ( 1 - this.exchangerSymbol.exchanger.getSellTax().toPercent()));
  //   return value.round(2);
  // }
  //
  // public nominalGain():number{
  //   return this.sellPrice().proportionOn(this.entryPrice).overPercentage().round(2);
  // }
  //
  // public grossGainValue(): number{
  //     return (this.investiment * (1 + this.nominalGain().toPercent()));
  // }
  //
  // public sellValueWithoutTaxes():number{
  //   return (this.grossGainValue() * (1 - this.exchangerSymbol.exchanger.getSellTax().toPercent()) - (this.investiment * this.exchangerSymbol.exchanger.getBuyTax().toPercent())).round(2);
  // }
  //
  // public realGainInPercentage(): number{
  //   return (this.investiment / this.sellValueWithoutTaxes()).round(2).overPercentage();
  // }
  //
  // public expectedProfitValue(): number{
  //   return this.investiment * (1 + this.expectedProfit.toPercent());
  // }
  //
  // public finalProfit(): number{
  //   return this.sellValueWithoutTaxes() - this.investiment;
  // }
  //
  // public getExpectedFinalPercentage(finalPrice){
  //   return ((1 - (finalPrice / this.entryPrice)) * 100).round(2);
  // }
  //
  // public finalExpectedValueRealValueErrors(): number{
  //   return (1 - (this.sellValueWithoutTaxes() / this.expectedProfitValue()));
  // }
  //
  // public returnVariablesOnJson(): string{
  //   return JSON.stringify({
  //       entryPrice: this.entryPrice,
  //       investiment: this.investiment,
  //       expectedProfit: this.expectedProfit,
  //       buyTax: this.exchangerSymbol.exchanger.getBuyTax(),
  //       sellTax : this.exchangerSymbol.exchanger.getSellTax()
  //   });
  // }

}

//Operação conjunto de movimentos.
