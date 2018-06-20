import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/internal/Observable';
import { IMovie } from '../shared/interfaces/IMovie';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GetMovieService {

  constructor(private http: HttpClient) { }
  
  searchMovies(url : string) : Observable<IMovie>
  {
      return this.http.get<IMovie>(url)
          .do(data => console.log('All: ' + JSON.stringify(data)))
          .catch(this.handleError);
  }

  private handleError(err : HttpErrorResponse){
      console.log(err.message);
      return Observable.throw(err.message);
  }
}