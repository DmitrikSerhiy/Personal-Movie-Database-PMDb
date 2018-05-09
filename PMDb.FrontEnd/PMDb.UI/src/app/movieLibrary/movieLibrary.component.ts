import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { MovieService } from './movie.service';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ListInitializerService } from '../list-initializer/list-initializer.service';


@Component({
    templateUrl: './movieLibrary.component.html',
    styleUrls: ['./movieLibrary.component.css']
    //changeDetection: ChangeDetectionStrategy.OnPush
})
export class MovieLibraryComponent implements OnInit{

    errorMessage : string = '';
    title : string ='PMDb library';
    movies : ISimplifiedMovie[] = [];
    posterWidth : number = 60;
    posterHeight : number = 100;
    showPoster : boolean = false;
    listFilter : string = '';
    maxRating: number = 10;
    isRatingReadonly: boolean = true;
    public isShareIconsShowen : boolean = false;
  
    constructor(private _movieService : MovieService,
                private _ListInitializer : ListInitializerService){
    }

    ngOnInit() : void {
        
        this._movieService.getMovies()
                .subscribe( 
                        (movies : ISimplifiedMovie[]) => {
                            this.movies = movies;
                            this._ListInitializer.setMovies(this.movies);
                            this._ListInitializer.initIcons()
                        },
                        error => this.errorMessage = <any>error),
                        () => console.log('ITS DONE!');
    }

      
    trackByFunction(index : number, item : ISimplifiedMovie) {
        return item.htmlId; // or item.id
    }

    toggelePoster() : void{
        this.showPoster = !this.showPoster;
    }

    ShowShareIcons(index : number) : void {
        this.isShareIconsShowen = !this.isShareIconsShowen;
        this._ListInitializer.changeShareIcon(index);
    }

    AddToWatchLater(index : number) : void {
        this._ListInitializer.changeWatchLaterIcon(index);
    }

    AddToFavorite(index : number) : void {
        this._ListInitializer.changeFavoriteIcon(index);
    }

    AddHashtag(index : number) : void {
        this._ListInitializer.changeHashtagIcon(index);
    }

    AddReview(index : number) : void {
        this._ListInitializer.changeReviewIcon(index);
    }

    ShareViaGoogle(index : number) : void {

    }

    ShareViaFacebook(index : number) : void {

    }

}