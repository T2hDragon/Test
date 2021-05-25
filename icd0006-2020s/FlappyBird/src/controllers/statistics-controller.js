import StatisticsView from '../views/statistics-view.js';
import GameScore from '../model/gameScore.js'

export default class StatisticsController {

    constructor(model, viewContainer, controlView) {
        this.viewContainer = viewContainer;
        this.model = model;
        this.isRunning = false;
        this.controlView = controlView;
        this.statisticView = new StatisticsView((name) => {
        this.model.loadPlayerScore(name);
        this.unloadLeaderBoard();
        this.loadLeaderBoard();
        this.model.newGame();
    });
    }

    

    loadLeaderBoard(){
        this.isRunning = true;
        this.viewContainer.innerHTML = '';
        this.controlView.showName(this.model.getPlayerName());
        this.statisticView.changePlayerName(this.model.getPlayerName());
        console.log(this.model.getPlayerName());
        this.model.getScoreBoard().forEach(gameScore => {
            this.statisticView.addPlayerToScoreBoard(gameScore.name, gameScore.bestScore);
        });
        this.viewContainer.append(this.statisticView.getContent());
    }

    unloadLeaderBoard(){
        this.isRunning = false;
        this.statisticView.cleanLeaderboard();
    }
}
