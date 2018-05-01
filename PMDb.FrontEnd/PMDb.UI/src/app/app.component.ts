import { Component, OnInit } from '@angular/core';
import { MovieService } from './movieLibrary/movie.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [MovieService]
})

export class AppComponent {
  constructor(private _movieService: MovieService ){

  }


}  
