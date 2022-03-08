import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {InvoiceModule} from "./invoice-module/invoice.module";
import {InvoicePageComponent} from "./invoice-module/invoice-page/invoice-page.component";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    InvoiceModule,
    RouterModule.forRoot([
      {path: 'invoice', component: InvoicePageComponent},
      {path: '', redirectTo: 'invoice', pathMatch: 'full'}
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
