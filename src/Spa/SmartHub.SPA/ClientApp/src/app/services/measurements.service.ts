import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ConfigurationService } from './configuration.service';
import { Measurement } from '../models/measurement.model';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {

    baseUrl: string = "";
    
    measurementsUrl: string = "api/measurement"
  
    constructor(private http: HttpClient, private configurationService: ConfigurationService) {
      if (this.configurationService.isLoaded) {
          this.baseUrl = this.configurationService.configuration.edgeUrl; 
      }
    }


    getMeasurements(id: string): Observable<Measurement[]> {
        const url = `${this.baseUrl}/${this.measurementsUrl}/${id}`;
        return this.http.get<Measurement[]>(url)
          .pipe(
            tap(a => console.log(a))
          );
      }
}
