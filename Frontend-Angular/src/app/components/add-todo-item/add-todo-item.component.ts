import { Component, EventEmitter, Output } from '@angular/core';
import { TodoItem } from 'src/app/models/TodoItem';

@Component({
  selector: 'app-add-todo-item',
  templateUrl: './add-todo-item.component.html',
  styleUrls: ['./add-todo-item.component.css']
})
export class AddTodoItemComponent {
  @Output() onAddClick: EventEmitter<TodoItem> = new EventEmitter();
  @Output() onClearClick: EventEmitter<string> = new EventEmitter();
  description: string;

  handleAdd(description: string) {
    const newTodoItem: TodoItem = { description : this.description, id:null, isCompleted:false };
    this.onAddClick.emit(newTodoItem);
  }

  handleClear() {
    this.description = "";
    this.onClearClick.emit(this.description);
  }
}
