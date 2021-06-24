export class Review {

    constructor(public asin: string, 
        public reviewTitle: string, public reviewContent: string, 
        public reviewDate: Date, public reviewRating: number) { }
        
}