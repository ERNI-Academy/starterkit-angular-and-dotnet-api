import { APPRoutes } from './../../../../core/constants/constants';
import { Component, OnInit } from '@angular/core';
import { Tab } from '@app/shared/component/tabs/tabs.component';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  tabs: Tab[];

  constructor() { }

  ngOnInit(): void {
    this.tabs = [
      {title: 'Status', route: APPRoutes.CONTROL.STATUS},
      {title: 'Tab 2', route: APPRoutes.CONTROL.TAB2},
    ];
  }

}
