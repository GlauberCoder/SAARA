import { ExchangerSymbol } from './exchanger-symbol';
import { TypesOfTransaction } from '../enum/variables.enum';

export class Moviment {

  constructor(
    public entryPrice?: number,
    public entryValue?: number,
    public tax?: number,
    public type?: TypesOfTransaction,
  ) {}

  valueSpentOnTaxes(): number {
    return this.entryValue * this.tax.toPercent();
  }

  movimentValueWithTaxes() {
    return this.entryValue * (1 - this.tax.toPercent());
  }

  public returnVariablesOnJson(): string {
    return JSON.stringify({
        entryPrice: this.entryPrice,
        entryValue: this.entryValue,
        tax: this.tax,
        type: this.type
    });
  }
}
