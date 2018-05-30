export interface ISimplifiedMovie{

        isInWatchLater : boolean;
        watchLaterListIconSrc : string;

        isInFavoriteList: boolean;
        favoriteListIconSrc : string;

        hasHashtag : boolean;
        hashtagIconSrc : string;

        hasReview : boolean;
        reviewIconSrc : string;

        shareIconSrc : string;
        shareViaGoogleIconSrc : string;
        shareViaFacebookIconSrc : string;

        htmlId : number;
        title: string;
        year : string;
        poster: string;
        mark : number;
        runtime : string;
        tags : any[]; 
        review : string;
}