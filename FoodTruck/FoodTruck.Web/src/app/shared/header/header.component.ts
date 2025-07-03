import { Component, inject, signal } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  hideSideMenu = signal(true);


  toogleSideMenu() {
    this.hideSideMenu.update((prevState) => !prevState);
  }
}
