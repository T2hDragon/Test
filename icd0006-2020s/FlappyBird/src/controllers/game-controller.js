import {GameView} from '../views/game-view.js';
export default class GameController {

    constructor(model, viewContainer, controlView) {
        this.viewContainer = viewContainer;
        this.model = model;
        this.isRunning = false;
        this.gameView = new GameView(this.model.getGameBoard());
        this.controlView = controlView;
        window.addEventListener('resize', () => {this.resizeUi();});
    }
    
    run() {
        console.log("Game Controller run: " + this.model.name);
        this.isRunning = true;
        // draw the initial game board, start the game
        this.viewContainer.innerHTML = '';
        this.viewContainer.append(this.gameView.getBoardHtml());

        // start countdown

        // start game-loop
        this.controlView.showScore();
        this.runGame();
    }


    stop(){
        this.isRunning = false;
    }

    runGame(){
                setTimeout(() => {
        if (!this.isRunning) {return;}
        if (!this.model.gameOver){
            this.model.nextGameTick()
            this.gameView.updateBoardHtml(this.model.getGameBoard());
        }
        if (this.gameView.jumped){ 
            if (this.model.gameOver){
                this.gameView.jumped = false;
                this.model.gameOver = false;
                this.model.newGame();
            } else {
            this.model.bird.jump();
            this.gameView.jumped = false;
            }
        }
        if (this.gameView.stop ){ this.model.gameOver != this.model.gameOver;}
        this.controlView.updateGameScore(this.model.getBestScore(), this.model.getCurrentScore());
        this.runGame();
        },40);
    }

    resizeUi(){
        // redraw
        this.gameView.loadBoardHtml();
    }
    

}