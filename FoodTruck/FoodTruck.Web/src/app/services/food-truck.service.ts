import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { FoodTruck } from '../models/food-truck.model';
import {GoogleMap} from '@angular/google-maps';

@Injectable({
  providedIn: 'root'
})
export class FoodTruckService {
  private apiUrl = 'http://localhost:5000/api/foodtruck';

  constructor(private http: HttpClient) {}

  getFoodTrucks(): Observable<FoodTruck[]> {
    return this.http.get<FoodTruck[]>(this.apiUrl);
  }

  getNearbyFoodTrucks(point: google.maps.LatLngLiteral): Observable<FoodTruck[]> {
    const url = `${this.apiUrl}?lat=${point.lat}&lng=${point.lng}`;
    return this.http.get<FoodTruck[]>(url);
  }

  getFilteredFoodTrucks(
    point: google.maps.LatLngLiteral,
    categories: string[]
  ): Observable<FoodTruck[]> {
    let params = new HttpParams()
      .set('lat', point.lat.toString())
      .set('lng', point.lng.toString());

    if (categories.length > 0) {
      categories.forEach(cat => {
        params = params.append('categories', cat);
      });
    }

    return this.http.get<FoodTruck[]>(this.apiUrl, { params });
  }
}