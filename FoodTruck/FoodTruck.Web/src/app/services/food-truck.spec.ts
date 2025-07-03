import { TestBed } from '@angular/core/testing';
import { FoodTruckService } from './food-truck.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('FoodTruck', () => {
  let service: FoodTruckService;

  beforeEach(() => {
    TestBed.configureTestingModule({
          imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(FoodTruckService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
