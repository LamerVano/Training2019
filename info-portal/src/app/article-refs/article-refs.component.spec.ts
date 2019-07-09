import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleRefsComponent } from './article-refs.component';

describe('ArticleRefsComponent', () => {
  let component: ArticleRefsComponent;
  let fixture: ComponentFixture<ArticleRefsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleRefsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleRefsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
