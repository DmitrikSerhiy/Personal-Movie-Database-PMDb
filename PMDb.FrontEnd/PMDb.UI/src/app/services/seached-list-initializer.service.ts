import { Injectable } from '@angular/core';
import { ISearchedMovie } from '../shared/interfaces/ISearchMovie';

@Injectable({
  providedIn: 'root'
})
export class SeachedListInitializerService {

  
  addIconSrc = './assets/add_icon.png'
  // rateIcon: string = './assets/rate_icon.png';
  // favoriteIcon: string = '/assets/favorite_icon.png';
  // hashtagIonc: string = '/assets/hashtag_icon.png';
  // reviewIcon: string = '/assets/review_icon.png';

  constructor() { }

  initIcons(movies: ISearchedMovie[]): void {
    movies.forEach(movie => {
      movie.addIconSrc = movie.isInLib ? '' : this.addIconSrc;
    });
  }
}
