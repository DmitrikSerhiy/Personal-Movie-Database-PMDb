import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-empty-component',
  templateUrl: './empty-component.component.html',
  styleUrls: ['./empty-component.component.css']
})
export class EmptyComponentComponent implements OnInit, OnDestroy {


  private routeSubscription: Subscription;
  private querySubscription: Subscription;
  pageSize: number;
  currPage: number;
  movieToSearch;

  constructor(private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => this.movieToSearch = params['movieTitle']);
    this.querySubscription = this.route.queryParams.subscribe(
      (queryParam: any) => {
        this.pageSize = queryParam['pageSize'];
        this.currPage = queryParam['currPage'];
      });
    this.ngOnDestroy();
  }

  ngOnDestroy(): void {
    this.routeSubscription.unsubscribe();
    this.querySubscription.unsubscribe();
    window.location.reload(false); 
    this.goto();
  }

  goto() {
    if (this.movieToSearch) {
      this.router.navigate(['/search', this.movieToSearch],
        {
          queryParams: {
            'pageSize': 12,
            'currPage': 1,
          }
        });
    }
  }

}
