import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { MovieLibraryComponent } from './movieLibrary/movieLibrary.component';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { MovieListComponent } from './movie-list/movie-list.component';

import { CustomRuntimePipe } from './Shared/custom-time-pipe.pipe';
import { CustomeDecimalPipePipe } from './Shared/custome-decimal-pipe.pipe';

import { NgxPopperModule } from 'ngx-popper';
import { TooltipModule } from 'ng2-tooltip-directive';
import { AccordionModule, BsDropdownModule,  PaginationModule,  PopoverModule, RatingModule  } from 'ngx-bootstrap';
import { BarRatingModule } from "ngx-bar-rating";
import {MatSliderModule} from '@angular/material/slider';
import {MatFormFieldModule} from '@angular/material/form-field';



@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RatingModule,
    AccordionModule.forRoot(),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    PopoverModule.forRoot(),
    TooltipModule,
    NgxPopperModule,
    BarRatingModule,
    MatSliderModule,
    MatFormFieldModule,
    RouterModule.forRoot([
      {path: '', component: WelcomeComponent },
      {path: 'library', component: MovieLibraryComponent},
      {path: 'tempList', component: MovieListComponent},
      {path: '#', component: NotFoundComponent },
      {path: '**', component: NotFoundComponent }
    ])
  ],
  declarations: [
    AppComponent,
    MovieLibraryComponent,
    NotFoundComponent,
    WelcomeComponent,
    CustomRuntimePipe,
    CustomeDecimalPipePipe,
    MovieListComponent,
    
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
