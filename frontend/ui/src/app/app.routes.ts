import { Routes } from '@angular/router';
import { GamesListPage } from './games-list.page';
import { GameEditPage } from './game-edit.page';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'games' },
  { path: 'games', component: GamesListPage },
  { path: 'games/:id/edit', component: GameEditPage },
];
