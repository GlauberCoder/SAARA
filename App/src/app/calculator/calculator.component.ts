import { Component, OnInit } from '@angular/core';
import {OperationModel} from '../../models/operations.model';
import '../../extensions/number.extensions'

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit {

  public buyPrice;
  public buyAmount;
  public expectedProfit;
  public buyTax;
  public sellTax;
  public projections: Array<OperationModel> = [];

  public projection: OperationModel;

  constructor() {
    this.getProjectionsOnLocalStorage();
    this.calculateAmountOfCurrency(this.projections);
  }

  ngOnInit() {
  }

  calculateProjection() {
    localStorage.clear();
    this.projection = new OperationModel(parseFloat(this.buyPrice), parseFloat(this.buyAmount), parseFloat(this.expectedProfit), parseFloat(this.buyTax), parseFloat(this.sellTax));
    this.saveProjectionOnLocalStorage();
  }


  saveProjectionOnLocalStorage(){
    this.projections.push(this.projection);
    localStorage.setItem('projections', JSON.stringify(this.projections));
  }

  clearLocalStorage(){
    localStorage.clear();
  }

  getProjectionsOnLocalStorage(){
    if(!localStorage.getItem('projections')){
      this.projections = [];
    } else{
      let items = JSON.parse(localStorage.getItem('projections'));
      for (let item of items ){
        this.projections.push(new OperationModel(parseFloat(item.buyPrice), parseFloat(item.buyAmount), parseFloat(item.excpectedProfit), parseFloat(item.buyTax), parseFloat(item.sellTax)));
      }
    }
  }

  public calculateAmountOfCurrency(values: Array<OperationModel>){
    let sum = 0;
    for(let item of values){
      sum += item.buyAmount/item.buyPrice;
    }
    return sum;
  }

  public calculateAvaregeCurrencyPrice(values: Array<OperationModel>){
    let sum = 0, weight = 0;
    for(let item of values){
      sum += item.buyPrice * item.buyAmount;
      weight += item.buyAmount;
    }
    return sum / weight;
  }

  public calculateOverallProjectionProfit(values: Array<OperationModel>){
    let sum = 0;
    for(let item of values){
      sum += item.finalProfit();
    }
    return sum ;
  }


}
