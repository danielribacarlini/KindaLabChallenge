import { Component, signal, inject, viewChild, viewChildren, computed, effect } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { FoodTruckService } from '../../services/food-truck.service';
import { FoodTruck } from '../../models/food-truck.model';
import { GoogleMap, MapAdvancedMarker, MapInfoWindow } from '@angular/google-maps';

@Component({
  selector: 'app-food-truck-map',
  standalone: false,
  templateUrl: './food-truck-map.component.html',
  styleUrl: './food-truck-map.component.scss'
})

export class FoodTruckMapComponent {
  
  private foodTruckService = inject(FoodTruckService);

  get hasSelectedCategories(): boolean {
    return Object.values(this.selectedCategoriesMap).some(v => v);
  }

  get selectedMap() {
    return this.selectedCategoriesMap();
  }

  constructor() {
    effect(() => {
      const centerValue = this.center();
      const selected = this.selectedCategories();

      this.isLoading.set(true); 

      this.foodTruckService
        .getFilteredFoodTrucks(centerValue, selected)
        .subscribe({
          next: (trucks) => this.foodTrucks.set(trucks),
          error: (err) => console.error(err),
          complete: () => this.isLoading.set(false)
      });
    });
  }
  
  categories = [
    { name: 'Mexican Food', icon: '🌮' },
    { name: 'Mediterranean', icon: '🥙' },
    { name: 'Seafood', icon: '🦞' },
    { name: 'Pizza', icon: '🍕' },
    { name: 'Indian', icon: '🍛' },
    { name: 'Juice & Smoothies', icon: '🥤' },
    { name: 'Coffee & Tea', icon: '☕' },
    { name: 'Desserts', icon: '🍩' },
    { name: 'Fast Food', icon: '🍔' },
    { name: 'BBQ', icon: '🍖' },
    { name: 'Healthy', icon: '🥗' },
    { name: 'Soups', icon: '🍲' },
    { name: 'Snacks', icon: '🍿' }
  ];
  
  center = signal<google.maps.LatLngLiteral>({lat: 37.7749, lng: -122.4194});
  zoom = signal(12);

  selectedCategories = signal<string[]>([]);
  selectedCategoriesMap = signal<{ [key: string]: boolean }>({});
  foodTrucks = signal<FoodTruck[]>([]);
  isLoading = signal(false);

  toggleCategory(categoryName: string, checked: boolean) {
    const newMap = structuredClone(this.selectedCategoriesMap());
    newMap[categoryName] = checked;
    this.selectedCategoriesMap.set(newMap);

    const selected = Object.entries(newMap)
      .filter(([_, v]) => v)
      .map(([k]) => k);
    this.selectedCategories.set(selected);  
  }

  clearFilters() {
    const cleared: { [key: string]: boolean } = {};
    for (const key of Object.keys(this.selectedCategoriesMap())) {
      cleared[key] = false;
    }
    this.selectedCategoriesMap.set(cleared);
    this.selectedCategories.set([]);  
  }
  
  // Map functions

  infoWindow = viewChild.required(MapInfoWindow);
  markersRef = viewChildren(MapAdvancedMarker);

  selectedTruck = signal<FoodTruck | null>(null);

openInfoWindow(foodtruck: FoodTruck, marker: MapAdvancedMarker){
  this.selectedTruck.set(foodtruck);
  this.infoWindow().open(marker);
}

  // openInfoWindow(foodtruck: FoodTruck, marker: MapAdvancedMarker){
  //   const content = `
  //     <h1 class="font-bold text-kl">${foodtruck.applicant}</h1>
  //     <p>${foodtruck.locationDescription}</p>
  //   `;
  //   this.infoWindow().open(marker, false, content);
  // }

  goToPoint(foodTruck: FoodTruck, position: number){
    const markers = this.markersRef();
    const markerRef = markers[position];

    this.openInfoWindow(foodTruck, markerRef);
  }
}