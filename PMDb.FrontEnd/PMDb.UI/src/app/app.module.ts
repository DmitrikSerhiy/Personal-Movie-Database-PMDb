import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';
import { MovieLibraryComponent } from './movieLibrary/movieLibrary.component';
import { RouterModule, RouteReuseStrategy, Routes } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { MovieListComponent } from './movie-list/movie-list.component';

import { CustomRuntimePipe } from './Shared/custom-time-pipe.pipe';
import { CustomeDecimalPipePipe } from './Shared/custome-decimal-pipe.pipe';

import { NgxPopperModule } from 'ngx-popper';
import { TooltipModule } from 'ng2-tooltip-directive';
import { AccordionModule, BsDropdownModule,  PaginationModule,  PopoverModule, RatingModule  } from 'ngx-bootstrap';
import { ButtonsModule } from 'ngx-bootstrap';
import { BarRatingModule } from "ngx-bar-rating";
import {MatSliderModule} from '@angular/material/slider';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MovieCardComponent } from './movie-card/movie-card.component';
import { OrderModule } from 'ngx-order-pipe';
import { SearchListComponent } from './search-list/search-list.component';
import { CustomReuseStrategy } from './services/CustomReuseStrategy';
import { AppGuard } from './shared/AppGuard';
import { EmptyComponentComponent } from './empty-component/empty-component.component';



const routes: Routes = [
  {path: '', component: WelcomeComponent },
  {path: 'library', component: MovieListComponent},
  {path: 'tempList', component: MovieListComponent},
  {path: 'watchLater', component: MovieListComponent},
  {path: 'favorite', component: MovieListComponent},
  {path: 'empty/:movieTitle', component: EmptyComponentComponent},
  {path: 'search/:movieTitle', component: SearchListComponent, runGuardsAndResolvers: 'always'},
  {path: '#', component: NotFoundComponent },
  {path: '**', component: NotFoundComponent },
];

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
    ButtonsModule.forRoot(),
    TooltipModule,
    NgxPopperModule,
    BarRatingModule,
    MatSliderModule,
    MatFormFieldModule,
    OrderModule,
    RouterModule.forRoot(routes, {
      onSameUrlNavigation: 'reload'
    })],
  declarations: [
    AppComponent,
    MovieLibraryComponent,
    NotFoundComponent,
    WelcomeComponent,
    CustomRuntimePipe,
    CustomeDecimalPipePipe,
    MovieListComponent,
    MovieCardComponent,
    SearchListComponent,
    EmptyComponentComponent,
  ],

  providers: [{provide: RouteReuseStrategy, useClass: CustomReuseStrategy}, AppGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
