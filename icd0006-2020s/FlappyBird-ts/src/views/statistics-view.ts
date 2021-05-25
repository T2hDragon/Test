export default class StatisticsView {
        private content!: HTMLDivElement;
        private leaderBoard!: HTMLDivElement;
        private nameField!: HTMLInputElement;
        private confirmButton!: HTMLButtonElement;

    constructor(event: (name:string)=> void){
        this.createContent();
        this.createPlayerNameChange();
        this.eventOnPlayerNameChange(event);
        this.createLeaderBoard();
    }
    createContent(): void{
        this.content = document.createElement('div');
        this.content.classList.add("statistics-container");
        this.content.style.textAlign = "center";
        this.content.style.paddingTop = "10%";
    }

    createLeaderBoard(): void{
        this.leaderBoard = document.createElement('div');
        this.leaderBoard.classList.add("sleaderbooard");
        this.leaderBoard.style.textAlign = "center";
        this.leaderBoard.style.paddingTop = "5%";
        this.content.append(this.leaderBoard);
    }


    createPlayerNameChange():void{
        this.nameField = document.createElement('input');
        this.confirmButton = document.createElement('button');
        this.confirmButton.innerText = "Change";
        this.confirmButton.id = "player-name-confirmation";
        this.content.append(this.nameField);
        this.content.append(this.confirmButton);
    }

    eventOnPlayerNameChange(event: (playerName:string) => void):void{
        this.confirmButton.addEventListener('click', () => {event(this.nameField.value)});
    }


    changePlayerName(currentName: string): void{
        this.nameField.value = currentName;
    }

    addPlayerToScoreBoard(name = "guest", score = 0): void{
        let playerDiv: HTMLDivElement = document.createElement('div');
        playerDiv.classList.add('player-entry');
        let nameSpan: HTMLSpanElement = document.createElement('span');
        let nameScoreDivider: HTMLSpanElement = document.createElement('span');
        let scoreSpan: HTMLSpanElement = document.createElement('span');

        nameSpan.classList.add('player-name');
        nameScoreDivider.classList.add('player-name-score-divider');
        scoreSpan.classList.add('player-score');

        nameSpan.innerText = name;
        nameScoreDivider.innerText = " - ";
        scoreSpan.innerText = score.toString();
        
        playerDiv.append(nameSpan);
        playerDiv.append(nameScoreDivider);
        playerDiv.append(scoreSpan);
        this.leaderBoard.append(playerDiv);
    }



    cleanLeaderboard(): void{
        this.leaderBoard.innerHTML = '';
    }

    getContent(): HTMLDivElement{
        return this.content;
    }
}