import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TodoItem } from 'src/app/models/TodoItem';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css']
})
export class TodoItemComponent implements OnInit {
    @Input() item: TodoItem;
    @Output() onClickReminder: EventEmitter<TodoItem> = new EventEmitter();

    constructor() {}

    ngOnInit(): void {}
    
    handleMarkAsComplete(task) {
      this.onClickReminder.emit(task);
    }
}