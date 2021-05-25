export default class ControlView {
    constructor(eventHandler){
        this.createContent();
        this.createStatisticsButton();
        this.createGameButton();
        this.createInformationElem();
        this.showScore();
        this.content.append(this.statisticsButton);
        this.content.append(this.playerInformationElem);
        this.content.append(this.gameButton);
        this.statisticsButton.addEventListener('click', eventHandler);
        this.gameButton.addEventListener('click', eventHandler);
    }

    createContent(){
        this.content = document.createElement('div');
        this.content.style.height = "50px";
        this.content.id = 'control';
    }

    createStatisticsButton(){
        this.statisticsButton = document.createElement('button');
        this.statisticsButton.classList.add('statistics-button');
        this.statisticsButton.innerText='Statistics';
        this.statisticsButton.style.display = "inline-block";
        this.statisticsButton.style.position = "absolute";
        this.statisticsButton.style.left = "0px";
        this.statisticsButton.style.width = "40%";
        this.statisticsButton.style.height = "50px";
    }

    createGameButton(){
        this.gameButton = document.createElement('button');
        this.gameButton.classList.add('game-button');
        this.gameButton.innerText='Game';
        this.gameButton.style.display = "inline-block";
        this.gameButton.style.position = "absolute";
        this.gameButton.style.right = "0px";
        this.gameButton.style.width = "40%";
        this.gameButton.style.height = "50px";
    }

    createInformationElem(){
        this.playerInformationElem = document.createElement('div');
        this.playerInformationElem.style.display = "inline-block";
        this.playerInformationElem.style.height = "50px";
        this.playerInformationElem.style.width = "20%";
        this.playerInformationElem.style.marginLeft = "40%";
        this.playerInformationElem.style.textAlign = "center";
        this.playerInformationElem.style.fontFamily = "Lucida Console", "Monaco";
        this.playerInformationElem.classList.add('information');        
    }

    showScore(){
        this.playerInformationElem.innerHTML = '';
        this.playerTopScore = document.createElement('div');
        this.playerCurrentScore = document.createElement('div');

        this.playerTopScoreText = document.createElement('span');
        this.playerCurrentScoreText = document.createElement('span');

        this.playerTopScoreText.innerText = "HighScore: ";
        this.playerCurrentScoreText.innerText = "Current: ";

        this.playerTopScoreCounter = document.createElement('span');
        this.playerCurrentScoreCounter = document.createElement('span');

        this.playerTopScoreCounter.classList.add('top-score');
        this.playerCurrentScoreCounter.classList.add('current-score');

        this.playerTopScoreCounter.innerText = "";
        this.playerCurrentScoreCounter.innerText = "";

        this.playerTopScore.append(this.playerTopScoreText);
        this.playerCurrentScore.append(this.playerCurrentScoreText);

        this.playerTopScore.append(this.playerTopScoreCounter);
        this.playerCurrentScore.append(this.playerCurrentScoreCounter);

        this.playerInformationElem.append(this.playerTopScore);
        this.playerInformationElem.append(this.playerCurrentScore);
    }

    showName(name){
        this.playerInformationElem.innerHTML = '';
        this.playerName = document.createElement('div');
        this.playerName.innerText = name;
        this.playerInformationElem.append(this.playerName);
    }

    
    getContent(){return this.content;}

    updateGameScore(topScore, currentScore){
        this.playerTopScoreCounter.innerText = topScore;
        this.playerCurrentScoreCounter.innerText = currentScore;
    }
}

