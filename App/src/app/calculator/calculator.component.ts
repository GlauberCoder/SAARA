import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {Moviment} from '../../domain/moviment';
import '../../extensions/number.extensions';
import {Exchanger} from '../../domain/exchanger';
import {OperationCalculator} from '../../domain/operation-calculator';
import {TypesOfTransaction} from '../../enum/variables.enum';
import {PlanningCalculatorService} from '../../providers/planning-calculator.service';
import { ExchangerService } from '../../providers/exchanger.service';
import { ExchangerSymbol } from '../../domain/exchanger-symbol';

@Component({
  selector: 'app-calculator',
  templateUrl: './calculator.component.html'
})
export class CalculatorComponent implements OnInit {
  public exchangers: Array<ExchangerSymbol>;
  public calculator: OperationCalculator;

  constructor(public planningCalculatorService: PlanningCalculatorService, public exchangerService: ExchangerService) {
    this.calculator = new OperationCalculator();
    this.calculator.type = TypesOfTransaction.long;
    this.exchangers = this.exchangerService.getExchangers();
    this.calculator.exchangerSymbol = this.exchangers[0];
  }

  ngOnInit() {
  }

  private returnIfIsNumber(value): number {
    return !isNaN(value) ? value : null;
  }

  private calculateExitPL() {
    this.calculator.exitPL = this.returnIfIsNumber(this.calculator.calculateExitPL());
  }

  private calculateEntryValue() {
    this.calculator.entryValue = this.returnIfIsNumber(this.calculator.calculateEntryValue());
  }

  private calculateExitValue() {
    this.calculator.exitValue = this.returnIfIsNumber(this.calculator.calculateExitValue());
  }

  private calculateExitPLpercent() {
    this.calculator.exitPLpercent = this.returnIfIsNumber(this.calculator.calculateExitPLPercentage());
  }

  private calculateAmount() {
    this.calculator.amount = this.returnIfIsNumber(this.calculator.calculateAmount());
  }

  private calculateExitPrice(value) {
    this.calculator.exitPrice = this.returnIfIsNumber(value);
  }

  onSetAmount() {
    this.calculateEntryValue();
    this.calculateExitValue();
    this.calculateExitPL();
    this.calculateExitPLpercent();
  }

  onSetEntryPrice() {
    this.calculateEntryValue();
    this.calculateExitPL();
    this.calculateExitPLpercent();
  }

  onSetEntryValue() {
    this.calculateAmount();
    this.calculateExitValue();
    this.calculateExitPL();
    this.calculateExitPLpercent();
  }

  onSetExitPrice() {
    this.calculateExitValue();
    this.calculateExitPL();
    this.calculateExitPLpercent();
  }

  onSetExitValue() {
    this.calculateExitPL();
    this.calculateExitPLpercent();
    this.calculateExitPrice(this.calculator.calculateExitPrice());
  }

  onSetExitPL() {
    this.calculateExitPrice(this.calculator.calculateExitPriceWithPL());
    this.calculateExitValue();
    this.calculateExitPLpercent();
  }

  onSetExitPLPercent() {
    this.calculateExitPrice(this.calculator.calculateExitPriceWithPLPercentage());
    this.calculateExitValue();
    this.calculateExitPL();
  }

  onSetType() {
    this.calculateExitPL();
    this.calculateExitPLpercent();
  }

  onSetExchanger(value) {
    this.calculator.exchangerSymbol = this.exchangers[value];
    this.calculateEntryValue();
    this.calculateExitValue();
    this.calculateExitPL();
    this.calculateExitPLpercent();
  }

  sendVariables() {
    this.planningCalculatorService.passingData.emit(this.calculator.sendMoviment());
  }

}
