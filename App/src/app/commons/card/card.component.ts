import { Input, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html'
})
export class CardComponent implements OnInit {

  @Input() color;
  @Input() backgroundColor;

  constructor() { }

  ngOnInit() {
  }

}
