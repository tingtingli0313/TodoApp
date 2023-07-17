import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppModule } from '../../app.module'; // Import the module containing AppComponent

import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ApiService } from 'src/api/api.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToDoComponent } from './todo-items.component';
import { of } from 'rxjs';
class MockApiService {
  get(any: any) {
    return [{"description" : "my task one."}];
  }
}
 
describe('ToDoComponent', () => {
  let component: ToDoComponent;
  let fixture: ComponentFixture<ToDoComponent>;
  let apiService: ApiService;
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, AppModule, ReactiveFormsModule, FormsModule],
      providers: [{provide:ApiService}]
    }).compileComponents();

    fixture = TestBed.createComponent(ToDoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(
    () => {
       apiService = TestBed.inject(ApiService);
    }
  );
  describe('to do items', () => {
    it('component should be created', () => {
        expect(component).toBeTruthy();
    });

    it('should display the value returned by the API service', () => {
      spyOn(apiService, 'get').and.returnValue( of([{"description" : "my task one."}]));
   
      expect(component.items.length == 1);
    });

    it('should display no item when api load item failed', () => {
      spyOn(apiService, 'get').and.returnValue( of([{"error" : "internal server error"}]));

      expect(component.items.length == 0);
    });
  });
});
