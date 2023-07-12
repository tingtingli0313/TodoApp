import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from '../app.component';
import { AppModule } from '../app.module'; // Import the module containing AppComponent

import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ApiService } from 'src/api/api.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToDoComponent } from './todo-item.component';

describe('ToDoComponent', () => {
  let component: ToDoComponent;
  let fixture: ComponentFixture<ToDoComponent>;
  let apiService: ApiService;
  beforeEach(async () => {

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, AppModule, ReactiveFormsModule, FormsModule],
      providers: [ApiService]
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
  });
});
