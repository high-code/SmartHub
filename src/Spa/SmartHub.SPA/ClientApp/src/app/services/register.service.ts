import { Injectable } from '@angular/core';
import { ConfigurationService } from './configuration.service';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Register } from '../models/register.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private configurationService: ConfigurationService, private http: HttpClient) {
  }

  public register(registerModel: Register): Observable<any> {
    const identityUrl = this.configurationService.configuration.identityUrl;

    const registerUrl = `${identityUrl}/registration/register`

    return this.http.post<any>(registerUrl, registerModel)
  }
}
