import { ITodo } from "../domain/ITodo";

export class AppState {
    public todos: readonly ITodo[] = [];

    constructor() {
        this.todos = [

            {
                description: 'World domination',
                done: false,
            },
            {
                description: 'Define more homeworks',
                done: false,
            },
        ];
    }

    addTodo(todo: ITodo): void {
        this.todos = [...this.todos, todo];
    }

    removeTodo(elemNo: number): void {
        this.todos = this.todos.filter((elem, index) => index !== elemNo);
    }

    countToDos(): number {
        return this.todos.length;
    }
}