import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlService } from '../../Services/url.service';

@Component({
  selector: 'app-redirect',
  standalone: true,
  imports: [],
  templateUrl: './redirect.component.html',
  styleUrl: './redirect.component.scss'
})
export class RedirectComponent implements OnInit {

  private urlService = inject(UrlService);

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    const code = this.route.snapshot.paramMap.get('code');
    this.urlService.get(code?.toString() || '').subscribe(
      (response) => {
        const longUrl = response.url;
        window.location.href = longUrl;
      },
      (error) => {
        console.error('Error occurred while retrieving URL:', error);
      }
    );
  }

}
