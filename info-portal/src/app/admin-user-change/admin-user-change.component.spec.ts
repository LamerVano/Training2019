import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminUserChangeComponent } from './admin-user-change.component';

describe('AdminUserChangeComponent', () => {
  let component: AdminUserChangeComponent;
  let fixture: ComponentFixture<AdminUserChangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminUserChangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminUserChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
