import StatisticsView from '../views/statistics-view';
import ControlView from '../views/control-view';
import GameBrain from '../model/gamebrain';
import GameScore from '../model/gameScore';

export default class StatisticsController {
    public isRunning: boolean;
    private statisticsView: StatisticsView;

    constructor(private model: GameBrain, private viewContainer: HTMLDivElement, private controlView: ControlView) {
        this.model = model;
        this.isRunning = false;
        this.statisticsView = new StatisticsView((name: string) => {
        this.model.loadPlayerScore(name);
        this.unloadLeaderBoard();
        this.loadLeaderBoard();
        this.model.newGame();
    });
    }

    

    loadLeaderBoard(): void{
        this.isRunning = true;
        this.viewContainer.innerHTML = '';
        this.controlView.showName(this.model.getPlayerName());
        this.statisticsView.changePlayerName(this.model.getPlayerName());
        this.model.getScoreBoard().forEach((gameScore: GameScore) => {
            this.statisticsView.addPlayerToScoreBoard(gameScore.getName(), gameScore.getBestScore());
        });
        this.viewContainer.append(this.statisticsView.getContent());
    }

    unloadLeaderBoard():void{
        this.isRunning = false;
        this.statisticsView.cleanLeaderboard();
    }
}
