import { Component, OnInit } from "@angular/core";
import { MovieService } from "./movie.service";
import { ISimplifiedMovie } from "../interfaces/ISimplifiedMovie";
import { RatingModule } from 'ngx-bootstrap/rating';


@Component({
    templateUrl: './movieLibrary.component.html',
    styleUrls: ['./movieLibrary.component.css']

})
export class MovieLibraryComponent implements OnInit{

    movies : ISimplifiedMovie[] = [];
    posterWidth : number = 60;
    posterHeight : number = 100;
    showPoster : boolean = false;
    listFilter : string = '';
    maxRating: number = 10;
    isRatingReadonly: boolean = true;
    


    errorMessage : string = '';
    title : string ='PMDb library';

    constructor(private _movieService : MovieService){
        
    }

    ngOnInit() : void {
        
        this._movieService.getMovies()
                .subscribe( 
                        (movies : ISimplifiedMovie[]) => {
                            this.movies = movies;
                        },
                        error => this.errorMessage = <any>error),
                        () => console.log('ITS DONE!');
    }

    toggelePoster() : void{
        this.showPoster = !this.showPoster;
    }
}