import {ExchangerSymbolDomain} from "./exchanger-symbol.domain";
import {TypesOfTransaction} from "../enum/variables.enum";

export class MovimentDomain{

  constructor(
    public entryPrice?: number,
    public entryValue?: number,
    public tax?: number,
    public type?: TypesOfTransaction,
  ){}

  valueSpentOnTaxes(): number{
    return this.entryValue * this.tax.toPercent();
  }

  movimentValueWithTaxes(){
    return this.entryValue * (1 - this.tax.toPercent());
  }

  //
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

  public returnVariablesOnJson(): string{
    return JSON.stringify({
        entryPrice: this.entryPrice,
        entryValue: this.entryValue,
        tax: this.tax,
        type: this.type
    });
  }

}

//Operação conjunto de movimentos.
