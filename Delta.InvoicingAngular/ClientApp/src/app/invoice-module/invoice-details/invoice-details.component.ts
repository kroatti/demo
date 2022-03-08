import {Component, Input, OnInit} from '@angular/core';
import {InvoiceGetInvoiceDetailsDto} from "../../../../gen";

@Component({
  selector: 'app-invoice-details',
  templateUrl: './invoice-details.component.html',
  styleUrls: ['./invoice-details.component.css']
})
export class InvoiceDetailsComponent implements OnInit {

  @Input() invoice?: InvoiceGetInvoiceDetailsDto;

  constructor() { }

  ngOnInit(): void {
  }

}
