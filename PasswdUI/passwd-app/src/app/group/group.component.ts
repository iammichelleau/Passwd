import { Component, OnInit } from '@angular/core';
import GroupService  from '../group.service'

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  response: string = "";

  constructor() {}

  ngOnInit() {
  }

  async submit(Url: string) {
    Url = Url.trim();
    if (!Url) { return; }
    this.response = await GroupService.submit(Url);
  }
}
