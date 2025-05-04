import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { HomeComponent } from "./components/home/home.component";
import { NavComponent } from './components/nav/nav.component';
import { PasswordCheckerComponent } from "./components/password-checker/password-checker.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent, PasswordCheckerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  http = inject(HttpClient)
  title = 'PassVerify';
  users: any;
  token: any;

  ngOnInit(): void {



  }
}
