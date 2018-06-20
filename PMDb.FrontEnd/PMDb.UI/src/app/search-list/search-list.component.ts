import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { JsonReaderService } from '../services/json-reader.service';
import { SearchMovieService } from '../services/search-movie.service';
import { ISearchedMovieList } from '../shared/interfaces/ISearchedMovieList';
import { ISearchedMovie } from '../shared/interfaces/ISearchMovie';
import { SeachedListInitializerService } from '../services/seached-list-initializer.service';

@Component({
  selector: 'app-search-list',
  templateUrl: './search-list.component.html',
  styleUrls: ['./search-list.component.css']
})
export class SearchListComponent implements OnInit, OnDestroy {


  errorMessage: string = '';
  private routeSubscription: Subscription;
  private querySubscription: Subscription;
  movieToSearch: string = '';
  pageSize: number;
  currPage: number;
  moviesAmount: number;
  movies: ISearchedMovie[];
  searchedMovieList: ISearchedMovieList;

  observer: Subscription;
  internalObserver: Subscription;
  fullURL: string = '';
  pagesAmount: number;
  isMovieLoaded: boolean = false;

  linksForPagination: any[];



  constructor(private route: ActivatedRoute,
    private jsonReaderService: JsonReaderService,
    private searchMovieService: SearchMovieService,
    private seachedListInitializerService: SeachedListInitializerService,
    private router: Router) {
  }

  setMovieToSearch(movie: string) {
    this.movieToSearch = movie;
  }
  
  ngOnInit() {

    this.routeSubscription = this.route.params.subscribe(params => this.movieToSearch = params['movieTitle']);
    this.querySubscription = this.route.queryParams.subscribe(
      (queryParam: any) => {
        this.pageSize = queryParam['pageSize'];
        this.currPage = queryParam['currPage'];
      });

    this.observer = this.jsonReaderService.getJSON()
      .subscribe(
        (json: any) => {
          if (!this.fullURL) {
            this.fullURL = this.generateURL(json);
          }
        },
        (error) => {
          this.errorMessage = <any>error;
          console.log(this.errorMessage)
        },
        () => {
          console.log("json with urls has been loaded");
          this.internalObserver = this.searchMovieService.searchMovies(this.fullURL)
            .subscribe(
              (searchedMovieList: ISearchedMovieList) => {
                this.searchedMovieList = searchedMovieList;
                this.movies = searchedMovieList.movies;
                this.seachedListInitializerService.initIcons(this.movies);
                this.isMovieLoaded = true;
                this.linksForPagination = searchedMovieList.linksForPagination;
                this.moviesAmount = searchedMovieList.totalMovies;
                this.pagesAmount = searchedMovieList.totalPages;
                console.log("page loaded with movies");
              },
              error => console.log(<any>error));
        });
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
    this.querySubscription.unsubscribe();
    this.internalObserver.unsubscribe();
    this.observer.unsubscribe();
    this.fullURL = '';
    console.clear();
  }

  generateURL(json: any): string {
    return json.searchMovies + this.movieToSearch + '?' + json.parameters.currPage +
      this.currPage + '&' + json.parameters.pageSize + this.pageSize;
  }

  AddToLibrary(imdbID: string) {
    //submit
  }

  changePage(): void {
    console.log("old uri" + this.fullURL);
    this.ngOnDestroy();
    this.fullURL = this.linksForPagination[this.currPage - 1].href;
    console.log("new uri" + this.fullURL);

    var urlTree = this.router.parseUrl(this.router.url);
    urlTree.queryParams['currPage'] = this.currPage.toString();
    this.router.navigateByUrl(urlTree);
    this.ngOnInit();
  }

}
