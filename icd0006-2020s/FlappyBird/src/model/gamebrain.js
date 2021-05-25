import { Bird } from './bird.js';
import * as C from './constants.js';
import GameScore from './gameScore.js'



export default class GameBrain {


    constructor(rowCount = 27, columnCount = 48, name = "guest") {
        this.rowCount = rowCount;
        this.columnCount = columnCount;
        this.gameOver = false;

        this.board = [];
        this.scoreBoard = []; // of GameScore
        this.loadPlayerScore(name);

        this.untilObstacle = 0;
        this.obstacleWidth = 4;
        this.obstacleSpaceRation = 1/3;
        this.obstacleSpaceVariation = 5;
        this.obstacleSpawnChanceModifier = 0.01;
        this.obstaclePadding = 5;

        this.newGame();
    }

    getBestScore(){
        return this.gameScore.bestScore;
    }

    getCurrentScore(){
        return this.gameScore.currentScore;
    }

    getScoreBoard(){
        return this.scoreBoard;
    }

    getPlayerName(){
        return this.gameScore.name;
    }


    loadPlayerScore(name){
        let foundPlayer = false;
        this.scoreBoard.forEach(gameScore => {
            if (gameScore.name === name){
                this.lastScore = 0;
                this.gameScore = gameScore;
                foundPlayer = true;
            }
        });
        if (!foundPlayer){
            this.gameScore = new GameScore(name);
            this.scoreBoard.push(this.gameScore);
        }
    }

    getCollision(){
        return this.board[this.bird.getColumn()][this.bird.getRow()];
    }


    nextGameTick(){
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
        switch (this.getCollision()) {
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



    newGame() {
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

    getGameBoard() {
        return this.board.slice(0,this.columnCount);
    }

    addColumn(hasObstacle = false, topObstacleLength = 0, space = 0, obstacleDepth = 0){
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