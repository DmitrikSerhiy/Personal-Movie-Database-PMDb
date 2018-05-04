import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse,  } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { ISimplifiedMovie } from "../interfaces/ISimplifiedMovie";
import { compile, compileFromFile } from 'json-schema-to-typescript'

@Injectable()
export class MovieService {

    private _getMoviesURL = 'http://localhost:56756/api/movies';

    constructor(private _http : HttpClient){

    }
    


    getMovies() : Observable<ISimplifiedMovie[]>
    {
        return this._http.get<ISimplifiedMovie[]>(this._getMoviesURL)
        // .map(response => response as ISimplifiedMovie[])
        //     .map(data => data.map(function(movie:any){
        //         return {Title : movie.Title,
        //                 Year : movie.Year,
        //                 Poster : movie.Poster,
        //                 Mark : movie.Mark,
        //                 Runtime : movie.Runtime,
        //                 Tags : movie.Tags  }
        //     }))
        // .map(data => data.forEach(movie => <ISimplifiedMovie>movie))
        //    .map(data => <ISimplifiedMovie[]>data)

          
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private handleError(err : HttpErrorResponse){
        console.log(err.message);
        return Observable.throw(err.message);
    }

    // moviesDemo : ISimplifiedMovie[] = [
    //     {
    //         "Title" : "Star Wars",
    //         "Year" : "2004",
    //         "Poster" : "some poster",
    //         "Mark" : 9,
    //         "Runtime" : "162",
    //         "Tags" : ['#vau!', ['#cool!']]
    //     },
    //     {
    //         "Title" : "Star Track",
    //         "Year" : "2008",
    //         "Poster" : "another poster",
    //         "Mark" : 8,
    //         "Runtime" : "125",
    //         "Tags" : ['#so-so', ['#okay']]
    //     },
    // ]
    
    // getMovies() : ISimplifiedMovie[]{
    //    return this.moviesDemo;
    // }
}
