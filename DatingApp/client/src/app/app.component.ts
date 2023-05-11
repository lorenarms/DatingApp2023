import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  getUsersUrl = 'https://localhost:5001/api/users';

  title = 'Dating App';
  users: any;
  

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get(this.getUsersUrl).subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')
    });
  }

}
