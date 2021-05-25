import { IListItem } from '../domain/IListItem';
import { bindable } from "@aurelia/runtime-html";

export class ListItemElement {

    @bindable public item: IListItem;
    
    @bindable public removeCallback : (index: number) => void = null;

    @bindable public checkCallback : (index: number) => void = null;

    @bindable listItemNo: number;

    removeListItem(index: number){
        this.removeCallback(index);
    }

    checkListItem(index: number){
        this.checkCallback(index);
    }
}