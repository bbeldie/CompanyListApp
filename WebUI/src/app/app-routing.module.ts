import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCompanyComponent } from './components/add-company/add-company.component';
import { CompanyDetailComponent } from './components/company-detail/company-detail.component';
import { CompanyListComponent } from './components/company-list/company-list.component';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/companies', pathMatch: 'full' },
  { path: 'companies', component: CompanyListComponent, canActivate: [AuthGuard] },
  { path: 'company/:id', component: CompanyDetailComponent, canActivate: [AuthGuard] },
  { path: 'add-company', component: AddCompanyComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }