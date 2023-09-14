import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  getUsersUrl = environment.apiUrl + 'users';
  registerMode = false;
  users: any;

  constructor() { }

  ngOnInit(): void {
    
  }


  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}
