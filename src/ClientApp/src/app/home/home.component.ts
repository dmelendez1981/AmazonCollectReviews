import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ReviewService } from '../services/review.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {


  constructor(private reviewService: ReviewService) {  }
    
  

}
