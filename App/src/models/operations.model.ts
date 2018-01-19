export class OperationModel{

  private _expectedProfitInPercent: number;
  private _sellTaxInPercent: number;
  private _buyTaxInPercent: number;

  constructor(
    public buyPrice?: number,
    public buyAmount?: number,
    public excpectedProfit?: number,
    public buyTax?: number,
    public sellTax? : number
  ){
    this._expectedProfitInPercent = excpectedProfit.toPercent();
    this._sellTaxInPercent = sellTax.toPercent();
    this._buyTaxInPercent = buyTax.toPercent();
  }

  public sellPrice(): number{
    let value = ((this.buyPrice * (1 + this._expectedProfitInPercent + this._buyTaxInPercent)) / ( 1 - this._sellTaxInPercent));
    return value.round(2);
  }

  public nominalGain():number{
    return this.sellPrice().proportionOn(this.buyPrice).overPercentage().round(2);
  }

  public grossGainValue(): number{
    return (this.buyAmount * (1 + this.nominalGain().toPercent()));
  }

  public sellValueWithoutTaxes():number{
    return (this.grossGainValue() * (1 - this._sellTaxInPercent) - (this.buyAmount * this._buyTaxInPercent)).round(2);
  }

  public realGainInPercentage(): number{
    return (this.buyAmount / this.sellValueWithoutTaxes()).round(2).overPercentage();
  }

  public expectedProfitValue(): number{
    return this.buyAmount * (1 + this._expectedProfitInPercent);
  }

  public finalProfit(): number{
    return this.sellValueWithoutTaxes() - this.buyAmount ;
  }

  public finalExpectedValueRealValueErrors(): number{
    return (1 - (this.sellValueWithoutTaxes() / this.expectedProfitValue()));
  }

  public returnVariablesOnJson(): string{
    return JSON.stringify({
        buyPrice: this.buyPrice,
        buyAmount: this.buyAmount,
        excpectedProfit: this.excpectedProfit,
        buyTax: this.buyTax,
        sellTax : this.sellTax
    });
  }

}
