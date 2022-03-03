import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent implements OnInit {

  readonly name = 'Logs Status';

  constructor() { }

  ngOnInit(): void {
  }

}
