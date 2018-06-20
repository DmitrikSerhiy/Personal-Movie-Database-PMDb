import { TestBed, inject } from '@angular/core/testing';

import { GetMovieService } from './get-movie.service';

describe('GetMovieService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GetMovieService]
    });
  });

  it('should be created', inject([GetMovieService], (service: GetMovieService) => {
    expect(service).toBeTruthy();
  }));
});
