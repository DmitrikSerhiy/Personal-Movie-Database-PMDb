import { TestBed, inject } from '@angular/core/testing';

import { SearchMovieService } from './search-movie.service';

describe('SearchMovieService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SearchMovieService]
    });
  });

  it('should be created', inject([SearchMovieService], (service: SearchMovieService) => {
    expect(service).toBeTruthy();
  }));
});
