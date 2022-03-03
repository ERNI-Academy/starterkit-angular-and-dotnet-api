import { Observable } from 'rxjs';
import { ItemsService } from './../../../../core/api/generated/api/items.service';
import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent implements OnInit {

  readonly name = 'Control Status';

  result: Observable<any>;

  constructor(
    private itemsService: ItemsService,
  ) { }

  ngOnInit(): void {
    this.result = this.itemsService.itemsGet();
  }

}
