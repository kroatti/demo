import {Component, OnInit} from '@angular/core';
import {
  InvoiceCreateCommand,
  InvoiceGetInvoiceDetailsDto,
  InvoiceSearchInvoiceDto,
  InvoiceService
} from "../../../../gen";
import {InvoiceFormComponent} from "../invoice-form/invoice-form.component";
import {HttpErrorResponse, HttpResponse} from "@angular/common/http";

@Component({
  selector: 'app-invoice-page',
  templateUrl: './invoice-page.component.html',
  styleUrls: ['./invoice-page.component.css']
})
export class InvoicePageComponent implements OnInit {
  selected?: InvoiceGetInvoiceDetailsDto;
  invoices: InvoiceSearchInvoiceDto[] = [];

  constructor(private api: InvoiceService) {
  }

  ngOnInit(): void {
    this.loadInvoices();
  }

  private loadInvoices() {
    this.api
      .apiInvoiceSearchGet()
      .subscribe(e => this.invoices = e);
  }

  select(id: number) {
    this.api
      .apiInvoiceIdGet(id)
      .subscribe(e => this.selected = e);
  }

  save($event: InvoiceCreateCommand, form: InvoiceFormComponent) {
    this.api
      .apiInvoicePut($event)
      .subscribe(_ => {
        form.form?.resetForm();
        this.loadInvoices();
      }, (e) => {
        if(e instanceof HttpErrorResponse && e.status === 400){
          // TODO set validatio error
          for(let key in e.error) {
            form.form?.controls[key]?.setErrors({serverErrors: e.error[key]})
          }
        }
      });
  }
}
