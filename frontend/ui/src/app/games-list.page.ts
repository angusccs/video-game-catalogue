import { Component, inject } from '@angular/core';
import { NgFor, AsyncPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { GamesApiService } from './games-api.service';
import { Router } from '@angular/router';


@Component({
  standalone: true,
  imports: [NgFor, AsyncPipe, RouterLink],
  template: `
    <div class="container py-4">
      <h2 class="h4 mb-3">Video Games</h2>

      <table class="table table-sm align-middle">
        <thead>
          <tr>
            <th>Title</th>
            <th>Platform</th>
            <th style="width: 1%"></th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let g of (games$ | async)">
            <td>{{ g.title }}</td>
            <td>{{ g.platform || '' }}</td>
            <td>
             <a class="btn btn-outline-primary btn-sm"
              (click)="deleteGame(g.id)">Delete</a>
            </td>
            <td>
              <a class="btn btn-outline-primary btn-sm"
                 [routerLink]="['/games', g.id, 'edit']">Edit</a>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `
})

export class GamesListPage {
  private api = inject(GamesApiService);
  games$ = this.api.getAll();
constructor(private router: Router) {}
deleteGame(id: number) {
  this.api.delete(id).subscribe(() => {
    window.location.reload();
  });
}

}
