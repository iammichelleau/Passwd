import { Component, OnInit } from '@angular/core';
import UserService  from '../user.service'

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  response: string = "";

  constructor() {}

  ngOnInit() {
  }

  async submit(Url: string) {
    Url = Url.trim();
    if (!Url) { return; }
    this.response = await UserService.submit(Url);
  }
}
