export interface ISearchedMovie{
    title: string;
    year: string;
    imdbID: string;
    type: string;
    poster: string;

    hasTags : boolean;
    tagIconSrc : string;

    hasReview : boolean;
    reviewIconSrc : string;

    hasRate: boolean;
    rateIconSrc : string;

    isInLib: boolean;
    isInFavorite : boolean;
    isInWatchLater: boolean;
    addIconSrc : string;
    toMovieIconSrc : string;
}