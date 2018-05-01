import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import { ISimplifiedMovie } from "../interfaces/ISimplifiedMovie";

@Injectable()
export class MovieService {

    private _getMoviesURL = 'http://localhost:56756/api/movies';

     constructor(private _http : HttpClient){

     }

    getMovies() : Observable<ISimplifiedMovie[]>
    {
        return this._http.get<ISimplifiedMovie[]>(this._getMoviesURL)
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
        //return;
    }

    private handleError(err : HttpErrorResponse){
        console.log(err.message);
        return Observable.throw(err.message);
    }
}
