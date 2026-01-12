import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { GamesApiService, Game } from './games-api.service';

@Component({
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  template: `
    <div class="container py-4">
      <h2 class="h4 mb-3">Edit Game</h2>

      <form [formGroup]="form" (ngSubmit)="save()">
        <div class="mb-3">
          <label class="form-label">Title</label>
          <input class="form-control" formControlName="title">
        </div>

        <div class="mb-3">
          <label class="form-label">Platform</label>
          <input class="form-control" formControlName="platform">
        </div>

        <button class="btn btn-primary" type="submit" [disabled]="form.invalid">Save</button>
        <a class="btn btn-link" routerLink="/games">Cancel</a>
      </form>
    </div>
  `
})
export class GameEditPage {
  private api = inject(GamesApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  private id = Number(this.route.snapshot.paramMap.get('id'));

  form = this.fb.group({
    id: [this.id, Validators.required],
    title: ['', Validators.required],
    platform: ['']
  });

  ngOnInit() {
    this.api.getById(this.id).subscribe(game => this.form.patchValue(game));
  }

  save() {
    if (this.form.invalid) return;
    this.api.update(this.form.getRawValue() as Game)
      .subscribe(() => this.router.navigateByUrl('/games'));
  }
}
