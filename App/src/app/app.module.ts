import { BrowserModule } from '@angular/platform-browser';
import { FormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';
import '../extensions/number.extensions';

import { AppComponent } from './app.component';
import { LeftBarComponent } from './left-bar/left-bar.component';
import { TopBarComponent } from './top-bar/top-bar.component';
import { CalculatorComponent } from './calculator/calculator.component';
import { RouterModule, Routes } from '@angular/router';
import { CardComponent } from './commons/card/card.component';
import { ExchangerService } from '../providers/exchanger.service';
import { PlanningCalculatorService } from '../providers/planning-calculator.service';
import { NumberInputComponent } from './commons/input/number-input/number-input.component';
import { InputComponent } from './commons/input/text-input/text-input.component';

const appRoutes: Routes = [
  {path: '', component: CalculatorComponent}
];


@NgModule({
  declarations: [
    AppComponent,
    LeftBarComponent,
    TopBarComponent,
    CalculatorComponent,
    InputComponent,
    CardComponent,
    NumberInputComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    ExchangerService,
    PlanningCalculatorService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
