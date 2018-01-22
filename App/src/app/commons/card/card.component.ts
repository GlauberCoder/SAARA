import { Input, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss']
})
export class CardComponent implements OnInit {

  @Input() color;
  @Input() backgroundColor;

  constructor() { }

  ngOnInit() {
  }

}
