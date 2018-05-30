import { Component, OnInit } from '@angular/core';
import { ListInitializerService } from './list-initializer/list-initializer.service';
import { MovieService } from './movie-list/movie.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [MovieService, ListInitializerService]
})

export class AppComponent {
  constructor(private _movieService: MovieService ){

  }


}  
