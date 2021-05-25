export {Bird}

class Bird{
    constructor(col, row, maxRow, jumpPower = 5, changePerTick = 0.2){
        this.col = col;
        this.row = row;
        this.maxRow = maxRow;

        this.jumpPower = jumpPower;
        this.currentPower = this.jumpPower;
        this.changePerTick = changePerTick
        this.change = this.urrentPower * this.changePerTick;
    }


    jump(){
        this.currentPower = this.jumpPower;
    }

    nextGameTick(){
      this.row -= this.currentPower * this.changePerTick;
      this.currentPower = this.currentPower - 0.5;
    }


    getRow(){
      return Math.round(this.row);
    }

    getColumn(){
      return Math.round(this.col);
    }

    checkOutOfBounds(){
      return (this.maxRow < this.row || 0 > this.row);
    }
    
  



    newPosition(col, row){
        this.col = col;
        this.row = row;
    }
}