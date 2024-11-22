import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-company-detail',
  templateUrl: './company-detail.component.html',
  styleUrls: ['./company-detail.component.css'],
})
export class CompanyDetailComponent implements OnInit {
  company: any = {};
  id: string = '';

  constructor(
    private route: ActivatedRoute,
    private companyService: CompanyService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') || '';
    this.fetchCompany();
  }

  fetchCompany(): void {
    this.companyService.getCompanyById(this.id).subscribe(
      (data) => {
        this.company = data;
      },
      (error) => {
        console.error('Error fetching company:', error);
      }
    );
  }

  updateCompany(): void {
    this.companyService.updateCompanyById(this.id, this.company).subscribe(
      () => {
        alert('Company updated successfully!');
        this.router.navigate(['/companies']);
      },
      (error) => {
        console.error('Error updating company:', error);
        alert('Failed to update company: ' + error.error);
      }
    );
  }
}
