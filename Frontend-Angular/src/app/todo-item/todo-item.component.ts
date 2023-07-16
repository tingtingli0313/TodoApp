import { Component, inject } from '@angular/core';
import { ApiService } from '../../api/api.service';
import { ApiEndpointKey, ApiEndpoints} from '../../api/api.model';
import { endpoints } from '../../api/api-endpoints-map';

export interface TodoItem {
  description: string;
  id: string;
  isCompleted: boolean;
}

@Component({
  selector: 'todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.css'],
})

export class ToDoComponent {
  items: TodoItem[] = [];
  description: string;
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

  onInput(description : string){
    this.description = description;
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

  handleAdd() {
    if (!this.description){
      this.errorMessage = "Description can not be empty";
      return;
    }

    this.errorMessage = "";
    const newTodoItem = { description : this.description };
    var response = this.apiService.post<any, TodoItem>(ApiEndpointKey.TODOITEMS, newTodoItem).subscribe(
        {
          next: (response) => {
            this.getItems();
            this.handleClear();
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

  handleClear() {
    this.description = '';
  }

  handleMarkAsComplete(item: TodoItem) {
    if(item.isCompleted){
      return;
    }
    item.isCompleted = true;
   // const url = `${this.endpoints[ApiEndpointKey.TODOITEMS].path}/${item.id}`;
    var response = this.apiService.put<any, TodoItem>(ApiEndpointKey.TODOITEMS, item).subscribe(
      {
        next: (response) => {
          this.getItems();
        },
        error: (error) => {
          //internal server eror
          this.errorMessage = "Error on update item from backend server.";
        }
      }
    );
  }
}
