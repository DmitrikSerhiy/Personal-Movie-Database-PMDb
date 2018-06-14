import { Injectable } from '@angular/core';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { IFilters } from '../shared/interfaces/IFilters';
import { filter } from 'rxjs/operators';
import { CustomeDecimalPipePipe } from '../Shared/custome-decimal-pipe.pipe';
import { CustomRuntimePipe } from '../Shared/custom-time-pipe.pipe';

@Injectable({
  providedIn: 'root'
})
export class WordsFilterService {

  constructor() { }

  filteredMovies: ISimplifiedMovie[];
  movies: ISimplifiedMovie[] = [];
  _filters: string;
  filterCriteria: string = 'none';

  get filters(): string {
    return this._filters;
  }
  set filters(value: string) {
    this._filters = value;
  }

  filterList: IFilters[] = [
    { filter: "none" },
    { filter: "Title" },
    { filter: "Tag" },
    { filter: "Year" },
    { filter: "Mark" },
    { filter: "Runtime" }];



  performFilter(symbolsFilterBy: string): ISimplifiedMovie[] {
    if(symbolsFilterBy){
    symbolsFilterBy = symbolsFilterBy.toLocaleLowerCase();
    return this.movies.filter( (movie: ISimplifiedMovie) => 
      this.getFiltrationParameter(movie).indexOf(symbolsFilterBy) !== -1);
    }
    return this.filteredMovies;
  }

  getFiltrationParameter(movie: ISimplifiedMovie): string {
    let filter = '';
    if (this.filterCriteria != 'none') {
      switch (this.filterCriteria) {
        case 'Title': {filter = movie.title; break;}
        case 'Tag': {movie.tags.forEach(tag => filter += tag.name.toString().substring(1)); break;}
        case "Year": {filter = movie.year; break;}
        case "Mark": {filter = new CustomeDecimalPipePipe().transform(movie.mark, false).toString(); break;}
        case "Runtime": {filter = new CustomRuntimePipe().transform(+movie.runtime, true); break;}
        default: {filter = movie.title; break}//tha's kind of stupid code
      }
    }
    else filter = movie.title;
    filter = filter.toString().toLocaleLowerCase();
    return filter;
  }

  passMoviesToFilter(moviesFromList: ISimplifiedMovie[]) {
    this.movies = moviesFromList;
  }
}
