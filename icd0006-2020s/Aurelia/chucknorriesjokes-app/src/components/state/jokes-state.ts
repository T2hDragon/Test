import { IJoke } from "../../domain/IJoke";


export class JokesState{
    private jokes: readonly IJoke[] = [];

    constructor(){

    }

    getJokes(): readonly IJoke[] {
        return [...this.jokes];
    }

    addJoke(joke: IJoke): void{
        this.jokes = [joke, ...this.jokes];
    }

    addJokes(jokes: IJoke[]): void{
        this.jokes = [...jokes, ...this.jokes];
    }

    removeJoke(index: number): void{
        this.jokes = this.jokes.filter((elem, elemIndex) => elemIndex !== elemIndex);
    }

    jokeExists(id: String) {
        return this.jokes.some(function(elem) {
            return elem.id == id;
        });
    }

    countTodos = () => this.jokes.length;
}