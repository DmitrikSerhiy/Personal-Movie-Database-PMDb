import { Injectable } from '@angular/core';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { IFilters } from '../shared/interfaces/IFilters';
import { filter } from 'rxjs/operators';

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
    this.filteredMovies = this.filters ? this.performFilter(this.filters) : this.movies;
  }

  filterList: IFilters[] = [
    { filter: "none" },
    { filter: "Title" },
    { filter: "Tag" },
    { filter: "Year" },
    { filter: "Mark" },
    { filter: "Runtime"}];



  performFilter(filterBy: string): ISimplifiedMovie[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.movies.filter((movie: ISimplifiedMovie) =>
      this.getFiltrationParameter(movie).toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  getFiltrationParameter(movie: ISimplifiedMovie): any {
    let filter = '';
    switch (this.filterCriteria) {
      // case 'Title': return movie.title;
      // case 'Tag': return movie.tags.toString();
      // case "Year": return movie.year;
      // case "Mark": return movie.mark.toString();
      // case "Runtime": return movie.runtime.toString();
      // default: return 'none';
      case 'Title': filter = movie.title; break;
      case 'Tag': filter = movie.tags.toString(); break;
      case "Year": filter = movie.year; break;
      case "Mark": filter = movie.mark.toString(); break;
      case "Runtime": filter = movie.runtime.toString(); break;
      default: filter = 'none';
      return filter
    }

  }

  passMoviesToFilter(moviesFromList: ISimplifiedMovie[]) {
    this.movies = moviesFromList;
    // this.filters = this. filters;//I know it's dumn!
  }

  passFilterCriteria(filterBy: string) {
    this.filterCriteria = filterBy;
  }
}
