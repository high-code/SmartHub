import { Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { IConfiguration } from "../models/configuration.model";
import { StorageService } from "./storage.service";
import { HttpClient } from "@angular/common/http";



@Injectable()
export class ConfigurationService {
  
    configuration : IConfiguration;
    isLoaded : boolean;
    configurationLoadedSource : Subject<IConfiguration> = new Subject();
    configurationLoaded$ = this.configurationLoadedSource.asObservable();
    
    constructor(private http: HttpClient, private storageService: StorageService) {
    }
    
    public load() {
       const baseURI = document.baseURI.endsWith('/') ? document.baseURI : document.baseURI + '/';
       const url = `${baseURI}Configuration/`
       this.http.get(url).subscribe(config => {
           this.configuration = config as IConfiguration;
           this.storageService.store("notificationServiceUrl", this.configuration.notificationServiceUrl);
           this.storageService.store("smartHubApiUrl", this.configuration.smartHubApiUrl);
           this.storageService.store("identityUrl", this.configuration.identityUrl);
           this.isLoaded = true;
           this.configurationLoadedSource.next();
       }) 

    }
}