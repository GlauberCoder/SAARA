export class Exchanger {

  constructor(
    public buyTax?: number,
    public sellTax?: number
  ) {}

  getBuyTax(): number {
    return this.buyTax;
  }

  getSellTax(): number {
    return this.sellTax;
  }

}
