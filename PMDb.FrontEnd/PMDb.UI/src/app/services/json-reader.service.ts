import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { Observable } from 'rxjs/Observable';

@Injectable()
export class JsonReaderService {
  private localJson : string = "assets/urls.json";
  public jsonfile;

  constructor(private http: HttpClient) {
}

public getJSON(): Observable<any> {
    return this.http.get(this.localJson)
}

}
