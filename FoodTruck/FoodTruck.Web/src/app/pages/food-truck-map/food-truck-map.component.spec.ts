import { Component } from '@angular/core';
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FoodTruckMapComponent } from './food-truck-map.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core'; 
import { Subject, of } from 'rxjs';
import { FoodTruckService } from '../../services/food-truck.service';
import { FoodTruck } from '../../models/food-truck.model';
import { MapAdvancedMarker, MapInfoWindow } from '@angular/google-maps';

@Component({
  standalone: false,
  selector: 'map-info-window',
  template: ''
})
class MockMapInfoWindowComponent {
  open() {}
  close() {}
  setOptions(_: any) {}
}

describe('FoodTruckMap', () => {
  let component: FoodTruckMapComponent;
  let fixture: ComponentFixture<FoodTruckMapComponent>;

  beforeAll(() => {
    (window as any).google = {
      maps: {
        Map: class {},
        Marker: class {},
        InfoWindow: class {},
        LatLng: class {},
        LatLngBounds: class {},
      }
    };
  });

  beforeEach(async () => {
    const mockService = jasmine.createSpyObj('FoodTruckService', ['getFilteredFoodTrucks']);
    mockService.getFilteredFoodTrucks.and.returnValue(of([])); // valor por defecto seguro

    await TestBed.configureTestingModule({
      declarations: [FoodTruckMapComponent, MockMapInfoWindowComponent],
      imports: [
        HttpClientTestingModule,
        FormsModule,
        CommonModule
      ],
      providers: [
        { provide: FoodTruckService, useValue: mockService }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();

    fixture = TestBed.createComponent(FoodTruckMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  //Component Instance
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  //Categories selection logic
  it('should toggle category on', () => {
    component.toggleCategory('Pizza', true);
    expect(component.selectedMap['Pizza']).toBeTrue();
    expect(component.selectedCategories()).toContain('Pizza');
  });

  it('should toggle category off', () => {
    component.toggleCategory('Pizza', true);
    component.toggleCategory('Pizza', false);
    expect(component.selectedMap['Pizza']).toBeFalse();
    expect(component.selectedCategories()).not.toContain('Pizza');
  });

  //Clear filters
  it('should clear all category filters', () => {
    component.toggleCategory('Pizza', true);
    component.toggleCategory('BBQ', true);
    component.clearFilters();
    expect(component.selectedCategories()).toEqual([]);
    expect(Object.values(component.selectedMap)).toEqual(
      Object.keys(component.selectedMap).map(() => false)
    );
  });

  //HasSelectedCategories property
  it('should return true if any category is selected', () => {
    component.toggleCategory('Coffee & Tea', true);
    expect(component.hasSelectedCategories).toBeTrue();
  });

  it('should return false if no category is selected', () => {
    component.clearFilters();
    expect(component.selectedCategories()).toEqual([]);
  });

  //Service Call
  it('should call service when center or selectedCategories changes', fakeAsync(() => {
      const mockService = TestBed.inject(FoodTruckService) as jasmine.SpyObj<FoodTruckService>;
      mockService.getFilteredFoodTrucks.calls.reset();

      // Update signals
      component.center.set({ lat: 1, lng: 1 });
      component.selectedCategories.set(['Pizza']);

      // Force execution of reactive effects
      fixture.detectChanges();
      tick(); 

      expect(mockService.getFilteredFoodTrucks).toHaveBeenCalledWith(
        { lat: 1, lng: 1 },
        ['Pizza']
      );
  }));
});
