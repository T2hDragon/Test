import { IListItem } from "../domain/IListItem";
import { EventAggregator, HttpClient, IDisposable } from "aurelia";
import { AppState } from "../state/app-state";
import { ListItemService } from "../services/list-item-service";

export class ListItemView
 {
  private subscriptions: IDisposable[] = [];
  private service: ListItemService = new ListItemService(this.httpClient);
  public filter = null;


  constructor(protected httpClient: HttpClient,
    private eventAggregator: EventAggregator,
    private appState: AppState
    ) {

    this.subscriptions.push(
      this.eventAggregator.subscribe('new-listItem', (descr: string) => this.addNewListItem(descr))
    );

  }


  async attached() {
    this.updateListItems();
  }

  updateListItems = (filt?: boolean): void => {
      this.service.getAll(filt).then((res) =>{
        if (res.data) {
          this.appState.setListItems(res.data as IListItem[]);
        } else {console.log(res)}
      });
  }

  addNewListItem = (descr: string): void => {
    let listItem = {description: descr, completed: false}
    this.service.create(listItem).then((res) =>{
      if (res.data) {
        this.appState.addListItem(res.data! as IListItem)
      } else {console.log(res)}
    });
  }

  removeListItem = (index: number): void => {
    this.service.delete(this.appState.listItems[index].id).then((res) =>{
      if (res.data) {
        this.appState.removeListItem(index)
      } else {console.log(res)}
    });
  }

  checkListItem = (index: number): void => {
    let listItem = this.appState.listItems[index];
    listItem = {
      id:listItem.id, 
      description:listItem.description, 
      completed: !listItem.completed
    };
    this.service.update(listItem.id, listItem)
    .then((res) =>{
      if (res.statusCode === 204) {
        this.updateListItems(this.filter)
      } else {console.log(res)}
    });
  }

  detached() {
    this.subscriptions.forEach(subscription => subscription.dispose());
    this.subscriptions = [];
  }

}
