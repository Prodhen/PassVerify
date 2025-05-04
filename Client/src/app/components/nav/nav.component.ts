import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms'

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Observable } from 'rxjs';

import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environment/environment';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {

  private toastr = inject(ToastrService);
  model: any = {};
  loggedIn = false;
  private apiUrl = environment.apiUrl;

  ngOnInit(): void {

  }
  login() {

    console.log(this.model);

  }


  

}
