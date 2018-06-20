import { IRating } from "./IRating";

export interface IMovie {
    title: string;
    year: string;
    Poster: string;
    imDbId: string;
    runtime: string;
    plot: string;
    review: string;
    GenreModels: any[];
    DirectorModels: any[];
    WriterModels: any[];
    ActorModels: any[];
    TagModels: any[];
    ratings: IRating;
    ListsWithCurrMovie: any[];
    IsInWatchLater: boolean;
    IsInFavoriteList: boolean;
    HasTags: boolean;
    HasReview: boolean;

    isInWatchLater : boolean;
    watchLaterListIconSrc : string;

    isInFavoriteList: boolean;
    favoriteListIconSrc : string;

    hasTags : boolean;
    tagIconSrc : string;

    hasReview : boolean;
    reviewIconSrc : string;

    shareIconSrc : string;
    shareViaGoogleIconSrc : string;
    shareViaFacebookIconSrc : string;
    links : any[];
}