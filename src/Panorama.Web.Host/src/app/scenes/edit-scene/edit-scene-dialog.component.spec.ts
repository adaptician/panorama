import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditSceneDialogComponent } from './edit-scene-dialog.component';

describe('EditSimulationDialogComponent', () => {
  let component: EditSceneDialogComponent;
  let fixture: ComponentFixture<EditSceneDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditSceneDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditSceneDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
