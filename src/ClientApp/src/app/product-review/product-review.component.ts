import { Component, Input, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Review } from 'src/app/models/review.model';

@Component({
  selector: 'app-product-review',
  templateUrl: './product-review.component.html'
})
export class ProductReviewComponent implements OnInit {

  reviewList: Review[] = [];
  loading: boolean = false;
  error: any;

  constructor(private reviewService: ReviewService) { }

  ngOnInit() {

  }

  findAllReviews(asin: string) {
    if (this.reviewList.length > 0) {
      this.reviewList = [];
    }
    this.reviewService.getTodoById(asin).subscribe(review => {
      this.reviewList = review
    });
  }

}
