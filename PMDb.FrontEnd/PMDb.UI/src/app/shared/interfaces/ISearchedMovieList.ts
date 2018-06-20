import { ISearchedMovie } from "./ISearchMovie";

export interface ISearchedMovieList{
    movies :ISearchedMovie[];
    searchedMovie: string;
    totalMovies : number;
    totalPages : number;
    links : any[];
    linksForPagination : any[];
}