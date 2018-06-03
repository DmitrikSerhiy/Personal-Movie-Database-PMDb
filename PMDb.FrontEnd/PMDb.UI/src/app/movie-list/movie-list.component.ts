import { Component, OnInit, ChangeDetectionStrategy, Input, SimpleChanges, OnDestroy } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { ListInitializerService } from '../list-initializer/list-initializer.service';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { Triggers } from 'ngx-popper';
import { CustomeDecimalPipePipe } from '../Shared/custome-decimal-pipe.pipe';
import { ChangeDetectorRef } from '@angular/core';
import { GetListService } from '../services/get-list.service';
import { JsonReaderService } from '../services/json-reader.service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Observable } from 'rxjs/Observable';
import { retryWhen, retry } from 'rxjs/operators';
import { Router } from '@angular/router';




@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit, OnDestroy {


  movies: ISimplifiedMovie[] = [];
  moviesRatings: [number[], number[]];
  currMovieMarkToSet: [number, number];
  updatedMark: [number, number];
  posterWidth: number = 60;
  posterHeight: number = 100;
  showPoster: boolean = false;
  maxRating: number = 10;
  isRatingReadonly: boolean = false;
  public isShareIconsShowen: boolean = false;
  isRateTen = false;
  unclicableButtons: boolean = true;
  listFilter: string = '';
  toolTipText: string = '';
  currTags: string[] = [];
  currReviewTitle: string = '';
  currReviewText: string = '';


  urlWithoutMovieListName: string;
  fullURL = '';
  errorMessage : string = '';
  observer : Subscription;
  movieListName: string = '';

  constructor(
    private _ListInitializer: ListInitializerService,
    private cdRef: ChangeDetectorRef,
    private getListService: GetListService,
    private jsonReaderService: JsonReaderService,
    private Router : Router
  ) {
  }

  ngOnInit() {

    this.setMovieListName();

    this.observer = this.jsonReaderService.getJSON()
      .subscribe(
        (json: any) => {
          this.urlWithoutMovieListName = json.getList;
          this.fullURL = this.setURLForList(this.urlWithoutMovieListName);
        },
        (error) => {
          this.errorMessage = <any>error;
          console.log(this.errorMessage)
        },
        () => {
          console.log("json with urls has been loaded");
          this.getListService.getMovieList(this.fullURL)
            .subscribe(
              (movieList: any) => {
                this.movies = movieList.movies as ISimplifiedMovie[];
                this._ListInitializer.setMovies(this.movies);
                this._ListInitializer.initIcons();
                this.setMoviesRatings();
              },
              error => console.log(<any>error));
              });
  }

  setMovieListName(){
    let currSegment = this.Router.url.toString().substr(1)
    this.movieListName = currSegment;
  }


  ngOnDestroy(): void {
    this.observer.unsubscribe();
    console.clear();
  }

  private setURLForList(partialURl: string): string {
    return partialURl + this.movieListName;
  }
  
  toggleClickability(): void {
    this.unclicableButtons = !this.unclicableButtons;
  }

  toggelePoster(): void {
    this.showPoster = !this.showPoster;
  }

  setMoviesRatings() {
    let localDecimalPipe = new CustomeDecimalPipePipe();
    let integerPartOfRate = [];
    let fractionalPartOfRate = [];

    this.movies.slice(0).forEach(movie => {
      integerPartOfRate.push(localDecimalPipe.transform(movie.mark, false));
      fractionalPartOfRate.push(localDecimalPipe.transform(movie.mark, true))
    });

    this.moviesRatings = [integerPartOfRate, fractionalPartOfRate]
  }

  setFirstPosition(index: number): number {
    var currRate = this.moviesRatings[1][index];
    if (!currRate)
      return 0;
    return currRate;
  }

  formatRateLabel(value: number) {
    if (this.currMovieMarkToSet[0] === 10) {
      this.isRateTen = true;
      this.cdRef.detectChanges();
      this.updatedMark = [10, this.currMovieMarkToSet[1]];
      return 10;
    }
    const integerRate = this.currMovieMarkToSet[0];

    if (!value) {
      this.updatedMark = [integerRate, this.currMovieMarkToSet[1]];
      return integerRate;
    }

    let formatedRate = integerRate + '.' + value;
    let Rate = Number.parseFloat(formatedRate);
    this.updatedMark = [Rate, this.currMovieMarkToSet[1]];
    return integerRate.toString() + "." + value;
  }

  setMark(integerMark: number, index: number): void {
    this.currMovieMarkToSet = [integerMark, index]
  }

  submitMark(): void {
    let localDecimalPipe = new CustomeDecimalPipePipe();
    this.moviesRatings[0][this.currMovieMarkToSet[1]] = localDecimalPipe.transform(this.updatedMark[0], false);
    this.moviesRatings[1][this.currMovieMarkToSet[1]] = localDecimalPipe.transform(this.updatedMark[0], true);

    //sent updatedMark first : mark, second index of movies array
    var markToUpdate = this.updatedMark;
  }

  ShowShareIcons(index: number): void {
    this.isShareIconsShowen = !this.isShareIconsShowen;
    this._ListInitializer.changeShareIcon(index);
  }

  AddToWatchLater(index: number): void {

    this.movies[index].isInWatchLater = !this.movies[index].isInWatchLater;

    this._ListInitializer.changeWatchLaterIcon(index);
  }

  AddToFavorite(index: number): void {

    this.movies[index].isInFavoriteList = !this.movies[index].isInFavoriteList;

    this._ListInitializer.changeFavoriteIcon(index);
  }

  AddHashtag(index: number): void {
    this.currTags = [];
    var tags = this.movies[index].tags;
    for (var i = 0; i < tags.length; i++) {
      this.currTags.push(tags[i].name);
    }


    this._ListInitializer.changeHashtagIcon(index);

  }

  ShowReview(index: number): void {
    this.currReviewTitle = this.movies[index].title;
    this.currReviewText = this.movies[index].review;
    //this._ListInitializer.changeReviewIcon(index);
  }

  ShareViaGoogle(index: number): void {

  }

  ShareViaFacebook(index: number): void {

  }




}
