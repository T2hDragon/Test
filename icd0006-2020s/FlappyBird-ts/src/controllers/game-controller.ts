import {GameView} from '../views/game-view';
import GameBrain from '../model/gamebrain';
import ControlView from '../views/control-view';

export default class GameController {
    public isRunning: boolean = false;
    private gameView: GameView = new GameView(this.model.getGameBoard());

    constructor(private model : GameBrain, private viewContainer: HTMLDivElement, private controlView: ControlView) {
        this.model = model;
        window.addEventListener('resize', () => {this.resizeUi();});
    }
    
    run() {
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
        if (!this.model.isGameOver()){
            this.model.nextGameTick()
            this.gameView.updateBoardHtml(this.model.getGameBoard());
        }
        if (this.gameView.jumped){ 
            if (this.model.isGameOver()){
                this.gameView.jumped = false;
                this.model.setGameOver(false);
                this.model.newGame();
            } else {
            this.model.bird.jump();
            this.gameView.jumped = false;
            }
        }
        if (this.gameView.stop ){ this.model.setGameOver(!this.model.isGameOver()) ;}
        this.controlView.updateGameScore(this.model.getBestScore(), this.model.getCurrentScore());
        this.runGame();
        },40);
    }

    resizeUi(){
        // redraw
        this.gameView.loadBoardHtml();
    }
    

}