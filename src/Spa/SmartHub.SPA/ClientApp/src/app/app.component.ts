import { Component } from '@angular/core';
import { ConfigurationService } from './services/configuration.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  title = 'smarthub';

  constructor(private configurationService : ConfigurationService) {
  }

  ngOnInit() {
    this.configurationService.load();
  }
}
