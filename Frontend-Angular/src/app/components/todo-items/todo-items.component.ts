import { Component, OnInit, inject } from '@angular/core';
import { ApiService } from '../../../api/api.service';
import { ApiEndpointKey, ApiEndpoints} from '../../../api/api.model';
import { endpoints } from '../../../api/api-endpoints-map';
import { TodoItem } from '../../models/TodoItem';

@Component({
  selector: 'todo-item',
  templateUrl: './todo-items.component.html',
  styleUrls: ['./todo-items.component.css'],
})

export class ToDoComponent implements OnInit {
  items: TodoItem[] = [];

  isCompleted: boolean;
  errorMessage: string = "";
  private readonly endpoints: ApiEndpoints;
  private apiService = inject(ApiService);

  public constructor() {
    this.endpoints = endpoints;
  }

  ngOnInit() {
     this.getItems();
  }

  getItems() {
    var response = this.apiService.get<any>(ApiEndpointKey.TODOITEMS).subscribe(
      {
        next: (response) => {
          this.items = response;
        },
        error: (error) => {
          this.errorMessage = "Error on loading items from backend server. Please make sure your host server is up running.";
        }
      }
    );
  }

  handleAdd(todoItem: TodoItem) {
    if (!todoItem.description){
      this.errorMessage = "Description can not be empty";
      return;
    }

    this.errorMessage = "";
    var response = this.apiService.post<any, TodoItem>(ApiEndpointKey.TODOITEMS, todoItem).subscribe(
        {
          next: (response) => {
            this.getItems();
          },
          error: (error) => {
            // Handle any errors
          if (error?.status == 400 && error?.error){
            this.errorMessage = `Failed add new item due to: ${error?.error}`;
          }
          else{
            //internal server eror
            this.errorMessage = "Error on update item from backend server.";
          }
        }
      }
    );
  }

  handleMarkAsComplete(item: TodoItem) {
    if(item.isCompleted){
      return;
    }
    item.isCompleted = true;
    var response = this.apiService.put<any, TodoItem>(ApiEndpointKey.TODOITEMS, item).subscribe(
      {
        next: (response) => {
          this.getItems();
        },
        error: (error) => {
          this.errorMessage = "Error on update item from backend server.";
        }
      }
    );
  }
}
