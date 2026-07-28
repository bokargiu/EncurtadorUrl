import { CommonModule } from '@angular/common';
import { Component, Inject, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UrlService } from '../../Services/url.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  url: string = '';
  shortUrl: string = '';
  
  private urlService = inject(UrlService);

  sendUrl() {
    this.urlService.post(this.url).subscribe(
      (response) => {
        this.shortUrl = response.url;
      },
      (error) => {
        console.error('Error occurred while shortening URL:', error);
      }
    );
  }
}
