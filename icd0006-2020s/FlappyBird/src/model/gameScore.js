export default class GameScore {
    constructor(name, bestScore = 0, currentScore = 0) {
        this.name = name;
        this.bestScore = bestScore;
        this.currentScore = currentScore;
    }

    increaseScore(){
        this.currentScore++;
        if (this.currentScore > this.bestScore){
            this.bestScore = this.currentScore;
        }
    }

    newGame(){
        this.currentScore = 0;
    }
}