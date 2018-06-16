import { Component, OnInit, NgZone } from '@angular/core';
import { ListInitializerService } from './services/list-initializer.service';
import { MovieService } from './services/movie.service';
import { JsonReaderService } from './services/json-reader.service';
import { GetListService } from './services/get-list.service';
import { WordsFilterService } from './services/words-filter.service';
import { ActivatedRoute, Router, RouteReuseStrategy, ActivatedRouteSnapshot } from '@angular/router';
import { SearchMovieService } from './services/search-movie.service';
import { SearchListComponent } from './search-list/search-list.component';
import { SeachedListInitializerService } from './services/seached-list-initializer.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [MovieService, JsonReaderService, ListInitializerService, GetListService,
    WordsFilterService, SearchMovieService,]
})

export class AppComponent {
  movieToSearch: string = '';
  constructor(
    private route: ActivatedRoute,
    private router: Router) {}

  searchMovie(movieTitle: string) {
    //tricky shitty boom
    var url = this.router.url;
    var toPage: string;
    if (url.indexOf('empty') === -1) toPage = '/empty';
    else if (url.indexOf('search') === -1) toPage = '/search';
    else toPage = '';

    if (movieTitle) {
      this.router.navigate([toPage, movieTitle],
        {
          queryParams: {
            'pageSize': 12,
            'currPage': 1,
          }
        });
    }
  }
}  
