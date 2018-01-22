import {Component, OnDestroy, OnInit} from '@angular/core';
import {OperationDomain} from "../../domain/operation.domain";
import {PlanningCalculatorService} from "../../providers/planning-calculator.service";
import {MovimentDomain} from "../../domain/moviment.domain";

@Component({
  selector: 'app-planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.scss']
})
export class PlanningComponent implements OnInit, OnDestroy {

  public currentPrice: number;
  public amountToUse: number;
  public currencyAmount: number;

  public planner: OperationDomain;
  public entryPrice: number;
  public investiment: number;
  public percentage: number;
  public transactionType: string;
  public moviments: Array<MovimentDomain> = [];


  public unsubGetCalculatorVariables;

  constructor(public planningCalculatorService: PlanningCalculatorService) {
    this.planner = new OperationDomain();

    this.unsubGetCalculatorVariables = this.planningCalculatorService.passingData.subscribe( (data) => {
      data.check = true;
      this.moviments.push(data);
      this.planner.moviments.push(data);
    });
  }

  ngOnInit() {
  }

  ngOnDestroy(){
    this.unsubGetCalculatorVariables.unsubscribe();
  }

  moveUpItem(index){
    if(index > 0){
      let item = this.planner.moviments.splice(index, 1)[0];
      this.planner.moviments.splice(index - 1, 0, item);
    }
  }

  moveDownItem(index){
    if(index < this.planner.moviments.length){
      let item = this.planner.moviments.splice(index, 1)[0];
      this.planner.moviments.splice(index + 1, 0, item);
    }
  }

  deleteItem(index){
    this.planner.moviments.splice(index, 1);
  }


}
