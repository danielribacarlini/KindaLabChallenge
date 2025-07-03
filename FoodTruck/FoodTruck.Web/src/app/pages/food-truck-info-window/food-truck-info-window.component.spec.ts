import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodTruckInfoWindowComponent } from './food-truck-info-window.component';

describe('FoodTruckInfoWindow', () => {
  let component: FoodTruckInfoWindowComponent;
  let fixture: ComponentFixture<FoodTruckInfoWindowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FoodTruckInfoWindowComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodTruckInfoWindowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
