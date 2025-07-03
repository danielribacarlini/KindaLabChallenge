import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodTruckMapComponent } from './food-truck-map.component';

describe('FoodTruckMap', () => {
  let component: FoodTruckMapComponent;
  let fixture: ComponentFixture<FoodTruckMapComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FoodTruckMapComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodTruckMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
