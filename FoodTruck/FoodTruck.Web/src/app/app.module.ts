import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { FoodTruckMapComponent } from './pages/food-truck-map/food-truck-map.component';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from './shared/header/header.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { GoogleMap, MapAdvancedMarker, MapInfoWindow } from '@angular/google-maps';
import { FoodTruckInfoWindowComponent } from './pages/food-truck-info-window/food-truck-info-window.component';


@NgModule({
  declarations: [
    AppComponent,
    FoodTruckMapComponent,
    HeaderComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    GoogleMap,
    MapAdvancedMarker,
    MapInfoWindow,
    FoodTruckInfoWindowComponent,
    RouterModule.forRoot([
      {    
        path: '',
        component: LayoutComponent,
        children: [     
          { path: 'foodtrucks', component: FoodTruckMapComponent },
          { path: '**', redirectTo: '' }]
      }
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
