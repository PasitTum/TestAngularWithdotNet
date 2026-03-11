import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-page-layout',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './page-layout.component.html',
})
export class PageLayoutComponent {
  @Input() title = '';
}
