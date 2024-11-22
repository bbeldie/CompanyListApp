import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-add-company',
  templateUrl: './add-company.component.html',
  styleUrls: ['./add-company.component.css'],
})
export class AddCompanyComponent implements OnInit {
  company: any;

  constructor(private companyService: CompanyService, private router: Router) { }

  ngOnInit(): void {
    this.company = {
      name: '',
      exchange: '',
      ticker: '',
      isin: '',
      website: ''
    };
  }

  addCompany(): void {
    this.companyService.addCompany(this.company).subscribe(
      () => {
        alert('Company added successfully!');
        this.router.navigate(['/companies']);
      },
      (error) => {
        console.error('Error adding company:', error);
        alert('Failed to add company: ' + error.error);
      }
    );
  }
}
