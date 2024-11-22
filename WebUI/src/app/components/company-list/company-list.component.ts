import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-company-list',
  templateUrl: './company-list.component.html',
  styleUrls: ['./company-list.component.css'],
})
export class CompanyListComponent implements OnInit {
  companies: any[] = [];
  displayedColumns: string[] = [];
  filters = {
    name: '',
    isin: '',
  };

  constructor(
    private companyService: CompanyService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.fetchCompanies();
  }

  fetchCompanies(): void {
    this.companyService.getFilteredCompanies().subscribe({
      next: (data) => {
        this.companies = data;
        if (data.length > 0) {
          this.displayedColumns = Object.keys(data[0]);
          this.displayedColumns.push('actions');
        }
      },
      error: (error) => {
        console.error('Error fetching companies:', error);
      }
    }
    );
  }

  applyFilters(): void {
    const { name, isin } = this.filters;

    this.companyService.getFilteredCompanies(name, isin).subscribe({
      next: (data) => {
        this.companies = data;
      },
      error: (error) => {
        console.error('Error filtering companies:', error);
      }
    }
    );
  }

  editCompany(id: string): void {
    this.router.navigate(['/company', id]);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']); // Redirect to the login page
  }
}
