export default class GameScore {
    constructor(private name: string, private bestScore: number = 0, private currentScore: number = 0) {
    }

    increaseScore(): void{
        this.currentScore++;
        if (this.currentScore > this.bestScore){
            this.bestScore = this.currentScore;
        }
    }

    resetCurrentScore(): void{
        this.currentScore = 0;
    }

    getBestScore(): number{
        return this.bestScore;
    }

    getCurrentScore(): number{
        return this.currentScore;
    }

    getName(): string{
        return this.name;
    }

    newGame(): void{
        this.currentScore = 0;
    }
}