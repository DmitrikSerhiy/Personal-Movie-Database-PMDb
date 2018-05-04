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
import { RatingModule } from 'ngx-bootstrap';



@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RatingModule,
    RouterModule.forRoot([
      {path: 'welcome', component: WelcomeComponent },
      {path: 'movies', component: MovieLibraryComponent},
      {path: '', redirectTo: 'welcome', pathMatch: 'full'},
      //{path: '**',  redirectTo: 'NotFoundComponent', pathMatch: 'full'}
    ])
  ],
  declarations: [
    AppComponent,
    MovieLibraryComponent,
    NotFoundComponent,
    WelcomeComponent,
    CustomRuntimePipe
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
