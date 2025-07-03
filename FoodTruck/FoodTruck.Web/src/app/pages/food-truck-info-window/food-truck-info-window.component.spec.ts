import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodTruckInfoWindowComponent } from './food-truck-info-window.component';

describe('FoodTruckInfoWindow', () => {
  let component: FoodTruckInfoWindowComponent;
  let fixture: ComponentFixture<FoodTruckInfoWindowComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [FoodTruckInfoWindowComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(FoodTruckInfoWindowComponent);
    component = fixture.componentInstance;

    component.foodTruck = {
      applicant: 'Test Truck',
      locationDescription: '123 Street',
      latitude: '0',
      longitude: '0',
      foodItems: '',
      foodItemsList: ['tacos', 'burritos']
    };

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

    it('should display food truck name and items', () => {
    component.foodTruck = {
      applicant: 'Test Truck',
      locationDescription: '123 Street',
      foodItemsList: ['Burger', 'Fries'],
      latitude: '1',
      longitude: '2',
      foodItems: ''
    };
    fixture.detectChanges();

    const compiled = fixture.nativeElement;
    expect(compiled.textContent).toContain('Test Truck');
    expect(compiled.textContent).toContain('Burger');
    expect(compiled.textContent).toContain('Fries');
  });
});
