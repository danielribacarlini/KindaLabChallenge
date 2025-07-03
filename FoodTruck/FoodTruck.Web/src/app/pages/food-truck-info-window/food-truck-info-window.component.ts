import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoodTruck } from '../../models/food-truck.model';

@Component({
  selector: 'app-food-truck-info-window',
  imports: [CommonModule],
  templateUrl: './food-truck-info-window.component.html',
  styleUrl: './food-truck-info-window.component.scss'
})
export class FoodTruckInfoWindowComponent {
     @Input() foodTruck!: FoodTruck;
}
