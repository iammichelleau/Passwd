import { Component, OnInit } from '@angular/core';
import { Config } from '../config'
import ConfigService  from '../config.service'

@Component({
  selector: 'app-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.css']
})
export class ConfigComponent implements OnInit {

  response: string = "";

  constructor() {}

  ngOnInit() {
  }

  async submit(Path: string) {
    Path = Path.trim();
    if (!Path) { return; }
    this.response = await ConfigService.submitConfig({ Path } as Config);
  }
}
