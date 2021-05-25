import { IListItem } from "../domain/IListItem";

export class AppState {
    public listItems: readonly IListItem[] = [];

    constructor() {
        this.listItems = []
    }

    setListItems(newListItems: IListItem[]): void {
        this.listItems = [...newListItems,];
    }

    addListItem(listItem: IListItem): void {
        this.listItems = [...this.listItems, listItem];
    }

    removeListItem(elemNo: number): void {
        this.listItems = this.listItems.filter((elem, index) => index !== elemNo);
    }

    countListItems(): number {
        return this.listItems.length;
    }
}