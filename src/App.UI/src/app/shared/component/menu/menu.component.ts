import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';


export interface Item {
  value: string;
  text: string;
}

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MenuComponent implements OnInit {

  @Input() items: Item[];
  selectedItem: Item;

  showMenu = false;

  constructor() { }

  ngOnInit(): void {
    this.selectedItem = this.items[0];
  }

  toggleMenu(): void {
    this.showMenu = !this.showMenu;
  }
}
