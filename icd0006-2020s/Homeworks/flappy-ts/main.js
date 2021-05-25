(()=>{"use strict";var t,e=function(){function t(t,e,o,n,i){void 0===n&&(n=5),void 0===i&&(i=.2),this.col=t,this.row=e,this.maxRow=o,this.jumpPower=n,this.changePerTick=i,this.currentPower=this.jumpPower}return t.prototype.jump=function(){this.currentPower=this.jumpPower},t.prototype.nextGameTick=function(){this.row-=this.currentPower*this.changePerTick,this.currentPower=this.currentPower-.5},t.prototype.getRow=function(){return Math.round(this.row)},t.prototype.getColumn=function(){return Math.round(this.col)},t.prototype.checkOutOfBounds=function(){return this.maxRow<this.row||0>this.row},t.prototype.newPosition=function(t,e){this.col=t,this.row=e},t}();!function(t){t.BACKGROUND_ID="0",t.BIRD_ID="1",t.OBSTACLE_ID="2",t.POINT_ID="3"}(t||(t={}));const o=t,n=function(){function t(t,e,o){void 0===e&&(e=0),void 0===o&&(o=0),this.name=t,this.bestScore=e,this.currentScore=o}return t.prototype.increaseScore=function(){this.currentScore++,this.currentScore>this.bestScore&&(this.bestScore=this.currentScore)},t.prototype.resetCurrentScore=function(){this.currentScore=0},t.prototype.getBestScore=function(){return this.bestScore},t.prototype.getCurrentScore=function(){return this.currentScore},t.prototype.getName=function(){return this.name},t.prototype.newGame=function(){this.currentScore=0},t}(),i=function(){function t(t,e,o){void 0===t&&(t=27),void 0===e&&(e=48),void 0===o&&(o="guest"),this.rowCount=t,this.columnCount=e,this.gameOver=!1,this.board=[],this.scoreBoard=[],this.obstacleWidth=4,this.untilObstacle=0,this.obstacleSpaceRation=1/3,this.obstacleSpaceVariation=5,this.obstacleSpawnChanceModifier=.01,this.obstaclePadding=5,this.loadPlayerScore(o),this.newGame()}return t.prototype.getBestScore=function(){return this.gameScore.getBestScore()},t.prototype.getCurrentScore=function(){return this.gameScore.getCurrentScore()},t.prototype.getScoreBoard=function(){return this.scoreBoard},t.prototype.getPlayerName=function(){return this.gameScore.getName()},t.prototype.isGameOver=function(){return this.gameOver},t.prototype.setGameOver=function(t){this.gameOver=t},t.prototype.loadPlayerScore=function(t){var e=this,o=!1;this.scoreBoard.forEach((function(n){if(n.getName()===t)return n.resetCurrentScore(),e.gameScore=n,void(o=!0)})),o||(this.gameScore=new n(t),this.scoreBoard.push(this.gameScore))},t.prototype.getCollisionObject=function(){return this.board[this.bird.getColumn()][this.bird.getRow()]},t.prototype.nextGameTick=function(){if(this.untilObstacle<0&&Math.abs(this.untilObstacle)*this.obstacleSpawnChanceModifier>=Math.random()){for(var t=this.rowCount*this.obstacleSpaceRation+Math.round((2*Math.random()-1)*this.obstacleSpaceVariation),e=this.rowCount-this.obstaclePadding-t,n=this.obstaclePadding,i=Math.floor(Math.random()*(e-n+1))+n,r=0;r<this.obstacleWidth;r++)this.addColumn(!0,i,t,r);this.untilObstacle=18}switch(this.board.length<=this.columnCount&&(this.addColumn(),this.untilObstacle--),this.board[this.bird.getColumn()][this.bird.getRow()]=o.BACKGROUND_ID,this.bird.nextGameTick(),this.board.shift(),this.getCollisionObject()){case o.OBSTACLE_ID:case void 0:this.gameOver=!0;break;case o.POINT_ID:this.gameScore.increaseScore()}this.board[this.bird.getColumn()][this.bird.getRow()]=o.BIRD_ID},t.prototype.newGame=function(){this.board=[],this.gameScore.newGame(),this.gameOver=!1,this.bird=new e(2,Math.floor(this.rowCount/2),this.rowCount);for(var t=0;t<this.columnCount;t++){for(var n=[],i=0;i<this.rowCount;i++)n.push(o.BACKGROUND_ID);this.board.push(n)}this.board[this.bird.getColumn()][this.bird.getRow()]=o.BIRD_ID},t.prototype.getGameBoard=function(){return this.board.slice(0,this.columnCount)},t.prototype.addColumn=function(t,e,n,i){void 0===t&&(t=!1),void 0===e&&(e=0),void 0===n&&(n=0),void 0===i&&(i=0);for(var r=[],s=0;s<this.rowCount;s++)t?s<e||s>e+n?r.push(o.OBSTACLE_ID):i?r.push(o.BACKGROUND_ID):r.push(o.POINT_ID):r.push(o.BACKGROUND_ID);this.board.push(r)},t}(),r=function(){function t(t){this.createContent(),this.createStatisticsButton(),this.createGameButton(),this.createInformationElem(),this.showScore(),this.content.append(this.statisticsButton),this.content.append(this.playerInformationElem),this.content.append(this.gameButton),this.statisticsButton.addEventListener("click",t),this.gameButton.addEventListener("click",t)}return t.prototype.createContent=function(){this.content=document.createElement("div"),this.content.style.height="50px",this.content.id="control"},t.prototype.createStatisticsButton=function(){this.statisticsButton=document.createElement("button"),this.statisticsButton.classList.add("statistics-button"),this.statisticsButton.innerText="Statistics",this.statisticsButton.style.display="inline-block",this.statisticsButton.style.position="absolute",this.statisticsButton.style.left="0px",this.statisticsButton.style.width="40%",this.statisticsButton.style.height="50px"},t.prototype.createGameButton=function(){this.gameButton=document.createElement("button"),this.gameButton.classList.add("game-button"),this.gameButton.innerText="Game",this.gameButton.style.display="inline-block",this.gameButton.style.position="absolute",this.gameButton.style.right="0px",this.gameButton.style.width="40%",this.gameButton.style.height="50px"},t.prototype.createInformationElem=function(){this.playerInformationElem=document.createElement("div"),this.playerInformationElem.style.display="inline-block",this.playerInformationElem.style.height="50px",this.playerInformationElem.style.width="20%",this.playerInformationElem.style.marginLeft="40%",this.playerInformationElem.style.textAlign="center",this.playerInformationElem.style.fontFamily="Lucida Console",this.playerInformationElem.classList.add("information")},t.prototype.showScore=function(){this.playerInformationElem.innerHTML="",this.playerTopScore=document.createElement("div"),this.playerCurrentScore=document.createElement("div"),this.playerTopScoreText=document.createElement("span"),this.playerCurrentScoreText=document.createElement("span"),this.playerTopScoreText.innerText="HighScore: ",this.playerCurrentScoreText.innerText="Current: ",this.playerTopScoreCounter=document.createElement("span"),this.playerCurrentScoreCounter=document.createElement("span"),this.playerTopScoreCounter.classList.add("top-score"),this.playerCurrentScoreCounter.classList.add("current-score"),this.playerTopScoreCounter.innerText="",this.playerCurrentScoreCounter.innerText="",this.playerTopScore.append(this.playerTopScoreText),this.playerCurrentScore.append(this.playerCurrentScoreText),this.playerTopScore.append(this.playerTopScoreCounter),this.playerCurrentScore.append(this.playerCurrentScoreCounter),this.playerInformationElem.append(this.playerTopScore),this.playerInformationElem.append(this.playerCurrentScore)},t.prototype.showName=function(t){this.playerInformationElem.innerHTML="";var e=document.createElement("div");e.innerText=t,this.playerInformationElem.append(e)},t.prototype.getContent=function(){return this.content},t.prototype.updateGameScore=function(t,e){this.playerTopScoreCounter.innerText=t.toString(),this.playerCurrentScoreCounter.innerText=e.toString()},t}();var s;!function(t){t.BACKGROUND_COLOR="#0b0a24",t.OBSTACLE_COLOR="#a52a2a",t.BIRD_COLOR="#9c8407",t.BACKGROUND_ID="0",t.BIRD_ID="1",t.OBSTACLE_ID="2",t.POINT_ID="3"}(s||(s={}));const a=s;var c=function(){function t(t){var e=this;this.cells=t,this.columnCount=this.cells.length,this.rowCount=0!==this.columnCount?t[0].length:0,this.initializeBoardHtml(),this.jumped=!1,this.stop=!1,document.addEventListener("keyup",(function(t){switch(t.code){case"Space":e.jumped=!0;break;case"KeyS":e.stop=!0}}))}return t.prototype.getRowHeight=function(){return(window.innerHeight-50)/this.rowCount},t.prototype.getColumnHeight=function(){return window.innerWidth/this.columnCount},t.prototype.updateBoardHtml=function(t){this.cells=t;for(var e=0;e<this.cells.length;e++)for(var o=this.cells[e],n=0;n<o.length;n++){var i=this.content.querySelector(".id-col-"+e+" .id-row-"+n);if(null!==i){var r=o[n];if(r===a.OBSTACLE_ID)i.style.backgroundColor=a.OBSTACLE_COLOR,i.dataset.type="obstacle";else if(r===a.BIRD_ID)i.style.backgroundColor=a.BIRD_COLOR,i.dataset.type="player";else if(r===a.BACKGROUND_ID)i.style.backgroundColor=a.BACKGROUND_COLOR,i.dataset.type="background";else{if(r!==a.POINT_ID)throw new Error('Unknown cell "'+r+'"');i.style.backgroundColor=a.BACKGROUND_COLOR,i.dataset.type="point"}}}},t.prototype.initializeBoardHtml=function(){this.content=document.createElement("div"),this.content.classList.add("game-container"),this.loadBoardHtml()},t.prototype.loadBoardHtml=function(){this.content.innerText="";for(var t=0;t<this.columnCount;t++)this.content.append(this.createColumn(t))},t.prototype.getBoardHtml=function(){return this.content},t.prototype.createColumn=function(t){var e=document.createElement("div");e.classList.add("col"),e.classList.add("id-col-"+t),e.style.minWidth=this.getColumnHeight()+"px",e.style.maxWidth=this.getColumnHeight()+"px";for(var o=0;o<this.rowCount;o++){var n=document.createElement("div");n.classList.add("id-row-"+o);var i=this.cells[t][o];if(i===a.OBSTACLE_ID)n.style.backgroundColor=a.OBSTACLE_COLOR,n.dataset.type="obstacle";else if(i===a.BIRD_ID)n.style.backgroundColor=a.BIRD_COLOR,n.dataset.type="player";else if(i=a.BACKGROUND_ID)n.style.backgroundColor=a.BACKGROUND_COLOR,n.dataset.type="background";else{if(!(i=a.POINT_ID))throw new Error('Unknown cell "'+i+'"');n.style.backgroundColor=a.BACKGROUND_COLOR,n.dataset.type="point"}n.style.minHeight=this.getRowHeight()+"px",n.style.minWidth=this.getColumnHeight()+"px",e.append(n)}return e.style.display="inline-block",e},t}();const l=function(){function t(t,e,o){var n=this;this.model=t,this.viewContainer=e,this.controlView=o,this.isRunning=!1,this.gameView=new c(this.model.getGameBoard()),this.model=t,window.addEventListener("resize",(function(){n.resizeUi()}))}return t.prototype.run=function(){this.isRunning=!0,this.viewContainer.innerHTML="",this.viewContainer.append(this.gameView.getBoardHtml()),this.controlView.showScore(),this.runGame()},t.prototype.stop=function(){this.isRunning=!1},t.prototype.runGame=function(){var t=this;setTimeout((function(){t.isRunning&&(t.model.isGameOver()||(t.model.nextGameTick(),t.gameView.updateBoardHtml(t.model.getGameBoard())),t.gameView.jumped&&(t.model.isGameOver()?(t.gameView.jumped=!1,t.model.setGameOver(!1),t.model.newGame()):(t.model.bird.jump(),t.gameView.jumped=!1)),t.gameView.stop&&t.model.setGameOver(!t.model.isGameOver()),t.controlView.updateGameScore(t.model.getBestScore(),t.model.getCurrentScore()),t.runGame())}),40)},t.prototype.resizeUi=function(){this.gameView.loadBoardHtml()},t}(),h=function(){function t(t){this.createContent(),this.createPlayerNameChange(),this.eventOnPlayerNameChange(t),this.createLeaderBoard()}return t.prototype.createContent=function(){this.content=document.createElement("div"),this.content.classList.add("statistics-container"),this.content.style.textAlign="center",this.content.style.paddingTop="10%"},t.prototype.createLeaderBoard=function(){this.leaderBoard=document.createElement("div"),this.leaderBoard.classList.add("sleaderbooard"),this.leaderBoard.style.textAlign="center",this.leaderBoard.style.paddingTop="5%",this.content.append(this.leaderBoard)},t.prototype.createPlayerNameChange=function(){this.nameField=document.createElement("input"),this.confirmButton=document.createElement("button"),this.confirmButton.innerText="Change",this.confirmButton.id="player-name-confirmation",this.content.append(this.nameField),this.content.append(this.confirmButton)},t.prototype.eventOnPlayerNameChange=function(t){var e=this;this.confirmButton.addEventListener("click",(function(){t(e.nameField.value)}))},t.prototype.changePlayerName=function(t){this.nameField.value=t},t.prototype.addPlayerToScoreBoard=function(t,e){void 0===t&&(t="guest"),void 0===e&&(e=0);var o=document.createElement("div");o.classList.add("player-entry");var n=document.createElement("span"),i=document.createElement("span"),r=document.createElement("span");n.classList.add("player-name"),i.classList.add("player-name-score-divider"),r.classList.add("player-score"),n.innerText=t,i.innerText=" - ",r.innerText=e.toString(),o.append(n),o.append(i),o.append(r),this.leaderBoard.append(o)},t.prototype.cleanLeaderboard=function(){this.leaderBoard.innerHTML=""},t.prototype.getContent=function(){return this.content},t}(),d=function(){function t(t,e,o){var n=this;this.model=t,this.viewContainer=e,this.controlView=o,this.model=t,this.isRunning=!1,this.statisticsView=new h((function(t){n.model.loadPlayerScore(t),n.unloadLeaderBoard(),n.loadLeaderBoard(),n.model.newGame()}))}return t.prototype.loadLeaderBoard=function(){var t=this;this.isRunning=!0,this.viewContainer.innerHTML="",this.controlView.showName(this.model.getPlayerName()),this.statisticsView.changePlayerName(this.model.getPlayerName()),this.model.getScoreBoard().forEach((function(e){t.statisticsView.addPlayerToScoreBoard(e.getName(),e.getBestScore())})),this.viewContainer.append(this.statisticsView.getContent())},t.prototype.unloadLeaderBoard=function(){this.isRunning=!1,this.statisticsView.cleanLeaderboard()},t}();document.body.style.margin="0",document.body.style.padding="0",document.body.style.overflow="hidden";var u,p=new i,m=((u=document.createElement("div")).classList.add("main"),u),y=function(){var t=document.createElement("div");return t.classList.add("view-container"),t.innerText="",t}(),g=new r((function(t){if(t.target.classList.contains("game-button"))f.isRunning||(C.unloadLeaderBoard(),f.run());else{if(!t.target.classList.contains("statistics-button"))throw new Error('Pressed unknown control button with class "'+t.target.classList+'"');C.isRunning||(f.stop(),C.loadLeaderBoard())}})),f=new l(p,y,g),C=new d(p,y,g);document.body.appendChild(m),m.append(g.getContent()),m.append(y),f.run()})();