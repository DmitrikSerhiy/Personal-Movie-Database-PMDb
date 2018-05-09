import { Injectable } from '@angular/core';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';

@Injectable()
export class ListInitializerService {

  shareIcon : string = './assets/share_icon.png';
  watchLaterIcon: string = './assets/watchLater_icon.png';
  favoriteIcon: string = '/assets/favorite_icon.png';
  hashtagIonc: string = '/assets/hashtag_icon.png';
  reviewIcon: string = '/assets/review_icon.png';

  emptyshareIcon : string = './assets/emptyShare_icon.png';
  emptyWatchLaterIcon: string = './assets/emptyWatchLater_icon.png';
  emptyFavoriteIcon: string = '/assets/emptyFavorite_icon.png';
  emptyHashtagIonc: string = '/assets/emptyHashtag_icon.png';
  emptyReviewIcon: string = '/assets/emptyReview_icon.png';

  shareViaGoogleIcon : string = 'assets/google_icon.png';
  shareViaFacebookIcon : string = 'assets/facebook_icon.png';

  
  private movies;

  setMovies(movies : ISimplifiedMovie[]){
    this.movies = movies; 
  }

  initIcons() : void {
    this.movies.forEach(movie => {
        movie.watchLaterListIconSrc = movie.isInWatchLater ? this.watchLaterIcon : this.emptyWatchLaterIcon;
        movie.favoriteListIconSrc = movie.isInFavoriteList ? this.favoriteIcon : this.emptyFavoriteIcon;
        movie.hashtagIconSrc = movie.hasHashtag ? this.hashtagIonc : this.emptyHashtagIonc;
        movie.reviewIconSrc = movie.hasReview ? this.reviewIcon : this.emptyReviewIcon;
        movie.shareIconSrc = this.emptyshareIcon;
        movie.shareViaGoogleIconSrc = this.shareViaGoogleIcon;
        movie.shareViaFacebookIconSrc = this.shareViaFacebookIcon;
      });
}

changeShareIcon(index : number) : void {
  
  var tempShareIcon = this.movies[index].shareIconSrc;
  this.shareIcon = this.emptyshareIcon;
  this.emptyshareIcon = tempShareIcon;
  this.movies[index].shareIconSrc = this.shareIcon;
}

changeWatchLaterIcon(index : number) : void {
  const actualsrc = this.movies[index].watchLaterListIconSrc;
  if(actualsrc){
    this.movies[index].watchLaterListIconSrc = actualsrc === this.watchLaterIcon ? this.emptyWatchLaterIcon : this.watchLaterIcon;
  }else {
    this.movies[index].watchLaterListIconSrc = this.emptyWatchLaterIcon;
  }
}

changeFavoriteIcon(index : number) : void{
  const actualsrc = this.movies[index].favoriteListIconSrc;
  if(actualsrc){
    this.movies[index].favoriteListIconSrc = actualsrc === this.favoriteIcon ? this.emptyFavoriteIcon : this.favoriteIcon;
  }else {
    this.movies[index].favoriteListIconSrc = this.emptyFavoriteIcon;
  }
}

changeHashtagIcon(index : number) : void{
  const actualsrc = this.movies[index].hashtagIconSrc;
  if(actualsrc){
    this.movies[index].hashtagIconSrc = actualsrc === this.hashtagIonc ? this.emptyHashtagIonc : this.hashtagIonc;
  }else {
    this.movies[index].hashtagIconSrc = this.emptyHashtagIonc;
  }
}

changeReviewIcon(index : number) : void{
  const actualsrc = this.movies[index].reviewIconSrc;
  if(actualsrc){
    this.movies[index].reviewIconSrc = actualsrc === this.reviewIcon ? this.emptyReviewIcon : this.reviewIcon;
  }else {
    this.movies[index].reviewIconSrc = this.emptyReviewIcon;
  }
}

}
