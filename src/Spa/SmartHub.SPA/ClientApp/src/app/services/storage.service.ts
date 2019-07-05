import { Injectable } from "@angular/core";

@Injectable()
export class StorageService {

    storage : any;

    constructor() {
        this.storage = sessionStorage;
    }

    get(key: string) {
       
        let jsonValue : string = this.storage.getItem(key);
        
        if(jsonValue)
        {
            let value = JSON.parse(jsonValue);
            return value;
        }
 
        return;
    }

    store(key: string, value: any) {
      this.storage.setItem(key, JSON.stringify(value));
    }

}