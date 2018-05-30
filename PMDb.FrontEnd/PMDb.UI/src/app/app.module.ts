import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { MovieLibraryComponent } from './movieLibrary/movieLibrary.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { CustomRuntimePipe } from './Shared/custom-time-pipe.pipe';
import { CustomeDecimalPipePipe } from './Shared/custome-decimal-pipe.pipe';
import { RatingModule } from 'ngx-bootstrap/rating';
//import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { NgxPopperModule } from 'ngx-popper';

//import { TooltipModule } from 'ngx-bootstrap';
import { TooltipModule } from 'ng2-tooltip-directive';
import { AccordionModule } from 'ngx-bootstrap';
import { BsDropdownModule } from 'ngx-bootstrap';
import { PaginationModule } from 'ngx-bootstrap';
import { PopoverModule } from 'ngx-bootstrap';
import { BarRatingModule } from "ngx-bar-rating";

import {MatSliderModule} from '@angular/material/slider';
import {MatFormFieldModule} from '@angular/material/form-field';
// import {RatingModule} from "ngx-rating";


//import { TimepickerModule } from 'ngx-bootstrap';
//import { bs-dropdown } from 'ngx-bootstrap/ BsDropdownDirective';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
//  import { MatTooltipModule } from '@angular/material/tooltip';
//  import { MatButtonModule } from '@angular/material/button';





@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RatingModule,
    //TooltipModule.forRoot(),
    AccordionModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    PopoverModule.forRoot(),
    ////TimepickerModule.forRoot();
    TooltipModule,
    NgxPopperModule,
    BarRatingModule,
    // BrowserAnimationsModule,
    //  MatTooltipModule,
    //  MatButtonModule,
    MatSliderModule,
    MatFormFieldModule,
    RouterModule.forRoot([
      {path: 'welcome', component: WelcomeComponent },
      {path: 'movies', component: MovieLibraryComponent},
      //{path: '', redirectTo: 'welcome', pathMatch: 'full'},
      //{path: '**',  redirectTo: 'NotFoundComponent', pathMatch: 'full'}
    ])
  ],
  declarations: [
    AppComponent,
    MovieLibraryComponent,
    NotFoundComponent,
    WelcomeComponent,
    CustomRuntimePipe,
    CustomeDecimalPipePipe,
    
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
