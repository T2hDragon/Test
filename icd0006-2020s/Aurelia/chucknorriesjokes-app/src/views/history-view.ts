import { JokesState } from "../components/state/jokes-state";
import { IJoke } from "../domain/IJoke";
import { JokesService } from "../services/joke-services";

export class historyView{
    private jokes: IJoke[] = [];
    public previousJokes: readonly IJoke[] = [];

    constructor(private jokesService: JokesService,
        private jokesState: JokesState) {
        this.previousJokes = this.jokesState.getJokes();
    }

    async attached(){
        var count = 0
        while (this.jokes.length < 5 && count < 15){
            var joke = await this.jokesService.getJoke("history");
            if (!this.jokesState.jokeExists(joke.id)) {
                this.jokes.push(joke);
                this.jokesState.addJoke(joke);
            }
            count++;
        }
    }
}