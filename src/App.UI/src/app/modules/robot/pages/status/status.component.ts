import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent implements OnInit {

  readonly name = 'Robot Status';

  constructor() { }

  ngOnInit(): void {
  }

}
