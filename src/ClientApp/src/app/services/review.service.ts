import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Review } from '../models/review.model';


const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
};


@Injectable({
    providedIn: 'root'
})
export class ReviewService {

    private readonly amazonReviewsEndpoint: string = "/api/reviews/";

    constructor(private http: HttpClient) { }

    getTodoById(id: string): Observable<Review[]> {
        return this.http.get<Review[]>(this.amazonReviewsEndpoint + id);
    }

}