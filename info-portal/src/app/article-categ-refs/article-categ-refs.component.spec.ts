import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleCategRefsComponent } from './article-categ-refs.component';

describe('ArticleCategRefsComponent', () => {
  let component: ArticleCategRefsComponent;
  let fixture: ComponentFixture<ArticleCategRefsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleCategRefsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleCategRefsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
