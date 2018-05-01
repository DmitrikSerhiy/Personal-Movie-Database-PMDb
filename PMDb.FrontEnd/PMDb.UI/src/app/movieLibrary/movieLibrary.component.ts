import { Component, OnInit } from "@angular/core";
import { MovieService } from "./movie.service";
import { ISimplifiedMovie } from "../interfaces/ISimplifiedMovie";

@Component({
    selector: 'pmdb-movieLibrary',
    templateUrl: './movieLibrary-component.html'

})
export class MovieLibrary implements OnInit{

    movies : ISimplifiedMovie[] = [];
    errorMessage : string = '';

    constructor(private _movieService : MovieService){
        
    }
    

    ngOnInit() : void {
        this._movieService.getMovies()
                    .subscribe(movies => this.movies = movies,
                    error => this.errorMessage = <any>error);
    }
}