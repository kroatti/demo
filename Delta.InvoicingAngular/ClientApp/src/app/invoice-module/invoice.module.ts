import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {InvoiceListComponent} from './invoice-list/invoice-list.component';
import {InvoiceDetailsComponent} from './invoice-details/invoice-details.component';
import {InvoiceFormComponent} from './invoice-form/invoice-form.component';
import {InvoicePageComponent} from './invoice-page/invoice-page.component';
import {FormsModule} from "@angular/forms";
import {ApiModule, BASE_PATH} from "../../../gen";


@NgModule({
  declarations: [
    InvoiceListComponent,
    InvoiceDetailsComponent,
    InvoiceFormComponent,
    InvoicePageComponent,
  ],
  exports: [
    InvoicePageComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ApiModule
  ],
  providers: [
    {provide: BASE_PATH, useValue: 'https://localhost:7226'}
  ]
})
export class InvoiceModule {
}
