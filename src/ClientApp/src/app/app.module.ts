import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { HomeComponent } from './home/home.component';
import { ProductReviewComponent } from './product-review/product-review.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ProductReviewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
