import { TestBed, inject } from '@angular/core/testing';

import { MovieInitializerService } from './movie-initializer.service';

describe('MovieInitializerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MovieInitializerService]
    });
  });

  it('should be created', inject([MovieInitializerService], (service: MovieInitializerService) => {
    expect(service).toBeTruthy();
  }));
});
