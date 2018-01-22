import {EventEmitter, Injectable} from '@angular/core';

@Injectable()
export class PlanningCalculatorService {

  public passingData = new EventEmitter<any>();

  constructor() { }



}
