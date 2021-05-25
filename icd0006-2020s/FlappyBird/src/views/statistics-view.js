export default class StatisticsView {
    constructor(event){
        this.createContent();
        this.createPlayerNameChange();
        this.eventOnPlayerNameChange(event);
        this.createLeaderBoard();
    }
    createContent(){
        this.content = document.createElement('div');
        this.content.classList.add("statistics-container");
        this.content.style.textAlign = "center";
        this.content.style.paddingTop = "10%";
    }

    createLeaderBoard(){
        this.leaderBoard = document.createElement('div');
        this.leaderBoard.classList.add("sleaderbooard");
        this.leaderBoard.style.textAlign = "center";
        this.leaderBoard.style.paddingTop = "5%";
        this.content.append(this.leaderBoard);
    }


    createPlayerNameChange(){
        this.nameField = document.createElement('input');
        this.confirmButton = document.createElement('button');
        this.confirmButton.innerText = "Change";
        this.confirmButton.id = "player-name-confirmation";
        this.nameField.htmlFor = "player-name-confirmation";
        this.content.append(this.nameField);
        this.content.append(this.confirmButton);
    }

    eventOnPlayerNameChange(event){
        this.confirmButton.addEventListener('click', () => {event(this.nameField.value)});
    }


    changePlayerName(currentName){
        this.nameField.value = currentName;
    }

    addPlayerToScoreBoard(name = "guest", score = 0){
        let player = document.createElement('div');
        player.classList.add('player-entry');
        let nameContent = document.createElement('span');
        let nameScoreDivider = document.createElement('span');
        let scoreContent = document.createElement('span');

        nameContent.classList.add('player-name');
        nameScoreDivider.classList.add('player-name-score-divider');
        scoreContent.classList.add('player-score');

        nameContent.innerText = name;
        nameScoreDivider.innerText = " - ";
        scoreContent.innerText = score;
        
        player.append(nameContent);
        player.append(nameScoreDivider);
        player.append(scoreContent);
        this.leaderBoard.append(player);
    }



    cleanLeaderboard(){
        this.leaderBoard.innerHTML = '';
    }

    getContent(){
        return this.content;
    }
}