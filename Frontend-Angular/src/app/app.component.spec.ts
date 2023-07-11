import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { AppModule } from './app.module'; // Import the module containing AppComponent

import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ApiService } from 'src/api/api.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('ToDoComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let apiService: ApiService;
  beforeEach(async () => {

    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, AppModule, ReactiveFormsModule, FormsModule],
      providers: [ApiService]
    }).compileComponents();

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(
    () => {
       // apiService = TestBed.inject(ApiService);
        // fixture = TestBed.createComponent(AppComponent);
        // component = fixture.componentInstance;
        // fixture.detectChanges();
    }
  );
  describe('to do items', () => {
    it('component should be created', () => {
        expect(component).toBeTruthy();
    });

    it('should have correct item data', (done: DoneFn) => {
        const payload = 'thi mobile';
        component.getItems();
        expect(
            TestBed.inject(ApiService).get
          ).toHaveBeenCalled();
    });
  });
});
