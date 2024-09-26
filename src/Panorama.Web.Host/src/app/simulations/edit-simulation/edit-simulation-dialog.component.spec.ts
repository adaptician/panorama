import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSimulationDialogComponent } from './edit-simulation-dialog.component';

describe('EditSimulationDialogComponent', () => {
  let component: EditSimulationDialogComponent;
  let fixture: ComponentFixture<EditSimulationDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditSimulationDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditSimulationDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
