import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JsonReaderService } from '../services/json-reader.service';
import { GetMovieService } from '../services/get-movie.service';
import { MovieInitializerService } from '../services/movie-initializer.service';
import { IMovie } from '../shared/interfaces/IMovie';
import { Subscribable, Subscription } from 'rxjs';
import { CustomeDecimalPipePipe } from '../Shared/custome-decimal-pipe.pipe';

@Component({
  selector: 'app-movie-page',
  templateUrl: './movie-page.component.html',
  styleUrls: ['./movie-page.component.css']
})
export class MoviePageComponent implements OnInit {


  private movie: IMovie;
  routeSubscription : Subscription;
  observer: Subscription;
  internalObserver: Subscription;
  fullURL: string = '';
  errorMessage: string = '';
  movieTitle : string = '';
  composedIMDbId : string =  'https://www.imdb.com/title/';
  moviesRatings: [number, number];
  currMovieMarkToSet: number;
  isRateTen = false;
  // cdRef: ChangeDetectorRef;

  imdbLogo : string = './assets/imdb_logo.png';
  RTLogo : string = './assets/rottenTomatos_logo.png';
  MTLogo : string = './assets/metacritic_logo.png';
  updatedMark: number;


  constructor(private route: ActivatedRoute,
    private jsonReaderService: JsonReaderService,
    private getMovieService: GetMovieService,
    private movieInitializerService: MovieInitializerService,
    private router: Router) {

      this.routeSubscription = this.route.params.subscribe(params => this.movieTitle = params['movieTitle']);
  }


  ngOnInit() {
    this.observer = this.jsonReaderService.getJSON()
      .subscribe(
        (json: any) => {
          this.fullURL = json.getMovie + this.movieTitle;
        },
        (error) => {
          this.errorMessage = <any>error;
          console.log(this.errorMessage)
        },
        () => {
          console.log("json with urls has been loaded");
          this.internalObserver = this.getMovieService.searchMovies(this.fullURL)
            .subscribe(
              (movie: IMovie) => {
                this.movie = movie;
                this.composedIMDbId = this.composedIMDbId +  movie.imDbId + '/';
                this.setMoviesRatings();
              },
              (error) => {
                this.errorMessage = <any>error;
                console.log(this.errorMessage)
              },
              () => console.log("page with movie loaded")
            )
        }
      )
  }

  setMoviesRatings() {
    let localDecimalPipe = new CustomeDecimalPipePipe();
    let integerPartOfRate = localDecimalPipe.transform(this.movie.ratings.mark, false);
    let fractionalPartOfRate = localDecimalPipe.transform(this.movie.ratings.mark, true);

    this.moviesRatings = [integerPartOfRate, fractionalPartOfRate];
  }

  setFirstPosition(): number {
    var currRate = this.moviesRatings[1];
    if (!currRate)
      return 0;
    return currRate;
  }

  formatRateLabel(value: number) {
    if (this.currMovieMarkToSet === 10) {
      this.isRateTen = true;
      //let cdRef: ChangeDetectorRef;
     // cdRef.detectChanges();// detectChanges();
      this.updatedMark = 10;
      return 10;
    }
    const integerRate = this.currMovieMarkToSet;

    if (!value) {
      this.updatedMark = integerRate;
      return integerRate;
    }

    let formatedRate = integerRate + '.' + value;
    let Rate = Number.parseFloat(formatedRate);
    this.updatedMark = Rate;
    return integerRate.toString() + "." + value;
  }

  setMark(integerMark: number): void {
    this.currMovieMarkToSet = integerMark
  }

  submitMark(): void {
    let localDecimalPipe = new CustomeDecimalPipePipe();
    this.moviesRatings[0] = localDecimalPipe.transform(this.updatedMark, false);
    this.moviesRatings[1] = localDecimalPipe.transform(this.updatedMark, true);
    var markToUpdate = this.updatedMark;
  }

  



}
