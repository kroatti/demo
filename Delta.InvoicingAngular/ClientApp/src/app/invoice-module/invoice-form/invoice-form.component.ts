import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {InvoiceCreateCommand} from "../../../../gen";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-invoice-form',
  templateUrl: './invoice-form.component.html',
  styleUrls: ['./invoice-form.component.css']
})
export class InvoiceFormComponent implements OnInit {

  @Output() save = new EventEmitter();
  @ViewChild('form') form?: NgForm;

  model: InvoiceCreateCommand = {
    invoiceItems: [{name: 'lorem 1', quantity: -12, unit: 'ipsum', unitPrice: 12},
      {name: 'lorem 2', quantity: 12, unit: 'ipsum', unitPrice: 12},
      {name: 'lorem 3', quantity: 12, unit: 'ipsum', unitPrice: 12}]
  };

  ngOnInit(): void {
  }

  submit() {
    this.save.emit(this.model);
  }
}
