import { Component, OnInit } from '@angular/core';
import { ListInitializerService } from './services/list-initializer.service';
import { MovieService } from './services/movie.service';
import { JsonReaderService } from './services/json-reader.service';
import { GetListService } from './services/get-list.service';
import { WordsFilterService } from './services/words-filter.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [MovieService, JsonReaderService, ListInitializerService, GetListService, WordsFilterService]
})

export class AppComponent {
  constructor(private _movieService: MovieService ){

  }


}  
