import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PasswordRequestModel, PasswordResponseModel } from '../../models/password.model';
import { PasswordService } from '../../services/password.service';


@Component({
  selector: 'app-password-checker',
  standalone: true, // ✅ Make sure it's standalone
  imports: [CommonModule, FormsModule], // ✅ Include FormsModule here
  templateUrl: './password-checker.component.html',
  styleUrls: ['./password-checker.component.css']
})
export class PasswordCheckerComponent {
  password = '';
  result: PasswordResponseModel | null = null;
  showPassword: boolean = false;

  constructor(private passwordService: PasswordService) {}
  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
  checkPassword() {
    const request: PasswordRequestModel = { password: this.password };
    this.passwordService.verifyPassword(request).subscribe({
      next: (res: PasswordResponseModel) => this.result = res,
      error: (err: any) => console.error('API error:', err)
    });
    
  }
}
