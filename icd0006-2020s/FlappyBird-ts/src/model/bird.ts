export {Bird}

class Bird{
  
    private currentPower: number;

    constructor(private col: number, private row: number, private maxRow: number, private jumpPower: number = 5, private changePerTick: number = 0.2){
        this.currentPower = this.jumpPower;
    }


    jump(): void{
        this.currentPower = this.jumpPower;
    }

    nextGameTick(): void{
      this.row -= this.currentPower * this.changePerTick;
      this.currentPower = this.currentPower - 0.5;
    }


    getRow(): number{
      return Math.round(this.row);
    }

    getColumn() : number{
      return Math.round(this.col);
    }

    checkOutOfBounds(): boolean{
      return (this.maxRow < this.row || 0 > this.row);
    }
    
  



    newPosition(col: number, row: number): void{
        this.col = col;
        this.row = row;
    }
}