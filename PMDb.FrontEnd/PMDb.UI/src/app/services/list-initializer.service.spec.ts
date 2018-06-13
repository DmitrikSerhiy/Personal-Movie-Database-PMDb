import { TestBed, inject } from '@angular/core/testing';

import { ListInitializerService } from './list-initializer.service';

describe('ListInitializerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ListInitializerService]
    });
  });

  it('should be created', inject([ListInitializerService], (service: ListInitializerService) => {
    expect(service).toBeTruthy();
  }));
});
