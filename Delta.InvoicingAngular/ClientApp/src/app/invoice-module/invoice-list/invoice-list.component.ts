import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { InvoiceSearchInvoiceDto} from "../../../../gen";

@Component({
  selector: 'app-invoice-list',
  templateUrl: './invoice-list.component.html',
  styleUrls: ['./invoice-list.component.css']
})
export class InvoiceListComponent implements OnInit {

  @Input() invoices: InvoiceSearchInvoiceDto[] = [];
  @Output() selected = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void {
  }

}
