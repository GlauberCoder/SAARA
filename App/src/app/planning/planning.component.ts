import { Component, OnInit } from '@angular/core';
import {Operation} from "../../domain/operation.domain";
import { TypesOfTransaction } from "../../enum/variables.enum";
import {MovimentDomain} from "../../domain/moviment.domain";

@Component({
  selector: 'app-planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.scss']
})
export class PlanningComponent implements OnInit {

  public currentPrice: number;
  public amountToUse: number;
  public currencyAmount: number;

  public planner: Operation;
  public entryPrice: number;
  public investiment: number;
  public percentage: number;
  public transactionType: string;

  constructor() {
    this.planner = new Operation();
  }

  ngOnInit() {
  }

  // addTransaction(){
  //   this.planner.Transactions.push( this.createNewTransaction(this.entryPrice, this.investiment, this.percentage, this.transactionType));
  // }
  //
  // createNewTransaction(entryPrice: number, investiment: number, percentage: number, transactionType: string){
  //   return new Transaction(new MovimentDomain(entryPrice, investiment) , percentage,
  //     transactionType === 'buy' ? TypesOfTransaction.buy : TypesOfTransaction.stop);
  // }
  //
  // calculateProjection(){
  //   this.planner.checkTransactions(this.planner.Transactions, 100, 0);
  // }

}
