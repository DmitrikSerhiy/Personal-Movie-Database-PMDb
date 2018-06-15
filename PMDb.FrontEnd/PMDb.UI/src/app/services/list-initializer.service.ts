import { Injectable } from '@angular/core';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';

@Injectable()
export class ListInitializerService {

  shareIcon: string = './assets/share_icon.png';
  watchLaterIcon: string = './assets/watchLater_icon.png';
  favoriteIcon: string = '/assets/favorite_icon.png';
  hashtagIonc: string = '/assets/hashtag_icon.png';
  reviewIcon: string = '/assets/review_icon.png';

  emptyShareIcon: string = './assets/emptyShare_icon.png';
  emptyWatchLaterIcon: string = './assets/emptyWatchLater_icon.png';
  emptyFavoriteIcon: string = '/assets/emptyFavorite_icon.png';
  emptyHashtagIonc: string = '/assets/emptyHashtag_icon.png';
  emptyReviewIcon: string = '/assets/emptyReview_icon.png';

  shareViaGoogleIcon: string = 'assets/google_icon.png';
  shareViaFacebookIcon: string = 'assets/facebook_icon.png';
  viewListIconpath: string = './assets/viewList_icon.png';
  viewCardIconpath: string = './assets/viewCard_icon.png';
  editIcon: string = './assets/edit_icon.png';
  descendSortIconSrc = './assets/sortDescend_icon.png';
  ascendSortIconSrc = './assets/sortAscend_icon.png';

  private movies;

  setMovies(movies: ISimplifiedMovie[]) {
    this.movies = movies.slice(0);
  }

  initIcons(): void {
    this.movies.forEach(movie => {
      movie.watchLaterListIconSrc = movie.isInWatchLater ? this.watchLaterIcon : this.emptyWatchLaterIcon;
      movie.favoriteListIconSrc = movie.isInFavoriteList ? this.favoriteIcon : this.emptyFavoriteIcon;
      movie.tagIconSrc = movie.hasTags ? this.hashtagIonc : this.emptyHashtagIonc;
      movie.reviewIconSrc = movie.hasReview ? this.reviewIcon : this.emptyReviewIcon;
      movie.shareIconSrc = this.emptyShareIcon;
      movie.shareViaGoogleIconSrc = this.shareViaGoogleIcon;
      movie.shareViaFacebookIconSrc = this.shareViaFacebookIcon;
    });
  }

  changeSortIcon(previousPath: string) {
    if(previousPath === this.ascendSortIconSrc) return this.descendSortIconSrc;
    return this.ascendSortIconSrc;
  }

  changeTooltipWatchLaterText(index: number): void {
    const actualsrc = this.movies[index].watchLaterListIconSrc;
    if (actualsrc) {
      this.movies[index].watchLaterListIconSrc = actualsrc === this.watchLaterIcon ? this.emptyWatchLaterIcon : this.watchLaterIcon;
    } else {
      this.movies[index].watchLaterListIconSrc = this.emptyWatchLaterIcon;
    }
  }

  changeShareIcon(index: number): void {

    const actualsrc = this.movies[index].shareIconSrc;
    if (actualsrc) {
      this.movies[index].shareIconSrc = actualsrc === this.shareIcon ? this.emptyShareIcon : this.shareIcon;
    } else {
      this.movies[index].shareIconSrc = this.emptyShareIcon;
    }

  }

  changeWatchLaterIcon(index: number): void {
    const actualsrc = this.movies[index].watchLaterListIconSrc;
    if (actualsrc) {
      this.movies[index].watchLaterListIconSrc = actualsrc === this.watchLaterIcon ? this.emptyWatchLaterIcon : this.watchLaterIcon;
    } else {
      this.movies[index].watchLaterListIconSrc = this.emptyWatchLaterIcon;
    }
  }

  changeFavoriteIcon(index: number): void {
    const actualsrc = this.movies[index].favoriteListIconSrc;
    if (actualsrc) {
      this.movies[index].favoriteListIconSrc = actualsrc === this.favoriteIcon ? this.emptyFavoriteIcon : this.favoriteIcon;
    } else {
      this.movies[index].favoriteListIconSrc = this.emptyFavoriteIcon;
    }
  }

  changeHashtagIcon(index: number): void {
    const actualsrc = this.movies[index].hashtagIconSrc;
    if (actualsrc) {
      this.movies[index].hashtagIconSrc = actualsrc === this.hashtagIonc ? this.emptyHashtagIonc : this.hashtagIonc;
    } else {
      this.movies[index].hashtagIconSrc = this.emptyHashtagIonc;
    }
  }

  // changeReviewIcon(index : number) : void{
  //   const actualsrc = this.movies[index].reviewIconSrc;
  //   if(actualsrc){
  //     this.movies[index].reviewIconSrc = actualsrc === this.reviewIcon ? this.emptyReviewIcon : this.reviewIcon;
  //   }else {
  //     this.movies[index].reviewIconSrc = this.emptyReviewIcon;
  //   }
  // }

}
