import { Injectable, OnInit } from '@angular/core';
import { JsonReaderService } from './json-reader.service';
import { ISimplifiedMovie } from '../shared/interfaces/ISimplifiedMovie';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'; 
import { Observable } from 'rxjs/Observable';

//implements OnInit
@Injectable()
export class GetListService  {
  private urlWithoutMovieListName : any;
  errorMessage : string = '';

  private listName : string = '';

  constructor(private jsonReader : JsonReaderService,
              private http: HttpClient) { }

  // ngOnInit(): void {
  //   // this.jsonReader.getJSON().subscribe( (json : any) =>{
  //   //   this.urlWithoutMovieListName = json.getList;
  //   // },
  //   // error => this.errorMessage = <any>error),
  //   // () => console.log('json loaded');
  // }

  getMovieList(url : string) : Observable<ISimplifiedMovie[]>
  {
      return this.http.get<ISimplifiedMovie[]>(url)
          .do(data => console.log('All: ' + JSON.stringify(data)))
          .catch(this.handleError);
  }

  private handleError(err : HttpErrorResponse){
      console.log(err.message);
      return Observable.throw(err.message);
  }

  // setListName(listName : string){
  //   this.listName = listName;
  // }

  // private getURLForList() : string{
  //   return this.urlWithoutMovieListName + "/" + this.listName;
  // }
}
