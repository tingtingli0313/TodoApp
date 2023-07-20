import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TodoItem } from 'src/app/models/TodoItem';

@Component({
  selector: 'app-todo-item-list',
  templateUrl: './todo-item-list.component.html',
  styleUrls: ['./todo-item-list.component.css']
})
export class TodoItemComponent implements OnInit {
    @Input() items: TodoItem[];
    @Output() onClickReminder: EventEmitter<TodoItem> = new EventEmitter();

    constructor() {}

    ngOnInit(): void {}
    
    handleMarkAsComplete(task: TodoItem) {
      task.isCompleted = true;
      this.onClickReminder.emit(task);
    }
}