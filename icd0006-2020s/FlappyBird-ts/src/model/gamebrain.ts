import { Bird } from './bird';
import C from './constants';
import GameScore from './gameScore'



export default class GameBrain {
    private gameOver: boolean = false;
    private board: C[][] = [];
    private scoreBoard: GameScore[] = [];
    private obstacleWidth: number = 4;
    private untilObstacle: number = 0;
    private obstacleSpaceRation: number = 1/3;
    private obstacleSpaceVariation: number = 5;
    private obstacleSpawnChanceModifier: number = 0.01;
    private obstaclePadding: number = 5;
    private gameScore!: GameScore;
    public bird!: Bird;

    constructor(private rowCount: number = 27, private columnCount: number = 48, name: string = "guest") {
        this.loadPlayerScore(name);
        this.newGame();
    }

    getBestScore(): number{
        return this.gameScore.getBestScore();
    }

    getCurrentScore(): number{
        return this.gameScore.getCurrentScore();
    }


    getScoreBoard(): GameScore[]{
        return this.scoreBoard;
    }

    getPlayerName() : string{
        return this.gameScore.getName();
    }

    isGameOver(): boolean{
        return this.gameOver;
    }
    setGameOver(gameOver: boolean): void{
        this.gameOver = gameOver;
    }


    loadPlayerScore(name: string): void{
        let foundPlayer: boolean = false;
        this.scoreBoard.forEach((gameScore : GameScore)=> {
            if (gameScore.getName() === name){
                gameScore.resetCurrentScore();
                this.gameScore = gameScore;
                foundPlayer = true;
                return;
            }
        });
        if (!foundPlayer){
            this.gameScore = new GameScore(name);
            this.scoreBoard.push(this.gameScore);
        }
    }

    getCollisionObject(): C{
        return this.board[this.bird.getColumn()][this.bird.getRow()];
    }


    nextGameTick(): void{
        if (this.untilObstacle < 0) {
            let obstacleSpawnChance = Math.abs(this.untilObstacle) * this.obstacleSpawnChanceModifier;
            if (obstacleSpawnChance >= Math.random()){
                let obstacleSpace = (this.rowCount * this.obstacleSpaceRation) + Math.round((Math.random() * 2 - 1 )* this.obstacleSpaceVariation);
                let max = this.rowCount - this.obstaclePadding - obstacleSpace;
                let min = this.obstaclePadding;
                let topObstacleLength = Math.floor(Math.random() * (max - min + 1)) + min;
                
                for (let index = 0; index < this.obstacleWidth; index++) {
                    this.addColumn(true, topObstacleLength, obstacleSpace, index);
                }
                this.untilObstacle = 18;
            }
        } 
        if (this.board.length <= this.columnCount){
            this.addColumn();
            this.untilObstacle--;  
        }

        this.board[this.bird.getColumn()][this.bird.getRow()] = C.BACKGROUND_ID;
        this.bird.nextGameTick();
        this.board.shift();
        switch (this.getCollisionObject()) {
            case C.OBSTACLE_ID:
                this.gameOver = true;
                break;
            case undefined:
                this.gameOver = true;
                break;
            case C.POINT_ID:
                this.gameScore.increaseScore();
                break;
            default:
                break;
        }

        this.board[this.bird.getColumn()][this.bird.getRow()] = C.BIRD_ID;
    }



    newGame():void {
        this.board = [];
        this.gameScore.newGame();
        this.gameOver = false;
        this.bird = new Bird(2, Math.floor(this.rowCount/2), this.rowCount);
        for (let colIndex = 0; colIndex < this.columnCount; colIndex++) {
            let columnCells = [];
            for (let rowIndex = 0; rowIndex < this.rowCount; rowIndex++) {
                columnCells.push(C.BACKGROUND_ID);
            }
            this.board.push(columnCells);
        }
        this.board[this.bird.getColumn()][this.bird.getRow()] = C.BIRD_ID;
    }

    getGameBoard(): C[][] {
        return this.board.slice(0,this.columnCount);
    }

    addColumn(hasObstacle = false, topObstacleLength = 0, space = 0, obstacleDepth = 0): void{
        let columnCells = [];
        for (let rowIndex = 0; rowIndex < this.rowCount; rowIndex++) {
            if (hasObstacle){
                if ((rowIndex<topObstacleLength ||
                rowIndex>(topObstacleLength + space))){
                    columnCells.push(C.OBSTACLE_ID);
                } else if (!obstacleDepth) {
                    columnCells.push(C.POINT_ID);
                } else {
                    columnCells.push(C.BACKGROUND_ID);
                }
            } else {
                columnCells.push(C.BACKGROUND_ID);
            }
        } 
        this.board.push(columnCells);
    }
}