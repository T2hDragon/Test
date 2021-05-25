export {GameView};
import * as C from './constants.js';
class GameView{

    constructor(cells){
        this.cells = cells;
        this.columnCount = this.cells.length;
        this.rowCount = ((this.columnCount !== 0) ? cells[0].length : 0);
        this.initializeBoardHtml();
        this.jumped = false;
        this.stop = false;
        document.addEventListener('keyup', (e) => {
                                            switch (e.code){
                                                case 'Space':
                                                    this.jumped = true;
                                                    break;
                                                case 'KeyS':
                                                    this.stop = true;
                                                    break;
                                            }
                                        });}

    getRowHeight(){ return (window.innerHeight - 50) / this.rowCount;}
    getColumnHeight(){ return (window.innerWidth) / this.columnCount;}


    updateBoardHtml(cells){
        this.cells = cells;
        for (let columnIndex = 0; columnIndex < this.cells.length; columnIndex++) {
            let cellColumn = this.cells[columnIndex];
            for (let rowIndex = 0; rowIndex < cellColumn.length; rowIndex++) {
                let cellElem = this.content.querySelector(".id-col-" + columnIndex + " .id-row-" + rowIndex);
                if (cellElem === null) {continue;}
                let cell = cellColumn[rowIndex];
                if (cell === C.OBSTACLE_ID){
                    cellElem.style.backgroundColor = C.OBSTACLE_COLOR;
                    cellElem.dataset.type = "obstacle";
                } else if (cell === C.BIRD_ID){
                    cellElem.style.backgroundColor = C.BIRD_COLOR;
                    cellElem.dataset.type = "player";
                } else if (cell === C.BACKGROUND_ID){
                    cellElem.style.backgroundColor = C.BACKGROUND_COLOR;
                    cellElem.dataset.type = "background";
                } else if (cell === C.POINT_ID){
                    cellElem.style.backgroundColor = C.BACKGROUND_COLOR;
                    cellElem.dataset.type = "point";
                } else {
                    throw new Error('Unknown cell \"' + cell + '\"');
                }
                
            }
        }
    }



    initializeBoardHtml(){
        this.content = document.createElement('div');
        this.content.classList.add("game-container");
        this.loadBoardHtml();
    }

    loadBoardHtml(){
        this.content.innerText = '';
        for (let columnIndex = 0; columnIndex < this.columnCount; columnIndex++) {
            this.content.append(this.createColumn(columnIndex));
        }
    }


    getBoardHtml() {return this.content};

    
    createColumn(columnIndex){
        let colElem = document.createElement('div')
        colElem.classList.add('col');
        colElem.classList.add('id-col-' + columnIndex);

        colElem.style.minWidth = this.getColumnHeight() + "px";
        colElem.style.maxWidth = this.getColumnHeight() + "px";

        for (let rowIndex = 0; rowIndex < this.rowCount; rowIndex++) {
            let rowElem = document.createElement('div')
            rowElem.classList.add('id-row-' + rowIndex);
            let cell = this.cells[columnIndex][rowIndex];
            if (cell === C.OBSTACLE_ID){
                rowElem.style.backgroundColor = C.OBSTACLE_COLOR;
                rowElem.dataset.type = "obstacle";
            } else if (cell === C.BIRD_ID){
                rowElem.style.backgroundColor = C.BIRD_COLOR;
                rowElem.dataset.type = "player";
            } else if (cell = C.BACKGROUND_ID){
                rowElem.style.backgroundColor = C.BACKGROUND_COLOR;
                rowElem.dataset.type = "background";
            } else if (cell = C.POINT_ID){
                rowElem.style.backgroundColor = C.BACKGROUND_COLOR;
                rowElem.dataset.type = "point";
            } else {
                throw new Error('Unknown cell \"' + cell + '\"');
            }
            rowElem.style.minHeight = this.getRowHeight() + "px";
            rowElem.style.minWidth = this.getColumnHeight() + 'px';
            colElem.append(rowElem)
        } 
        colElem.style.display = 'inline-block';
        return colElem;
    }
    
}




