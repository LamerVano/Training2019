import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleChangeComponent } from './article-change.component';

describe('ArticleChangeComponent', () => {
  let component: ArticleChangeComponent;
  let fixture: ComponentFixture<ArticleChangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleChangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
