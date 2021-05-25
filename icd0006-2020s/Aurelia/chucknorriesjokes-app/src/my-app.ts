import { bindable, EventAggregator, IDisposable} from "aurelia";
import { JokesState } from "./components/state/jokes-state";
import { IJoke } from "./domain/IJoke";

export class MyApp {

  constructor(private jokesState: JokesState){
  }


}
