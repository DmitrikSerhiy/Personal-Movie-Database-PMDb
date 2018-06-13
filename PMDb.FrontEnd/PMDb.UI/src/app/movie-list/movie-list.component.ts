import { Component, OnInit, ChangeDetectionStrategy, Input, SimpleChanges, OnDestroy } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { ListInitializerService } from '../services/list-initializer.service';
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
import { WordsFilterService } from '../services/words-filter.service';





@Component({
  selector: 'app-movie-list',
  templateUrl: //'../movie-card/movie-card.component.html',
    './movie-list.component.html',
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
  toolTipText: string = '';
  currTags: string[] = [];
  currReviewTitle: string = '';
  currReviewText: string = '';
  isCurrListWatchLater: boolean = false;
  isCurrListFavorite: boolean = false;
  isCurrListLibrary: boolean = false;
  isListDefault : boolean = false;
  viewStyle = "grid";
  showTableView = true;
  showCardView = false;
  currPage: number = 1;
  pageSize: number = 10;
  moviesAmount: number = 0;
  pagesAmount: number = 0;
  isMovieLoaded: boolean = false;
  firstPageOfMovies;
  setFilter : string = 'none';

  links: any[];//not used by far
  linksForPagination: any[];

  urlWithoutMovieListName: string;
  fullURL;
  errorMessage: string = '';
  observer: Subscription;
  internalObserver: Subscription;
  movieListName: string = '';


  viewListIconpath: string = './assets/viewList_icon.png';
  viewCardIconpath: string = './assets/viewCard_icon.png';
  editIcon : string = './assets/edit_icon.png';

  constructor(
    private listInitializer: ListInitializerService,
    private cdRef: ChangeDetectorRef,
    private getListService: GetListService,
    private jsonReaderService: JsonReaderService,
    private router: Router,
    private wordsFilterService : WordsFilterService
  ) {
  }

  _listFilter: string;
  filtredMovies: ISimplifiedMovie[];


  ngOnInit() {

    this.setMovieListName();

    this.observer = this.jsonReaderService.getJSON()
      .subscribe(
        (json: any) => {
          if (!this.fullURL) {
            if (!this.isCurrListLibrary) {
              this.urlWithoutMovieListName = json.getList;
              this.fullURL = this.setURLForList(this.urlWithoutMovieListName);
            }
            else {
              this.fullURL = json.getLibrary;
            }
            this.setPagination(json);
          }
        },
        (error) => {
          this.errorMessage = <any>error;
          console.log(this.errorMessage)
        },
        () => {
          console.log("json with urls has been loaded");
          this.internalObserver = this.getListService.getMovieList(this.fullURL)
            .subscribe(
              (movieList: any) => {
                this.movies = movieList.movies as ISimplifiedMovie[];
                this.moviesAmount = movieList.listLength;
                this.movieListName = movieList.name;
                this.isMovieLoaded = true;
                this.isListDefault = movieList.isDefault;
                this.setLinks(movieList);
                this.pagesAmount = this.linksForPagination.length;
                this.listInitializer.setMovies(this.movies);
                this.listInitializer.initIcons();
                this.setMoviesRatings();
                this.firstPageOfMovies = this.movies;
                this.wordsFilterService.passMoviesToFilter(this.movies);
                console.log("page loaded with movies");
              },
              error => console.log(<any>error));
        });
  }

  ngOnDestroy(): void {
    this.internalObserver.unsubscribe();
    this.observer.unsubscribe();
    console.clear();
    console.log("page destroyed")
  }

  changeFilter(filter : string){
    this.setFilter = filter;
  }

  filterMovies(filterBy : string){
    this.wordsFilterService.passFilterCriteria(filterBy);
    this.wordsFilterService.filters = this.wordsFilterService.filters;
    if(this.wordsFilterService.filters || this.wordsFilterService.filters.length != 0){
      this.movies = this.wordsFilterService.filteredMovies;
    }
    else
      this.movies = this.firstPageOfMovies.slice(0);
  }

  changeViewStyle() {
    this.showTableView = !this.showTableView;
    this.showCardView = !this.showCardView;
  }

  setMovieListName() {
    let currSegment = this.router.url.toString().substr(1)
    this.movieListName = currSegment;
    if (this.movieListName === 'watchLater') this.isCurrListWatchLater = true;
    if (this.movieListName === 'favorite') this.isCurrListFavorite = true;
    if (this.movieListName === 'library') this.isCurrListLibrary = true;
    console.log(this.movieListName);
  }

  setPagination(json: any) {
    this.fullURL = this.fullURL + "?" + json.parameters.currPage + this.currPage + '&';
    this.fullURL = this.fullURL + json.parameters.pageSize + this.pageSize;
  }

  setLinks(movieList: any) {
    this.linksForPagination = movieList.linksForPagination;
    this.links = movieList.links;
  }

  resizeList(newPageSize: number) {
    //RETARDED CODE!!!
    this.pageSize = newPageSize;
    let ps = 'pageSize=';
    let oldURL = this.fullURL as string;
    let index = oldURL.indexOf(ps);
    let stringToReplace = ps + oldURL.charAt(index + 9) + oldURL.charAt(index + 10);
    this.fullURL = oldURL.replace(stringToReplace, ps+newPageSize.toString());
    console.log("new url:" + this.fullURL);
    this.ngOnDestroy();
    this.ngOnInit();
  }

  changePage(): void {
    console.log("old uri" + this.fullURL);
    this.ngOnDestroy();
    this.fullURL = this.linksForPagination[this.currPage-1].href;
    console.log("new uri" + this.fullURL);
    this.ngOnInit();
    
  }

  setURLForList(partialURl: string): string {
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
    this.listInitializer.changeShareIcon(index);
  }

  AddToWatchLater(index: number): void {

    this.movies[index].isInWatchLater = !this.movies[index].isInWatchLater;

    this.listInitializer.changeWatchLaterIcon(index);
  }

  AddToFavorite(index: number): void {

    this.movies[index].isInFavoriteList = !this.movies[index].isInFavoriteList;

    this.listInitializer.changeFavoriteIcon(index);
  }

  AddHashtag(index: number): void {
    this.currTags = [];
    var tags = this.movies[index].tags;
    for (var i = 0; i < tags.length; i++) {
      this.currTags.push(tags[i].name);
    }


    this.listInitializer.changeHashtagIcon(index);

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
