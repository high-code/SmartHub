import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ConfigurationService } from '../services/configuration.service';
import { RegisterService } from '../services/register.service';
import { Register } from '../models/register.model';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {


  isSubmitted : boolean = false;
  

  registerModel: Register = new Register();

  constructor(private registerService: RegisterService) {
      
   }
  
  

  ngOnInit() {

  }


  onSubmit() {
    this.registerService.register(this.registerModel).subscribe(a => {
      this.isSubmitted = true;
    })
    
    console.log("success");
  }

  

}
