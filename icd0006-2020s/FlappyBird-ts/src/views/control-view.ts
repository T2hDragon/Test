export default class ControlView {
    private statisticsButton!: HTMLButtonElement;
    private gameButton!: HTMLButtonElement;
    private playerInformationElem!: HTMLDivElement;
    private content!: HTMLDivElement;
    private playerTopScore!: HTMLDivElement;
    private playerCurrentScore!: HTMLDivElement;
    private playerTopScoreText!: HTMLSpanElement;
    private playerCurrentScoreText!: HTMLSpanElement;
    private playerTopScoreCounter!: HTMLSpanElement;
    private playerCurrentScoreCounter!: HTMLSpanElement;


    constructor(eventHandler: any){
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

    createContent(): void{
        this.content = document.createElement('div');
        this.content.style.height = "50px";
        this.content.id = 'control';
    }

    createStatisticsButton(): void{
        this.statisticsButton = document.createElement('button');
        this.statisticsButton.classList.add('statistics-button');
        this.statisticsButton.innerText='Statistics';
        this.statisticsButton.style.display = "inline-block";
        this.statisticsButton.style.position = "absolute";
        this.statisticsButton.style.left = "0px";
        this.statisticsButton.style.width = "40%";
        this.statisticsButton.style.height = "50px";
    }

    createGameButton(): void{
        this.gameButton = document.createElement('button');
        this.gameButton.classList.add('game-button');
        this.gameButton.innerText='Game';
        this.gameButton.style.display = "inline-block";
        this.gameButton.style.position = "absolute";
        this.gameButton.style.right = "0px";
        this.gameButton.style.width = "40%";
        this.gameButton.style.height = "50px";
    }

    createInformationElem(): void{
        this.playerInformationElem = document.createElement('div');
        this.playerInformationElem.style.display = "inline-block";
        this.playerInformationElem.style.height = "50px";
        this.playerInformationElem.style.width = "20%";
        this.playerInformationElem.style.marginLeft = "40%";
        this.playerInformationElem.style.textAlign = "center";
        this.playerInformationElem.style.fontFamily = "Lucida Console", "Monaco";
        this.playerInformationElem.classList.add('information');        
    }

    showScore(): void{
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

    showName(name: string): void{
        this.playerInformationElem.innerHTML = '';
        let playerNameDiv = document.createElement('div');
        playerNameDiv.innerText = name;
        this.playerInformationElem.append(playerNameDiv);
    }

    
    getContent(): HTMLDivElement{return this.content;}

    updateGameScore(topScore: number, currentScore: number): void{
        this.playerTopScoreCounter.innerText = topScore.toString();
        this.playerCurrentScoreCounter.innerText = currentScore.toString();
    }
}

