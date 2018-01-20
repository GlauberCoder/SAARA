import {TypesOfTransaction} from "../enum/variables.enum";
import {MovimentDomain} from "./moviment.domain";
import {ExchangerSymbolDomain} from "./exchanger-symbol.domain";

export class Operation{

  public currencyAmount: number;
  public Moviments: Array<MovimentDomain> = [];
  public exchangerSymbol: ExchangerSymbolDomain;

  constructor(){}

  // checkTransactions(transaction: Array<Transaction>, amount?: number, bought?: number){
  //   let amountBought = bought || 0, amountMoney = amount || 0; //TO DO: Trocar os nomes
  //   for(let item of transaction){
  //     if(item.type === TypesOfTransaction.buy){
  //       if(amountMoney > 0){
  //         amountBought += (amountMoney * item.percentage.toPercent()) / item.MovimentDomain.entryPrice;
  //         amountMoney = (amountMoney * (1 - item.percentage.toPercent()));
  //       } else {
  //         console.log('can\'t buy, don\'t have enougth money')
  //       }
  //     } else {
  //       if(amountBought > 0){
  //         amountMoney += (amountBought * (item.percentage.toPercent())) * item.MovimentDomain.entryPrice;
  //         amountBought -= (amountBought * (item.percentage.toPercent()));
  //       } else {
  //         console.log('can\'t sell, don\'t have enougth amount')
  //       }
  //     }
  //   }
  //   return {amountBought, amountMoney, profit: amountMoney/amount };
  // }
  //
  // averageSpent(){
  //   let poundedAverage;
  //   poundedAverage = this.pundedAverageOfTransactions(this.Transactions);
  //   return (poundedAverage.sum/ poundedAverage.amountOfItems).round(2);
  // }
  //
  // pundedAverageOfTransactions(array: Array<Transaction>){
  //   let sum = 0, amountOfItems = 0;
  //   for(let item of array){
  //     sum += item.MovimentDomain.entryPrice * item.MovimentDomain.investiment;
  //     amountOfItems+= item.MovimentDomain.investiment;
  //   }
  //   return {sum, amountOfItems}
  // }

}

export class MoviemntPlan{

  constructor(
    public MovimentDomain?: MovimentDomain,
    // public limitValue?: number,
    public percentage?: number,
  ){};


}
