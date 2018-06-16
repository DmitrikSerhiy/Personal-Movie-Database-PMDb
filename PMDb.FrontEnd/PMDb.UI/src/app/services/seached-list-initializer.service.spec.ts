import { TestBed, inject } from '@angular/core/testing';

import { SeachedListInitializerService } from './seached-list-initializer.service';

describe('SeachedListInitializerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SeachedListInitializerService]
    });
  });

  it('should be created', inject([SeachedListInitializerService], (service: SeachedListInitializerService) => {
    expect(service).toBeTruthy();
  }));
});
