import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CompanyService {
  private apiUrl = '/api/company';

  constructor(private http: HttpClient) { }

  getCompanyById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  getFilteredCompanies(name: string = '', isin: string = ''): Observable<any[]> {
    let params = new HttpParams();
    if (name) {
      params = params.set('name', name);
    }
    if (isin) {
      params = params.set('isin', isin);
    }

    return this.http.get<any[]>(`${this.apiUrl}/filtered`, { params });
  }

  updateCompanyById(id: string, data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(`${this.apiUrl}/${id}`, data, { headers });
  }

  addCompany(company: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(`${this.apiUrl}`, company, { headers });
  }
}
