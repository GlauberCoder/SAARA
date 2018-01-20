import {Component, Input, OnInit} from '@angular/core';
import {MovimentDomain} from '../../domain/moviment.domain';
import '../../extensions/number.extensions'
import {ExchangerDomain} from "../../domain/exchanger.domain";
import {CalculatorDomain} from "../../domain/calculator.domain";
import {TypesOfTransaction} from "../../enum/variables.enum";

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html',
  styleUrls: ['./calculator.component.scss']
})
export class CalculatorComponent implements OnInit {

  public amount: number;
  public entryPrice: number;
  public entryValue: number;
  public exitPrice: number;
  public exitValue: number;
  public exitPL: number;
  public exchanger: ExchangerDomain;
  public calculator: CalculatorDomain;

  constructor() {
    this.calculator = new CalculatorDomain();
    this.calculator.type = TypesOfTransaction.long;
    this.calculator.exchanger = new ExchangerDomain(0.5, 0.5);
  }

  ngOnInit() {
  }

  onSetAmount(){
      this.calculator.entryValue = !isNaN(this.calculator.calculateEntryValue()) ? this.calculator.calculateEntryValue() : null;
      this.calculator.exitValue = !isNaN(this.calculator.calculateExitValue()) ? this.calculator.calculateExitValue() : null;
      this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
      this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
  }

  onSetEntryPrice(){

    this.calculator.entryValue = !isNaN(this.calculator.calculateEntryValue()) ? this.calculator.calculateEntryValue() : null;
    this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
    this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
  }

  onSetEntryValue() {
    this.calculator.amount = !isNaN(this.calculator.calculateAmount()) ? this.calculator.calculateAmount() : null;
    this.calculator.exitValue = !isNaN(this.calculator.calculateExitValue()) ? this.calculator.calculateExitValue() : null;
    this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
    this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
  }

  onSetExitPrice(){
    this.calculator.exitValue = !isNaN(this.calculator.calculateExitValue()) ? this.calculator.calculateExitValue() : null;
    this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
    this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
  }

  onSetExitValue(){
    this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
    this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
    this.calculator.exitPrice = !isNaN(this.calculator.calculateExitPrice()) ? this.calculator.calculateExitPrice() : null;
  }

  onSetExitPL(){
    this.calculator.exitPrice = !isNaN(this.calculator.calculateExitPriceWithPL()) ? this.calculator.calculateExitPriceWithPL() : null;
    this.calculator.exitValue = !isNaN(this.calculator.calculateExitValue()) ? this.calculator.calculateExitValue() : null;
    this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
  }

  onSetExitPLPercent(){
    this.calculator.exitPrice = !isNaN(this.calculator.calculateExitPriceWithPLPercentage()) ? this.calculator.calculateExitPriceWithPLPercentage() : null;
    this.calculator.exitValue = !isNaN(this.calculator.calculateExitValue()) ? this.calculator.calculateExitValue() : null;
    this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
  }

  onSetType(){
    this.calculator.exitPL = !isNaN(this.calculator.calculateExitPL()) ? this.calculator.calculateExitPL() : null;
    this.calculator.exitPLpercent = !isNaN(this.calculator.calculateExitPLPercentage()) ? this.calculator.calculateExitPLPercentage() : null;
  }


}
