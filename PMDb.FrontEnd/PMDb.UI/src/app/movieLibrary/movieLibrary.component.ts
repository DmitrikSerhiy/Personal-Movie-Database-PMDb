import { Component, OnInit } from "@angular/core";
import { MovieService } from "./movie.service";
import { ISimplifiedMovie } from "../interfaces/ISimplifiedMovie";
//import { }

@Component({
    //selector: 'pmdb-movieLibrary',
    templateUrl: './movieLibrary.component.html'

})
export class MovieLibraryComponent implements OnInit{

    movies : ISimplifiedMovie[] = [];
    errorMessage : string = '';
    title : string ='PMDb library'

    constructor(private _movieService : MovieService){
        
    }
    

    ngOnInit() : void {
        this._movieService.getMovies()
                    .subscribe( 
                        movies => this.movies = movies,
                        error => this.errorMessage = <any>error);
    }
}