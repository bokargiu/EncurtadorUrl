import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  constructor(private http: HttpClient) { }

  public post(url: string): Observable<any> {
    return this.http.post('http://localhost:5131/api/Url', {
      url: url,
      domain: 'http://localhost:57650'
    });
  }

  public get(code: string): Observable<any> {
    return this.http.get(`http://localhost:5131/api/Url/${code}`);
  }
}
