import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  getUsersUrl = 'https://localhost:5001/api/users';
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
