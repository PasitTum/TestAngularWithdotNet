import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

interface MenuItem {
  label: string;
  route: string;
  enabled: boolean;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  menuItems: MenuItem[] = [
    { label: 'IT 01', route: '/it01', enabled: true },
    { label: 'IT 02', route: '/it02/login', enabled: true },
    { label: 'IT 03', route: '/it03', enabled: true },
    { label: 'IT 04', route: '/it04', enabled: true },
    { label: 'IT 05', route: '/it05', enabled: true },
    { label: 'IT 06', route: '/it06', enabled: true },
    { label: 'IT 07', route: '/it07', enabled: true },
    { label: 'IT 08', route: '/it08', enabled: true },
    { label: 'IT 09', route: '/it09', enabled: true },
    { label: 'IT 10', route: '/it10', enabled: true },
  ];
}
