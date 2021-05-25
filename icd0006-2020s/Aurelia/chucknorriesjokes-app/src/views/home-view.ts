import { JokesState } from "../components/state/jokes-state";
import { IJoke } from "../domain/IJoke";
import { JokesService } from "../services/joke-services";

export class homeView{
    public jokes: readonly IJoke[] = [];

    constructor(private jokesState: JokesState) {
        this.jokes = this.jokesState.getJokes();
    }
    
}