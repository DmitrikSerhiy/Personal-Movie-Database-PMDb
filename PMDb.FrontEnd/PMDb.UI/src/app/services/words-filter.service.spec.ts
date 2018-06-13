import { TestBed, inject } from '@angular/core/testing';

import { WordsFilterService } from './words-filter.service';

describe('WordsFilterService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WordsFilterService]
    });
  });

  it('should be created', inject([WordsFilterService], (service: WordsFilterService) => {
    expect(service).toBeTruthy();
  }));
});
