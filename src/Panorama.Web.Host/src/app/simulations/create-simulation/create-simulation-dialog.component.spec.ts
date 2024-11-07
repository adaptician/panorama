import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSimulationDialogComponent } from './create-simulation-dialog.component';

describe('CreateSimulationComponent', () => {
  let component: CreateSimulationDialogComponent;
  let fixture: ComponentFixture<CreateSimulationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateSimulationDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateSimulationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
