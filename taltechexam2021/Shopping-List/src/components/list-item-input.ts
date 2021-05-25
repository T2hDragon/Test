import { bindable, EventAggregator, IDisposable } from "aurelia";

export class ListItemInput {

    @bindable public placeholder: string = "Default";
    public description: string = '';

    private subscriptions: IDisposable[] = [];

    constructor(private eventAggregator: EventAggregator) {

    }


    detached() {
        this.subscriptions.forEach(subscription => subscription.dispose());
        this.subscriptions = [];
    }


    addNewListItem() {

        this.eventAggregator.publish('new-listItem', this.description);
        setTimeout(() => {
            this.description = '';
        }, 100);
    }

}