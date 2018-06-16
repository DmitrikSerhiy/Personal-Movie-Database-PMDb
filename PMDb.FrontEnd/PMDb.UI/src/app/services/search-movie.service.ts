import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ISearchedMovieList } from '../shared/interfaces/ISearchedMovieList';

@Injectable({
  providedIn: 'root'
})
export class SearchMovieService {

  constructor(private http: HttpClient) { }
  
  searchMovies(url : string) : Observable<ISearchedMovieList>
  {
      return this.http.get<ISearchedMovieList>(url)
          .do(data => console.log('All: ' + JSON.stringify(data)))
          .catch(this.handleError);
  }

  private handleError(err : HttpErrorResponse){
      console.log(err.message);
      return Observable.throw(err.message);
  }
}
