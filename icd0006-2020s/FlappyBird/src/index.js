import GameBrain from './model/gamebrain.js';


import MainView from './views/main-view.js';
import ViewContainer from './views/view-container.js';
import ControlView from './views/control-view.js';

import GameController from './controllers/game-controller.js';
import StatisticsController from './controllers/statistics-controller.js';

// Constant values
const COLUMN_COUNT = 48;
const ROW_COUNT = 27;
const OBSTACLE_SPACE_RATIO = 1/3;
const OBSTACLE_SPACE_VARIATION = 5;
const OBSTACLE_WIDTH = 2;
const OBSTACLE_SPAWN_CHANCE = 80;
const OBSTACLE_PADDING = 1;
const GAME_LENGTH = 5;

//CSS
document.body.style.margin = 0;
document.body.style.padding = 0;
document.body.style.overflow = "hidden";

let brain = new GameBrain();
let mainView = new MainView();
let mainContainer = new ViewContainer();
let controlView = new ControlView(viewSelectionClick);

let gameController = new GameController(brain, mainContainer, controlView);
let statisticsController = new StatisticsController(brain, mainContainer, controlView);

document.body.appendChild(mainView);
mainView.append(controlView.getContent());
mainView.append(mainContainer);

gameController.run();



function viewSelectionClick(e) {
    if (e.target.classList.contains("game-button")){
        if (!gameController.isRunning){
            statisticsController.unloadLeaderBoard();
            gameController.run();
        }
    } else if (e.target.classList.contains("statistics-button")){
        if (!statisticsController.isRunning){
            gameController.stop();
            statisticsController.loadLeaderBoard();
        }
    } else {
        throw new Error('Pressed unknown control button with class \"' + e.target.classList + '\"');
    }
}


