import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { LoadingService } from './loading.service';

@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit, OnDestroy {

  loading = false;
  loadingSuscription!: Subscription;

  constructor(private loadingService: LoadingService) { }

  ngOnInit() {
    this.loadingSuscription = this.loadingService.loadingStatus
      .subscribe((value: boolean) => {
        this.loading = value;
      });
  }

  ngOnDestroy() {
    this.loadingSuscription.unsubscribe();
  }

}
