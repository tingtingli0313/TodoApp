import { Component, inject } from '@angular/core';
import { ApiService } from '../api/api.service';
import { ApiEndpointKey, ApiEndpoints} from '../api/api.model';
import { endpoints } from '../api/api-endpoints-map';

export interface TodoItem {
  /** @minItems 1 */
  description: string;
  id: string;
  isCompleted: boolean;
}

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})

//TODO: move this to a ToDoItem Component
export class AppComponent {
  items: TodoItem[] = [];
  description: string = '';
  canAddItem: boolean;
  isCompleted: boolean;
  private readonly endpoints: ApiEndpoints;
  private apiService = inject(ApiService);

  public constructor() {
    this.endpoints = endpoints;
    this.onInit();
  }

  onInit() {
     this.getItems();
  }

  getItems() {
    return this.apiService
              .get<any>(ApiEndpointKey.TODOITEMS).subscribe((response: any) => {
                  // Handle the response
                  this.items = response;
                },
                (error: any) => {
                  // Handle any errors
                  alert("error");
                }
              );
  }

  handleAdd(description: string) {
    const newTodoItem =
    {
      description : description,
    };

    this.apiService.post<any, TodoItem>(
      ApiEndpointKey.TODOITEMS,
      {
        description: description
      }
    ).subscribe(
      (response: any) => {
         this.getItems();
      },
      (error: any) => {
        // Handle any errors
        alert('error on update');
      }
    );;
  }

  handleClear() {
    this.description = '';
  }

  handleMarkAsComplete(item: TodoItem) {
    if(item.isCompleted){
      return;
    }

    item.isCompleted = true;
    const url = `${this.endpoints[ApiEndpointKey.TODOITEMS].path}/${item.id}`;
    this.apiService.put(url, item)
    .subscribe(
      () => {
         this.getItems();
      },
      (error: any) => {
        // Handle any errors
        alert('error on update');
      }
    );;
  }
}
