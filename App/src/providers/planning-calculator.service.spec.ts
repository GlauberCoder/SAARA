import { TestBed, inject } from '@angular/core/testing';

import { PlanningCalculatorService } from './planning-calculator.service';

describe('PlanningCalculatorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PlanningCalculatorService]
    });
  });

  it('should be created', inject([PlanningCalculatorService], (service: PlanningCalculatorService) => {
    expect(service).toBeTruthy();
  }));
});
