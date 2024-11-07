import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateSceneDialogComponent } from './create-scene-dialog.component';

describe('CreateSimulationDialogComponent', () => {
  let component: CreateSceneDialogComponent;
  let fixture: ComponentFixture<CreateSceneDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateSceneDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateSceneDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
