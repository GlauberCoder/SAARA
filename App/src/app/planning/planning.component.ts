import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.scss']
})
export class PlanningComponent implements OnInit {

  public currentPrice: number;
  public amountToUse: number;
  public currencyAmount: number;

  upperLimits: Array<{limit: number, percentage: number}>;
  lowerLimits: Array<{limit: number, percentage: number}>;

  constructor() { }

  ngOnInit() {
  }

  checkSuperiorLimits(){

  }

  checkInferiorLimits(){

  }


}
