import { Injectable, OnInit } from '@angular/core';
import { JsonReaderService } from './json-reader.service';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'; 
import { Observable } from 'rxjs/Observable';

@Injectable()
export class GetListService  {
  private urlWithoutMovieListName : any;
  errorMessage : string = '';

  private listName : string = '';

  constructor(private http: HttpClient) { }


  getMovieList(url : string) : Observable<ISimplifiedMovie[]>
  {
      return this.http.get<ISimplifiedMovie[]>(url)
          .do(data => console.log('All: ' + JSON.stringify(data)))
          .catch(this.handleError);
  }

  private handleError(err : HttpErrorResponse){
      console.log(err.message);
      return Observable.throw(err.message);
  }
}
