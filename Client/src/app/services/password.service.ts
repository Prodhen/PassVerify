import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PasswordRequestModel, PasswordResponseModel } from '../models/password.model';


@Injectable({
  providedIn: 'root'
})
export class PasswordService {
  private apiUrl = 'http://localhost:5092/api/Password/verify';

  constructor(private http: HttpClient) {}

  verifyPassword(model: PasswordRequestModel): Observable<PasswordResponseModel> {
    return this.http.post<PasswordResponseModel>(this.apiUrl, model);
  }
}
