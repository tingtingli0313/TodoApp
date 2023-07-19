import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { ToDoComponent } from './components/todo-items/todo-items.component';
import { TodoItemComponent } from './components/todo-item-list/todo-item-list.component';
import { AddTodoItemComponent } from './components/add-todo-item/add-todo-item.component';

@NgModule({
  declarations: [AppComponent, ToDoComponent, TodoItemComponent, AddTodoItemComponent],
  imports: [BrowserModule, HttpClientModule, FormsModule],
  providers: [],
  bootstrap: [AppComponent],
})

export class AppModule { }